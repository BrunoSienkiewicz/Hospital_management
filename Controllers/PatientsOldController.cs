//using Hospital_Management.Data;
//using Hospital_Management.Models;
//using Hospital_Management.Models.Domain;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace Hospital_Management.Controllers
//{
//    public class PatientsController : Controller
//    {
//        private readonly HospitalDbContext hospitalDbContext;

//        public PatientsController(HospitalDbContext hospitalDbContext)
//        {
//            this.hospitalDbContext = hospitalDbContext;
//        }

//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            var patients = await hospitalDbContext.Patients.ToListAsync();
//            return View(patients);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Index(string pesel)
//        {
//            var patient = await hospitalDbContext.Patients.Where(p => p.Pesel.Contains(pesel)).FirstOrDefaultAsync();
//			return View(patient);
//		}

//        [HttpGet]
//        public IActionResult Add()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Add(AddPatientViewModel addPatientRequest)
//        {
//            int maxId = await hospitalDbContext.Patients.MaxAsync(p => p.PatientId);

//            var patient = new Patient()
//            {
//                PatientId = maxId+1,
//                FirstName = addPatientRequest.FirstName,
//                LastName = addPatientRequest.LastName,
//                Email = addPatientRequest.Email,
//                PhoneNumber = addPatientRequest.PhoneNumber,
//                Address = addPatientRequest.Address,
//                Pesel = addPatientRequest.Pesel,
//                DateofBirth = addPatientRequest.DateofBirth,
//                UserId = addPatientRequest.UserId
//            };

//            await hospitalDbContext.Patients.AddAsync(patient);
//            await hospitalDbContext.SaveChangesAsync();
//            return RedirectToAction("Index");
//        }

//        [HttpGet]
//        public async Task<IActionResult> View(int id)
//        {
//            var patient = await hospitalDbContext.Patients.FirstOrDefaultAsync(p => p.PatientId == id);

//            if (patient != null)
//            {
//                var viewModel = new UpdatePatientViewModel()
//                {
//                    PatientId = id,
//                    FirstName = patient.FirstName,
//                    LastName = patient.LastName,
//                    Email = patient.Email,
//                    PhoneNumber = patient.PhoneNumber,
//                    Address = patient.Address,
//                    Pesel = patient.Pesel,
//                    DateofBirth = patient.DateofBirth,
//                    UserId = patient.UserId
//                };

//                return await Task.Run(() => View("View", viewModel));
//            }

//			return RedirectToAction("Index");
//		}

//        [HttpPost]
//        public async Task<IActionResult> View(UpdatePatientViewModel model)
//        {
//            var patient = await hospitalDbContext.Patients.FindAsync(model.PatientId);

//            if (patient != null)
//            {
//                patient.Pesel = model.Pesel;
//                patient.FirstName = model.FirstName;
//                patient.LastName = model.LastName;
//                patient.Email = model.Email;
//                patient.PhoneNumber = model.PhoneNumber;
//                patient.Address = model.Address;
//                patient.DateofBirth = model.DateofBirth;

//                await hospitalDbContext.SaveChangesAsync();

//                return RedirectToAction("Index");
//            }

//            return RedirectToAction("Index");
//        }

//        [HttpPost]
//        public async Task<IActionResult> Delete(UpdatePatientViewModel model)
//        {
//            var patient = await hospitalDbContext.Patients.FindAsync(model.PatientId);

//            if (patient != null)
//            {
//                hospitalDbContext.Remove(patient);
//                await hospitalDbContext.SaveChangesAsync();

//                return RedirectToAction("Index");
//            }

//            return RedirectToAction("Index");
//        }

//        [HttpGet]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var patient = await hospitalDbContext.Patients.FirstOrDefaultAsync(p => p.PatientId == id);

//            if (patient != null)
//            {
//                var model = new UpdatePatientViewModel()
//                {
//                    PatientId = id,
//                    FirstName = patient.FirstName,
//                    LastName = patient.LastName,
//                    Email = patient.Email,
//                    PhoneNumber = patient.PhoneNumber,
//                    Address = patient.Address,
//                    Pesel = patient.Pesel,
//                    DateofBirth = patient.DateofBirth,
//                    UserId = patient.UserId
//                };

//                return await Task.Run(() => Delete(model));
//            }

//            return RedirectToAction("Index");
//        }
//    }
//}
