using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Skreenkinikor_Project.Repositories
{//This class returns true or false if IsAdmin is checked.
    public class IsAdminRepo : RepoBase
    {
        public bool GetAdmin(string username)
        {
            using (var con = GetConnection())
            using (var com = new SqlCommand())
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT IsAdmin FROM Login_Table WHERE Username = @username";
                com.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;

                using(var reader = com.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        bool isAdmin = Convert.ToBoolean(reader["IsAdmin"]);
                        return isAdmin;
                    }
                    return false;
                }
            }
        }
    }
}
