using AutoMapper;
using CompanyMvc.Dox.DAL.Model;

namespace CompanyMvc.Dox.PL.ViewModels.Users
{
	public class UserProfile:Profile
	{

        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>();
        }
    }
}
