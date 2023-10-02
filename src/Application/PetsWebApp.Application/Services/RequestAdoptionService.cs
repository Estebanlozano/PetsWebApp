using PetsWebApp.Core.Entities;
using PetsWebApp.Core.Exceptions;
using PetsWebApp.Core.Interfaces.Repositories;
using PetsWebApp.Core.Interfaces.Services;
using PetsWebApp.Core.Interfaces.Validators;

namespace PetsWebApp.Application.Services
{
    public class RequestAdoptionService : IRequestAdoptionService
    {
        private readonly IRequestAdoptionValidator _requestValidator;
        private readonly IRequestAdoptionRepository _requestRepository;

        public RequestAdoptionService(IRequestAdoptionRepository requestRepository, IRequestAdoptionValidator requesAdoptionValidator)
        {
            _requestRepository = requestRepository;
            _requestValidator = requesAdoptionValidator;
        }

        public async Task CreateRequest(RequestAdoption requestAdoption, int petId, Guid userId )
        {
            (var IsValid, var errors ) =   _requestValidator.IsValid(requestAdoption );
            if (!IsValid)
            {
                throw new ValidationException(errors);
            }

            try
            {
                 await _requestRepository.AddRequest(requestAdoption, petId, userId );
            }
            catch (Exception ex)
            {
                throw new RepositoryException(string.Empty, ex);
            }
        }

        public async Task<IEnumerable<RequestAdoption>> GetAllRequest(Guid userId)
        {
            try
            {
                return await _requestRepository.GetAllRequest( userId );
            }
            catch (Exception ex) 
            {
                throw new RepositoryException(string.Empty, ex);
            }
        }
        public async Task<IEnumerable<RequestAdoption>> GetAllRequest(int petId)
        {
            try
            {
                return await _requestRepository.GetAllRequest(petId);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(string.Empty, ex);
            }
        }

        public async Task GetRequest(int Id)
        {
            if (Id <= 0)
            {
                throw new ValidationException(new List<string>{"Id is not valid "});
            }
            try
            {
                await _requestRepository.GetRequest(Id);
            }
            catch (Exception ex) 
            {
                throw new RepositoryException (string.Empty, ex);
            }
            
        }

        public async Task UpdateRequest(RequestAdoption requestAdoption)
        {
            try
            {
                await _requestRepository.UpadateRequest(requestAdoption);
            }
            catch(Exception ex) 
            {
                throw new RepositoryException(string.Empty, ex);
            }
        }
    }
}
