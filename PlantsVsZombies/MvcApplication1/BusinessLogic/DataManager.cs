using BusinessLogic.Interfaces;

namespace BusinessLogic
{
    //Класс, через который централизованно просиходит обмен данными в приложении
    public class DataManager
    {
        private IUsersRepository usersRepository;
        private IGameResultsRepository gameResultsRepository;
        private IGameSettingsRepository gameSettingsRepository;
        private PrimaryMembershipProvider provider;

        public DataManager(IUsersRepository usersRepository,
                           IGameResultsRepository gameResultsRepository,
                           IGameSettingsRepository gameSettingsRepository,
                           PrimaryMembershipProvider provider)
        { 
            this.usersRepository = usersRepository;
            this.gameResultsRepository = gameResultsRepository;
            this.gameSettingsRepository = gameSettingsRepository;
            this.provider = provider;
        }

        public IUsersRepository Users { get { return usersRepository; } }
        public IGameResultsRepository GameResults { get { return gameResultsRepository; } }
        public IGameSettingsRepository GameSettings { get { return gameSettingsRepository; } }
        public PrimaryMembershipProvider MembershipProvider { get { return provider; } }
    }
}
