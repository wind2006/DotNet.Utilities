using System;
using System.Collections.Generic;
using System.Data;
using YanZhiwei.DotNet2.Utilities.Enums;

namespace YanZhiwei.DotNet2.Utilities.Core
{
    /// <summary>
    /// 用于构建缓存的类
    /// </summary>
    internal sealed class BuilderKey
    {
        private readonly IList<string> _dataRecordNames;
        private readonly Type _destinationType;
        private readonly DBTypeName _dbType;

        public BuilderKey(Type destinationType, IDataRecord record, DBTypeName dbType)
        {
            _destinationType = destinationType;
            _dbType = dbType;
            _dataRecordNames = new List<string>(record.FieldCount);
            for (int i = 0; i < record.FieldCount; i++)
            {
                _dataRecordNames.Add(record.GetName(i));
            }
        }

        public override int GetHashCode()
        {
            int hash = _destinationType.GetHashCode();
            foreach (string name in _dataRecordNames)
            {
                hash = hash * 37 + name.GetHashCode();
            }
            return hash + _dbType.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            BuilderKey builderKey = obj as BuilderKey;
            if (builderKey == null)
                return false;

            if (_dbType != builderKey._dbType)
                return false;

            if (_destinationType != builderKey._destinationType)
                return false;

            if (this._dataRecordNames.Count != builderKey._dataRecordNames.Count)
                return false;

            for (int i = 0; i < _dataRecordNames.Count; i++)
            {
                if (this._dataRecordNames[i] != builderKey._dataRecordNames[i])
                    return false;
            }
            return true;
        }
    }
}