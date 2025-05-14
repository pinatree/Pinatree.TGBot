using Pinatree.TGBot.ICore.InputHandlers;
using Pinatree.TGBot.ICore.Responsers.Base;
using Pinatree.TGBot.IUsersDataService.DataSource;
using Pinatree.TGBot.IUsersDataService.Entities;

namespace Pinatree.TGBot.Core.InputHandlers
{
    public sealed class InputHandler : IInputHandler
    {
        IChatsDataSource _chatsDataSource;
        IResponsersFabric _responsersFabric;

        public InputHandler(IChatsDataSource chatsDataSource, IResponsersFabric responsersFabric)
        {
            _chatsDataSource = chatsDataSource;
            _responsersFabric = responsersFabric;
        }

        public async Task HandleMessage(long chatId, string message)
        {
            ChatData neededChatData = _chatsDataSource.GetChatDataById(chatId);

            ByStateResponser responseHandler = _responsersFabric.GetByStateResponser(neededChatData.CurrentChatThread);

            await responseHandler.ResponseMessage(chatId, message);
        }
    }
}