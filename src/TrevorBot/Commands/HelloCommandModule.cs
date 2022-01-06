using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace TrevorBot.Commands
{
    internal class HelloCommandModule : BaseCommandModule
    {
        public Random Rng { get; set; }

        private int _count = 0;

        [Command("HelloTrevor"), Aliases("ht", "trevor")]
        [Description("Simple Greeting from Trevor Bot")]
        public async Task HelloCommand(CommandContext ctx)
        {
            await ctx.RespondAsync("Oh! Hello there! My name is Trevor.");
        }

        [Command("ping"), Aliases("p")]
        [Description("Used to verify that Trevor bot it online")]
        public async Task PingCommand(CommandContext ctx)
        {
            var msg = await new DiscordMessageBuilder()
                .WithContent("PONG!")
                .SendAsync(ctx.Channel);

            // await ctx.RespondAsync("pong");
        }

        [Command("hello"), Aliases("h")]
        [Description("Say hello to Trevor or have Trevor say hello to someone else")]
        public async Task HelloTextCommand(CommandContext ctx, [RemainingText] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "my name is Trevor!";
            }

            if (name == "world" || name == "world!")
            {
                await ctx.RespondAsync("Hello You!");
                return;
            }

            await ctx.RespondAsync($"Oh! Hello there, {name}!");
        }

        [Command("greet")]
        [Description("Trevor will attempt to mention discord member")]
        public async Task GreetMemberCommand(CommandContext ctx, DiscordMember member)
        {
            Console.WriteLine("Hello!");
            await ctx.RespondAsync($"Hello, {member.Mention}! Enjoy the mention!");
        }

        [Command("random"), Aliases("r")]
        [Description("Creates a Random Number in range")]
        public async Task RandomCommand(CommandContext ctx,
            [Description("Loweest number in range")] int min = 1,
            [Description("Highest number in range")] int max = 20)
        {
            if (min > max)
            {
                var temp = min;
                min = max;
                max = temp;
            }

            await ctx.RespondAsync($"Your random number is {Rng.Next(min, max + 1)}");
        }

        [Command("count")]
        public async Task CountCommand(CommandContext ctx)
        {
            _count++;
            await ctx.RespondAsync($"Count is at {_count}");
        }
    }
}
