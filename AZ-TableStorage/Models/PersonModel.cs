namespace AZ_TableStorage.Models
{
    public class PersonModel
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }
}
