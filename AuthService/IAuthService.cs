using AuthService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService
{
    public interface IAuthService
    {
        public Task<RegisterResponceDto> Register (RegisterReqiestDto registerReqiestDto);
        public Task<LoginResponseDto> Login (LoginRequestDto loginRequestDto);
    }
}
