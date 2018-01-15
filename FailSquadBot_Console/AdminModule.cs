using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
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
                var messageCollection = await Context.Channel.GetMessagesAsync(count).Flatten();
                await Context.Channel.DeleteMessagesAsync(messageCollection);
            }
        }
    }
}