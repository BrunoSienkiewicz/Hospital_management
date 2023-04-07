using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models.Domain;
using Hospital_Management.Models.Dto;
using Hospital_Management.Models.ViewModels.Patient;
using Hospital_Management.Repositiory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Controllers
{
    public class PatientsController : BaseController<Patient, AddPatientViewModel, UpdatePatientViewModel, PatientDto, IPatientRepository>
    {
        private readonly IUserRepository userRepository;
        public PatientsController(IPatientRepository patientRepository, IUserRepository userRepository) : base(patientRepository)
        {
			this.userRepository = userRepository;
        }

        protected async override Task<UpdatePatientViewModel> mapToUpdateEntityModel(Patient entity)
        {
            var user = await userRepository.GetEntityById(entity.UserId);
            var model = new UpdatePatientViewModel
            {
                PatientId = entity.PatientId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Address = entity.Address,
                Pesel = entity.Pesel,
                DateofBirth = entity.DateofBirth,
                UserId = entity.UserId,
                Username = user.Username,
                Password = user.Password
            };
            return model;
        }

        public override async Task<IActionResult> Add(AddPatientViewModel entity)
        {
			var user = new User
            {
				UserId = await userRepository.GetMaxId() + 1,
				Username = entity.Username,
				Password = entity.Password,
				UserType = "User"
			};

			await userRepository.AddEntity(user);
			entity.UserId = user.UserId;
			return await base.Add(entity);
		}

		public override async Task<IActionResult> Update(UpdatePatientViewModel entity)
		{
            var user = await userRepository.GetEntityById(entity.PatientId);
            user.Username = entity.Username;
            user.Password = entity.Password;
            await userRepository.UpdateEntity(user);
            return await base.Update(entity);
        }
	}
}
