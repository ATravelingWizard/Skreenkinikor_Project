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
        private readonly string _conString;
        public RepoBase()
        {
            _conString = "Data Source=.;Initial Catalog=LoginDB;Integrated Security=True";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_conString);
        }

    }
}
