using Pinatree.TGBot.Core.StateResponsers.Main;
using Pinatree.TGBot.ICore.Responsers.Base;
using Pinatree.TGBot.ISender;
using Pinatree.TGBot.IUsersDataService.DataSource;
using Pinatree.TGBot.IUsersDataService.Entities;

namespace Pinatree.TGBot.ICore.Responsers.Routers
{
    public class ResponsersFabric : IResponsersFabric
    {
        IChatsDataSource _chatsDataSource;
        IMessageResponseSender _responseSender;

        public ResponsersFabric(IChatsDataSource chatsDataSource, IMessageResponseSender responseSender)
        {
            _chatsDataSource = chatsDataSource;
            _responseSender = responseSender;
        }

        public ByStateResponser GetByStateResponser(RESP_TYPE chatThread)
        {
            switch (chatThread)
            {
                case RESP_TYPE.MAIN:
                    return new MainResponser(_chatsDataSource, this, _responseSender);
                case RESP_TYPE.SERVICES:
                    return new ServicesResponser(_chatsDataSource, this, _responseSender);
                case RESP_TYPE.FEEDBACK:
                    return new FeedbackResponser(_chatsDataSource, this, _responseSender);
                case RESP_TYPE.TECHSUPPORT:
                    return new TechSupport(_chatsDataSource, this, _responseSender);
                default:
                    return new ServicesResponser(_chatsDataSource, this, _responseSender);
            }
        }
    }
}
