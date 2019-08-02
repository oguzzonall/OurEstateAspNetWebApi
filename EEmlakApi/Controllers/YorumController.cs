using EEmlakApi.Dto.RequestDto;
using EEmlakApi.Dto.ResponseDto;
using EEmlakApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EEmlakApi.Controllers
{
    [RoutePrefix("api/Yorum")]
    public class YorumController : ApiController
    {
        private eemlakDBEntities context = new eemlakDBEntities();

        [Route("YorumYap")]
        [HttpPost]
        public YorumYapResponse YorumYap(YorumYapRequestDto yorumYapRequestDto)
        {
            IlanDetay ilan = context.IlanDetay.FirstOrDefault(x => x.IlanId == yorumYapRequestDto.IlanId);
            if (ilan != null)
            {
                Yorumlar yorum = new Yorumlar()
                {
                    Icerik = yorumYapRequestDto.Icerik,
                    IlanId = yorumYapRequestDto.IlanId,
                    KisiId = yorumYapRequestDto.KisiId,
                    YorumTarihi = DateTime.Now,
                    YorumBasligi = yorumYapRequestDto.YorumBasligi
                };
                context.Yorumlar.Add(yorum);
                if (context.SaveChanges() > 0)
                {
                    ilan.YorumSayisi = context.Yorumlar.Count(x => x.IlanId == yorumYapRequestDto.IlanId);
                    context.IlanDetay.AddOrUpdate(ilan);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                    }

                    return new YorumYapResponse()
                    {
                        Sonuc = true,
                        Mesaj = "İşleminiz Başarılıdır."
                    };
                }
                else
                {
                    return new YorumYapResponse()
                    {
                        Sonuc = false,
                        Mesaj = "İşleminiz Gerçekleştirilemedi.Lütfen Tekrar Deneyiniz."
                    };
                }
            }
            else
            {
                return new YorumYapResponse()
                {
                    Sonuc = false,
                    Mesaj = "Yorum Yapmak İstediğiniz İlan Silinmiş Olabilir."
                };
            }
        }

        [Route("GetYorumlarById")]
        [HttpGet]
        public List<YorumlarResponse> GetYorumlarById(int id)
        {
            if (context.Yorumlar.FirstOrDefault(x => x.IlanId == id) != null)
            {
                var list = (from y in context.Yorumlar.Where(x => x.IlanId == id).ToList()
                            select new YorumlarResponse()
                            {
                                Baslik = y.YorumBasligi,
                                Icerik = y.Icerik,
                                KullaniciAdi = y.Kisiler.KullaniciAdi,
                                Sonuc = true,
                                Tarih = y.YorumTarihi.ToString()
                            }).ToList();
                if (list.Count > 0)
                {
                    return list;
                }
                else
                {
                    return new List<YorumlarResponse>()
                {
                    new YorumlarResponse()
                    {
                        Sonuc=false
                    }
                };
                }
            }
            else
            {
                return new List<YorumlarResponse>()
                {
                    new YorumlarResponse()
                    {
                        Sonuc=false
                    }
                };
            }
        }

        [Route("GetYorumlarTop3ById")]
        [HttpGet]
        public List<YorumlarResponse> GetYorumlarTop3ById(int id)
        {
            if (context.Yorumlar.FirstOrDefault(x => x.IlanId == id) != null)
            {
                var list = (from y in context.Yorumlar.Where(x => x.IlanId == id).OrderByDescending(x => x.YorumID).Take(3).ToList()
                            select new YorumlarResponse()
                            {
                                Baslik = y.YorumBasligi,
                                Icerik = y.Icerik,
                                KullaniciAdi = y.Kisiler.KullaniciAdi,
                                Sonuc = true,
                                Tarih = y.YorumTarihi.ToString()
                            }).ToList();
                if (list.Count > 0)
                {
                    return list;
                }
                else
                {
                    return new List<YorumlarResponse>()
                {
                    new YorumlarResponse()
                    {
                        Sonuc=false
                    }
                };
                }
            }
            else
            {
                return new List<YorumlarResponse>()
                {
                    new YorumlarResponse()
                    {
                        Sonuc=false
                    }
                };
            }
        }
    }
}
