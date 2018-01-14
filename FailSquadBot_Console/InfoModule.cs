using System.Threading.Tasks;
using Discord.Commands;

namespace FailSquadBot_Console
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("say")]
        [Summary("Echos a message.")]
        public async Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
        {
            await ReplyAsync(echo);
        }

        [Command("ping")]
        [Summary("Writes pong.")]
        public async Task PingAsync()
        {
            await ReplyAsync("Pong!");
        }
    }
}