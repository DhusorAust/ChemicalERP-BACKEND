using ChemicalERP.Models;

namespace ChemicalERP.Interfaces
{
    public interface IDropDownListService
    {
         public List<DropDownList> Getprojects();
         public List<DropDownList> Getstores();
         public List<DropDownList> Getpersons();
         public List<DropDownList> Getsections();
         public List<DropDownList> Getuoms();
         public List<DropDownList> Getchemicals();

    }
}
