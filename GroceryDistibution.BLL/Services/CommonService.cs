using AutoMapper;
using GroceryDistibution.BLL.Interfaces;
using GroceryDistibution.BLL.ViewModels;
using GroceryDistibution.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryDistibution.BLL.Services
{
    public class CommonService : ICommon
    {
        public GroceryDistibutionDbContext dbContext { get; }
        public IConfiguration configuration { get; }
        private readonly IMapper mapper;
        public CommonService(GroceryDistibutionDbContext _dbContext, IConfiguration _configuration, IMapper _mapper)
        {
            dbContext = _dbContext;
            configuration = _configuration;
            mapper = _mapper;
        }
        public CommonService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<ApplicationReleaseVM> GetVersionDetails(string version)
        {
            var applicationRelease = mapper.Map<ApplicationReleaseVM>(await dbContext.ApplicationReleases.Where(o =>o.Version == version).FirstOrDefaultAsync());
            if(applicationRelease == null)
            {
                return new ApplicationReleaseVM() { IsVersionPresent = false };
            }
            else
            {
                return applicationRelease;
            }
        }
    }
}