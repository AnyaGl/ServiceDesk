using ServiceDesk.DTO.Employee;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk.Cotrollers
{
    public class Token
    {
        private readonly IEmployeeService _employeeService;
        private readonly ushort secretKey = 0x0050;
        public Token(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public string CreateToken(string login, string password, string userId)
        {
            return EncodeDecrypt($"{login}.{password}.{userId}");
        }

        public async Task CheckTokenAsync(string token)
        {
            var decrypted = EncodeDecrypt(token);
            string[] subs = decrypted.Split('.');
            var result = await _employeeService.GetEmployee(
                new Authorization
                {
                    login = subs[0],
                    password = subs[1]
                });
            if (result.guid.Length == 0 || result.guid != subs[2])
            {
                throw new Exception("Invalid token");
            }
        }

        private string EncodeDecrypt(string str)
        {
            var ch = str.ToArray();
            string newStr = "";
            foreach (var c in ch)
            {
                newStr += TopSecret(c);
            }
            return newStr;
        }

        private char TopSecret(char character)
        {
            character = (char)(character ^ secretKey);
            return character;
        }
    }
}
