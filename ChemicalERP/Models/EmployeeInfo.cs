namespace ChemicalERP.Models
{
    public class EmployeeInfo
    {
        public int EmployeeId { get; set; }
        public string? EmployeeNo { get; set; }
        public string? EmployeeName { get; set; }
        public string? DesignationName { get; set; }
        public string? DepartmentName { get; set; }
        public string? UnitName { get; set; }
        public string? JoinDate { get; set; }
        public string? CardNo { get; set; }
        public string? InPunch { get; set; }
        public string? OutPunch { get; set; }
        public byte[]? Photo { get; set; }
        public string? EmailID { get; set; }
        public string? PabxNo { get; set; }
        public string? MobileNo { get; set; }
        public string? BloodGroupName { get; set; }

        public int UserRoleID { get; set; }
        public string? UserRoleName { get; set; }
        public int EmployeeTypeID { get; set; }
    }
}
