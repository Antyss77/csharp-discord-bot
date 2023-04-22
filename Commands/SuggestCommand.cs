using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Threading.Tasks;

namespace HyBot.Commands
{
    public class SuggestCommand : ApplicationCommandModule
    {
        [SlashCommand("suggest", "Suggest a new feature for the bot.")]
        public async Task SuggestCommandAsync(InteractionContext ctx, [Option("suggestion", "Your suggestion.")] string suggestion)
        {
            // Get the suggestion channel
            var suggestionChannel = ctx.Guild.GetChannel(1088113798939955321);

            // Check if the suggestion channel exists
            if (suggestionChannel == null || suggestionChannel.Type != ChannelType.Text)
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("The suggestion channel was not found."));
                return;
            }

            // Create the embed for the suggestion
            var embed = new DiscordEmbedBuilder()
                .WithTitle("New suggestion")
                .WithDescription(suggestion)
                .WithFooter($"Suggestion made by {ctx.User.Username} at {DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")} in #{ctx.Channel.Name}")
                .WithColor(DiscordColor.Green);

            // Check if the user has a profile picture
            if (ctx.User.AvatarUrl != null)
            {
                // Add the user's profile picture to the embed
                embed.WithThumbnail(ctx.User.AvatarUrl);
            }

            // Send the embed to the suggestion channel
            await suggestionChannel.SendMessageAsync(embed: embed.Build());

            // Respond to the user to confirm that the suggestion was sent
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Thank you for your suggestion!"));
        }
    }
}