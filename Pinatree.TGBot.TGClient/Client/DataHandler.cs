using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Pinatree.TGBot.IUsersDataService.DataSource;
using Pinatree.TGBot.ICore.InputHandlers;
using Pinatree.TGBot.Core.InputHandlers;
using Pinatree.TGBot.DataHandler.IHandler;
using Pinatree.TGBot.ICore.Responsers.Routers;
using Pinatree.TGBot.DefaultSender;
using Pinatree.TGBot.ISender;
using Pinatree.TGBot.ICore.Responsers.Base;
using Autofac;
using Pinatree.TGBot.InMemoryUsersDataService.DataSource;

namespace Pinatree.TGBot.DataHandler.Handler
{
    public class DefaultDataHandler : IDataHandler
    {
        ITelegramBotClient? _botClient;
        ReceiverOptions? _receiverOptions;
        string _accessToken;

        IContainer _innerContainer;

        public DefaultDataHandler(string accessToken)
        {
            _accessToken = accessToken;
        }

        public async Task RunServe()
        {
            //Установка с соединением и авторизация
            InitConnection();

            _innerContainer = BuildDIContainer();

            _botClient?.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions);

            await Task.Delay(-1);
        }

        IContainer BuildDIContainer()
        {
            //TGBotClient is instance
            //IChatsDataSource is instance
            //TGBotClient -> MessageResponseSender
            //IChatsDataSource, MessageResponseSender -> ResponsersFabric
            //IChatsDataSource, ResponsersFabric -> InputHandler

            //Создаем контейнер
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<InputHandler>().As<IInputHandler>();
            builder.RegisterType<ResponsersFabric>().As<IResponsersFabric>();
            builder.RegisterType<ApacheCassandraUsersDataService.ApacheCassandraUsersDataService>().As<IChatsDataSource>();
            builder.RegisterType<MessageResponseSender>().As<IMessageResponseSender>();
            builder.RegisterInstance(_botClient).As<ITelegramBotClient>();

            return builder.Build();
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
                IInputHandler handler = _innerContainer.Resolve<IInputHandler>();

                long chatId = update.Message.Chat.Id;
                string message = update.Message.Text;

                await handler.HandleMessage(chatId, message);
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
