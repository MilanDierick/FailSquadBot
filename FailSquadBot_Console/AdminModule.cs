using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;

namespace FailSquadBot_Console
{
    [Group("admin")]
    public class AdminModule : ModuleBase<SocketCommandContext>
    {
        [Group("clean")]
        public class CleanModule : ModuleBase<SocketCommandContext>
        {
            [Command("messages")]
            public async Task Messages(int count = 10)
            {
                await Context.Channel.DeleteMessagesAsync(Context.Channel.GetMessagesAsync(count).First().Result.AsEnumerable());

                Console.ReadLine();
            }
        }
    }
}