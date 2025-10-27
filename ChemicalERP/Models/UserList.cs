using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ChemicalERP.Models
{
    public class UserList : BaseEntity
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime PasswordExpireDate { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeNo { get; set; }
        public int UserRoleID { get; set; }
        public int IsAuthorizedLogin { get; set; }
        public int UserTypeID { get; set; }
        public byte IsActive { get; set; }
        public string EmployeeName { get; set; }
        public int CompanyID { get; set; }
        public int UnitID { get; set; }
        public int JobLocationID { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int DesignationID { get; set; }
        public string DesignationName { get; set; }
        public int SectionID { get; set; }
        public string SectionName { get; set; }
        public int SubSectionID { get; set; }
        public string SubSectionName { get; set; }
        public string UserRoleName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserTypeName { get; set; }
        public string UserStatus { get; set; }
        public byte[] EmpPhoto { get; set; }
        public string Image64 { get { return EmpPhoto != null ? Convert.ToBase64String(EmpPhoto) : null; } }

        public string AppVersion { get; set; }
        public int IsAllowHRMApp { get; set; }
        public int? EmployeeTypeID { get; set; }

    }
}
