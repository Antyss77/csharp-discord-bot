using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace HyBot.Commands {
    public class InfoCommand : ApplicationCommandModule {
        [SlashCommand("info", "Get public information about a Discord user.")]
        public async Task InfoCommandAsync(InteractionContext ctx,
            [Option("user", "The user to get information about.")]
            DiscordUser user) {
            var guild = await ctx.Client.GetGuildAsync(
                1078750780485537872); // Identifiant serveur
            var member = await guild.GetMemberAsync(user.Id);

            var responseBuilder = new DiscordInteractionResponseBuilder()
                .WithContent($"Here is the public information about the user {member.Username} :")
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithTitle("Public Information")
                    .WithDescription($"Public information about the user {member.Username} :")
                    .AddField("Name", member.Username, true)
                    .AddField("ID", member.Id.ToString(), true)
                    .AddField("#", member.Discriminator, true)
                    .AddField("Surnom", member.Nickname ?? "Aucun", true)
                    .AddField("Created on", member.CreationTimestamp.ToString("dd/MM/yyyy"), true)
                    .AddField("Joined the server on", member.JoinedAt.ToString("dd/MM/yyyy") ?? "Inconnu", true)
                    .AddField("Bot", member.IsBot.ToString(), true)
                    .AddField("Permissions", string.Join(", ", member.Permissions.ToString()), true)
                    .AddField("Roles", string.Join(", ", member.Roles.Select(r => r.Name)), true)
                    .WithThumbnail(member.AvatarUrl)
                    .WithColor(DiscordColor.Green));

            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, responseBuilder);
        }
    }
}