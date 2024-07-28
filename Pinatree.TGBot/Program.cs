using Pinatree.TGBot.DataHandler.IHandler;
using Pinatree.TGBot.DataHandler.Handler;

namespace Pinatree.TGBot.Startup
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string accessToken = args[0];

            Console.WriteLine("Server initializing...");

            IDataHandler client = new DefaultDataHandler(accessToken);

            await client.RunServe();
        }
    }
}
