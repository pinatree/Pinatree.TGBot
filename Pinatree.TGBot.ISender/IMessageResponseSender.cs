namespace Pinatree.TGBot.ISender
{
    public interface IMessageResponseSender
    {
        Task SendMessage(long chatId, string message);
        Task SendMessageWithScreenKeyboard(long chatId, string message, IEnumerable<IEnumerable<string>> btns);
    }
}
