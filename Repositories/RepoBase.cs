using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Skreenkinikor_Project.Repositories
{
    public abstract class RepoBase
    {
        private readonly string _conString; //Secure ROS
        public RepoBase()
        {
            _conString = "Data Source=.;Initial Catalog=LoginDB;Integrated Security=True"; //Connection string (login database)
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_conString); //Sets connection string
        }

    }
}
