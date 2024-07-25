namespace Pinatree.TGBot.ICore.InputHandlers
{
    public interface IInputHandler
    {
        Task HandleMessage(long chatId, string message);
    }
}
