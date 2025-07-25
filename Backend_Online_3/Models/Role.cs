using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend_Online_3.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public ICollection<AllowAccess>? AllowAccesses { get; set; }
    }
}
