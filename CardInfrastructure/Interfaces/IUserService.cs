using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary.Models;

namespace CardInfrastructure.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateNewUserAsync(string firstName, string lastName, string email);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(string id);
        Task UpdateUser(string id, string firstName, string lastName, string email);
        Task DeleteUserById(string id);
    }
}
