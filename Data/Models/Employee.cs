namespace OfficeStaff.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Department Department { get; set; }
        public int? PositionId { get; set; }
        public Position? Position { get; set; }
        public int? managerId { get; set; }
        public ICollection<PositionHistory> PositionHistory { get; set; }
    }
}