using AutoMapper;

namespace FinanceAssist.API
{
    internal class AutomapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Domain.Expense, API.ViewModels.Expense>()
            .ForMember(dest => dest.ExpneseDate, opt => opt.MapFrom(src => src.ExpneseDate.ToString("yyyy/M/d")))
            .ReverseMap()
            );

        }
    }
}