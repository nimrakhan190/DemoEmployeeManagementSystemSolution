using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Department : BaseEntity
    {

        //Many to one relationship with general department

        public GeneralDepartment? GeneralDepartment { get; set; }

        public int GeneralDepartmentId { get; set; }

        //one to many relationship with branch
        [JsonIgnore]
        public List<Branch>? Branches { get; set; }


    }
}
