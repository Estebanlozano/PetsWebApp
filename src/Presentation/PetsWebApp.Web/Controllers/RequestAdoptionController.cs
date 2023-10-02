using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetsWebApp.Core.Entities;
using PetsWebApp.Core.Exceptions;
using PetsWebApp.Core.Interfaces.Repositories;
using PetsWebApp.Core.Interfaces.Services;
using PetsWebApp.Web.ViewModels;
using System.Security.Claims;

namespace PetsWebApp.Web.Controllers
{

    public class RequestAdoptionController : Controller
    {
        private readonly IMapper mapper;
        private readonly IRequestAdoptionRepository _adoptionRepository;
        private readonly IRequestAdoptionService _adoptionService;

        public RequestAdoptionController(IMapper mapper, IRequestAdoptionRepository adoptionRepository, IRequestAdoptionService adoptionService)
        {
            this.mapper = mapper;
            _adoptionService = adoptionService;
            _adoptionRepository = adoptionRepository;
        }
        public ActionResult Index( int petId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View("FormRequestAdoption", new RequestAdoptionViewModel 
            { 
                PetId = petId,
                UserId= Guid.Parse(userId)
            });
        }

        // GET: RequestAdoptionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RequestAdoptionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequestAdoptionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(RequestAdoptionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {

            ViewBag.Errors = new List<string>()
            {
                "error al crear la request"
            };
            return View("FormRequestAdoption",viewModel);
            }

            try
            {
                RequestAdoption requestAdoption = mapper.Map<RequestAdoption>(viewModel);
                if (requestAdoption.Id == 0)
                {
                    await _adoptionService.CreateRequest(requestAdoption, viewModel.PetId, viewModel.UserId);

                    return View("confirmacion");
                }
                else
                {
                    await _adoptionService.UpdateRequest(requestAdoption);
                }
                return View("confirmacion");
            }
            catch (ValidationException ex)
            {
                ViewBag.ErrorInfo = ex.Errors;
                return View("Error");
            }
            catch (RepositoryException)
            {
                ViewBag.Errors = new List<string>()
                {
                    "error al crear la request"
                };
                return View("FormRequestAdoption", viewModel);
            }
        }

        // GET: RequestAdoptionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RequestAdoptionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RequestAdoptionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RequestAdoptionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
