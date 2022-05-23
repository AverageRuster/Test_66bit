using Test_66bit.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Test_66bit.Hubs
{
    public class PlayersHub : Hub
    {
        public async Task Send(Player player, int? rowId, string teamName)
        {
            await this.Clients.All.SendAsync("Send", player, rowId, teamName);
        }
    }
}
