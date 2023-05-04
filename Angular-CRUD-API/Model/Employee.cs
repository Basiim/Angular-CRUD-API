using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Angular_CRUD_API.Model
{
    [Table("Employee")]
    public class Employee
    {
        [JsonIgnore]
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public double salary { get; set; }
        public string department { get; set; }
    }
}
