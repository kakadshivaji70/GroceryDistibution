using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryDistibution.BLL.Interfaces;
using GroceryDistibution.BLL.ViewModels;
using GroceryDistibution.Common.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GroceryDistibution.WebClient.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        IUser userService;
        protected readonly ILogger<UserController> logger;
        public UserController(IUser _userService, ILogger<UserController> _logger)
        {
            userService = _userService;
            logger = _logger;
        }

        [HttpGet("[action]")]
        public async Task<ApiResponse<IEnumerable<UserTypeVM>>> GetUserTypes()
        {
            ApiResponse<IEnumerable<UserTypeVM>> response = new ApiResponse<IEnumerable<UserTypeVM>>() { Status = EnumApiResponseStatus.Success };
            try
            {
                response.Result = await userService.GetUserTypes();
            }
            catch (Exception ex)
            {
                logger.LogError("GetUserTypes ", ex);
                response.Status = EnumApiResponseStatus.Error;
                response.Message = "Error occured at server";
            }
            return response;
        }

        //[HttpGet("GetUserById/id")]
        [HttpGet]
        [Route("GetUserById/{id}")]
        public async Task<ApiResponse<UserVM>> GetUserById(int id)
        {
            ApiResponse<UserVM> response = new ApiResponse<UserVM>() { Status = EnumApiResponseStatus.Success };
            try
            {
                response.Result = await userService.GetUserById(id);
                if (response.Result != null)
                {
                    if (response.Result.IsDeleted == true)
                    {
                        response.Message = "User account is not no longer present/deleted";
                        response.Status = EnumApiResponseStatus.Error;
                    }
                    else if (response.Result.IsActive == false)
                    {
                        response.Message = "User account is not active, please activate";
                        response.Status = EnumApiResponseStatus.Info;
                    }
                }
                else//(response.Result == null)
                {
                    response.Message = "User account is not exists";
                    response.Status = EnumApiResponseStatus.Error;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("GetUserById ", ex);
                response.Status = EnumApiResponseStatus.Error;
                response.Message = "Error occured at server";
            }
            return response;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<ApiResponse<bool>> CreateUser([FromBody] UserVM user)
        {
            ApiResponse<bool> response = new ApiResponse<bool>() { Status = EnumApiResponseStatus.Success };
            try
            {
                if (string.IsNullOrEmpty(user.PasswordHash))
                {
                    response.Message = "Please provide password";
                    response.Status = EnumApiResponseStatus.Error;
                }
                else
                {
                    response.Result = await userService.CreateUser(user);

                    if (response.Result)
                    {
                        response.Message = "Your account has been created successfully!";
                    }
                    else if (user.IsUserNameExists)
                    {
                        response.Message = "Username is already exists, please try with different username";
                        response.Status = EnumApiResponseStatus.Info;
                    }
                    else
                    {
                        response.Message = "Your account not created, please try again";
                        response.Status = EnumApiResponseStatus.Error;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("CreateUser", ex);
                response.Status = EnumApiResponseStatus.Error;
                response.Message = "Error occured at server, please try again";
            }
            return response;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ApiResponse<UserVM>> Login([FromBody] UserVM user)
        {
            ApiResponse<UserVM> response = new ApiResponse<UserVM>() { Status = EnumApiResponseStatus.Success };
            try
            {
                if (string.IsNullOrWhiteSpace(user.PasswordHash))
                {
                    response.Message = "Please provide password";
                    response.Status = EnumApiResponseStatus.Error;
                }
                else
                {
                    response.Result = await userService.Login(user);

                    if (user.IsDeleted == true && user.IsObjectChanged)
                    {
                        response.Message = "Your account is not no longer present/deleted";
                        response.Status = EnumApiResponseStatus.Error;
                        response.Result = null;
                    }
                    else if (user.IsActive == false && user.IsObjectChanged)
                    {
                        response.Message = "Your account is not active, please contact admin";
                        response.Status = EnumApiResponseStatus.Info;
                        response.Result = null;
                    }                    
                    else if (!user.IsPasswordMatch && user.IsObjectChanged)
                    {
                        response.Message = "Username and Password doesnot match, please try again";
                        response.Status = EnumApiResponseStatus.Error;
                        response.Result = null;
                    }
                    else if (response.Result != null)
                        response.Message = "Successfully logged in";
                }
            }
            catch (Exception ex)
            {
                logger.LogError("CreateUser", ex);
                response.Status = EnumApiResponseStatus.Error;
                response.Message = "Error occured at server, please try again";
            }
            return response;
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<ApiResponse<bool>> ChangePassword([FromBody] UserVM user)
        {
            ApiResponse<bool> response = new ApiResponse<bool>() { Status = EnumApiResponseStatus.Success };
            try
            {
                if (string.IsNullOrEmpty(user.PasswordHash) || string.IsNullOrEmpty(user.PasswordHashNew))
                {
                    response.Message = "Please provide password";
                    response.Status = EnumApiResponseStatus.Error;
                }
                else
                {
                    response.Result = await userService.ChangePassword(user);

                    if (!response.Result)
                    {
                        if (user.IsDeleted == true)
                        {
                            response.Message = "Your account is not no longer present/deleted";
                            response.Status = EnumApiResponseStatus.Error;
                        }
                        else if (user.IsActive == false)
                        {
                            response.Message = "Your account is not active, please contact admin";
                            response.Status = EnumApiResponseStatus.Info;
                        }
                        else if (!user.IsPasswordMatch)
                        {
                            response.Message = "Your account password & does not match";
                            response.Status = EnumApiResponseStatus.Error;
                        }
                    }
                    response.Message = "Your password has been changed successfully";
                }                
            }
            catch (Exception ex)
            {
                logger.LogError("CreateUser", ex);
                response.Status = EnumApiResponseStatus.Error;
                response.Message = "Error occured at server, please try again";
            }
            return response;
        }
    }
}