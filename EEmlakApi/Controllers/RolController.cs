using EEmlakApi.Dto.ResponseDto;
using EEmlakApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EEmlakApi.Controllers
{
    public class RolController : ApiController
    {
        private eemlakDBEntities context = new eemlakDBEntities();

        [HttpGet]
        public List<RollerResponseDto> GetRolsById(int id)
        {
            var list = (from r in context.KisiRol.Where(x => x.KisiID == id).ToList()
                        select new RollerResponseDto()
                        {
                            Rols = r.Roller.RolAdi
                        }).ToList();
            return list;
        }

    }
}
