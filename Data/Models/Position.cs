using Microsoft.AspNetCore.Authentication.Cookies;

namespace OfficeStaff.Data.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PositionHistory> PositionHistory { get; set; }
    }
}
