using Microsoft.EntityFrameworkCore;
using ChemicalERP.Interfaces;
using ChemicalERP.Models;
using System.Data.SqlClient;
using System.Data;
using ChemicalERP.Models.KendoGridManager;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Text;

namespace ChemicalERP.Services
{
    public class SettingService : ISettingService
    {
        private readonly DapperContext<Bas_Bank> _context;
        private readonly SqlConnection _connection;

        public SettingService(DapperContext<Bas_Bank> context)
        {
            _context = context;
            _connection = _context.Connection;
        }
        public async Task<GridEntity<Bas_Bank>> GetBankListAsync(Status status, string? q)
        {
            var where = new StringBuilder("WHERE 1=1");

            var dp = new DynamicParameters(); 

            if (status == Status.EDIT)
            {
                where.Append(" and ISNULL(a.Approved,0)=0");
            }
               
            else if (status == Status.APPROVED)
            {
                where.Append(" and ISNULL(a.Approved,0)=1");
            }
               

            if (!string.IsNullOrWhiteSpace(q))
            {
                dp.Add("@q", $"%{q.Trim()}%");
                where.Append(" AND (a.BankName LIKE @q OR a.BankShortName LIKE @q OR a.BankAddress LIKE @q OR a.SwiftCode LIKE @q)");
            } 

            var sql = $@"
                        SELECT *
                        FROM bas_Bank a
                        {where}
                        ORDER BY a.BankID DESC;";

            var countSql = $@"SELECT COUNT(1) FROM bas_Bank a {where};";

            if (_connection.State != ConnectionState.Open) _connection.Open();
            try
            {
                var items = (await _connection.QueryAsync<Bas_Bank>(sql, dp)).AsList();  
                var total = await _connection.ExecuteScalarAsync<int>(countSql, dp);    

                var result = new GridResult<Bas_Bank>().Data(items, total);
                result.Columnses ??= new List<GridColumns>();
                return result;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open) _connection.Close();
            }

        }

        public async Task<Bas_Bank> SaveAsync(Bas_Bank entity)
        {
            var parameters = (dynamic)null;
            try
            {
                parameters = new DynamicParameters();
                parameters.Add("@SaveOption", entity.SaveOption);
                parameters.Add("@BankID", entity.BankID);
                parameters.Add("@BankCode", entity.BankCode);
                parameters.Add("@BankName", entity.BankName);
                parameters.Add("@BankShortName", entity.BankShortName);
                parameters.Add("@BankAddress", entity.BankAddress);
                parameters.Add("@SwiftCode", entity.SwiftCode);
                parameters.Add("@ADCode", entity.ADCode);
                parameters.Add("@IsBeneficiaryBank", entity.IsBeneficiaryBank);
                parameters.Add("@IsAdvisingBank", entity.IsAdvisingBank);
                parameters.Add("@IsNegoBank", entity.IsNegoBank);
                parameters.Add("@IsActive", entity.IsActive);
                parameters.Add("@Approved", entity.Approved);
                parameters.Add("@UserBy", entity.UserBy);
                parameters.Add("@IdentityValue", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                parameters.Add("@ErrNo", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                entity.NoofRows = await _context.ExecuteAsync("spSet_bas_Bank", parameters, 30, CommandType.StoredProcedure);
                entity.ResultId = parameters.Get<int>("@IdentityValue");
                entity.ErrorNo = parameters.Get<int>("@ErrNo");


                if (entity.NoofRows > 0 && entity.ResultId > 0)
                {
                    entity.Message = "Save operation successful.";
                }
                else
                {
                    entity.Message = "Save operation failed.";
                }
            }
            catch (Exception ex)
            {
                entity.Message = ex.Message;
            }
            finally
            {
                parameters = null;
            }
            return entity;
        }
         

    }
}
