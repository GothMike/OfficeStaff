namespace OfficeStaff.Data.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}