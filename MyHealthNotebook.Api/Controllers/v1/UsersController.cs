using Microsoft.AspNetCore.Mvc;
using MyHealthNotebook.DataService.IConfiguration;
using MyHealthNotebook.Entities.Dtos.Incoming;

namespace MyHealthNotebook.Api.Controllers.v1
{
    public class UsersController : BaseController
    {
        public UsersController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _unitOfWork.Users.All();
            if(users == null)
                return NotFound("There are not users");
            
            var listOfUsersDto = await _unitOfWork.TranslateListOfEntities(users);
            
            return Ok(listOfUsersDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserDto userDto)
        {
            var user = await _unitOfWork.ToEntityTranslator.ToUser(userDto);
            await _unitOfWork.Users.Add(user);
            await _unitOfWork.CompleteAsync();
            // return CreatedAtRoute("GetUser", new {id = user.Id}, user);
            return Ok(user.Id);
        }
        
        [HttpGet]
        [Route("GetUser", Name = "GetUser")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _unitOfWork.Users.GetById(id);
            if(user == null)
                 return BadRequest("User not found");

            var userDto = await _unitOfWork.ToDtoTranslator.ToUserDtoTranslator(user); 
            return Ok(userDto);
        }

    }
}