using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using BusinessLogic.Interfaces;
using Domain;
using Domain.Entities;

namespace BusinessLogic.Implementations
{
    public class EFGameResultsRepository : IGameResultsRepository
    {
        private EFDbContext context;

        public EFGameResultsRepository(EFDbContext context)
        {
            this.context = context;
        }
        
        public void AddGameResult(int userId, int destroyedZombies, DateTime gameDate)
        {
            GameResult res = new GameResult
            {
                UserId = userId,
                DestroyedZombies = destroyedZombies,
                GameDate = gameDate
            };
            context.GameResults.Add(res);
            context.SaveChanges();
        }

        public IEnumerable<int> GetTopResultsForUser(int topNum, int userId)
        {
            return (from res in context.GameResults
                    where res.UserId == userId
                    orderby res.DestroyedZombies descending, res.GameDate descending 
                    select res.Id).Take(topNum).ToList();
        }

        public IEnumerable<int> GetTopResults(int topNum)
        {
            return (from res in context.GameResults
                    orderby res.DestroyedZombies descending, res.GameDate descending
                    select res.Id).Take(topNum).ToList();
        }


        public Int32 GetUserIdById(Int32 id)
        {
            return (from res in context.GameResults
                where res.Id == id
                select res.UserId).FirstOrDefault();
        }

        public Int32 GetDestroyedZombiesById(Int32 id)
        {
            return (from res in context.GameResults
                    where res.Id == id
                    select res.DestroyedZombies).FirstOrDefault();
        }

        public DateTime GetDateTimeById(Int32 id)
        {
            return (from res in context.GameResults
                    where res.Id == id
                    select res.GameDate).FirstOrDefault();
        }
    }
}