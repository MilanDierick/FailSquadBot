using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace FailSquadBot_Console
{
    internal class Program
    {
        private DiscordSocketClient _client;
        private CommandService _commandService;
        private IServiceProvider _serviceProvider;
        private const string Token = "NDAxNzE0MTI2Mzk0NDkwODgw.DTuTMA.VbWXmH7-YLojgbvA-mxii1WzwGQ";
        
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _commandService = new CommandService();

            _serviceProvider = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commandService)
                .BuildServiceProvider();
            
            _client.Log += ClientOnLog;
            _client.MessageReceived += ClientOnMessageReceived;

            await _client.LoginAsync(TokenType.Bot, Token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private static async Task ClientOnMessageReceived(SocketMessage socketMessage)
        {
            if (socketMessage.Content == "!ping")
            {
                await socketMessage.Channel.SendMessageAsync("Pong!");
            }
        }

        private static Task ClientOnLog(LogMessage logMessage)
        {
            Console.WriteLine(logMessage.ToString());
            return Task.CompletedTask;
        }
    }
}