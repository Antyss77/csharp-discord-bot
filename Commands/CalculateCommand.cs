using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace HyBot.Commands {
    public class CalculateCommand : ApplicationCommandModule {
        [SlashCommand("calculate", "Calculate a math expression.")]
        public async Task CalculatorCommandAsync(InteractionContext ctx,
            [Option("expression", "The math expression to calculate.")] string expression) {
            // Calculate the result of the expression
            double result = 0;
            try
            {
                result = Convert.ToDouble(new System.Data.DataTable().Compute(expression, null));
            }
            catch (Exception)
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder().WithContent("Invalid expression."));
                return;
            }

            // Create the embed for the result
            var embed = new DiscordEmbedBuilder()
                .WithTitle("Calculator")
                .WithDescription($"`{expression}` = **{result}**")
                .WithFooter(
                    $"Calculated by {ctx.User.Username} at {DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")} in #{ctx.Channel.Name}")
                .WithColor(DiscordColor.Green);

            // Send the embed to the channel
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder().AddEmbed(embed.Build()));
        }
    }
}