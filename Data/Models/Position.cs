using Microsoft.AspNetCore.Authentication.Cookies;

namespace OfficeStaff.Data.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAManagerPosition { get; set; }
        public ICollection<PositionHistory> PositionHistory { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
