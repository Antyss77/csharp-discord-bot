using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace HyBot.Commands {
    public class ClearCommand : ApplicationCommandModule {
        [SlashCommand("clear", "Deletes a specified number of messages in a given channel.")]
        public async Task ClearCommandAsync(InteractionContext ctx,
            [Option("number", "The number of messages to delete.")]
            long number) {
            if (number < 1 || number > 100)
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder()
                        .WithContent("The number of messages must be between 1 and 100."));
                return;
            }

            var channel = ctx.Channel;
            var messages = await ctx.Channel.GetMessagesAsync((int) number);

            await channel.DeleteMessagesAsync(messages);

            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder()
                    .WithContent($"Successfully deleted {number} messages in {channel.Mention}."));

            // Auto-delete the response message after 3 seconds
            await Task.Delay(3000);
            await ctx.DeleteResponseAsync();
        }
    }
}