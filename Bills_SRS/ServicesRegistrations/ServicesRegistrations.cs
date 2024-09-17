using bill_DataAccess.Implementation;
using bill_Entities.Mapper;
using bill_Entities.Repoistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.ServicesRegistrations
{
    public static class ServicesRegistrations
    {
        public static void AddServicesRegistrations(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICompanyRepoistory, CompanyRepoistory>();
            services.AddScoped<ITypeRepoistory, TypeRepoistory>();
            services.AddScoped<IItemRepoistory, ItemRepoistory>();
            services.AddScoped<IClientRepoistory, ClientRepoistory>();
            services.AddScoped<ISalesInvoiceRepoistory, SalesInvoiceRepoistroy>();
            services.AddScoped<IUnitssRepoistory, UnitssRepoistory>();


            // Mapper

            services.AddAutoMapper(typeof(Program));
            services.AddAutoMapper(typeof(MappingProfile));

        }
    }
}
