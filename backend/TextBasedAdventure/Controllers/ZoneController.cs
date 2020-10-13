using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TextBasedAdventure.Models;
using TextBasedAdventure.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextBasedAdventure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : Controller
    {
        private readonly IDapperService _dapper;

        public ZoneController(IDapperService dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(Create))]
        public async Task<Zone> Create(Zone zone)
        {
            var result = await Task.FromResult(_dapper.Insert<Zone>($"insert into Zone(ZoneName) values ('{zone.ZoneName}')", null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetById))]
        public async Task<Zone> GetById(int ZoneId)
        {
            var result = await Task.FromResult(_dapper.Get<Zone>($"select * from Zone z inner join Monster m on m.ZoneId = {ZoneId} where ZoneId = {ZoneId}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int ZoneId)
        {
            var result = await Task.FromResult(_dapper.Execute($"delete from Zone where ZoneId = {ZoneId}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpPatch(nameof(Update))]
        public async Task<Zone> Update(Zone zone)
        {
            var result = await Task.FromResult(_dapper.Update<Zone>($"update Zone set ZoneName = '{zone.ZoneName}' where ZoneId = {zone.ZoneId}", null, commandType: CommandType.Text));
            return result;
        }

                
    }
}
