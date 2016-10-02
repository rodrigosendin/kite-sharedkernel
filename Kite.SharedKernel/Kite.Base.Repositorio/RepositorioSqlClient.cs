using System;
using System.Data;
using System.Data.SqlClient;

namespace Kite.Base.Repositorio
{
    public class RepositorioSqlClient
    {
        public string ConnectionString{ get; set;}

        public RepositorioSqlClient(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public DataTable ConsultaDataTable(string commandText)
        {
            var conn = new SqlConnection(ConnectionString);
            var dt = new DataTable();
            try
            {
                conn.Open();
                var comm = new SqlCommand
                {
                    CommandText = commandText,
                    CommandType = CommandType.Text,
                    Connection = conn,
                    CommandTimeout = 120
                };

                var da = new SqlDataAdapter { SelectCommand = comm };
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable ConsultaDataTable(string tabela, string where, string order = null)
        {
            var conn = new SqlConnection(ConnectionString);
            var dt = new DataTable();
            try
            {
                var commandText = string.IsNullOrWhiteSpace(where)
                                         ? string.Format("SELECT * FROM {0}", tabela)
                                         : string.Format("SELECT * FROM {0} WHERE {1}", tabela, where);

                if (!string.IsNullOrWhiteSpace(order))
                    commandText += string.Format(" ORDER BY {0}", order);

                conn.Open();
                var comm = new SqlCommand
                {
                    CommandText = commandText,
                    CommandType = CommandType.Text,
                    Connection = conn,
                    CommandTimeout = 120
                };

                var da = new SqlDataAdapter { SelectCommand = comm };
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable ConsultaDataTable(string tabela, int inicio, int limite, string where = null, string order = null)
        {
            var conn = new SqlConnection(ConnectionString);
            var dt = new DataTable();
            try
            {
                if(string.IsNullOrWhiteSpace(order)) order = "Id";

                var commandText =
                    string.Format(
                        "SELECT * FROM (SELECT ROW_NUMBER() OVER ( ORDER BY {1} ) AS Indice, * FROM {0}) AS RowConstrainedResult {2}",
                        tabela,
                        order,
                        "WHERE Indice BETWEEN 1 AND 1000 ORDER BY Indice");
                    
                conn.Open();
                var comm = new SqlCommand
                {
                    CommandText = commandText,
                    CommandType = CommandType.Text,
                    Connection = conn,
                    CommandTimeout = 120
                };

                var da = new SqlDataAdapter { SelectCommand = comm };
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable ConsultaDataReader(string tabela, string where = null, string order = null)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnectionString);
                var commandText = string.IsNullOrWhiteSpace(where)
                         ? string.Format("SELECT * FROM {0}", tabela)
                         : string.Format("SELECT * FROM {0} WHERE {1}", tabela, where);

                if (!string.IsNullOrWhiteSpace(order))
                    commandText += string.Format(" ORDER BY {0}", order);

                var cmd = new SqlCommand(commandText, conn);
                conn.Open();
                var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                var dt = new DataTable();
                dt.Load(dr);
                return dt;

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable ConsultaDataTableComQuery(string query)
        {
            var conn = new SqlConnection(ConnectionString);
            var dt = new DataTable();
            try
            {
                conn.Open();
                var comm = new SqlCommand
                {
                    CommandText = query,
                    CommandType = CommandType.Text,
                    Connection = conn
                };

                var da = new SqlDataAdapter { SelectCommand = comm };
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataRowView ConsultaDataRowView(string tabela, long id)
        {
            var dt = ConsultaDataTable(tabela, string.Format("Id = {0}",id));
            return dt.Rows.Count > 0 ? dt.DefaultView[0] : null;
        }

        public bool ExecutaComando(string comando)
        {
            var conn = new SqlConnection(ConnectionString);
            bool ok;
            try
            {
                conn.Open();
                var comm = new SqlCommand
                {
                    CommandText = comando,
                    CommandType = CommandType.Text,
                    Connection = conn
                };

                comm.ExecuteScalar();
                ok = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return ok;
        }

        public bool ExecutaComando(string[] comandos)
        {
            var conn = new SqlConnection(ConnectionString);
            bool ok;
            try
            {
                conn.Open();
                foreach (var comando in comandos)
                {
                    var comm = new SqlCommand
                    {
                        CommandText = comando,
                        CommandType = CommandType.Text,
                        Connection = conn
                    };
                    comm.ExecuteScalar();    
                }
                ok = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return ok;
        }

        public decimal ExecutaSoma(string comando)
        {
            decimal resultado = 0;
            var conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                var comm = new SqlCommand
                {
                    CommandText = comando,
                    CommandType = CommandType.Text,
                    Connection = conn
                };

                resultado = comm.ExecuteScalar() is decimal ? (decimal) comm.ExecuteScalar() : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return resultado;
        }

        public bool TestarConexao()
        {
            var conn = new SqlConnection(ConnectionString);
            bool conectou;
            try
            {
                conn.Open();
                conectou = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return conectou;
        }
    }
}
