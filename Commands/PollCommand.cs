using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Threading.Tasks;

namespace HyBot.Commands {
    public class PollCommand : ApplicationCommandModule {
        [SlashCommand("poll", "Create a new poll.")]
        public async Task VoteCommandAsync(InteractionContext ctx,
            [Option("question", "The poll question.")]
            string question,
            [Option("option1", "Option 1.")] string option1,
            [Option("option2", "Option 2.")] string option2,
            [Option("option3", "Option 3.")] string option3 = null,
            [Option("option4", "Option 4.")] string option4 = null,
            [Option("option5", "Option 5.")] string option5 = null) {
            // Check if the user is an administrator
            if (!ctx.Member.Permissions.HasPermission(Permissions.Administrator))
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder().WithContent(
                        "You must be an administrator to create a poll."));
                return;
            }

            // Create the poll embed
            var pollEmbed = new DiscordEmbedBuilder()
                .WithTitle("New Poll")
                .WithDescription(question)
                .AddField("Option 1", option1, true)
                .AddField("Option 2", option2, true);

            // Add the additional options to the embed
            if (option3 != null)
                pollEmbed.AddField("Option 3", option3, true);

            if (option4 != null)
                pollEmbed.AddField("Option 4", option4, true);

            if (option5 != null)
                pollEmbed.AddField("Option 5", option5, true);

            // Add the user and date information to the embed footer
            pollEmbed.WithFooter($"Started by {ctx.User.Username} on {DateTime.Now.ToString()}");

            // Send the poll embed
            var message = await ctx.Channel.SendMessageAsync(embed: pollEmbed);

            // Add the reactions to the poll message
            await message.CreateReactionAsync(DiscordEmoji.FromUnicode("1️⃣"));
            await message.CreateReactionAsync(DiscordEmoji.FromUnicode("2️⃣"));

            if (option3 != null)
                await message.CreateReactionAsync(DiscordEmoji.FromUnicode("3️⃣"));

            if (option4 != null)
                await message.CreateReactionAsync(DiscordEmoji.FromUnicode("4️⃣"));

            if (option5 != null)
                await message.CreateReactionAsync(DiscordEmoji.FromUnicode("5️⃣"));
        }
    }
}