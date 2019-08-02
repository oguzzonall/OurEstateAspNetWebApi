using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using EEmlakApi.Dto.RequestDto;
using EEmlakApi.Dto.ResponseDto;
using EEmlakApi.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

namespace EEmlakApi.Controllers
{
    [RoutePrefix("api/Ilan")]
    public class IlanController : ApiController
    {
        private eemlakDBEntities context = new eemlakDBEntities();

        [Route("PostIlanVer")]
        [HttpPost]
        public IlanVerResponse PostIlanVer(IlanVerRequestDto ilanVerRequestDto)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Ilanlar ilan = new Ilanlar()
                    {
                        IlanBaslik = ilanVerRequestDto.IlanBaslik,
                        Durum = true,
                        IlanTarihi = DateTime.Now
                    };
                    context.Ilanlar.Add(ilan);

                    Sehirler sehir = context.Sehirler.FirstOrDefault(x => x.SehirAdi == ilanVerRequestDto.Il);
                    Ilceler ilce = context.Ilceler.FirstOrDefault(x => x.SehirId == sehir.SehirID && x.IlceAdi == ilanVerRequestDto.Ilce);
                    Kisiler kisi = context.Kisiler.FirstOrDefault(x => x.KisilerID == ilanVerRequestDto.IlanSahibi);
                    EmlakTipi emlakTipi = context.EmlakTipi.FirstOrDefault(x => x.TipAdi == ilanVerRequestDto.EmlakTipi);

                    Adres adres = new Adres()
                    {
                        IlceId = ilce.IlceID,
                        SehirId = sehir.SehirID,
                        Diger = ilanVerRequestDto.Diger
                    };

                    context.Adres.Add(adres);
                    if (context.SaveChanges() > 0)
                    {
                        IlanDetay ilandetay = new IlanDetay()
                        {
                            AdresId = adres.AdresID,
                            BegenmeSayisi = 0,
                            BinaYasi = ilanVerRequestDto.BinaYasi,
                            BulunduguKat = ilanVerRequestDto.BulunduguKat,
                            Durum = true,
                            EmlakSahibiId = kisi.KisilerID,
                            EmlakTipiId = emlakTipi.EmlakTipiID,
                            Esyali = ilanVerRequestDto.Esyali,
                            Fiyat = ilanVerRequestDto.Fiyat,
                            GoruntulenmeSayisi = 0,
                            IlanId = ilan.IlanID,
                            KatSayisi = ilanVerRequestDto.KatSayisi,
                            MetreKare = ilanVerRequestDto.MetreKare,
                            OdaSayisi = ilanVerRequestDto.OdaSayisi,
                            KoordinatX = ilanVerRequestDto.KoordinatX,
                            KoordinatY = ilanVerRequestDto.KoordinatY,
                            YorumSayisi = 0
                        };
                        context.IlanDetay.Add(ilandetay);
                        if (context.SaveChanges() > 0)
                        {

                            foreach (var resim in ilanVerRequestDto.IlanResimleri)
                            {
                                try
                                {
                                    string baslik = Guid.NewGuid().ToString();
                                    string yol = "/ilanresimleri/" + baslik + ".jpg";

                                    Resimler resimler = new Resimler()
                                    {
                                        IlanID = ilan.IlanID,
                                        ResimYolu = yol
                                    };
                                    context.Resimler.Add(resimler);
                                    context.SaveChanges();

                                    byte[] imageBytes = Convert.FromBase64String(resim);
                                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

                                    // Convert byte[] to Image
                                    ms.Write(imageBytes, 0, imageBytes.Length);
                                    Image image = Image.FromStream(ms, true);
                                    string savePath = HttpContext.Current.Server.MapPath(yol);
                                    image.Save(savePath, ImageFormat.Jpeg);
                                }
                                catch (Exception)
                                {
                                    transaction.Rollback();
                                    return new IlanVerResponse()
                                    {
                                        Sonuc = false,
                                    };
                                }
                            }
                            transaction.Commit();
                            return new IlanVerResponse()
                            {
                                Sonuc = true,
                                IlanId = ilan.IlanID
                            };
                        }
                        else
                        {
                            transaction.Rollback();
                            return new IlanVerResponse()
                            {
                                Sonuc = false
                            };
                        }

                    }
                    else
                    {
                        transaction.Rollback();
                        return new IlanVerResponse()
                        {
                            Sonuc = false
                        };
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return new IlanVerResponse()
                    {
                        Sonuc = false
                    };
                }
            }
        }

        [Route("GetIlanDetayById")]
        [HttpGet]
        public IlanDetayResponse GetIlanDetayById(int id)
        {
            IlanDetay ilan = context.IlanDetay.FirstOrDefault(x => x.IlanId == id);
            if (ilan != null)
            {
                Kisiler kisi = context.Kisiler.FirstOrDefault(x => x.KisilerID == ilan.EmlakSahibiId);
                EmlakTipi emlakTipi = context.EmlakTipi.FirstOrDefault(x => x.EmlakTipiID == ilan.EmlakTipiId);

                Adres adres = context.Adres.FirstOrDefault(x => x.AdresID == ilan.AdresId);

                return new IlanDetayResponse()
                {
                    BegenmeSayisi = ilan.BegenmeSayisi,
                    BinaYasi = ilan.BinaYasi,
                    BulunduguKat = ilan.BulunduguKat,
                    Durum = ilan.Durum,
                    Esyali = ilan.Esyali,
                    KoordinatX = ilan.KoordinatX,
                    KoordinatY = ilan.KoordinatY,
                    Fiyat = ilan.Fiyat,
                    GoruntulenmeSayisi = ilan.GoruntulenmeSayisi,
                    KatSayisi = ilan.KatSayisi,
                    MetreKare = ilan.MetreKare,
                    OdaSayisi = ilan.OdaSayisi,
                    EmlakSahibi = kisi.Adi + " " + kisi.SoyAdi,
                    EmlakTipi = emlakTipi.TipAdi,
                    Il = adres.Sehirler.SehirAdi,
                    Ilce = adres.Ilceler.IlceAdi,
                    Diger = adres.Diger,
                    IlanBaslik = ilan.Ilanlar.IlanBaslik,
                    OtherId = kisi.KisilerID.ToString(),
                    YorumSayisi = ilan.YorumSayisi
                };
            }
            else
            {
                return new IlanDetayResponse();
            }
        }

        [Route("GetFavoriIlanlarTop3")]
        [HttpGet]
        public List<OneCikanIlanlarResponse> GetFavoriIlanlarTop3()
        {
            var list = (from i in context.IlanDetay.OrderByDescending(x => x.BegenmeSayisi).Take(3).ToList()
                        select new OneCikanIlanlarResponse()
                        {
                            IlanId = i.IlanId,
                            IlanBaslik = i.Ilanlar.IlanBaslik,
                            ResimYolu = i.Ilanlar.Resimler.FirstOrDefault(x => x.IlanID == i.IlanId).ResimYolu
                        }).ToList();
            return list;
        }

        [Route("GetFavoriIlanlar")]
        [HttpGet]
        public List<OneCikanIlanlarResponse> GetFavoriIlanlar()
        {
            var list = (from i in context.IlanDetay.OrderByDescending(x => x.BegenmeSayisi).ToList()
                        select new OneCikanIlanlarResponse()
                        {
                            IlanId = i.IlanId,
                            IlanBaslik = i.Ilanlar.IlanBaslik,
                            ResimYolu = i.Ilanlar.Resimler.FirstOrDefault(x => x.IlanID == i.IlanId)?.ResimYolu
                        }).ToList();

            return list;
        }

        [Route("GetEnCokZiyaretEdilenlerTop3")]
        [HttpGet]
        public List<EnCokZiyaretEdilenlerResponse> GetEnCokZiyaretEdilenlerTop3()
        {
            var list = (from i in context.IlanDetay.OrderByDescending(x => x.GoruntulenmeSayisi).Take(3).ToList()
                        select new EnCokZiyaretEdilenlerResponse()
                        {
                            IlanId = i.IlanId,
                            IlanBaslik = i.Ilanlar.IlanBaslik,
                            ResimYolu = i.Ilanlar.Resimler.FirstOrDefault(x => x.IlanID == i.IlanId).ResimYolu
                        }).ToList();
            return list;
        }

        [Route("GetEnCokZiyaretEdilenler")]
        [HttpGet]
        public List<EnCokZiyaretEdilenlerResponse> GetEnCokZiyaretEdilenler()
        {
            var list = (from i in context.IlanDetay.OrderByDescending(x => x.GoruntulenmeSayisi).ToList()
                        select new EnCokZiyaretEdilenlerResponse()
                        {
                            IlanId = i.IlanId,
                            IlanBaslik = i.Ilanlar.IlanBaslik,
                            ResimYolu = i.Ilanlar.Resimler.FirstOrDefault(x => x.IlanID == i.IlanId).ResimYolu
                        }).ToList();
            return list;
        }

        [Route("GetEnUygunFiyatlarTop3")]
        [HttpGet]
        public List<EnUygunFiyatlarResponse> GetEnUygunFiyatlarTop3()
        {
            var list = (from i in context.IlanDetay.OrderBy(x => x.Fiyat).Take(3).ToList()
                        select new EnUygunFiyatlarResponse()
                        {
                            IlanId = i.IlanId,
                            IlanBaslik = i.Ilanlar.IlanBaslik,
                            ResimYolu = i.Ilanlar.Resimler.FirstOrDefault(x => x.IlanID == i.IlanId)?.ResimYolu
                        }).ToList();
            return list;
        }

        [Route("GetEnUygunFiyatlar")]
        [HttpGet]
        public List<EnUygunFiyatlarResponse> GetEnUygunFiyatlar()
        {
            var list = (from i in context.IlanDetay.OrderBy(x => x.Fiyat).ToList()
                        select new EnUygunFiyatlarResponse()
                        {
                            IlanId = i.IlanId,
                            IlanBaslik = i.Ilanlar.IlanBaslik,
                            ResimYolu = i.Ilanlar.Resimler.FirstOrDefault(x => x.IlanID == i.IlanId).ResimYolu
                        }).ToList();
            return list;
        }

        [Route("GetIlanResimleriListById")]
        [HttpGet]
        public List<GetIlanResimleriResponse> GetIlanResimleriListById(int id)
        {
            var resimler = (from r in context.Resimler.Where(x => x.IlanID == id).ToList()
                            select new GetIlanResimleriResponse()
                            {
                                ResimYolu = r.ResimYolu
                            }).ToList();
            return resimler;
        }

        [Route("GetAramaSonucuIlanlar")]
        [HttpGet]
        public List<AramaSonucuIIanlarResponse> GetAramaSonucuIlanlar([FromUri] AramaSonucuIlanlarRequestDto aramaSonucuIlanlarRequestDto)
        {
            List<AramaSonucuIIanlarResponse> list;

            try
            {
                list = (from ilanresimleri in context.Resimler.ToList()
                        join ilandetay in context.IlanDetay.Where(x => x.Adres.Sehirler.SehirAdi == aramaSonucuIlanlarRequestDto.Sehir
                        && x.Adres.Ilceler.IlceAdi == aramaSonucuIlanlarRequestDto.Ilce
                        && x.EmlakTipi.TipAdi == aramaSonucuIlanlarRequestDto.KonutTipi
                        && x.Fiyat > aramaSonucuIlanlarRequestDto.EnDusukFiyat
                        && x.Fiyat < aramaSonucuIlanlarRequestDto.EnYuksekFiyat).ToList() on ilanresimleri.IlanID equals context.Resimler.Where(x => x.IlanID == ilandetay.IlanId).Take(1).First().IlanID
                        select new AramaSonucuIIanlarResponse()
                        {
                            IlanBaslik = ilandetay.Ilanlar.IlanBaslik,
                            IlanId = ilandetay.IlanId,
                            ResimYolu = ilanresimleri.ResimYolu,
                            Sonuc = true,
                            Aciklama = "Arama Sonuçlarınız Listeleniyor..."
                        }).ToList();
            }
            catch (Exception)
            {
                list = new List<AramaSonucuIIanlarResponse>()
                {
                   new AramaSonucuIIanlarResponse()
                   {
                       Sonuc=false,
                       Aciklama="Sunucu Hatası"
                   }
                };
                return list;
            }

            if (list.Count > 0)
            {
                return list;
            }
            else
            {
                list = new List<AramaSonucuIIanlarResponse>()
                {
                    new AramaSonucuIIanlarResponse()
                    {
                        Sonuc=false,
                        Aciklama="Aradığınız Kriterlere Göre Bir Ev Bulunamadı."
                    }
                };
                return list;
            }
        }

        [Route("KisiBegeniKontrol")]
        [HttpGet]
        public KisiBegeniKontrolResponse KisiBegeniKontrol([FromUri]IsIlanBegeniRequestDto isIlanBegeniRequestDto)
        {
            var begeni = context.Begenmeler.FirstOrDefault(x =>
                 x.KisiId == isIlanBegeniRequestDto.KisiID && x.IlanId == isIlanBegeniRequestDto.IlanID);
            if (begeni == null)
            {
                return new KisiBegeniKontrolResponse()
                {
                    IsBegeni = false
                };
            }
            else
            {
                return new KisiBegeniKontrolResponse()
                {
                    IsBegeni = true
                };
            }
        }

        [Route("BegeniArttirAzalt")]
        [HttpGet]
        public IlanBegeniResponseDto BegeniArttirAzalt([FromUri]IsIlanBegeniRequestDto begeniArttirAzaltRequestDto)
        {
            int begenisayisiguncel;
            IlanDetay ilanDetay = context.IlanDetay.FirstOrDefault(x => x.IlanId == begeniArttirAzaltRequestDto.IlanID);
            if (ilanDetay != null)
            {
                Begenmeler begeni = context.Begenmeler.FirstOrDefault(x =>
                    x.KisiId == begeniArttirAzaltRequestDto.KisiID && x.IlanId == begeniArttirAzaltRequestDto.IlanID);
                if (begeni == null)
                {
                    Begenmeler begenilecek = new Begenmeler()
                    {
                        IlanId = begeniArttirAzaltRequestDto.IlanID,
                        KisiId = begeniArttirAzaltRequestDto.KisiID
                    };
                    context.Begenmeler.Add(begenilecek);
                    if (context.SaveChanges() > 0)
                    {
                        begenisayisiguncel = context.Begenmeler.Count(x => x.IlanId == begeniArttirAzaltRequestDto.IlanID);
                        ilanDetay.BegenmeSayisi = begenisayisiguncel;
                        context.IlanDetay.AddOrUpdate(ilanDetay);
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception)
                        {
                        }
                        return new IlanBegeniResponseDto()
                        {
                            BegenmeSayisi = begenisayisiguncel,
                            Sonuc = true,
                            Begeni = true,
                        };
                    }
                    else
                    {
                        return new IlanBegeniResponseDto()
                        {
                            Sonuc = false,
                            Begeni = false,
                            Mesaj = "İşleminiz Gerçekleştirilemedi"
                        };
                    }
                }
                else
                {
                    context.Begenmeler.Remove(begeni);
                    if (context.SaveChanges() > 0)
                    {
                        begenisayisiguncel = context.Begenmeler.Count(x => x.IlanId == begeniArttirAzaltRequestDto.IlanID);

                        ilanDetay.BegenmeSayisi = begenisayisiguncel;
                        context.IlanDetay.AddOrUpdate(ilanDetay);
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception)
                        {
                        }
                        return new IlanBegeniResponseDto()
                        {
                            Begeni = false,
                            Sonuc = true,
                            BegenmeSayisi = begenisayisiguncel,
                        };
                    }
                    else
                    {
                        return new IlanBegeniResponseDto()
                        {
                            Begeni = false,
                            Sonuc = false,
                            Mesaj = "İşleminiz Gerçekleştirilemedi"
                        };
                    }
                }
            }
            else
            {
                return new IlanBegeniResponseDto()
                {
                    Begeni = false,
                    Sonuc = false,
                    Mesaj = "İşleminiz Gerçekleştirilemedi"
                };
            }
        }

        [Route("IlanGoruntulenmeKontrol")]
        [HttpGet]
        public void IlanGoruntulenmeKontrol([FromUri]IsIlanGoruntulenmeRequestDto isIlanGoruntulenmeRequestDto)
        {
            Goruntulenmeler goruntu = context.Goruntulenmeler.FirstOrDefault(x =>
                x.IlanId == isIlanGoruntulenmeRequestDto.IlanID && x.KisiId == isIlanGoruntulenmeRequestDto.KisiID);

            if (goruntu == null)
            {
                IlanDetay ilanDetay =
                    context.IlanDetay.FirstOrDefault(x => x.IlanId == isIlanGoruntulenmeRequestDto.IlanID);
                if (ilanDetay != null)
                {
                    context.Goruntulenmeler.Add(new Goruntulenmeler()
                    {
                        IlanId = isIlanGoruntulenmeRequestDto.IlanID,
                        KisiId = isIlanGoruntulenmeRequestDto.KisiID
                    });
                    if (context.SaveChanges() > 0)
                    {
                        ilanDetay.GoruntulenmeSayisi = context.Goruntulenmeler.Count(x => x.IlanId == isIlanGoruntulenmeRequestDto.IlanID);
                        context.IlanDetay.AddOrUpdate(ilanDetay);
                        context.SaveChanges();
                    }
                }
            }
        }

    }
}
