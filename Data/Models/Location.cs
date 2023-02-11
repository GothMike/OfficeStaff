namespace OfficeStaff.Data.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public ICollection<Department>? Departments { get; set; }
    }
}
