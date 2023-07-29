using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Skreenkinikor_Project.Model
{   //User interface housing add remove edit and authenticate methods to manage User database
    public interface IUserRepo
    {
        bool AuthUser(NetworkCredential cred);
        void Add(UserModel userModel);
        void Remove(int id);
        void Edit(UserModel userModel);
        UserModel GetId(int id);
        UserModel GetUsername(string username);
    }
}
