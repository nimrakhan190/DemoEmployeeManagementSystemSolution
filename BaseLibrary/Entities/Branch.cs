
namespace BaseLibrary.Entities
{
    public class Branch : BaseEntity
    {

        //many to one relationship qith department

        //many to one relationship with department

        public Department? Department { get; set; }
        public int DepartmentId { get; set; }

        //one to many relationship with employees
        public List<Employee>? Employees { get; set; }
    }
}
