using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEmlakApi.Dto.RequestDto
{
    public class IlanVerRequestDto
    {
        public int BinaYasi { get; set; }
        public int BulunduguKat { get; set; }
        public string EmlakTipi { get; set; }
        public string Esyali { get; set; }
        public int Fiyat { get; set; }
        public int KatSayisi { get; set; }
        public int MetreKare { get; set; }
        public int OdaSayisi { get; set; }
        public string KoordinatX { get; set; }
        public string KoordinatY { get; set; }
        public string Il { get; set; }
        public string Ilce { get; set; }
        public string Diger { get; set; }
        public string IlanBaslik { get; set; }
        public int IlanSahibi { get; set; }
        public List<string> IlanResimleri { get; set; }
    }
}