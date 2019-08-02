using EEmlakApi.Dto.ResponseDto;
using EEmlakApi.Models;
using System.Web.Http;

namespace EEmlakApi.Controllers
{
    public class UserController : ApiController
    {
        private eemlakDBEntities context = new eemlakDBEntities();

        [HttpGet]
        public KullaniciAdSoyadResponseDto GetAdSoyadById(int id)
        {
            Kisiler kisi = context.Kisiler.Find(id);
            if(kisi!=null)
            {
                return new KullaniciAdSoyadResponseDto() {
                    AdSoyad=kisi.Adi+" "+kisi.SoyAdi,
                    Sonuc=true
                };
            }
            else
            {
                return new KullaniciAdSoyadResponseDto() {
                    Sonuc=false
                };
            }
        }
    }
}
