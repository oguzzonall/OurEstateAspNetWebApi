using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEmlakApi.Dto.ResponseDto
{
    public class IlanDetayResponse
    {
        public int BinaYasi { get; set; }
        public int BulunduguKat { get; set; }
        public bool Durum { get; set; }
        public string EmlakTipi { get; set; }
        public string EmlakSahibi { get; set; }
        public string Esyali { get; set; }
        public int Fiyat { get; set; }
        public int KatSayisi { get; set; }
        public int MetreKare { get; set; }
        public int OdaSayisi { get; set; }
        public int GoruntulenmeSayisi { get; set; }
        public int BegenmeSayisi { get; set; }
        public string KoordinatX { get; set; }
        public string KoordinatY { get; set; }
        public string Il { get; set; }
        public string Ilce { get; set; }
        public string Diger { get; set; }
        public string IlanBaslik { get; set; }
        public string OtherId { get; set; }
        public int YorumSayisi { get; set; }
    }
}