//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace aspapp.Controllers
//{
//    public class IdentityModel : PageModel
//    {
//        private readonly SignInManager<IdentityUser> _signInManager;

//        public IdentityModel(SignInManager<IdentityUser> signInManager)
//        {
//            _signInManager = signInManager;
//        }

//        public async Task<IActionResult> SignOutIdentity()
//        {
//            await _signInManager.SignOutAsync();
//            return RedirectToPage("/Index");
//        }
//    }
//}
