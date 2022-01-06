using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrevorBot.Commands
{
    internal class InteractivityCommandModule : BaseCommandModule
    {
        private string _loremIpsum = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Etiam tempor orci eu lobortis elementum nibh tellus molestie nunc. Feugiat in fermentum posuere urna nec. Lobortis mattis aliquam faucibus purus in massa tempor nec. Tincidunt eget nullam non nisi est sit. At auctor urna nunc id cursus metus. In ante metus dictum at. Sed libero enim sed faucibus turpis. Eleifend quam adipiscing vitae proin sagittis nisl rhoncus mattis. Vestibulum morbi blandit cursus risus.

Amet purus gravida quis blandit turpis cursus in hac habitasse.Arcu bibendum at varius vel. Odio ut enim blandit volutpat maecenas. Proin sagittis nisl rhoncus mattis rhoncus urna neque. Eu ultrices vitae auctor eu augue ut.Adipiscing elit duis tristique sollicitudin nibh sit amet. Malesuada nunc vel risus commodo viverra maecenas accumsan lacus vel. Congue quisque egestas diam in arcu cursus euismod quis. Eget lorem dolor sed viverra ipsum nunc aliquet bibendum enim. At elementum eu facilisis sed odio. Consequat mauris nunc congue nisi vitae suscipit tellus mauris a. Ultrices tincidunt arcu non sodales neque sodales ut etiam sit. Commodo quis imperdiet massa tincidunt nunc pulvinar.Pharetra vel turpis nunc eget lorem dolor sed viverra ipsum. Sit amet luctus venenatis lectus magna fringilla urna porttitor.Nulla facilisi cras fermentum odio.Ut faucibus pulvinar elementum integer enim.

Aliquam ut porttitor leo a diam sollicitudin tempor. Ut enim blandit volutpat maecenas volutpat blandit aliquam etiam erat. Nullam ac tortor vitae purus.Eget lorem dolor sed viverra ipsum nunc.Enim ut sem viverra aliquet eget. Elit scelerisque mauris pellentesque pulvinar pellentesque habitant morbi tristique.Gravida cum sociis natoque penatibus et. Consectetur a erat nam at lectus urna duis. Porttitor lacus luctus accumsan tortor posuere ac.Lobortis feugiat vivamus at augue eget arcu dictum varius duis. In est ante in nibh mauris cursus mattis. Tempor orci eu lobortis elementum nibh tellus molestie nunc non.

Magna etiam tempor orci eu lobortis. Rhoncus est pellentesque elit ullamcorper dignissim cras tincidunt. Fringilla ut morbi tincidunt augue interdum velit euismod in. Convallis posuere morbi leo urna.Sagittis id consectetur purus ut.Viverra suspendisse potenti nullam ac tortor vitae purus faucibus.Tristique sollicitudin nibh sit amet commodo. Ut tristique et egestas quis ipsum suspendisse.Vulputate sapien nec sagittis aliquam.Leo vel fringilla est ullamcorper eget nulla facilisi. Vel turpis nunc eget lorem dolor sed viverra ipsum.Eu sem integer vitae justo eget. Feugiat scelerisque varius morbi enim nunc. Et sollicitudin ac orci phasellus egestas tellus rutrum. Dapibus ultrices in iaculis nunc sed augue lacus viverra vitae.Vitae ultricies leo integer malesuada nunc vel risus commodo.

Arcu risus quis varius quam quisque id diam. Faucibus in ornare quam viverra orci sagittis eu volutpat.Commodo viverra maecenas accumsan lacus vel facilisis volutpat est.Diam vulputate ut pharetra sit amet aliquam.Amet volutpat consequat mauris nunc congue nisi vitae suscipit tellus. Erat velit scelerisque in dictum non consectetur a erat nam. Netus et malesuada fames ac turpis egestas.Ut faucibus pulvinar elementum integer.Ipsum dolor sit amet consectetur adipiscing elit duis tristique sollicitudin. Tincidunt vitae semper quis lectus nulla at volutpat diam.Leo urna molestie at elementum eu. Dolor magna eget est lorem ipsum dolor sit amet.A diam sollicitudin tempor id eu nisl nunc mi ipsum. At tellus at urna condimentum mattis. Ut consequat semper viverra nam libero justo laoreet. Elementum facilisis leo vel fringilla est ullamcorper eget nulla facilisi. Nibh sed pulvinar proin gravida hendrerit lectus.Orci ac auctor augue mauris augue neque gravida. Aliquet eget sit amet tellus cras adipiscing enim eu turpis.";

        [Command("lorem")]
        [Description("get a few pages of jibberish to read")]
        public async Task MyPagesCommand(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var pages = interactivity.GeneratePagesInEmbed(_loremIpsum);

            await ctx.Channel.SendPaginatedMessageAsync(ctx.Member, pages, null, ButtonPaginationBehavior.DeleteButtons);
        }

        [Command("thumb")]
        public async Task ReactionCommand(CommandContext ctx, DiscordMember member)
        {
            var emoji = DiscordEmoji.FromName(ctx.Client, ":thumbsup:");
            var message = await ctx.RespondAsync($"{member.Mention}, react with {emoji}.");

            var result = await message.WaitForReactionAsync(member, emoji);

            if (!result.TimedOut) await ctx.RespondAsync("Thank you!");
        }

        [Command("collect")]
        public async Task CollectionCommand(CommandContext ctx)
        {
            var message = await ctx.RespondAsync("30 seconds to React here!");
            var reactions = await message.CollectReactionsAsync();

            var strBuilder = new StringBuilder();
            foreach (var reaction in reactions)
            {
                strBuilder.AppendLine($"{reaction.Emoji}: {reaction.Total}");
            }

            await ctx.RespondAsync(strBuilder.ToString());
        }

        [Command("confirm")]
        public async Task ConfirmCommand(CommandContext ctx)
        {
            await ctx.RespondAsync($"{ctx.Message.Author.Mention}, Respond with *confirm* to continue.");
            var result = await ctx.Message.GetNextMessageAsync(m =>
            {
                if (m.Content.ToLower() != "confirm")
                {
                    ctx.RespondAsync("I don't under stand that, please respond with *confirm*")
                        .GetAwaiter().GetResult();
                    return false;
                }
                return true;
            });

            if (result.TimedOut)
            {
                await ctx.RespondAsync($"{ctx.Message.Author.Mention}, ran out of time");
                return;
            }
            
            await ctx.RespondAsync($"Action confirmed by {result.Result.Author.Mention}");
        }
    }
}
