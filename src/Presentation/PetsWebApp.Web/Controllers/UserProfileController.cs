using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsWebApp.Core.Entities;
using PetsWebApp.Core.Exceptions;
using PetsWebApp.Core.Interfaces.Repositories;
using PetsWebApp.Core.Interfaces.Services;
using System.Security.Claims;

namespace PetsWebApp.Web.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IImageRepository imageRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IImageRepository imageRepository, IWebHostEnvironment hostingEnvironment, IUserProfileService userProfileService)
        {
            this.imageRepository = imageRepository; 
            this.hostingEnvironment = hostingEnvironment; 
            _userProfileService = userProfileService;
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                UserProfile? profile = await _userProfileService.GetUser(userId);
                if (profile == null)
                {
                    return View(new UserProfile());
                }

                return View(profile);
            }
            catch (ValidationException)
            {
                ModelState.AddModelError(string.Empty, "Se produjo un error al validar los datos.");
                return View("Index");
            }
            catch (RepositoryException)
            {
                ModelState.AddModelError(string.Empty, "Se produjo un error en el repositorio de datos.");
                return View("Index");
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "Se produjo al mostrar los datos.");
                return View("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUserProfile(UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", userProfile);
            }
            
            IFormFile? imageFile = Request.Form.Files["Image"];
            if (imageFile is not null)
            {
                userProfile.Avatar = await imageRepository.Add(imageFile);
            }

            try
            {
                if (userProfile.Id == 0)
                {
                    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    userProfile.UserId = Guid.Parse(userId);

                    await _userProfileService.CreateUser(userProfile);
                }
                else
                {
                    await _userProfileService.UpdateUser(userProfile);
                }

            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Index", "UserProfile");
            }
            catch (RepositoryException)
            {

                ModelState.AddModelError(string.Empty, "Se produjo un error en el repositorio de datos.");
                return View("Index", "UserProfile");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Se produjo un error al crear o actualizar el perfil del usuario.");
                return View("Index", "UserProfile");
            }

            return RedirectToAction("Index");
        }
    }
}
