using Skreenkinikor_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Skreenkinikor_Project.Repositories
{
    public class UserRepo : RepoBase, IUserRepo
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthUser(NetworkCredential cred)
        {
            bool validUser;
            using (var con=GetConnection())
            using (var com=new SqlCommand())
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT * FROM [Login_Table] WHERE Username = @username AND [Password] = @password";
                com.Parameters.Add("@username", SqlDbType.NVarChar).Value = cred.UserName;
                com.Parameters.Add("@password", SqlDbType.NVarChar).Value = cred.Password;
                validUser = com.ExecuteScalar() == null ? false : true;

            }
                return validUser;
        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetId(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetUsername(string username)
        {
            UserModel user = null;
            using (var con = GetConnection())
            using (var com = new SqlCommand())
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT * FROM [Login_Table] WHERE Username = @username";
                com.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = com.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        user = new UserModel()
                        {
                            Username = reader[1].ToString(),
                            Password = String.Empty,
                            Name = reader[3].ToString(),
                            Lastname = reader[4].ToString(),
                        };
                    }
                }
            }
            return user;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
