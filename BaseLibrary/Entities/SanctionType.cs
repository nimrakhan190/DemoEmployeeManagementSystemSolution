namespace BaseLibrary.Entities
{
    public class SanctionType : BaseEntity
    {

        //Many to one relationship with vacation

        public List<Sanction>? Sanctions { get; set; }

    }
}
