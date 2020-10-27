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
    public class QuestController : ControllerBase
    {

        private readonly IDapperService _dapper;

        public QuestController(IDapperService dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(Create))]
        public async Task<Quest> Create(Quest Quest)
        {
            var result = await Task.FromResult(_dapper.Insert<Quest>("insert into Quest(XpReward, GoldReward, IsComplete, NpcId)" +
            $" values({Quest.XpReward}, {Quest.GoldReward}, {(Quest.IsComplete ? 1 : 0)}, {Quest.Npc.NpcId})", null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetById))]
        public async Task<Quest> GetById(int QuestId)
        {
            var result = await Task.FromResult(_dapper.Get<Quest>($"select * from Quest q where q.QuestId = {QuestId}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<List<Quest>> GetAll()
        {
            var result = await Task.FromResult(_dapper.GetAll<Quest>("select * from Quest", null, commandType: CommandType.Text));
            return result;
        }

        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int QuestId)
        {
            var result = await Task.FromResult(_dapper.Execute($"delete from Quest where QuestId = {QuestId}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpPatch(nameof(Update))]
        public async Task<Quest> Update(Quest Quest)
        {
            var result = await Task.FromResult(_dapper.Update<Quest>($"update Quest set XpReward = {Quest.XpReward}, GoldReward = {Quest.GoldReward}," +
            $"IsComplete = {(Quest.IsComplete ? 1 : 0)}, NpcId = {Quest.Npc.NpcId}", null, commandType: CommandType.Text));
            return result;
        }
    }
}
