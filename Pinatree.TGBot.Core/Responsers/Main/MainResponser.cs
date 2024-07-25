using Pinatree.TGBot.IUsersDataService.DataSource;
using Pinatree.TGBot.IUsersDataService.Entities;
using Pinatree.TGBot.ICore.Responsers.Base;
using Pinatree.TGBot.ISender;

namespace Pinatree.TGBot.Core.StateResponsers.Main
{
    public class MainResponser(IChatsDataSource chatsDataSource, IResponsersFabric responsersFabric, IMessageResponseSender responseSender) : ByStateResponser(chatsDataSource, responsersFabric, responseSender)
    {
        public override async Task ResponseMessage(long chatId, string messageText)
        {
            switch (messageText) {
                case "Список услуг":
                    await _responsersFabric.GetByStateResponser(RESP_TYPE.SERVICES).NavTo(chatId, messageText);
                    return;
                case "Запросить обратную связь":
                    await _responsersFabric.GetByStateResponser(RESP_TYPE.FEEDBACK).NavTo(chatId, messageText);
                    return;
                case "Техническая поддержка":
                    await _responsersFabric.GetByStateResponser(RESP_TYPE.TECHSUPPORT).NavTo(chatId, messageText);
                    return;
                default:
                    await NavTo(chatId, messageText);
                    return;
            }
        }

        public override async Task NavTo(long chatId, string messageText)
        {
            _chatsDataSource.SetChatThread(chatId, RESP_TYPE.MAIN);

            IEnumerable<IEnumerable<string>> availableKeys = new List<List<string>>()
            {
                new List<string>
                {
                    "Список услуг",
                    "Запросы на услуги",
                },
                new List<string>
                {
                    "Запросить обратную связь",
                    "Техническая поддержка",
                }
            };

            await _messageResponseSender.SendMessageWithScreenKeyboard(chatId, "Добро пожаловать в страничку компании", availableKeys);
        }
    }
}
