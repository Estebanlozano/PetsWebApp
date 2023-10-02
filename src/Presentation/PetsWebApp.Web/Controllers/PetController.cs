using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsWebApp.Core.Entities;
using PetsWebApp.Core.Exceptions;
using PetsWebApp.Core.Interfaces.Repositories;
using PetsWebApp.Core.Interfaces.Services;
using PetsWebApp.Web.ViewModels;
using System.Security.Claims;


namespace PetsWebApp.Web.Controllers
{
    [Authorize]
    public class PetController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPetService petService;
        private readonly IImageRepository imageRepository;

        public PetController(IMapper mapper, IPetService petService, IImageRepository imageRepository)
        {
            this.mapper = mapper;
            this.petService = petService;
            this.imageRepository = imageRepository;
        }

        public async Task<IActionResult> Index(string nombreFiltro, string descripcionFiltro, bool? castradoFiltro, int? edadFiltro, string archivedFiltroSelect, int? page)
        {
            try
            {
                int itemsPerPage = 10; // Mostrar 10 mascotas por página
                int pageNumber = page ?? 1;

                var (pets, pagination) = await petService.GetAll(nombreFiltro, descripcionFiltro, castradoFiltro, edadFiltro, archivedFiltroSelect, pageNumber, itemsPerPage);

                ViewBag.FirstPage = pagination.firstPage;
                ViewBag.LastPage = pagination.lastPage;
                ViewBag.PageNumber = pageNumber;

                IEnumerable<PetViewModel> mappedPets = pets.Select(x => mapper.Map<PetViewModel>(x));

                return View(mappedPets);
            }
            catch (ValidationException ex)
            {
                ViewBag.Errors = ex.Errors;
                return View();
            }
            catch (RepositoryException)
            {
                ViewBag.Errors = new List<string>()
                {
                    "Error al obtener las mascotas"
                };
                return View();
            }
            catch (Exception)
            {
                ViewBag.Errors = new List<string>()
                {
                    "Error al obtener las mascotas"
                };
                return View();
            }
        }

        public ActionResult Add()
        {
            return View(new PetViewModel());
        }

        public async Task<ActionResult> See(int id)
        {
            try
            {
                Pet? pet = await petService.Get(id);
                if (pet is null)
                {
                    return NotFound();
                }

                PetViewModel mappedPet = mapper.Map<PetViewModel>(pet);

                return View(mappedPet);
            }
            catch (ValidationException ex)
            {
                ViewBag.Errors = ex.Errors;
                return View();
            }
            catch (RepositoryException)
            {
                ViewBag.Errors = new List<string>()
                {
                    "Error getting the pet"
                };
                return View();
            }
            catch (Exception)
            {
                ViewBag.Errors = new List<string>()
                {
                    "Error getting the pet"
                };
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                Pet? pet = await petService.Get(id);
                if (pet is null)
                {
                    return NotFound();
                }

                PetViewModel mappedPet = mapper.Map<PetViewModel>(pet);

                return View("Add", mappedPet);
            }
            catch (ValidationException ex)
            {
                ViewBag.Errors = ex.Errors;
                return View();
            }
            catch (RepositoryException)
            {
                ViewBag.Errors = new List<string>()
                {
                    "Error getting the pet"
                };
                return View();
            }
            catch (Exception)
            {
                ViewBag.Errors = new List<string>()
                {
                    "Error getting the pet"
                };
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Save(PetViewModel viewModel, List<IFormFile> images)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", viewModel);
            }

            try
            {
                Pet pet = mapper.Map<Pet>(viewModel);

                pet.Photos = new List<PetPhoto2>();
                foreach (var image in images)
                {
                    string url = await imageRepository.Add(image);
                    pet.Photos.Add(new PetPhoto2
                    {
                        Path = url,
                    });
                }

                if (pet.Id == 0)
                {
                    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    pet.UserId = Guid.Parse(userId);

                    await petService.CreatePet(pet);
                }
                else
                {
                    //await petService.UpdatePet(pet, petPost, petPhotos);
                }

                return RedirectToAction(nameof(See), new { id = pet.Id });
            }
            catch (ValidationException ex)
            {
                ViewBag.Errors = ex.Errors;
                return View("Add", viewModel);
            }
            catch (RepositoryException)
            {
                ViewBag.Errors = new List<string>()
                {
                    "Error creating the pet"
                };
                return View("Add", viewModel);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await petService.DeletePet(id);

                return Ok();
            }
            catch 
            {
                return NotFound();
            }
        }
    }
}