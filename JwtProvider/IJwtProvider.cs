using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtProvider
{
    public interface IJwtProvider
    {
        public Task<string> GenerateToken(AppUser user);
    }
}
