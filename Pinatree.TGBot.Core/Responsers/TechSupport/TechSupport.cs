using Pinatree.TGBot.ICore.Responsers.Base;
using Pinatree.TGBot.ISender;
using Pinatree.TGBot.IUsersDataService.DataSource;
using Pinatree.TGBot.IUsersDataService.Entities;

namespace Pinatree.TGBot.Core.StateResponsers.Main
{
    public sealed class TechSupport(IChatsDataSource chatsDataSource, IResponsersFabric responsersFabric, IMessageResponseSender responseSender) : ByStateResponser(chatsDataSource, responsersFabric, responseSender)
    {
        public override async Task ResponseMessage(long chatId, string messageText)
        {
            switch (messageText) {
                case "Назад":
                    await _responsersFabric.GetByStateResponser(RESP_TYPE.MAIN).NavTo(chatId, messageText);
                    return;
                default:
                    await HandleSupportRequest(chatId, messageText);
                    break;
            }
        }

        async Task HandleSupportRequest(long chatId, string messageText)
        {
            Console.WriteLine(messageText);

            await _responsersFabric.GetByStateResponser(RESP_TYPE.MAIN).NavTo(chatId, messageText);
        }

        public override async Task NavTo(long chatId, string messageText)
        {
            _chatsDataSource.SetChatThread(chatId, RESP_TYPE.TECHSUPPORT);

            IEnumerable<IEnumerable<string>> availableKeys = new List<List<string>>()
            {
                new List<string>
                {
                    "Назад",
                }
            };

            await _messageResponseSender.SendMessageWithScreenKeyboard(chatId, "Обращение в техподдержку", availableKeys);
        }

    }
}
