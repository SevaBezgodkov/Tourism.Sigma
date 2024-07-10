using AuthUsers.Models;
using AutoMapper;
using Domain.Models;

namespace AuthUsers.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<UserModel, User>();
        }
    }
}
