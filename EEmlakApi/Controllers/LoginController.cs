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
    public class LoginController : ApiController
    {
        private eemlakDBEntities context = new eemlakDBEntities();

        [HttpPost]
        public LoginResponseDto GirisYap(LoginRequestDto loginRequestDto)
        {
            string sifre = Encryption.DESSifrele(loginRequestDto.Sifre, Constant.Key1, Constant.Key2);

            Kisiler kisi = context.Kisiler.FirstOrDefault(x => x.KullaniciAdi == loginRequestDto.Kad && x.Sifre == sifre);

            if (kisi != null)
            {
                return new LoginResponseDto()
                {
                    KisiId = kisi.KisilerID,
                    Mesaj = "İşlem Başarılı",
                    Kad=kisi.KullaniciAdi,
                    Sonuc = true
                };
            }
            else
            {
                return new LoginResponseDto()
                {
                    Mesaj = "Kayıtlı Kullanıcı Bulunamadı.Bilgilerinizi Kontrol Ediniz.",
                    Sonuc = false
                };
            }
        }
    }
}
