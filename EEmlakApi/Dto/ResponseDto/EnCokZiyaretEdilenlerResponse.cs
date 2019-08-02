using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEmlakApi.Dto.ResponseDto
{
    public class EnCokZiyaretEdilenlerResponse
    {
        public int IlanId { get; set; }
        public string IlanBaslik { get; set; }
        public string ResimYolu { get; set; }
    }
}