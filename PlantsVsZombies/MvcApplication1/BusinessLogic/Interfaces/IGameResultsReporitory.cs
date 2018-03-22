using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IGameResultsRepository
    {
        void AddGameResult(Int32 userId, Int32 destroyedZombies, DateTime gameDate);

        IEnumerable<Int32> GetTopResultsForUser(Int32 topNum, Int32 userId);
        IEnumerable<Int32> GetTopResults(Int32 topNum);

        Int32 GetUserIdById(Int32 id);
        Int32 GetDestroyedZombiesById(Int32 id);
        DateTime GetDateTimeById(Int32 id);
    }
}
