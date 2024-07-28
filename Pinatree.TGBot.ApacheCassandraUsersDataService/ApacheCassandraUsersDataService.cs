using Pinatree.TGBot.IUsersDataService.DataSource;
using Pinatree.TGBot.IUsersDataService.Entities;
using Cassandra;

namespace Pinatree.TGBot.ApacheCassandraUsersDataService
{
    public sealed class ApacheCassandraUsersDataService : IChatsDataSource
    {
        public ChatData GetChatDataById(long chatId)
        {
            ISession session = GetCassandraSession();

            string query = "SELECT * FROM chat where tg_id = " + chatId.ToString() + ";";

            List<Tuple<long, string>> neededChatData = session.Execute(query)
                .Select(
                    (row) =>
                    {
                        long id = row.GetValue<long>("tg_id");
                        string state = row.GetValue<string>("state");
                        return new Tuple<long, string>(id, state);
                    }
                ).ToList();
                    
            
            if(neededChatData.Count() == 0)
            {
                //create chat
                return CreateChatWithId(chatId);
            }

            Tuple<long, string> firstRecord = neededChatData.First();

            return new ChatData()
            {
                ChatId = firstRecord.Item1,
                CurrentChatThread = RESP_TYPE_HELPER.GetFromString(firstRecord.Item2)
            };
        }

        public void SetChatThread(long chatId, RESP_TYPE threadName)
        {
            ISession session = GetCassandraSession();

            string newState = RESP_TYPE_HELPER.GetString(threadName);
            RowSet neededChatData = session.Execute("SELECT * FROM chat where tg_id = " + chatId.ToString() + ";");
            if (neededChatData.Count() == 0)
            {
                //create chat
                CreateChatWithId(chatId);
            }

            session.Execute("UPDATE chat SET state = '" + newState + "' where tg_id = " + chatId.ToString() + ";");
        }

        ChatData CreateChatWithId(long chatId)
        {
            ISession session = GetCassandraSession();

            RESP_TYPE initialRespType = RESP_TYPE_HELPER.DEFAULT_STATE;

            string stringedInitialRespType = RESP_TYPE_HELPER.GetString(initialRespType);

            session.Execute("INSERT INTO chat(tg_id, state) VALUES(" + chatId.ToString() + ", '" + stringedInitialRespType + "');");

            return new ChatData()
            {
                ChatId = chatId,
                CurrentChatThread = initialRespType
            };
        }

        ISession GetCassandraSession()
        {
            Cluster cluster = Cluster.Builder().AddContactPoints("127.0.0.1").WithPort(9042).Build();
            ISession session = cluster.Connect();

            session.Execute("use telegram_bot");

            return session;
        }
    }
}
