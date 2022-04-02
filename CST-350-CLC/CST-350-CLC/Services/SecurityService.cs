using CST_350_CLC.Models;

namespace CST_350_CLC.Services {
    public class SecurityService {

        UsersDAO usersDAO = new UsersDAO();

        public SecurityService() {

        }

        public bool IsValid(UserModel user) {
            // a lookup to see if there is an item in the db
            return usersDAO.FindUserByNameAndPassword(user);
        }
    }
}

