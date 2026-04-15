using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tienda_Abarrotes.Model
{
    public interface IUserRepository
    {
      bool AuthenticateUser(NetworkCredential credential);
        void Add(UserModel userModel);
        void Update(UserModel userModel);
        void Delete(UserModel userModel);
        UserModel GetByUserName(string userName);
        bool TestConnection(out string message);
        IEnumerable<UserModel> GetAllUsers();

    }
  
}
