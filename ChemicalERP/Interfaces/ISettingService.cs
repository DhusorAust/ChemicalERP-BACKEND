using ChemicalERP.Models;
using ChemicalERP.Models.KendoGridManager;

namespace ChemicalERP.Interfaces
{
    public interface ISettingService
    {
        Task<GridEntity<Bas_Bank>> GetBankListAsync(Status status, string? q);
    }
}
