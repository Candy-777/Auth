using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.DTOs
{
    public class LoginRequestDto
    {
        public string EmailOrUsername { get; set; }

        public string Password { get; set; }
    }
    public class LoginResponseDto
    {
        public string Token {  get; set; }
    }
}
