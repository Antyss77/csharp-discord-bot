using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace HyBot.Commands; 

public class PingCommand : ApplicationCommandModule {
    [SlashCommand("ping", "Check the bot's latency.")]
    public async Task PingCommandAsync(InteractionContext ctx) {
        var builder = new DiscordEmbedBuilder()
            .WithTitle("Pong!")
            .WithDescription($"Latency: {ctx.Client.Ping}ms")
            .WithColor(DiscordColor.Gold);

        await ctx.Channel.SendMessageAsync(embed: builder);
    }
    
    
}
