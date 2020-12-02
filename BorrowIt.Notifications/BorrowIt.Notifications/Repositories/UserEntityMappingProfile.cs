using AutoMapper;
using BorrowIt.Notifications.Domain.Users;
using BorrowIt.Notifications.Entities;

namespace BorrowIt.Notifications.Repositories
{
    public class UserEntityMappingProfile : Profile
    {
        public UserEntityMappingProfile()
        {
            CreateMap<UserEntity, User>();
        }
    }
}