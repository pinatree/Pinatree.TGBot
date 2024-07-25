using Pinatree.TGBot.IUsersDataService.Entities;

namespace Pinatree.TGBot.IUsersDataService.DataSource
{
    public interface IChatsDataSource
    {
        ChatData GetChatDataById(long chatId);

        void SetChatThread(long chatId, RESP_TYPE threadName);
    }
}
