using ApiTestProject.Models;
using ApiTestProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace ApiTestProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet()]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        public ActionResult GetById(int id)
        {
            UserDto selectedUser = null;

            try
            {
                selectedUser = _userRepository.GetUserById(id);
            }
            catch (ArgumentException)
            {
                return NotFound("Объект по данному Id не найден");
            }

            _logger.LogInformation($"Get ----> {id}");
            return Ok(selectedUser);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        public ActionResult GetUsers()
        {
            return Ok(_userRepository.GetUsers());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(UserDto))]
        public ActionResult AddNewUser([FromBody] UserDto user)
        {
            if (user == null)
                return BadRequest("Отсутствует передаваемое значение");

            var newUserId = _userRepository.AddNewUser(user);

            return Ok(newUserId);
        }

        [HttpDelete]
        [Route("{userDeleteId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteUser(int userDeleteId)
        {
             try
             {
                 _userRepository.DeleteUserById(userDeleteId);
             }
             catch (ArgumentException)
             {
                 return NotFound("Не найдено пользователя по данному идентификатору");
             }

             return NoContent();
        }

        [HttpPost()]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateUser(int id, [FromBody] UserDto user)
        {
            if (user == null)
                return BadRequest("Отсутствует текст запроса");

            user.Id = id;

            try
            {
                _userRepository.UpdateUser(user);
            }
            catch (ArgumentException)
            {
                return NotFound("Не найдено пользователя с таким идентификатором");
            }

            return NoContent();
           
        }
    }
}
