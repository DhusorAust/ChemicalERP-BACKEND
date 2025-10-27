using Microsoft.EntityFrameworkCore;
using ChemicalERP.Interfaces;
using ChemicalERP.Models;
using System.Data.SqlClient;
using System.Data;

namespace ChemicalERP.Services
{
    public class LoginService : ILoginService
    {
        private readonly DapperContext<UserList> _context;
        private readonly SqlConnection _connection;
        public LoginService(DapperContext<UserList> context)
        {
            _context = context;
            _connection = _context.Connection;
        }
        public async Task<UserList> GetLoginUserInfo(string username, string password)
        {
            string strQuery = "";

            strQuery = $@"
                    Select 
                        UL.UserID, ul.UserName, urp.UserRoleID, UR.UserRoleName, UL.EmployeeID, EL.EmployeeName, EL.EmployeeNo, Isnull(EL.DepartmentID, 0)DepartmentID,
                        D.DepartmentName, Isnull(EL.DesignationID, 0)DesignationID, DEG.DesignationName, Isnull(EL.SectionID, 0)SectionID, 
                        S.SectionName, Isnull(EL.SubSectionID, 0)SubSectionID, SS.SubSectionName,
                        UL.UserTypeID, UT.UserTypeName
                        ,IsAuthorizedLogin=case when el.EmpStatusID<>1 and sep.EffectDate<=cast(Getdate() as date) and urp.ProjectID in ({AppGlobalFields.ProjectID}) then 0 else 1 end 
                        ,photo.EmpPhoto
                        ,EL.UnitID
                    From 
                        {DBSource.ERPUSERDB}..UserList UL
                        Left Join {DBSource.ERPUSERDB}..UserRolePermission urp On urp.UserID = UL.UserID And urp.ProjectID = {AppGlobalFields.ProjectID} 
                        LEFT JOIN {DBSource.ERPUSERDB}..UserRole UR ON UR.UserRoleID = urp.UserRoleID 
                        Left Join {DBSource.ERPUSERDB}..UserType UT On UT.UserTypeID = UL.UserTypeID 
                        -- Employee Info
                        LEFT Join {DBSource.PDLHRM}..emp_EmploymentInfo EL on EL.EmployeeID = UL.EmployeeID  
                        LEFT Join {DBSource.PDLHRM}..bas_Department D ON D.DepartmentID = EL.DepartmentID
                        LEFT JOIN {DBSource.PDLHRM}..bas_Designation DEG ON DEG.DesignationID = EL.DesignationID
                        LEFT JOIN {DBSource.PDLHRM}..bas_Section S ON S.SectionID = EL.SectionID
                        LEFT JOIN {DBSource.PDLHRM}..bas_SubSection SS ON SS.SubSectionID = EL.SubSectionID  
                        LEFT JOIN {DBSource.PDLHRM}..emp_EmployeeSeparation sep on sep.EmployeeID=el.EmployeeID and sep.IsActive=1
                        LEFT JOIN {DBSource.PDLHRM}..emp_EmployeePhoto photo on photo.EmployeeID=el.EmployeeID
                    Where 
                        urp.ProjectID = {AppGlobalFields.ProjectID} And UL.UserName = '{username}' AND UL.IsActive = 1 --AND UL.PasswordExpireDate > GETDATE() ";

            var userData = await _context.GetSingleDataAsync<UserList>(strQuery);

            if (userData == null)
            {
                var usr = new UserList();
                usr.Message = "User Not Found!";

                return usr;
            }
            else
            {
                return userData;
            }

        }

        public   List<Menu> GetMenuLoad(int UserID)
        {

            List<Menu> ListObjMenu = new List<Menu>();
            string steMenu = $@"
            Begin 
	            If Not Exists(Select * From {DBSource.ERPUSERDB}..MenuPermissionUser a inner join {DBSource.ERPUSERDB}..Menu b on a.MenuID = b.MenuID Where UserID = {UserID} and b.ProjectID = {AppGlobalFields.ProjectID})
	            Begin
		            Select M.MenuID, M.ProjectID, P.ProjectName, M.ProjectModuleID, PM.ModuleName, M.ViewName, M.Controller, M.MenuHead, M.MenuPriority, 
		            M.ParentMenuID, M.PageName,  M.MenuTitle, M.MenuTitle2, M.MenuTitle3, M.MenuTitle4, M.MenuIcon, M.IsActive, 
		            MP.IsPermission, MPAR.ParameterValue, MPAR.SequenceNo 
		            From {DBSource.ERPUSERDB}..Menu M 
                    Inner Join {DBSource.ERPUSERDB}..Project P ON P.ProjectID = M.ProjectID
		            left Join {DBSource.ERPUSERDB}..ProjectModule PM ON PM.ProjectModuleID = M.ProjectModuleID
		            Inner Join {DBSource.ERPUSERDB}..MenuPermissionRole MP ON MP.MenuID = M.MenuID
                    Inner Join {DBSource.ERPUSERDB}..UserRolePermission urp ON urp.UserRoleID = MP.UserRoleID And urp.ProjectID = {AppGlobalFields.ProjectID}
		            Inner Join {DBSource.ERPUSERDB}..UserList UL ON UL.UserID = urp.UserID
		            Left Join {DBSource.PDLHRM}..emp_EmploymentInfo E ON E.EmployeeID = UL.EmployeeID
		            Left Join {DBSource.ERPUSERDB}..MenuParameter MPAR ON MPAR.MenuID = M.MenuID
		            WHERE m.ProjectID = {AppGlobalFields.ProjectID} And P.ProjectID = {AppGlobalFields.ProjectID} And M.IsActive = 1 And P.IsActive = 1 And MP.IsPermission = 1 And UL.IsActive = 1  
                    And   UL.UserID = {27}
		            Order By ParentMenuID, MenuPriority
	            End
	            Else
	            Begin
		            Select M.MenuID, M.ProjectID, P.ProjectName, M.ProjectModuleID, PM.ModuleName, M.ViewName, M.Controller, M.MenuHead, M.MenuPriority, 
		            M.ParentMenuID, M.PageName,  M.MenuTitle, M.MenuTitle2, M.MenuTitle3, M.MenuTitle4, M.MenuIcon, M.IsActive, 
		            MP.IsPermission, MPAR.ParameterValue, MPAR.SequenceNo   
		            From {DBSource.ERPUSERDB}..Menu M 
                    Inner Join {DBSource.ERPUSERDB}..Project P ON P.ProjectID = M.ProjectID
		            Left Join {DBSource.ERPUSERDB}..ProjectModule PM ON PM.ProjectModuleID = M.ProjectModuleID
		            Inner Join {DBSource.ERPUSERDB}..MenuPermissionUser MP ON MP.MenuID = M.MenuID
		            Inner Join {DBSource.ERPUSERDB}..UserList UL ON UL.UserID = MP.UserID
		            Left Join {DBSource.PDLHRM}..emp_EmploymentInfo E ON E.EmployeeID = UL.EmployeeID
		            Left Join {DBSource.ERPUSERDB}..MenuParameter MPAR ON MPAR.MenuID = M.MenuID
		            WHERE P.ProjectID = {AppGlobalFields.ProjectID} And M.IsActive = 1 And P.IsActive = 1 And MP.IsPermission = 1 And UL.IsActive = 1 
                    AND UL.UserID = {UserID}
		            Order By ParentMenuID, MenuPriority
	            End
            End ";

            DataTable dtMenu = _context.ExecuteQuery(steMenu);

            for (int i = 0; i < dtMenu.Rows.Count; i++)
            {
                Menu item = new Menu();
                item.MenuID = Convert.ToInt32(dtMenu.Rows[i]["MenuID"].ToString());
                item.ProjectID = Convert.ToInt32(dtMenu.Rows[i]["ProjectID"].ToString());
                item.ProjectName = dtMenu.Rows[i]["ProjectName"].ToString();
                item.ProjectModuleID = Convert.ToInt32(dtMenu.Rows[i]["ProjectModuleID"].ToString());
                item.ModuleName = dtMenu.Rows[i]["ModuleName"].ToString();
                item.ViewName = dtMenu.Rows[i]["ViewName"].ToString();
                item.Controller = dtMenu.Rows[i]["Controller"].ToString();
                item.MenuHead = dtMenu.Rows[i]["MenuHead"].ToString();
                item.MenuPriority = Convert.ToInt32(dtMenu.Rows[i]["MenuPriority"].ToString());
                item.ParentMenuID = Convert.ToInt32(dtMenu.Rows[i]["ParentMenuID"].ToString());
                item.ParameterValue = dtMenu.Rows[i]["ParameterValue"].ToString();
                item.SequenceNo = dtMenu.Rows[i]["SequenceNo"].ToString();
                item.PageName = dtMenu.Rows[i]["PageName"].ToString();
                item.IsPermission = Convert.ToInt32(dtMenu.Rows[i]["IsPermission"].ToString());
                item.MenuTitle = dtMenu.Rows[i]["MenuTitle"].ToString();
                item.MenuTitle2 = dtMenu.Rows[i]["MenuTitle2"].ToString();
                item.MenuTitle3 = dtMenu.Rows[i]["MenuTitle3"].ToString();
                item.MenuTitle4 = dtMenu.Rows[i]["MenuTitle4"].ToString();
                item.MenuIcon = dtMenu.Rows[i]["MenuIcon"].ToString();
                ListObjMenu.Add(item);
            }
            return ListObjMenu; 

        }
    }
}
