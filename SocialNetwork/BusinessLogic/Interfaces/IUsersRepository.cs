using System;
using System.Collections.Generic;
using System.Web.Security;

namespace BusinessLogic.Interfaces
{
    public interface IUsersRepository
    {
        MembershipUser GetMembershipUserByName(String userName);
        String GetUserNameByEmail(String email);

        void CreateUser(String username, String password, String email, String firstName,
            String lastName, String middleName);

        Boolean ValidateUser(String userName, String password);

        IEnumerable<Int32> SearchUsers(String firstName, String lastName, String middleName);

        String GetFirstNameById(Int32 id);
        String GetLastNameById(Int32 id);
        String GetMiddleNameById(Int32 id);
    }
}
