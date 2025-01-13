using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MyEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public string YouTubeUrl { get; set; }

        [JsonIgnore]
        public AppUser User { get; set; }

        
        public Guid UserId { get; set; }
    }
}
