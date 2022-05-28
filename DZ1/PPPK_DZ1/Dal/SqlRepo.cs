using PPPK_DZ1.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PPPK_DZ1.Dal
{
    class SqlRepo : IRepo
    {
        private string SERVER;
        private string USER_ID;
        private string PASSWORD;

        private string cs;
        private const string ConnectionString = "Server={0};User Id={1};Password={2}";
        private const string DatabaseConnectionString = "Server={0};Database={1};User Id={2};Password={3}";
        private const string SelectDatabases = "SELECT name As Name from sys.databases";
        private const string SelectTables = "SELECT TABLE_SCHEMA AS [Schema], TABLE_NAME AS Name FROM {0}.INFORMATION_SCHEMA.TABLES";
        private const string SelectViews = "SELECT TABLE_SCHEMA AS [Schema], TABLE_NAME AS Name FROM {0}.INFORMATION_SCHEMA.VIEWS";
        private const string SelectProcedures = "SELECT SPECIFIC_NAME as Name, ROUTINE_DEFINITION as Definition FROM {0}.INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE'";
        private const string SelectColumns = "SELECT COLUMN_NAME as Name, DATA_TYPE as DataType FROM {0}.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{1}'";
        private const string SelectProcedureParameters = "SELECT PARAMETER_NAME as Name, PARAMETER_MODE as Mode, DATA_TYPE as DataType FROM {0}.INFORMATION_SCHEMA.PARAMETERS WHERE SPECIFIC_NAME='{1}'";
        private const string SelectQuery = "SELECT * FROM {0}.{1}.{2}";


        public void LogIn(string server, string username, string password)
        {
            using (SqlConnection con = new SqlConnection(string.Format(ConnectionString, server, username, password)))
            {
                cs = con.ConnectionString;
                con.Open();
            }

            SERVER = server;
            USER_ID = username;
            PASSWORD = password;
        }

        public IEnumerable<Database> GetDatabases()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = SelectDatabases;
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new Database
                            {
                                Name = dr[nameof(Database.Name)].ToString()
                            };
                        }
                    }
                }
            }
        }

        public IEnumerable<DBEntity> GetDBEntities(Database database, DBEntityType entityType)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    switch (entityType)
                    {
                        case DBEntityType.Table:
                            cmd.CommandText = string.Format(SelectTables, database.Name);
                            break;
                        case DBEntityType.View:
                            cmd.CommandText = string.Format(SelectViews, database.Name);
                            break;

                    }
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new DBEntity
                            {
                                Name = dr[nameof(DBEntity.Name)].ToString(),
                                Schema = dr[nameof(DBEntity.Schema)].ToString(),
                                Database = database
                            };
                        }
                    }
                }
            }
        }

        public IEnumerable<Procedure> GetProcedures(Database database)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = string.Format(SelectProcedures, database.Name);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new Procedure
                            {
                                Name = dr[nameof(Procedure.Name)].ToString(),
                                Definition = dr[nameof(Procedure.Definition)].ToString(),
                                Database = database
                            };
                        }
                    }
                }
            }
        }

        public IEnumerable<Column> GetColumns(DBEntity entity)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = string.Format(SelectColumns, entity.Database.Name, entity.Name);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new Column
                            {
                                Name = dr[nameof(Column.Name)].ToString(),
                                DataType = dr[nameof(Column.DataType)].ToString()
                            };
                        }
                    }
                }
            }
        }

        public IEnumerable<Parameter> GetParameters(Procedure procedure)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = string.Format(SelectProcedureParameters, procedure.Database.Name, procedure.Name);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new Parameter
                            {
                                Name = dr[nameof(Parameter.Name)].ToString(),
                                Mode = dr[nameof(Parameter.Mode)].ToString(),
                                DataType = dr[nameof(Parameter.DataType)].ToString()
                            };
                        }
                    }
                }
            }
        }

        public DataSet CreateDataSet(DBEntity dbEntity)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter(string.Format(SelectQuery, dbEntity.Database, dbEntity.Schema, dbEntity.Name), con);
                DataSet ds = new DataSet(dbEntity.Name);
                da.Fill(ds);
                ds.Tables[0].TableName = dbEntity.Name;
                return ds;
            }
        }

        public DataSet ExecuteQuery(Database database, string command)
        {
            using (SqlConnection con = new SqlConnection(string.Format(DatabaseConnectionString, SERVER, database.Name, USER_ID, PASSWORD)))
            {
                SqlDataAdapter da = new SqlDataAdapter(command, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        public int ExecuteNonQuery(Database database, string command)
        {
            using (SqlConnection con = new SqlConnection(string.Format(DatabaseConnectionString, SERVER, database.Name, USER_ID, PASSWORD)))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = command;
                    cmd.CommandType = CommandType.Text;
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
