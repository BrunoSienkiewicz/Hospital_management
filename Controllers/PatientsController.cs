using Hospital_Management.Data;
using Hospital_Management.Models;
using Hospital_Management.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class PatientsController : BaseController<Patient, AddPatientViewModel, UpdatePatientViewModel>
    {

        public PatientsController(HospitalDbContext hospitalDbContext) : base(hospitalDbContext)
        {
        }
    }
}
