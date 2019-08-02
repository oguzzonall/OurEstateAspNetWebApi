using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEmlakApi.Dto.RequestDto
{
    public class AramaSonucuIlanlarRequestDto
    {
        public string Sehir { get; set; }
        public string Ilce { get; set; }
        public string KonutTipi { get; set; }
        public int EnDusukFiyat { get; set; }
        public int  EnYuksekFiyat { get; set; }
    }
}