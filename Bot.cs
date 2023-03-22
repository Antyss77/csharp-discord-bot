using System.Text;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using HyBot.Commands;
using Newtonsoft.Json;

namespace HyBot {
    public class Bot {
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync() {
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync();

            var configJson = JsonConvert.DeserializeObject<ConfigJSON>(json);
            var config = new DiscordConfiguration()
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            Client = new DiscordClient(config);
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });


            var slashCommandsConfig = Client.UseSlashCommands();
            slashCommandsConfig.RegisterCommands<PingCommand>();
            slashCommandsConfig.RegisterCommands<InfoCommand>();
            slashCommandsConfig.RegisterCommands<ClearCommand>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }


        private Task OnClientReady(ReadyEventArgs e) {
            return Task.CompletedTask;
        }
    }
}