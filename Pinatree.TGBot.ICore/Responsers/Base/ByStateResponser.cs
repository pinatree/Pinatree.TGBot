using Pinatree.TGBot.ISender;
using Pinatree.TGBot.IUsersDataService.DataSource;

namespace Pinatree.TGBot.ICore.Responsers.Base
{
    public abstract class ByStateResponser
    {
        protected IChatsDataSource _chatsDataSource;
        protected IResponsersFabric _responsersFabric;
        protected IMessageResponseSender _messageResponseSender;

        public ByStateResponser(IChatsDataSource chatsDataSource, IResponsersFabric responsersFabric, IMessageResponseSender messageResponseSender)
        {
            _chatsDataSource = chatsDataSource;
            _responsersFabric = responsersFabric;
            _messageResponseSender = messageResponseSender;
        }

        public abstract Task ResponseMessage(long chatId, string messageText);
        public abstract Task NavTo(long chatId, string messageText);
    }
}
