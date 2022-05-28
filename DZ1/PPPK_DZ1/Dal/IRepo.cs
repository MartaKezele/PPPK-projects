using PPPK_DZ1.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPK_DZ1.Dal
{
    interface IRepo
    {
        void LogIn(string server, string username, string password);
        IEnumerable<Database> GetDatabases();
        DataSet CreateDataSet(DBEntity dbEntity);
        IEnumerable<Column> GetColumns(DBEntity entity);
        IEnumerable<DBEntity> GetDBEntities(Database database, DBEntityType entityType);
        IEnumerable<Parameter> GetParameters(Procedure procedure);
        IEnumerable<Procedure> GetProcedures(Database database);
        DataSet ExecuteQuery(Database database, string command);
        int ExecuteNonQuery(Database database, string command);
    }
}
