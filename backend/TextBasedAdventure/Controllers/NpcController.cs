using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextBasedAdventure.Models;
using TextBasedAdventure.Services;

namespace TextBasedAdventure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NpcController : ControllerBase
    {

        private readonly IDapperService _dapper;

        public NpcController(IDapperService dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(Create))]
        public async Task<Npc> Create(Npc Npc)
        {
            var result = await Task.FromResult(_dapper.Insert<Npc>("insert into Npc(NpcName, IsMerchant, GivesQuests)" +
            $" values('{Npc.NpcName}', {(Npc.IsMerchant ? 1 : 0)}, {(Npc.GivesQuests ? 1 : 0)})", 
            null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetById))]
        public async Task<Npc> GetById(int NpcId)
        {
            var result = await Task.FromResult(_dapper.Get<Npc>($"select * from Npc where NpcId = {NpcId}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<List<Npc>> GetAll()
        {
            var result = await Task.FromResult(_dapper.GetAll<Npc>("select * from Npc", null, commandType: CommandType.Text));
            return result;
        }

        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int NpcId)
        {
            var result = await Task.FromResult(_dapper.Execute($"delete from Npc where NpcId = {NpcId}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpPatch(nameof(Update))]
        public async Task<Npc> Update(Npc Npc)
        {
            var result = await Task.FromResult(_dapper.Update<Npc>($"update Npc set NpcName = '{Npc.NpcName}', IsMerchant = {(Npc.IsMerchant ? 1 : 0)}, GivesQuests = {(Npc.GivesQuests ? 1 : 0)}",
            null, commandType: CommandType.Text));
            return result;
        }
    }
}
