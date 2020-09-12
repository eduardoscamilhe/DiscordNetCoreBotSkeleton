using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Linq;
namespace App.Modules
{
    public class Example : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingDefault()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Pong!").WithDescription("Pong Pong Pong!").WithColor(Color.Blue);
            await ReplyAsync("", false, builder.Build());
        }

        [Command("clean"), RequireUserPermission(ChannelPermission.ManageMessages)]
        public async Task Clean(int delnum = 0)
        {
            var messages = await Context.Channel.GetMessagesAsync(delnum + 1).FlattenAsync();
            var channel = (Context.Channel as SocketTextChannel);

            try
            {
                await channel.DeleteMessagesAsync(messages);
            }
            catch 
            {

                if (delnum > 100)
                {
                    await ReplyAsync("100 é o maximo de mensagens a serem excluidas.");
                    return;
                }

                foreach (var msg in messages)
                {
                    await channel.DeleteMessageAsync(msg.Id);
                }

            }
        }

        [Command("invite")]
        public async Task InviteDiscord()
        {
            try
            {
                var arrInvite = await Context.Guild.GetInvitesAsync();
                await Context.User.SendMessageAsync(arrInvite.FirstOrDefault().ToString());
            }
            catch { }
        }
    }
}
