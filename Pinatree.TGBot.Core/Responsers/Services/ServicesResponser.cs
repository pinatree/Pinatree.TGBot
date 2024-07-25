using Pinatree.TGBot.IUsersDataService.DataSource;
using Pinatree.TGBot.IUsersDataService.Entities;
using Pinatree.TGBot.ICore.Responsers.Base;
using Pinatree.TGBot.ISender;

namespace Pinatree.TGBot.Core.StateResponsers.Main
{
    public class ServicesResponser(IChatsDataSource chatsDataSource, IResponsersFabric responsersFabric, IMessageResponseSender responseSender) : ByStateResponser(chatsDataSource, responsersFabric, responseSender)
    {
        public override async Task ResponseMessage(long chatId, string messageText)
        {
            switch (messageText) {
                case "Назад":
                    await _responsersFabric.GetByStateResponser(RESP_TYPE.MAIN).NavTo(chatId, messageText);
                    return;
                default:
                    await NavTo(chatId, messageText);
                    break;
            }
        }

        public override async Task NavTo(long chatId, string messageText)
        {
            _chatsDataSource.SetChatThread(chatId, RESP_TYPE.SERVICES);

            IEnumerable<IEnumerable<string>> availableKeys = new List<List<string>>()
            {
                new List<string>
                {
                    "Дизайн",
                    "Лендинг",
                },
                new List<string>
                {
                    "Искусственный интеллект",
                    "Буст в дотане",
                },
                new List<string>
                {
                    "Назад",
                }
            };

            await _messageResponseSender.SendMessageWithScreenKeyboard(chatId, "Выберите интересующие вас услуги", availableKeys);
        }

    }
}
