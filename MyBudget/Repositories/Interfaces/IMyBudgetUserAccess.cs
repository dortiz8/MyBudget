public interface IMyBudgetUserAccess
{
    Task<bool> AddUser(string userName, string password);
    Task<bool> UpdateUser(string userName, string password);
    Task<bool> DeleteUser(string userName);
    Task<bool> UserExists(string userName);
    Task<bool> ValidateUser(string userName, string password);
}