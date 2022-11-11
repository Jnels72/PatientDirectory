using PatientDirectory.DataAccess;
using PatientDirectory.DataAccess.Controllers;
using PatientDirectory.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientDirectory.Models
{
    public class PatientDirectoyViewModel
    {
        private IPatientDirectoryConfigManager _configuration;
        public List<PatientDirectoryModel> PatientList { get; set; }
        public PatientDirectoryModel CurrentPatient { get; set; }
        public bool IsActionSuccess { get; set; }
        public string ActionMessage { get; set; }


        public PatientDirectoyViewModel(IPatientDirectoryConfigManager configuration)
        {
            _configuration = configuration;
            PatientList = GetAllPatients();
            CurrentPatient = PatientList.FirstOrDefault();
        }

        public PatientDirectoyViewModel(IPatientDirectoryConfigManager configuration, int patientID)
        {
            _configuration = configuration;
            PatientList = new List<PatientDirectoryModel>();

            if (patientID > 0)
            {
                CurrentPatient = GetPatient(patientID);
            }
            else
            {
                CurrentPatient = new PatientDirectoryModel();
            }
        }
        public List<PatientDirectoryModel> GetAllPatients()
        {
            return PatientDirectoryController.GetAllPatients(_configuration);
        }
        public PatientDirectoryModel GetPatient(int patientID)
        {
            return PatientDirectoryController.GetPatientByID(patientID, _configuration);
        }
    }
}
