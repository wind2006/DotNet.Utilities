namespace YanZhiwei.DotNet.Core.AdminPanel.Models
{
    [PetaPoco.TableName("Base_AppendPropertyInstance")]
    [PetaPoco.PrimaryKey("PropertyInstance_ID", autoIncrement = false)]
    [PetaPoco.ExplicitColumns]
    public class Base_AppendPropertyInstance
    {
        [PetaPoco.Column]
        public string PropertyInstance_ID { get; set; } // varchar(50), not null

        [PetaPoco.Column]
        public string Property_Control_ID { get; set; } // varchar(50), null

        [PetaPoco.Column]
        public string PropertyInstance_Value { get; set; } // varchar(max), null

        [PetaPoco.Column]
        public string PropertyInstance_Key { get; set; } // varchar(50), null
    }
}