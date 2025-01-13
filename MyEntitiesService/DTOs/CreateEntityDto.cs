


using System.Text.Json.Serialization;

namespace MyEntitiesService.DTOs
{
    public class CreateEntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string YouTubeUrl { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
