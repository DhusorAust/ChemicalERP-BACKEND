using ChemicalERP.Models;

namespace ChemicalERP.Interfaces
{
    public interface ILoginService
    {
        public Task<UserList> GetLoginUserInfo(string username, string password);
        public List<Menu> GetMenuLoad(int UserID);
       
    }
}
