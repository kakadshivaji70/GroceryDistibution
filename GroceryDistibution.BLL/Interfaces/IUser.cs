using GroceryDistibution.BLL.ViewModels;
using GroceryDistibution.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GroceryDistibution.BLL.Interfaces
{
    public interface IUser
    {
        Task<List<UserTypeVM>> GetUserTypes();
        Task<bool> CreateUser(UserVM user);
        Task<UserVM> GetUserById(int id);
        Task<UserVM> Login(UserVM user);
        Task<bool> ChangePassword(UserVM user);
    }
}
