namespace Pinatree.TGBot.Startup
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var client = new Pinatree.TGBot.TGClient.Client.TGClient("7320815095:AAENFUqW2J-UVAX2oCVK7e5Hkf7Dsq82pPE");

            await client.RunServe();
        }
    }
}
