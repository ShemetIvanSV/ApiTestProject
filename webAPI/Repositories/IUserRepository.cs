using ApiTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestProject.Repositories
{
    public interface IUserRepository
    {
        int AddNewUser(UserDto user);

        UserDto GetUserById(int id);

        IEnumerable<UserDto> GetUsers();

        void UpdateUser(UserDto user);

        void DeleteUserById(int id);
    }
}
