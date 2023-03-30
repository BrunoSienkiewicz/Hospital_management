using Hospital_Management.Data;
using Hospital_Management.Models.Domain;
using Hospital_Management.Models.ViewModels.Patient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Controllers
{
    public class PatientsController : BaseController<Patient, AddPatientViewModel, UpdatePatientViewModel>
    {

        public PatientsController(HospitalDbContext hospitalDbContext) : base(hospitalDbContext)
        {
        }

		[HttpPost]
		public async Task<IActionResult> Index(string pesel)
		{
			var patient = await hospitalDbContext.Patients.Where(p => p.Pesel.Contains(pesel)).FirstOrDefaultAsync();
			return View(patient);
		}


	}
}
