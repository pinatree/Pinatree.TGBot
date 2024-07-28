using Pinatree.TGBot.IUsersDataService.DataSource;
using Pinatree.TGBot.IUsersDataService.Entities;
using Pinatree.TGBot.ICore.Responsers.Base;
using Pinatree.TGBot.ISender;
using Pinatree.TGBot.Core.Responsers.WritingFeedbackMessage;
using Pinatree.TGBot.ICore.Responsers.Routers;

namespace Pinatree.TGBot.Core.StateResponsers.Main
{
    public class FeedbackResponser(IChatsDataSource chatsDataSource, IResponsersFabric responsersFabric, IMessageResponseSender responseSender) : ByStateResponser(chatsDataSource, responsersFabric, responseSender)
    {
        public override async Task ResponseMessage(long chatId, string messageText)
        {
            switch (messageText) {
                case "Назад":
                    await responsersFabric.GetByStateResponser(RESP_TYPE.MAIN).NavTo(chatId, messageText);
                    return;
                case "Написать сообщение":
                    await responsersFabric.GetByStateResponser(RESP_TYPE.WRITE_FEEDBACK_MESSAGE).NavTo(chatId, messageText);
                    return;
                default:
                    await NavTo(chatId, messageText);
                    break;
            }
        }

        public override async Task NavTo(long chatId, string messageText)
        {
            _chatsDataSource.SetChatThread(chatId, RESP_TYPE.FEEDBACK);

            IEnumerable<IEnumerable<string>> availableKeys = new List<List<string>>()
            {
                new List<string>
                {
                    "Написать сообщение",
                    "Реквизиты ООО",
                },
                new List<string>
                {
                    "Отзыв",
                    "Назад",
                }
            };

            await _messageResponseSender.SendMessageWithScreenKeyboard(chatId, "Мы рады любой обратной связи!", availableKeys);
        }
    }
}
