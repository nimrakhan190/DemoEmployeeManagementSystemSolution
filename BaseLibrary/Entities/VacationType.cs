namespace BaseLibrary.Entities
{
    public class VacationType : BaseEntity
    {
        //Many to one relationship with vacation

        public List<Vacation>? Vacations {  get; set; }
    }
}
