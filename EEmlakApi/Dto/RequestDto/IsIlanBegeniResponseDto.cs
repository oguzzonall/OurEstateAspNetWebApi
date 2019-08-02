using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEmlakApi.Dto.RequestDto
{
    public class IlanBegeniResponseDto
    {
        public int BegenmeSayisi { get; set; }
        public bool Begeni { get; set; }
        public bool Sonuc { get; set; }
        public string Mesaj { get; set; }

    }
}