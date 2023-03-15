namespace HyBot;

public class Start {
    static void Main(string[] args) {
        var bot = new Bot();
        bot.RunAsync().GetAwaiter().GetResult();
    }
}