using ChemicalERP.Models.KendoGridManager;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemicalERP.Models
{
    public class DapperContext<T> : DbContext where T : class
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public SqlConnection Connection { get; set; }
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        #region Get data using following function 
        public async Task<List<CT>> GetDataAsync<CT>(string query) where CT : class
        {
            try
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();

                await Connection.OpenAsync();
                var records = await Connection.QueryAsync<CT>(query);
                return records.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        #endregion Get data using following function

        public async Task<int> ExecuteAsync(string query, object param, int commandTimeOut = 30, CommandType commandType = CommandType.Text)
        {
            SqlTransaction transaction = null;

            try
            {
                await Connection.OpenAsync();
                transaction = Connection.BeginTransaction();
                int rows = await Connection.ExecuteAsync(query, param, transaction, commandTimeOut, commandType);
                transaction.Commit();
                return rows;
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Dispose();
                Connection.Close();
            }
        }
        public DataTable ExecuteQuery(string sQuery)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter myAdapter = new SqlDataAdapter(sQuery, Connection.ConnectionString);
                myAdapter.Fill(dt);
            }
            catch
            {

            }

            return dt;
        }
        public async Task<DataTable> SqlDataTable(string sql)
        {
            var dt = new DataTable();

            try
            {
                SqlCommand cmd = new SqlCommand(sql, Connection);
                await Connection.OpenAsync();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
                await Connection.CloseAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }
        public async Task<DataSet> SqlDataSet(string sqls)
        {
            var dset = new DataSet();
            try
            {
                SqlDataAdapter dadapter = new SqlDataAdapter(sqls, Connection);
                await Connection.OpenAsync();
                dadapter.Fill(dset);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                await Connection.CloseAsync();
            }
            return dset;
        }
        public async Task<CT> GetSingleDataAsync<CT>(string query) where CT : class
        {
            try
            {
                await Connection.OpenAsync();
                var records = await Connection.QueryAsync<CT>(query);
                return records.ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public async Task<GridEntity<T>> GetKendoGridDataAsync<CT>(GridOptions options, string query, string orderBy, string condition)
        {
            try
            {
                query = query.Replace(';', ' ');

                string sqlQuery = options != null ? GridQueryBuilder<T>.Query(options, query, orderBy, condition) : query;

                if (!string.IsNullOrEmpty(condition))
                {
                    condition = " WHERE " + condition;
                }

                var condition1 = options != null ? GridQueryBuilder<T>.FilterCondition(options.filter) : "";
                if (!string.IsNullOrEmpty(condition1))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        condition += " And " + condition1;
                    }
                    else
                    {
                        condition = " WHERE " + condition1;
                    }
                }

                Connection.Open();
                var records = Connection.Query<T>(sqlQuery).ToList();
                var sqlCount = "SELECT COUNT(*) FROM (" + query + " ) As tbl " + condition;
                var count = Connection.Query<int>(sqlCount).ToList();
                var totalCount = count[0];
                var result = new GridResult<T>().Data(records, totalCount);
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }

        }

    }
}
