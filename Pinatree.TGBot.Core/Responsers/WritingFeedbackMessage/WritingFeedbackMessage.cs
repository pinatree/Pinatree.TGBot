using Pinatree.TGBot.ICore.Responsers.Base;
using Pinatree.TGBot.ISender;
using Pinatree.TGBot.IUsersDataService.DataSource;
using Pinatree.TGBot.IUsersDataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinatree.TGBot.Core.Responsers.WritingFeedbackMessage
{
    public class WritingFeedbackMessage(IChatsDataSource chatsDataSource, IResponsersFabric responsersFabric, IMessageResponseSender responseSender) : ByStateResponser(chatsDataSource, responsersFabric, responseSender)
    {
        public override async Task ResponseMessage(long chatId, string messageText)
        {
            switch (messageText)
            {
                case "Назад":
                    await responsersFabric.GetByStateResponser(RESP_TYPE.FEEDBACK).NavTo(chatId, messageText);
                    return;
                default:
                    await HandleFeedbackMessage(chatId, messageText);
                    break;
            }
        }

        async Task HandleFeedbackMessage(long chatId, string messageText)
        {
            Console.WriteLine(messageText);

            await _responsersFabric.GetByStateResponser(RESP_TYPE.FEEDBACK).NavTo(chatId, messageText);
        }

        public override async Task NavTo(long chatId, string messageText)
        {
            _chatsDataSource.SetChatThread(chatId, RESP_TYPE.WRITE_FEEDBACK_MESSAGE);

            IEnumerable<IEnumerable<string>> availableKeys = new List<List<string>>()
            {
                new List<string>
                {
                    "Назад"
                }
            };

            await _messageResponseSender.SendMessageWithScreenKeyboard(chatId, "Напишите сообщение для нас, мы с вами свяжемся", availableKeys);
        }
    }
}
