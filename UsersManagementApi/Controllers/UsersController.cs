using AutoMapper;
using DataAccess;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersManagementApi.DTOs;

namespace UsersManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _unitOfWork.Users.GetAll();
                var userDtoList = _mapper.Map<IEnumerable<UserDTO>>(users);

                if (users.Count() > 0)
                {
                    FileLogger.Log($"{users.Count()} user(s) retrieved");
                    return Ok(userDtoList);
                }
                else
                {
                    return Ok(new
                    {
                        Message = "There are no users.",
                        Data = users
                    });
                }
            }
            catch (Exception ex)
            {
                FileLogger.Log("Error retrieving users");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _unitOfWork.Users.GetById(id);
                var userDto = _mapper.Map<UserDTO>(user);

                if (user == null)
                    return NotFound();

                FileLogger.Log($"User with ID: {id} retrieved");
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                FileLogger.Log($"Error retrieving user with ID: {id}.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDTO user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                if (string.IsNullOrWhiteSpace(user.LastName) ||
                    string.IsNullOrWhiteSpace(user.LastName))
                {
                    return BadRequest("User Fullname is required!");
                }

                var newUser = _mapper.Map<User>(user);

                _unitOfWork.Users.Add(newUser);
                _unitOfWork.Complete();

                FileLogger.Log($"User with ID: {newUser.Id} created");
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                FileLogger.Log($"Error creating user.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDTO user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                var existingUser = _unitOfWork.Users.GetById(id);

                if (existingUser == null)
                    return NotFound();

                if (!string.IsNullOrWhiteSpace(user.FirstName) && !string.IsNullOrWhiteSpace(user.LastName))
                {
                    _mapper.Map(user, existingUser);

                    _unitOfWork.Complete();

                    FileLogger.Log($"User with ID: {id} updated");
                    return Ok(user);
                }
                else
                {
                    return BadRequest("User Fullname is required!");
                }

            }
            catch (Exception ex)
            {
                FileLogger.Log($"Error updating user with ID: {id}.");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _unitOfWork.Users.GetById(id);

                if (user == null)
                    return NotFound();

                _unitOfWork.Users.Remove(user);
                _unitOfWork.Complete();

                FileLogger.Log($"User with ID: {id} deleted");
                return Ok($"User with ID: {id} deleted");
            }
            catch (Exception ex)
            {
                FileLogger.Log($"Error deleting user with ID: {id}.");
                return BadRequest(ex.Message);
            }
        }
    }
}
