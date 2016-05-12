using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using YanZhiwei.DotNet2.Utilities.Enums;

namespace YanZhiwei.DotNet2.Utilities.Core
{
    /// <summary>
    /// 动态创建实体类
    /// </summary>
    /// 时间：2016-02-15 15:17
    /// 备注：
    internal sealed class DynamicBuilder
    {
        private static Hashtable cache = Hashtable.Synchronized(new Hashtable());//缓存

        private static readonly MethodInfo getValueMethod = typeof(IDataRecord).GetMethod("get_Item", new Type[] { typeof(int) });
        private static readonly MethodInfo isDBNullMethod = typeof(IDataRecord).GetMethod("IsDBNull", new Type[] { typeof(int) });
        private static readonly MethodInfo getCharValueMethod = typeof(DynamicBuilder).GetMethod("ReadChar", BindingFlags.Static | BindingFlags.Public);
        private static readonly MethodInfo getOracleBoolValueMethod = typeof(DynamicBuilder).GetMethod("ReadBoolean", BindingFlags.Static | BindingFlags.Public);
        private static readonly MethodInfo convertToInt32 = typeof(Convert).GetMethod("ToInt32", new Type[] { typeof(object) });
        private static readonly MethodInfo convertToString = typeof(Convert).GetMethod("ToString", new Type[] { typeof(object) });

        private const string LinqBinary = "System.Data.Linq.Binary";
        private static readonly Type charType = typeof(char);
        private static readonly Type boolType = typeof(bool);
        private static readonly Type stringType = typeof(string);
        private static readonly Type guidType = typeof(Guid);
        private static readonly Type byteArrayType = typeof(byte[]);
        private static readonly Type objectType = typeof(object);

        public delegate object Build(IDataRecord dataRecord);//最终执行动态方法的一个委托

        /// <summary>
        /// emit绑定IDataRecord到实体类集合
        /// </summary>
        /// <param name="dataRecord">IDataRecord</param>
        /// <param name="destinationType">实体类类型</param>
        /// <param name="dbType">数据库类型(为兼容不同数据库，待完善)</param>
        /// <returns></returns>
        public static Build CreateBuilder(IDataRecord dataRecord, Type destinationType, DBTypeName dbType)
        {
            BuilderKey builderKey = new BuilderKey(destinationType, dataRecord, dbType);
            Build dynamicBuilder = (Build)cache[builderKey];
            if (dynamicBuilder != null)
                return dynamicBuilder;

            DynamicMethod method = new DynamicMethod("DynamicCreate", destinationType, new Type[] { typeof(IDataRecord) }, destinationType, true);
            ILGenerator il = method.GetILGenerator();
            LocalBuilder result = il.DeclareLocal(destinationType);
            il.Emit(OpCodes.Newobj, destinationType.GetConstructor(Type.EmptyTypes));//创建目标类型实例
            il.Emit(OpCodes.Stloc, result);

            il.BeginExceptionBlock();//try
            for (int i = 0; i < dataRecord.FieldCount; i++)
            {
                PropertyInfo propertyInfo = destinationType.GetProperty(dataRecord.GetName(i),
                      BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (propertyInfo != null && propertyInfo.GetSetMethod(true) != null)
                {
                    Label endIfLabel = il.DefineLabel();
                    /* The code then loops through the fields in the data reader, finding matching properties on the type passed in.
                     * When a match is found, the code checks to see if the value from the data reader is null.
                     */
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldc_I4, i);
                    il.Emit(OpCodes.Callvirt, isDBNullMethod);
                    il.Emit(OpCodes.Brtrue, endIfLabel);

                    //If the value in the data reader is not null, the code sets the value on the object.
                    il.Emit(OpCodes.Ldloc, result);
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldc_I4, i);
                    il.Emit(OpCodes.Callvirt, getValueMethod);

                    Type propertyType = propertyInfo.PropertyType;
                    Type nullableUnderlyingType = Nullable.GetUnderlyingType(propertyType);
                    Type propertyBaseType = nullableUnderlyingType != null ? nullableUnderlyingType : propertyType;
                    if (propertyType == charType)//char类型特殊
                    {
                        il.EmitCall(OpCodes.Call, getCharValueMethod, null);
                    }
                    else if (propertyType == byteArrayType)//byte[]
                    {
                        il.Emit(OpCodes.Castclass, dataRecord.GetFieldType(i));
                    }
                    else if (propertyType.FullName == LinqBinary)
                    {
                        il.Emit(OpCodes.Unbox_Any, byteArrayType);
                        il.Emit(OpCodes.Newobj, propertyType.GetConstructor(new Type[] { byteArrayType }));
                    }
                    else if (dbType == DBTypeName.Oracle && propertyBaseType == boolType)//oracle数据库bool类型转换
                    {
                        il.EmitCall(OpCodes.Call, getOracleBoolValueMethod, null);
                    }
                    else if (propertyBaseType == guidType && (dbType == DBTypeName.OleDb || dbType == DBTypeName.SQLite))
                    {
                        //支持access或sqlite数据库是Guid类型和字符串类型保存的Guid类型数据转换
                        if (dataRecord.GetFieldType(i) != stringType)
                            il.Emit(OpCodes.Call, convertToString);
                        il.Emit(OpCodes.Newobj, propertyBaseType.GetConstructor(new Type[] { stringType }));//转为Guid类型
                        if (nullableUnderlyingType != null)//赋值到Nullable类型
                            il.Emit(OpCodes.Newobj, propertyType.GetConstructor(new Type[] { propertyBaseType }));
                    }
                    else
                    {
                        if (nullableUnderlyingType != null)//可空类型(Nullable)
                        {
                            if (propertyBaseType.IsEnum)
                                il.Emit(OpCodes.Call, convertToInt32);
                            else
                            {
                                MethodInfo convertMethod = typeof(Convert).GetMethod("To" + propertyBaseType.Name, new Type[] { objectType });
                                if (convertMethod != null)
                                    il.Emit(OpCodes.Call, convertMethod);//将数据库类型强制转换为实体类定义的类型
                                else
                                    il.Emit(OpCodes.Unbox_Any, propertyBaseType);//Guid类型无Convert转换方法
                            }
                            il.Emit(OpCodes.Newobj, propertyType.GetConstructor(new Type[] { propertyBaseType }));
                        }
                        else
                        {
                            if (propertyBaseType.IsEnum)//支持数据库是char和int类型的枚举转换
                            {
                                il.Emit(OpCodes.Call, convertToString);//先转为string类型,防止特殊情况,如数据库是char类型存储
                                il.Emit(OpCodes.Call, convertToInt32);//再转为int类型
                            }
                            else
                            {
                                MethodInfo convertMethod = typeof(Convert).GetMethod("To" + propertyBaseType.Name, new Type[] { objectType });
                                if (convertMethod != null)
                                    il.Emit(OpCodes.Call, convertMethod);//强制转换，可能会出现类型转换异常
                                else
                                    il.Emit(OpCodes.Unbox_Any, dataRecord.GetFieldType(i));//Guid
                            }
                        }
                    }
                    /*The last part of the code returns the value of the local variable*/
                    il.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod(true));
                    il.MarkLabel(endIfLabel);
                }
            }
            il.BeginCatchBlock(typeof(InvalidCastException));//catch
            il.Emit(OpCodes.Rethrow);
            il.BeginCatchBlock(typeof(Exception));//catch
            il.Emit(OpCodes.Rethrow);
            il.EndExceptionBlock();//end catch

            il.Emit(OpCodes.Ldloc, result);
            il.Emit(OpCodes.Ret);
            dynamicBuilder = (Build)method.CreateDelegate(typeof(Build));
            cache[builderKey] = dynamicBuilder;
            return dynamicBuilder;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("This method is for internal usage only", false)]
        public static char ReadChar(object value)
        {
            if (value == null || value is DBNull) throw new ArgumentNullException("value");
            string str = value as string;
            if (str == null || str.Length != 1) throw new ArgumentException("A single-character was expected", "value");
            return str[0];
        }

        /// <summary>
        /// oracle对应bool类型转换
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("This method is for internal usage only", false)]
        public static bool ReadBoolean(object value)
        {
            if (value == null || value is DBNull) throw new ArgumentNullException("value");
            string str = value as string;
            if (str.ToUpper() == "Y" || str == "1")//支持number(1)、char(1)类型，定义为char类型时值为Y、N
                return true;
            return false;
        }
    }
}