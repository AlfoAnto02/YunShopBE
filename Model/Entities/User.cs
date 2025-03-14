using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Model.Entities {
    public class User {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Role { get; set; }
        public int Cart_Id { get; set; }
        public string Session_Id {get; set; }
        [JsonIgnore]
        public virtual ICollection<Category> CategoryCreated { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> ProductCreated { get; set; }
        [JsonIgnore]
        public virtual ICollection<Brand> BrandCreated { get; set; }
    }
}
