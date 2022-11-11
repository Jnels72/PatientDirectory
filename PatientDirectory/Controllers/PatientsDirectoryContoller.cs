using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System;
using PatientDirectory.DataAccess;
using PatientDirectory.DataAccess.Models;
using PatientDirectory.DataAccess.Controllers;
using PatientDirectory.Models;

namespace PatientDirectory.Controllers
{
    public class PatientsDirectoryController : Controller
    {
        private readonly IPatientDirectoryConfigManager _configuration;

        public PatientsDirectoryController(IPatientDirectoryConfigManager configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            PatientDirectoyViewModel model = new PatientDirectoyViewModel(_configuration);
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int patientId, string patientName, string patientNumber, string patientAddress, string emergencyContact, string emergencyContactNumber, string doctorName, string doctorNumber)
        {
            if (patientId != 0)
            {
                PatientDirectoryController.UpdatePatient(patientId, patientName, patientNumber, patientAddress, emergencyContact, emergencyContactNumber, doctorName, doctorNumber, _configuration);
            }
            else
            {
                PatientDirectoryController.CreatePatient(patientId, patientName, patientNumber, patientAddress, emergencyContact, emergencyContactNumber, doctorName, doctorNumber, _configuration);
            }

            PatientDirectoyViewModel model = new PatientDirectoyViewModel(_configuration);
            model.IsActionSuccess = true;
            model.ActionMessage = "Patient has been saved successfully";

            return View(model);
        }

        public IActionResult Update(int id)
        {
            PatientDirectoyViewModel model = new PatientDirectoyViewModel(_configuration, id);
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                PatientDirectoryController.DeletePatient(id, _configuration);
            }

            PatientDirectoyViewModel model = new PatientDirectoyViewModel(_configuration);
            model.IsActionSuccess = true;
            model.ActionMessage = "Patient has been deleted successfully";
            return View("Index", model);
        }
    }
}