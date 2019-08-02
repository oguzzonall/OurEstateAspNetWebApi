using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEmlakApi.Dto.RequestDto
{
    public class YorumYapRequestDto
    {
        public string Icerik { get; set; }
        public int KisiId { get; set; }
        public string YorumBasligi { get; set; }
        public int IlanId { get; set; }
    }
}