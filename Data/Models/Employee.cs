namespace OfficeStaff.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Department Department { get; set; }
        public Position Position { get; set; }
        public Employee? managerId { get; set; }
        public ICollection<PositionHistory> PositionHistory { get; set; }
    }
}