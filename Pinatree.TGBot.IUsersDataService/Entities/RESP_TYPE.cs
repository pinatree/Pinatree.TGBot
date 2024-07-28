using System;

namespace Pinatree.TGBot.IUsersDataService.Entities
{
    public enum RESP_TYPE
    {
        MAIN,
        SERVICES,
        FEEDBACK,
        TECHSUPPORT,
        WRITE_FEEDBACK_MESSAGE
    }

    public static class RESP_TYPE_HELPER
    {
        public static RESP_TYPE DEFAULT_STATE = RESP_TYPE.MAIN;
        
        public static RESP_TYPE GetFromString(string value)
        {
            return (RESP_TYPE)Enum.Parse(typeof(RESP_TYPE), value);
        }

        public static string GetString(RESP_TYPE self)
        {
            return self.ToString();
        }
    }
}
