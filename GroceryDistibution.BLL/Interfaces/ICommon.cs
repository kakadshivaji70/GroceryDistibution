using GroceryDistibution.BLL.ViewModels;
using System.Threading.Tasks;

namespace GroceryDistibution.BLL.Interfaces
{
    public interface ICommon
    {
        Task<ApplicationReleaseVM> GetVersionDetails(string version);
    }
}
