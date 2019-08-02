using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EEmlakApi.Dto;
using EEmlakApi.Dto.RequestDto;
using EEmlakApi.Dto.ResponseDto;
using EEmlakApi.Models;
using EEmlakApi.Utilities;

namespace EEmlakApi.Controllers
{
    public class KayitController : ApiController
    {
        private eemlakDBEntities context = new eemlakDBEntities();

        [HttpPost]
        public RegisterResponseDto KayitOl(RegisterRequestDto registerRequestDto)
        {
            Kisiler kisi = context.Kisiler.FirstOrDefault(x => x.KullaniciAdi == registerRequestDto.Kad || x.Email == registerRequestDto.Mail);
            if (kisi == null)
            {
                string sifre = Encryption.DESSifrele(registerRequestDto.Sifre, Constant.Key1, Constant.Key2);
                kisi = new Kisiler()
                {
                    KullaniciAdi = registerRequestDto.Kad,
                    Adi = registerRequestDto.Ad,
                    SoyAdi = registerRequestDto.Soyad,
                    Email = registerRequestDto.Mail,
                    Sifre = sifre,
                    KayitTarihi = DateTime.Now
                };
                context.Kisiler.Add(kisi);

                if (context.SaveChanges() > 0)
                {
                    if (registerRequestDto.IsMusteri)
                    {
                        context.KisiRol.Add(new KisiRol()
                        {
                            KisiID = kisi.KisilerID,
                            RolID = 1
                        });
                        context.SaveChanges();
                    }

                    if (registerRequestDto.IsSatici)
                    {
                        context.KisiRol.Add(new KisiRol()
                        {
                            KisiID = kisi.KisilerID,
                            RolID = 2
                        });
                        context.SaveChanges();
                    }
                    return new RegisterResponseDto()
                    {
                        Sonuc = true,
                        Mesaj = "İşlem Başarılı"
                    };
                }
                else
                {
                    return new RegisterResponseDto()
                    {
                        Sonuc = false,
                        Mesaj = "Bir Hata İle Karşılaşıldı."
                    };
                }
            }
            else
            {
                return new RegisterResponseDto()
                {
                    Mesaj = "Sistemde Böyle bir Kullanıcı Kaydı Bulunmaktadır.",
                    Sonuc = false
                };
            }
        }
    }
}
