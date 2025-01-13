using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class AppUser: IdentityUser<Guid>
    {        
        public ICollection<MyEntity> MyEntities { get; set; } = new List<MyEntity>();
    }
}
