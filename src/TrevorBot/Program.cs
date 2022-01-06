using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TrevorBot.Commands;
using TrevorBot.Commands.Converters;
using TrevorBot.Formatters;
using TrevorBot.Handlers;

class Program
{
    static bool _debugMode = true;

    private const string dateTimeFormat = "MMM dd yyyy - hh:mm:ss tt";
    private static readonly IEnumerable<string> _stringPrefixes = new[] { "!", ".", "#" };

    static async Task Main(string[] args)
    {
        await MainAsync(args);
    }

    static async Task MainAsync(string[] args)
    {
        var discord = new DiscordClient(new DiscordConfiguration
        {
            Token = Environment.GetEnvironmentVariable("DiscordToken"),
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged,
            MinimumLogLevel = _debugMode ? LogLevel.Debug : LogLevel.Information,
            LogTimestampFormat = dateTimeFormat
        });

        discord.MessageCreated += MessageCreatedHandler.Execute;

        var services = new ServiceCollection()
            .AddSingleton<Random>()
            .BuildServiceProvider();

        var commands = discord.UseCommandsNext(new CommandsNextConfiguration
        {
            StringPrefixes = _stringPrefixes,
            Services = services
        });

        commands.SetHelpFormatter<CustomHelpFormatter>();
        commands.RegisterConverter(new CustomArgumentConverter());
        commands.RegisterCommands<HelloCommandModule>();
        commands.RegisterCommands<InteractivityCommandModule>();

        discord.UseInteractivity(new InteractivityConfiguration()
        {
            PollBehaviour = PollBehaviour.KeepEmojis,
            Timeout = TimeSpan.FromSeconds(30)
        });

        await discord.ConnectAsync();
        await Task.Delay(-1);
    }
}