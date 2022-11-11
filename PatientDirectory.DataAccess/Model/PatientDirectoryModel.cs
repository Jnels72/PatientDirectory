using System;
using System.Collections.Generic;
using System.Text;

namespace PatientDirectory.DataAccess.Models
{
    public class PatientDirectoryModel
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string PatientNumber { get; set; }
        public string PatientAddress { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string DoctorName { get; set; }
        public string DoctorNumber { get; set; }
    }
}
