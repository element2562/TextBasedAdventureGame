using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using TextBasedAdventure.Services;
using System.Data;
using TextBasedAdventure.Models;

namespace TextBasedAdventure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly IDapperService _dapper;
        public PlayerController(IDapperService dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(Create))]
        public async Task<int> Create(Player data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("PlayerName", data.PlayerName, DbType.String);
            dbparams.Add("Level", data.Level, DbType.Int32);
            dbparams.Add("Health", data.Health, DbType.Int32);
            dbparams.Add("MaxHealth", data.MaxHealth, DbType.Int32);
            dbparams.Add("Strength", data.Strength, DbType.Int32);
            dbparams.Add("Defense", data.Defense, DbType.Int32);
            dbparams.Add("Gold", data.PlayerId, DbType.Int32);
            dbparams.Add("Experience", data.Experience, DbType.Int32);
            dbparams.Add("CurrentZoneId", data.CurrentZone.ZoneId, DbType.Int32);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_Create_Player]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<List<Player>> GetAll()
        {
            var result = await Task.FromResult(_dapper.GetAll<Player>("select * from Player", null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetById))]
        public async Task<Player> GetById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<Player>($"Select * from [Player] where PlayerId = {Id}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int Id)
        {
            var result = await Task.FromResult(_dapper.Execute($"Delete [Player] Where PlayerId = {Id}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpPatch(nameof(Update))]
        public Task<int> Update(Player data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("PlayerId", data.PlayerId, DbType.Int32);
            dbparams.Add("PlayerName", data.PlayerName, DbType.String);
            dbparams.Add("Level", data.Level, DbType.Int32);
            dbparams.Add("Health", data.Health, DbType.Int32);
            dbparams.Add("MaxHealth", data.MaxHealth, DbType.Int32);
            dbparams.Add("Strength", data.Strength, DbType.Int32);
            dbparams.Add("Defense", data.Defense, DbType.Int32);
            dbparams.Add("Gold", data.PlayerId, DbType.Int32);
            dbparams.Add("Experience", data.Experience, DbType.Int32);
            dbparams.Add("CurrentZoneId", data.CurrentZone.ZoneId, DbType.Int32);

            var updatePlayer = Task.FromResult(_dapper.Update<int>("[dbo].[SP_Update_Player]",
                            dbparams,
                            commandType: CommandType.StoredProcedure));
            return updatePlayer;
        }
    }
}

