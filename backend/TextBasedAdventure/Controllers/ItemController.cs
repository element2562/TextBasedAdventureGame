using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TextBasedAdventure.Models;
using TextBasedAdventure.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextBasedAdventure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IDapperService _dapper;
        public ItemController(IDapperService dapper)
        {
            _dapper = dapper;
        }
        
        [HttpPost(nameof(Create))]
        public async Task<Item> Create(Item item)
        {
            var result = await Task.FromResult(_dapper.Insert<Item>("insert into Item (ItemName, LevelRequirement, ItemType, Equipped, StrengthBonus, DefenseBonus, HealthBonus, BuyPrice, PlayerId)" +
            $" values('{item.ItemName}', {item.LevelRequirement}, '{item.ItemType}', {(item.Equipped ? 1 : 0)}, {item.StrengthBonus}, {item.DefenseBonus}, {item.HealthBonus}, " +
            $"{item.BuyPrice}, {item.Player.PlayerId})", null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetById))]
        public async Task<Item> GetById(int itemId)
        {
            var result = await Task.FromResult(_dapper.Get<Item>($"select * from Item i inner join Player p on p.PlayerId = {itemId} where i.ItemId = {itemId}", null, commandType: CommandType.Text));
            return result;
        }

        // My thinking with two get alls is one for seeing all items to edit/delete, then one for players to get all items they have in their inventory
        [HttpGet(nameof(GetAll))]
        public async Task<List<Item>> GetAll()
        {
            var result = await Task.FromResult(_dapper.GetAll<Item>("select * from Item", null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<List<Item>> GetAll(int playerId)
        {
            var result = await Task.FromResult(_dapper.GetAll<Item>($"select * from Item i inner join Player p on p.PlayerId = {playerId}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpPatch(nameof(Update))]
        public async Task<Item> Update(Item item)
        {
            var result = await Task.FromResult(_dapper.Update<Item>($"update Item set ItemName = '{item.ItemName}', LevelRequirement = {item.LevelRequirement}, ItemType = '{item.ItemType}'," +
            $" Equipped = {(item.Equipped ? 1 : 0)}, StrengthBonus = {item.StrengthBonus}, DefenseBonus = {item.DefenseBonus}, HealthBonus = {item.HealthBonus}, PlayerId = {item.Player.PlayerId}",
            null, commandType: CommandType.Text));
            return result;
        }

        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int ItemId)
        {
            var result = await Task.FromResult(_dapper.Execute($"delete from Item where ItemId = {ItemId}", null, commandType: CommandType.Text));
            return result;
        }

    }
}
