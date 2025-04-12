
public class MyBudgetUserAccess : IMyBudgetUserAccess
{
    public Task<bool> AddUser(string userName, string password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUser(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateUser(string userName, string password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UserExists(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ValidateUser(string userName, string password)
    {
        throw new NotImplementedException();
    }
}