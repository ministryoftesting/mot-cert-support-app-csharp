using Timesheet.DB;
using Timesheet.Models.User;

namespace Timesheet.Service
{

    public class UserService
    {

        private readonly IUserDB _userDB;

        public UserService(IUserDB userDB)
        {
            _userDB = userDB;
        }

        public UserService()
        {
            _userDB = new UserDB();
        }

        public (int code, User createdUser) CreateUser(User user)
        {
            User createdUser = _userDB.CreateUser(user);

            if(createdUser != null){
                return (201, createdUser);
            } else {
                return (500, null);
            }
        }

        public int DeleteUser(int userId)
        {
            if(_userDB.DeleteUser(userId)){
                return 202;
            } else {
                return 404;
            }
        }

        public (int code, User userProfile) GetUserProfile(int userId)
        {
            User user = _userDB.GetUserProfile(userId);

            if(user != null){
                return (200, user);
            } else {
                return (404, null);
            }
        }

        public int UpdateUser(int userId, User user)
        {
            if(_userDB.UpdateUser(userId, user)){
                return 202;
            } else {
                return 404;
            }
        }

        public (int code, List<User> userList) GetUsers()
        {
            List<User> users = _userDB.GetUsers();

            return (200, users);
        }

    }

}