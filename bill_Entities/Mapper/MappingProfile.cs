using AutoMapper;
using bill_Entities.Models;
using bill_Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company,CompanyViewModel>().ReverseMap();
            CreateMap<Client, ClientViewModel>().ReverseMap();
            CreateMap<SalesInvoice,SalesViewModel>().ReverseMap()
                                .ForMember(dest => dest.ClientId,src=>src.MapFrom(src=>src.ClientId))
                                .ForMember(dest => dest.TableItemId,src=>src.MapFrom(src=>src.TableItemId));
            CreateMap<tableItem,ItemViewModel>().ReverseMap()
                .ForMember(dest => dest.ItemName,src => src.MapFrom(src => src.ItemName));
            CreateMap<Report, ReportViewModel>().ReverseMap();
            CreateMap<Types, TypeViewModel>().ReverseMap();
            CreateMap<Unit, UnitViewModel>().ReverseMap();
        }
    }
}
