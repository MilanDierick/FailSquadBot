using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

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

        [Command("applicant")]
        [Summary("Gives the user the @applicant role.")]
        public async Task ApplicantAsync()
        {
            var roles = Context.Guild.Roles;
            if(!Context.Guild.GetUser(Context.User.Id).Roles.Contains(Context.Guild.GetRole(402265410696904705)))
                await Context.Guild.GetUser(Context.User.Id).AddRoleAsync(Context.Guild.GetRole(402265410696904705));
            else
            {
                await ReplyAsync(MentionUtils.MentionUser(Context.User.Id) + ", you already have this role!");
            }
        }
    }
}