using Hospital_Management.Data;
using Hospital_Management.Models;
using Hospital_Management.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Controllers
{
    public class PatientsController : Controller
    {
        private readonly HospitalDbContext hospitalDbContext;

        public PatientsController(HospitalDbContext hospitalDbContext)
        {
            this.hospitalDbContext = hospitalDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var patients = await hospitalDbContext.Patients.ToListAsync();
            return View(patients);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPatientViewModel addPatientRequest)
        {
            var patient = new Patient()
            {
                firstName = addPatientRequest.firstName,
                lastName = addPatientRequest.lastName,
                email = addPatientRequest.email,
                phone = addPatientRequest.phone,
                address = addPatientRequest.address,
                PESEL = addPatientRequest.PESEL,
                dateOfBirth = addPatientRequest.dateOfBirth,
                userId = addPatientRequest.userId
            };

            await hospitalDbContext.Patients.AddAsync(patient);
            await hospitalDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
