using System.Collections.Generic;
using UserManagement.Data.Audit;
using UserManagement.Models;

namespace UserManagement.Services.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    IEnumerable<User> FilterByActive(bool isActive);
    IEnumerable<User> GetAll();
    void CreateUser(User user);
    User GetUser(long id);

    //TODO: test
    public void UpdateUser(User user);

    //TODO: test
    public bool CheckIfUserExists(long id);
    public void DeleteUser(long id);
    public IEnumerable<Audit> GetUserLogs(long id);
}
