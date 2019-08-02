using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEmlakApi.Dto.RequestDto
{
    public class RegisterRequestDto
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Mail { get; set; }
        public string Kad { get; set; }
        public string Sifre { get; set; }
        public bool IsMusteri { get; set; }
        public bool IsSatici { get; set; }
    }
}