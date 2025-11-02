using Microsoft.EntityFrameworkCore;
using ChemicalERP.Interfaces;
using ChemicalERP.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace ChemicalERP.Services
{
    public class DropDownListService : IDropDownListService
    {
        private readonly DapperContext<DropDownList> _context;
        private readonly SqlConnection _connection;
        public DropDownListService(DapperContext<DropDownList> context)
        {
            _context = context;
            _connection = _context.Connection;
        }

        public List<DropDownList> Getprojects()
        {
            var sql = @"Select Cast(UnitID As varchar) id,UnitName as [text] 
            From PDLHRM..bas_UnitInfo
            Where IsActive = 1 And Approved = 1 And UnitID In(21);";
            var list = _connection.Query<DropDownList>(sql).ToList();
            return list;
        }

        public List<DropDownList> Getstores()
        {
            var sql = @" Select Cast(StoreID As varchar) id, StoreName as [text] 
            From  PDLHRM..bas_Store
            Where IsActive = 1 And StoreID In(12);";
            var list = _connection.Query<DropDownList>(sql).ToList();
            return list;
        }

        public List<DropDownList> Getpersons()
        {
            var sql = @"SELECT 
	            CAST(ei.EmployeeID AS VARCHAR) id, CONCAT(ei.EmployeeNo, '-', ei.EmployeeName) [text],
	            ei.DepartmentID As valueA, d.DepartmentName As valueD, ei.SectionID As valueB, s.SectionName As valueE, ei.SubSectionID As valueC, 
	            ss.SubSectionName As valueF 
            FROM 
	             PDLHRM..emp_EmploymentInfo ei
	            Left Join  PDLHRM..bas_Department d On d.DepartmentID = ei.DepartmentID
	            Left Join  PDLHRM..bas_Section s On s.SectionID = ei.SectionID
	            Left Join  PDLHRM..bas_SubSection ss On ss.SubSectionID = ei.SubSectionID 
            Where 
	            ei.EmpStatusID = 1  AND ei.UnitID = 2
            ORDER BY CONCAT(ei.EmployeeNo, '-', ei.EmployeeName)";
            var list = _connection.Query<DropDownList>(sql).ToList();
            return list;
        }

        public List<DropDownList> Getsections()
        {
            var sql = @" Select SectionID As id, SectionName As [text] 
            From  PDLHRM..bas_Section 
            Where IsActive = 1";
            var list = _connection.Query<DropDownList>(sql).ToList();
            return list;

        }
        public List<DropDownList> Getuoms()
        {
            var sql = @"SELECT 
            CompanyID AS id, 
            CompanyName AS text 
            FROM PDLHRM..bas_Company 
            WHERE IsActive = 1";
            var list = _connection.Query<DropDownList>(sql).ToList();
            return list;

        }

        public List<DropDownList> Getchemicals()
        {
            var sql = @" SELECT
	            ProductID AS id, ProductName AS [text], pm.UOMID As valueA, u.UOM As valueB
            FROM 
	            bas_CS_ProductMaster pm
	            Left Join bas_UOM u On u.UOMID = pm.UOMID
            Where 
	            pm.IsActive = 1
            ORDER BY ProductName;";
            var list = _connection.Query<DropDownList>(sql).ToList();
            return list;

        }
    }
}
