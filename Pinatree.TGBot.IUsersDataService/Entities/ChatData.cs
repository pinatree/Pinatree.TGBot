namespace Pinatree.TGBot.IUsersDataService.Entities
{
    [Serializable]
    public class ChatData
    {
        public long ChatId { get; set; }

        public RESP_TYPE CurrentChatThread { get; set; }
    }
}
