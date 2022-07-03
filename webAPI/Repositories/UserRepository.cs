using ApiTestProject.Models;
using DataLib;
using DataLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiTestProject.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly DataContext _dataContext;

        public UserRepository()
        {
            _dataContext = new DataContext();
        }

        public UserDto GetUserById(int id)
        {
            var userEntity = _dataContext.Users.FirstOrDefault(x => x.Id == id);

            if (userEntity == null)
                throw new ArgumentException();

            return new UserDto { Id = userEntity.Id, Name = userEntity.Name, Password = userEntity.Password, CreationDate = userEntity.CreationDate };
        }

        public IEnumerable<UserDto> GetUsers()
        {
            return _dataContext.Users.Select(x => new UserDto { Id = x.Id, Name = x.Name, Password = x.Password, CreationDate = x.CreationDate });
        }

        public void UpdateUser(UserDto user)
        {

            var selectedUser = _dataContext.Users.FirstOrDefault(x => x.Id == user.Id);

            if (selectedUser == null)
                throw new ArgumentException();

            selectedUser.CreationDate = user.CreationDate;
            selectedUser.Name = user.Name;
            selectedUser.Password = user.Password;
            _dataContext.SaveChanges();
        }

        public void DeleteUserById(int id)
        {
            var deletedUser = _dataContext.Users.FirstOrDefault(x => x.Id == id);
            if (deletedUser == null)
                throw new ArgumentException();

            _dataContext.Users.Remove(deletedUser);
            _dataContext.SaveChanges();
        }

        public int AddNewUser(UserDto user)
        {
            var res = _dataContext.Users.Add(new User()
            {
                CreationDate = user.CreationDate,
                Name = user.Name,
                Password = user.Password
            });

            _dataContext.SaveChanges();
            return res.Id;
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
