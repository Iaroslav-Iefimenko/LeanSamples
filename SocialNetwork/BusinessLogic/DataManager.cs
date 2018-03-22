using BusinessLogic.Interfaces;

namespace BusinessLogic
{
    //Класс, через который централизованно просиходит обмен данными в приложении
    public class DataManager
    {
        private IUsersRepository usersRepository;
        private IFriendsRepository friendsRepository;
        private IFriendRequestsRepository friendRequestsRepository;
        private IMessagesRepository messagesRepository;
        private IWallMessagesRepository wallMessagesRepository;
        private IPhotosRepository photosRepository;
        private PrimaryMembershipProvider provider;

        public DataManager(IUsersRepository usersRepository,
                           IFriendsRepository friendsRepository,
                           IFriendRequestsRepository friendRequestsRepository,
                           IMessagesRepository messagesRepository,
                           IWallMessagesRepository wallMessagesRepository,
                           IPhotosRepository photosRepository,
                           PrimaryMembershipProvider provider)
        { 
            this.usersRepository = usersRepository;
            this.friendsRepository = friendsRepository;
            this.friendRequestsRepository = friendRequestsRepository;
            this.messagesRepository = messagesRepository;
            this.wallMessagesRepository = wallMessagesRepository;
            this.photosRepository = photosRepository;
            this.provider = provider;
        }

        public IUsersRepository Users { get { return usersRepository; } }
        public IFriendsRepository Friends { get { return friendsRepository; } }
        public IFriendRequestsRepository FriendRequests { get { return friendRequestsRepository; } }
        public IMessagesRepository Messages { get { return messagesRepository; } }
        public IWallMessagesRepository WallMessages { get { return wallMessagesRepository; } }
        public IPhotosRepository Photos { get { return photosRepository; } }
        public PrimaryMembershipProvider MembershipProvider { get { return provider; } }
    }
}
