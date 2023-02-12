namespace OfficeStaff.Data.Models
{
    public class PositionHistory
    {
        public int EmployeeId { get; set; }
        public int PositionId { get; set; }
        public Employee? Employee { get; set; }
        public Position? Position { get; set; }
        public DateTime? Created { get; set; }
    }
}
