using AutoMapper;
using Demo.DAL.Entites;
using Demo.PL.ViewModels;

namespace Demo.PL.Mappers
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(dest => dest.UserName, src => src.MapFrom(src => GetUserName(src.Email)));
        }

        static string GetUserName(string Word)
        {
            return Word.Split('@')[0];
        }
    }
}
