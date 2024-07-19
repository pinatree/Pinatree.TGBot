using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace Pinatree.TGBot.TGClient.Client
{
    public class TGClient
    {
        static ITelegramBotClient _botClient;
        static ReceiverOptions _receiverOptions;
        string _accessToken;

        public TGClient(string accessToken)
        {
            _accessToken = accessToken;
        }

        public async Task RunServe()
        {
            InitConnection();

            using var cts = new CancellationTokenSource();

            _botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token);

            var me = await _botClient.GetMeAsync();

            await Task.Delay(-1);
        }

        void InitConnection()
        {
            _botClient = new TelegramBotClient(_accessToken);
            _receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = [ UpdateType.Message ],
                ThrowPendingUpdates = true,
            };
        }

        private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        {
                            Console.WriteLine("Пришло сообщение!");
                            return;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
        {
            var ErrorMessage = error switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => error.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

    }
}
