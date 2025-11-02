using ChemicalERP.Interfaces;
using ChemicalERP.Services;
using System.ComponentModel.Design;

namespace ChemicalERP.Models
{
    public class ServiceList
    {
        public static void RegisterServiceList(IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>(); 
            services.AddScoped<ISettingService, SettingService>(); 
            services.AddScoped<IDropDownListService, DropDownListService>(); 
        }
    }
}
