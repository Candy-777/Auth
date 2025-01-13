using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyEntitiesService.DTOs
{
    public class DeleteEntityDto
    {
        public Guid EntityId { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
