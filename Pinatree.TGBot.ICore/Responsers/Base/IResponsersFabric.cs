using Pinatree.TGBot.IUsersDataService.Entities;

namespace Pinatree.TGBot.ICore.Responsers.Base
{
    public interface IResponsersFabric
    {
        public ByStateResponser GetByStateResponser(RESP_TYPE chatThread);
    }
}
