using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEmlakApi.Dto.ResponseDto
{
    public class YorumlarResponse
    {
        public string Icerik { get; set; }
        public string Baslik { get; set; }
        public string KullaniciAdi { get; set; }
        public string Tarih { get; set; }
        public bool Sonuc { get; set; }
    }
}