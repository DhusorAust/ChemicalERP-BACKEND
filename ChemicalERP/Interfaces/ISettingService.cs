using ChemicalERP.Models;
using ChemicalERP.Models.KendoGridManager;

namespace ChemicalERP.Interfaces
{
    public interface ISettingService
    {
        Task<GridEntity<Bas_Bank>> GetBankListAsync(Status status, string? q);
        Task<Bas_Bank> SaveAsync(Bas_Bank entity);

    }
}
