using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Pinatree.TGBot.IUsersDataService.DataSource;
using Pinatree.TGBot.ICore.InputHandlers;
using Pinatree.TGBot.Core.InputHandlers;
using Pinatree.TGBot.ICore.Responsers.Base;
using Pinatree.TGBot.DataHandler.IHandler;
using Pinatree.TGBot.ICore.Responsers.Routers;
using Pinatree.TGBot.ISender;
using Pinatree.TGBot.DefaultSender;

namespace Pinatree.TGBot.DataHandler.Handler
{
    public class DefaultDataHandler : IDataHandler
    {
        IInputHandler messagesHandler;

        ITelegramBotClient? _botClient;
        ReceiverOptions? _receiverOptions;
        string _accessToken;
        IChatsDataSource _chatsDataSource;

        public DefaultDataHandler(string accessToken, IChatsDataSource chatsDataSource)
        {
            _accessToken = accessToken;
            _chatsDataSource = chatsDataSource;
        }

        public async Task RunServe()
        {
            InitConnection();

            messagesHandler = new InputHandler(_chatsDataSource, new ResponsersFabric(_chatsDataSource, new MessageResponseSender(_botClient)));

            using var cts = new CancellationTokenSource();

            _botClient?.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token);

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

        private async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                long chatId = update.Message.Chat.Id;
                string message = update.Message.Text;
                await messagesHandler.HandleMessage(chatId, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
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
