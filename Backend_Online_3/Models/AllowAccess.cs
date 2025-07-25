using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend_Online_3.Models
{
    public class AllowAccess
    {
        [Key]
        public int AllowAccessId { get; set; }
        public int RoleId { get; set; }
        public string? TableName { get; set; }
        public string? AccessProperties { get; set; }

        [JsonIgnore]
        public Role? Role { get; set; }
    }

}
