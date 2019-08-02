using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEmlakApi.Dto.RequestDto
{
    public class LoginRequestDto
    {
        public string Kad { get; set; }
        public string Sifre { get; set; }
    }
}