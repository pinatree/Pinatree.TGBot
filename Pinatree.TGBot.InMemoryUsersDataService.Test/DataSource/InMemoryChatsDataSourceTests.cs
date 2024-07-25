using Pinatree.TGBot.InMemoryUsersDataService.DataSource;
using Pinatree.TGBot.IUsersDataService.Entities;

namespace Pinatree.TGBot.InMemoryUsersDataService.Test.DataSource
{
    public sealed class Tests
    {
        [Test]
        public void NewUserAppendsSuccessfully()
        {
            InMemoryChatsDataSource _testInMemoryDataSource = new InMemoryChatsDataSource();

            long newUserId = 1;

            ChatData appendedUser = _testInMemoryDataSource.GetChatDataById(newUserId);

            Assert.IsNotNull(appendedUser);
        }

        [Test]
        public void NewChatsHasCorrectId()
        {
            InMemoryChatsDataSource _testInMemoryDataSource = new InMemoryChatsDataSource();

            long newUserId = 2;

            ChatData appendedUser = _testInMemoryDataSource.GetChatDataById(newUserId);

            Assert.IsTrue(appendedUser.ChatId == newUserId);
        }

        [Test]
        public void NewChatsHasInitiallyMainThread()
        {
            InMemoryChatsDataSource _testInMemoryDataSource = new InMemoryChatsDataSource();

            long newUserId = 3;

            ChatData appendedUser = _testInMemoryDataSource.GetChatDataById(newUserId);

            Assert.IsTrue(appendedUser.CurrentChatThread == RESP_TYPE.MAIN);
        }

        [Test]
        public void CanAppendManyChats()
        {
            InMemoryChatsDataSource _testInMemoryDataSource = new InMemoryChatsDataSource();

            long newUserId = 4;
            ChatData appendedUser_1 = _testInMemoryDataSource.GetChatDataById(newUserId);
            newUserId = 5;
            ChatData appendedUser_2 = _testInMemoryDataSource.GetChatDataById(newUserId);

            Assert.IsTrue(appendedUser_1.ChatId != appendedUser_2.ChatId);
        }

        [Test]
        public void CanChangeChatThread()
        {
            InMemoryChatsDataSource _testInMemoryDataSource = new InMemoryChatsDataSource();

            long newUserId = 6;
            ChatData appendedUser = _testInMemoryDataSource.GetChatDataById(newUserId);
            _testInMemoryDataSource.SetChatThread(appendedUser.ChatId, RESP_TYPE.SERVICES);
            ChatData reselectedUser = _testInMemoryDataSource.GetChatDataById(newUserId);

            Assert.IsTrue(reselectedUser.CurrentChatThread == RESP_TYPE.SERVICES);
        }
    }
}