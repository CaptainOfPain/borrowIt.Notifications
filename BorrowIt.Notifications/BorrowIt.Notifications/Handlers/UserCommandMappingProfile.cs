using AutoMapper;
using BorrowIt.Notifications.Commands;
using BorrowIt.Notifications.Domain.Users;

namespace BorrowIt.Notifications.Handlers
{
    public class UserCommandMappingProfile : Profile
    {
        public UserCommandMappingProfile()
        {
            CreateMap<SynchronizeUserCommand, UserDataStructure>()
                .ConstructUsing(x => new UserDataStructure(x.Id, x.UserName));
        }
    }
}