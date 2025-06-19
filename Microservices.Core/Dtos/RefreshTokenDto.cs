using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Core.Dtos
{
    public class RefreshTokenDto
    {
        public string OldToken { get; set; }
    }
}
