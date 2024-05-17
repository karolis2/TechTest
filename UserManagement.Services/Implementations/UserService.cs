using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;

    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public IEnumerable<User> FilterByActive(bool isActive)
    {
        return _dataAccess
            .GetAll<User>()
            .Where(user => user.IsActive == isActive);
    }

    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();

    public void CreateUser(User user) => _dataAccess.Create(user);

    public User GetUser(long id)
    {
        var all = _dataAccess.GetAll<User>();

        return all.Single(x => x.Id == id);
    }

    public void UpdateUser(User user)
    {
        _dataAccess.Update(user);
    }
    //TODO: UT
    public bool CheckIfUserExists(long id)
    {
        var all = _dataAccess.GetAll<User>();

        return all.Any(x => x.Id == id);
    }

    public void DeleteUser(long id)
    {
        var all = _dataAccess.GetAll<User>();

        var userToDelete = all.Single(x => x.Id == id);

        _dataAccess.Delete(userToDelete);
    }
}
