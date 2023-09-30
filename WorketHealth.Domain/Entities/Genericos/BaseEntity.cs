namespace WorketHealth.Domain.Entities.Genericos 
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }
    }
}
