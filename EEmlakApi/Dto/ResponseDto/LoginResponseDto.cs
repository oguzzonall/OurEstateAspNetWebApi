using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEmlakApi.Dto.ResponseDto
{
    public class LoginResponseDto
    {
        public bool Sonuc { get; set; }
        public int KisiId { get; set; }
        public string Mesaj { get; set; }
        public string Kad { get; set; }
    }
}