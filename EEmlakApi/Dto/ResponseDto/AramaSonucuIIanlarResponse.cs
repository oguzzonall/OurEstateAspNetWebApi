
namespace EEmlakApi.Dto.ResponseDto
{
    public class AramaSonucuIIanlarResponse
    {
        public int IlanId { get; set; }
        public string IlanBaslik { get; set; }
        public string ResimYolu { get; set; }
        public string Aciklama { get; set; }
        public bool Sonuc { get; set; }
    }
}