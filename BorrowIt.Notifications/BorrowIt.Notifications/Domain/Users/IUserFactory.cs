namespace BorrowIt.Notifications.Domain.Users
{
    public interface IUserFactory
    {
        User Create(UserDataStructure user);
    }
}