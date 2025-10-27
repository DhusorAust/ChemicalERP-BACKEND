using System;
using System.Collections.Generic;
using System.Text;

namespace ChemicalERP.Models
{
    public class Menu  
    {
        public Menu()
        { 
            ParentMenuID = 0;
            IsActive = 1;
            CreatedDate = DateTime.Now;
            IsExcelExportAllowed = 0;
        } 
        public int MenuID { get; set; }
        public int ProjectID { get; set; }
        public int ProjectModuleID { get; set; }
        public string ViewName { get; set; }
        public string Controller { get; set; }
        public string MenuHead { get; set; }
        public int MenuPriority { get; set; }
        public int ParentMenuID { get; set; }
        public string PageName { get; set; }
        public string MenuTitle { get; set; }
        public string MenuTitle2 { get; set; }
        public string MenuTitle3 { get; set; }
        public string MenuTitle4 { get; set; }
        public string MenuIcon { get; set; }
        public string Remarks { get; set; }
        public int IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? MenuReportID { get; set; }
        public int? IsExcelExportAllowed { get; set; }= 0; 

        #region Additional Columns   
        public bool IsParent { get; set; }
        public string[] sequence { get; set; }
        public string SequenceNo { get; set; }

        public string ParameterValue { get; set; }

        public int IsPermission { get; set; }
        public string ProjectName { get; set; }
        public string ModuleName { get; set; } 
        #endregion Additional Columns   
    }
}
