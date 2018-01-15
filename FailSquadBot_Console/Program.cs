using System;
using System.Linq;
using System.Reflection;
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
        private const string Token = "NDAxNzE0MTI2Mzk0NDkwODgw.DTuTMA.VbWXmH7-YLojgbvA-mxii1WzwGQ"; // TODO: Avoid hard coding my token!
        
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

            await InstallCommandsAsync();
            
            _client.Log += ClientOnLog;

            await _client.LoginAsync(TokenType.Bot, Token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task InstallCommandsAsync()
        {
            // Hook the MessageReceived Event into our command Handler.
            _client.MessageReceived += HandleCommandsAsync;

            // Discover all of the commands in this assembly and load them.
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandsAsync(SocketMessage arg)
        {
            // Don't process the command if it was a System message.
            if (!(arg is SocketUserMessage message)) return;
            
            // Create a number to track where the prefix ends and the command begins.
            var argPos = 0;
            
            // Determine if the message is a command, based on if it starts with '!' or a mention prefix.
            if (!(message.HasCharPrefix('!', ref argPos)) | message.HasMentionPrefix(_client.CurrentUser, ref argPos)) return ;

            // Create a Command Context
            var context = new SocketCommandContext(_client, message);
            
            // Execute the command. (result does not indicate a return value, rather an object stating if the command exectuted succesfully)
            var result = await _commandService.ExecuteAsync(context, argPos, _serviceProvider);
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }

        private static Task ClientOnLog(LogMessage logMessage)
        {
            Console.WriteLine(logMessage.ToString());
            return Task.CompletedTask;
        }
    }
}