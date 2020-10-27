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
    public class MonsterController : Controller
    {

        private readonly IDapperService _dapper;

        public MonsterController(IDapperService dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(Create))]
        public async Task<Monster> Create(Monster monster)
        {
            var result = await Task.FromResult(_dapper.Insert<Monster>("insert into Monster(MonsterName, Level, Health, MaxHealth, ZoneId)" +
            $" values('{monster.MonsterName}', {monster.Level}, {monster.Health}, {monster.MaxHealth}, {monster.Zone.ZoneId})", null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetById))]
        public async Task<Monster> GetById(int monsterId)
        {
            var result = await Task.FromResult(_dapper.Get<Monster>($"select * from Monster where MonsterId = {monsterId}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<List<Monster>> GetAll()
        {
            var result = await Task.FromResult(_dapper.GetAll<Monster>("select * from Monster", null, commandType: CommandType.Text));
            return result;
        }

        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int monsterId)
        {
            var result = await Task.FromResult(_dapper.Execute($"delete from Monster where MonsterId = {monsterId}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpPatch(nameof(Update))]
        public async Task<Monster> Update(Monster monster)
        {
            var result = await Task.FromResult(_dapper.Update<Monster>($"update Monster set MonsterName = '{monster.MonsterName}', Level = {monster.Level}, Health = {monster.Health}," +
            $" MaxHealth = {monster.MaxHealth}, ZoneId = {monster.Zone.ZoneId}", null, commandType: CommandType.Text));
            return result;
        }
    }
}
