using AutoMapper;
using Manager.Api.Utilities;
using Manager.Api.ViewModels;
using Manager.Core.Exceptions;
using Manager.Services.Dtos;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Manager.Api.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var users = await _userService.Get();

                var result = new ResultViewModel
                {
                    Message = null,
                    Success = true,
                    Data = users
                };

                return Ok(result);
            }
            catch (DomainException exception)
            {
                return BadRequest(Responses.DomainErrorMessage(exception.Message, exception.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAsync(
            [FromRoute] long id
        )
        {
            try
            {
                var users = await _userService.Get(id);

                var result = new ResultViewModel
                {
                    Message = null,
                    Success = true,
                    Data = users
                };

                return Ok(result);
            }
            catch (DomainException exception)
            {
                return BadRequest(Responses.DomainErrorMessage(exception.Message, exception.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Route("email")]
        [Authorize]
        public async Task<IActionResult> GetByEmailAsync(
            [FromQuery] string search
        )
        {
            try
            {
                var user = await _userService.GetByEmailAsync(search);

                var result = new ResultViewModel
                {
                    Message = null,
                    Success = true,
                    Data = user
                };

                return Ok(result);
            }
            catch (DomainException exception)
            {
                return BadRequest(Responses.DomainErrorMessage(exception.Message, exception.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Route("list-by-email")]
        [Authorize]
        public async Task<IActionResult> SearchByEmailAsync(
            [FromQuery] string search
        )
        {
            try
            {
                var users = await _userService.SearchByEmailAsync(search);

                var result = new ResultViewModel
                {
                    Message = null,
                    Success = true,
                    Data = users
                };

                return Ok(result);
            }
            catch (DomainException exception)
            {
                return BadRequest(Responses.DomainErrorMessage(exception.Message, exception.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Route("list-by-name")]
        [Authorize]
        public async Task<IActionResult> SearchByNameAsync(
            [FromQuery] string search
        )
        {
            try
            {
                var users = await _userService.SearchByNameAsync(search);

                var result = new ResultViewModel
                {
                    Message = null,
                    Success = true,
                    Data = users
                };

                return Ok(result);
            }
            catch (DomainException exception)
            {
                return BadRequest(Responses.DomainErrorMessage(exception.Message, exception.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAsync(
            [FromBody] CreateUserViewModel model
        )
        {
            try
            {
                var userDto = _mapper.Map<UserDto>(model);

                var userCreated = await _userService.Create(userDto);

                var result = new ResultViewModel
                {
                    Message = "User created successfully",
                    Success = true,
                    Data = userCreated
                };

                return Ok(result);
            }
            catch (DomainException exception)
            {
                return BadRequest(Responses.DomainErrorMessage(exception.Message, exception.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutAsync(
            [FromBody] UpdateUserViewModel model
        )
        {
            try
            {
                var userDto = _mapper.Map<UserDto>(model);

                var userUpdated = await _userService.Update(userDto);

                var result = new ResultViewModel
                {
                    Message = "User updated successfully",
                    Success = true,
                    Data = userUpdated
                };

                return Ok(result);
            }
            catch (DomainException exception)
            {
                return BadRequest(Responses.DomainErrorMessage(exception.Message, exception.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> PutAsync(
            [FromRoute] long id
        )
        {
            try
            {
                await _userService.Remove(id);

                var result = new ResultViewModel
                {
                    Message = "User removed successfully",
                    Success = true,
                    Data = null
                };

                return Ok(result);
            }
            catch (DomainException exception)
            {
                return BadRequest(Responses.DomainErrorMessage(exception.Message, exception.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }
    }
}
