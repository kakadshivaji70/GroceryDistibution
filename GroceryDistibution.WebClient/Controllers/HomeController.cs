using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class HomeController : Controller
    {
        ICommon commonService;
        protected readonly ILogger<HomeController> logger;
        public HomeController(ICommon _commonService, ILogger<HomeController> _logger)
        {
            commonService = _commonService;
            logger = _logger;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[Route("api/Home/GetVersionDetails/{id?}", Name = "GetVersionDetails")]
        //[Route("[action/{id?}]")]
        [HttpGet("GetVersionDetails/{version?}")]
        public async Task<ApiResponse<ApplicationReleaseVM>> GetVersionDetails(string version)
        {
            ApiResponse<ApplicationReleaseVM> response = new ApiResponse<ApplicationReleaseVM>() { Status = EnumApiResponseStatus.Success };
            try
            {
                if (string.IsNullOrEmpty(version))
                {
                    response.Message = "Version is not present, please retry again";
                    response.Status = EnumApiResponseStatus.Error;
                }
                else
                response.Result = await commonService.GetVersionDetails(version);

                if (response.Result.IsVersionPresent)
                {
                    response.Message = "Version doesnot exist";
                    response.Status = EnumApiResponseStatus.Error;
                }
                else if (response.Result.IsDeleted == true)
                {
                    response.Message = "Version has been deleted";
                    response.Status = EnumApiResponseStatus.Info;
                }
            }
            catch (Exception ex)
            {
                //use logger here
                logger.LogError("GetVersionDetails ", ex);
                response.Status = EnumApiResponseStatus.Error;
            }
            return response;
        }

        [HttpGet("[action]")]
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}