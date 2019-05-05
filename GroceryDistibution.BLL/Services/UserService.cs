using AutoMapper;
using GroceryDistibution.BLL.Authentication;
using GroceryDistibution.BLL.Interfaces;
using GroceryDistibution.BLL.ViewModels;
using GroceryDistibution.DAL;
using GroceryDistibution.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryDistibution.BLL.Services
{
    public class UserService : IUser
    {
        public GroceryDistibutionDbContext dbContext { get; }
        private readonly IMapper mapper;
        public UserService(GroceryDistibutionDbContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }
        public async Task<List<UserTypeVM>> GetUserTypes()
        {
            var userTypes = await dbContext.UserTypes.ToListAsync();
            return mapper.Map<List<UserTypeVM>>(userTypes);
        }
        public async Task<bool> CreateUser(UserVM user)
        {
            var usr = await dbContext.Users.Where(o => o.IsDeleted == false && o.UserName == user.UserName).FirstOrDefaultAsync();
            if(usr != null)
            {
                user.IsUserNameExists = true;
                return false;
            }
            else
            {
                var userObj = mapper.Map<User>(user);
                userObj.SaltValue = Salt.Create();
                userObj.PasswordHash = Hash.Create(userObj.PasswordHash, userObj.SaltValue);
                dbContext.Users.Add(userObj);
                return (await dbContext.SaveChangesAsync() > 0 ? true : false);
            }
        }

        public async Task<UserVM> GetUserById(int id)
        {
            var obj = mapper.Map<UserVM>(await dbContext.Users.FindAsync(id));
            if (obj != null)
            {
                obj.PasswordHash = null;
                obj.SaltValue = null;
                return obj;
            }
            return null;
        }

        public async Task<UserVM> GetUserByEmail(string email)
        {
            var obj = mapper.Map<UserVM>(await dbContext.Users.Where(o => o.Email == email && o.IsActive == true && o.IsDeleted == false).FirstOrDefaultAsync());
            if (obj != null)
            {
                obj.PasswordHash = null;
                obj.SaltValue = null;
                return obj;
            }
            return null;
        }

        public async Task<UserVM> GetUserByMobileNumber(string mobileNumber)
        {
            var obj = mapper.Map<UserVM>(await dbContext.Users.Where(o => o.MobileNumber == mobileNumber && o.IsActive == true && o.IsDeleted == false).FirstOrDefaultAsync());
            if (obj != null)
            {
                obj.PasswordHash = null;
                obj.SaltValue = null;
                return obj;
            }
            return null;
        }

        public async Task<UserVM> Login(UserVM user)
        {
            // use Encryption Algo here
            var usr = await dbContext.Users.Where(o => o.UserName == user.UserName).FirstOrDefaultAsync();
            if (usr == null)
                return null;

            user.IsObjectChanged = true;
            user.IsActive = usr.IsActive;
            user.IsDeleted = usr.IsDeleted;
            if (Hash.Validate(user.PasswordHash, usr.SaltValue, usr.PasswordHash) && usr.IsActive == true && usr.IsDeleted == false)
            {
                user.IsPasswordMatch = true; ;
                var obj = mapper.Map<UserVM>(usr);
                obj.PasswordHash = null;
                obj.SaltValue = null;
                return obj;
            }
            else
            {
                user.IsPasswordMatch = false;
                return null;
            }
        }

        public async Task<bool> ChangePassword(UserVM user)
        {
            var usr = await dbContext.Users.Where(o => o.IsDeleted == false && o.UserName == user.UserName).FirstOrDefaultAsync();
            if (usr == null)
                return false;

            if (Hash.Validate(user.PasswordHash, usr.SaltValue, usr.PasswordHash) && usr.IsActive == true && usr.IsDeleted == false)
            {
                usr.SaltValue = Salt.Create();
                usr.PasswordHash = Hash.Create(user.PasswordHashNew, usr.SaltValue);
                dbContext.Entry(usr).State = EntityState.Modified;
                return (await dbContext.SaveChangesAsync() > 0 ? true : false);
            }
            else
            {
                user.IsObjectChanged = true;
                user.IsActive = usr.IsActive;
                user.IsDeleted = usr.IsDeleted;
                return user.IsPasswordMatch = false;
            }
        }
    }
}