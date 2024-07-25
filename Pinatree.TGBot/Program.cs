using Pinatree.TGBot.DataHandler.IHandler;
using Pinatree.TGBot.DataHandler.Handler;
using Pinatree.TGBot.InMemoryUsersDataService.DataSource;
using Pinatree.TGBot.IUsersDataService.DataSource;

namespace Pinatree.TGBot.Startup
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string accessToken = args[0];

            Console.WriteLine("Server initializing...");

            IChatsDataSource dataSource = new InMemoryChatsDataSource();

            IDataHandler client = new DefaultDataHandler(accessToken, dataSource);

            await client.RunServe();
        }
    }
}
