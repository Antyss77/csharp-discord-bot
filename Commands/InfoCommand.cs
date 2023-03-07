using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace HyBot.Commands {
    public class InfoCommand : ApplicationCommandModule {
        [SlashCommand("info", "Get public information about a Discord user.")]
        public async Task InfoCommandAsync(InteractionContext ctx,
            [Option("user", "The user to get information about.")]
            DiscordUser user) {
            // Récupérer l'objet DiscordGuild représentant le serveur
            var guild = await ctx.Client.GetGuildAsync(
                1078750780485537872); // Identifiant serveur

            // Récupérer l'objet DiscordMember représentant l'utilisateur
            var member = await guild.GetMemberAsync(user.Id);

            // Créer une réponse avec les informations obtenues
            var responseBuilder = new DiscordInteractionResponseBuilder()
                .WithContent($"Voici les informations publiques sur l'utilisateur {member.Username} :")
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithTitle("Informations publiques")
                    .WithDescription($"Voici les informations publiques sur l'utilisateur {member.Username} :")
                    .AddField("Nom", member.Username, true)
                    .AddField("ID", member.Id.ToString(), true)
                    .AddField("#", member.Discriminator, true)
                    .AddField("Surnom", member.Nickname ?? "Aucun", true)
                    .AddField("Compte créé le", member.CreationTimestamp.ToString("dd/MM/yyyy"), true)
                    .AddField("A rejoint le serveur le", member.JoinedAt.ToString("dd/MM/yyyy") ?? "Inconnu", true)
                    .AddField("Bot", member.IsBot.ToString(), true)
                    .AddField("Permissions", string.Join(", ", member.Permissions.ToString()), true)
                    .AddField("Rôles", string.Join(", ", member.Roles.Select(r => r.Name)), true)
                    .WithThumbnail(member.AvatarUrl)
                    .WithColor(DiscordColor.Green));

            // Envoyer la réponse à la commande slash
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, responseBuilder);
        }
    }
}