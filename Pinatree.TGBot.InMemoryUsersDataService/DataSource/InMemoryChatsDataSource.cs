using Pinatree.TGBot.IUsersDataService.DataSource;
using Pinatree.TGBot.IUsersDataService.Entities;

namespace Pinatree.TGBot.InMemoryUsersDataService.DataSource
{
    public sealed class InMemoryChatsDataSource : IChatsDataSource
    {
        const RESP_TYPE INITIAL_CHAT_THREAD = RESP_TYPE.MAIN;
        static List<ChatData> _userData = new List<ChatData>();
        
        public ChatData GetChatDataById(long chatId)
        {
            ChatData? found = _userData.FirstOrDefault(x => x.ChatId == chatId);

            if(found != null)
            {
                return found;
            }

            ChatData newChat = CreateChatWithId(chatId);

            return newChat;
        }

        public void SetChatThread(long chatId, RESP_TYPE threadName)
        {
            ChatData foundChat = this.GetChatDataById(chatId);

            foundChat.CurrentChatThread = threadName;
        }

        ChatData CreateChatWithId(long chatId)
        {
            ChatData newChat = new ChatData()
            {
                ChatId = chatId,
                CurrentChatThread = INITIAL_CHAT_THREAD
            };

            _userData.Add(newChat);

            return newChat;
        }
    }
}
