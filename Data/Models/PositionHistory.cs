namespace OfficeStaff.Data.Models
{
    public class PositionHistory
    {
        public int ChangeId { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Position> Positions { get; set; }
        public DateTime? Created { get; set; }
    }
}
