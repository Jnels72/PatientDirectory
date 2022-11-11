using PatientDirectory.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PatientDirectory.DataAccess.Controllers
{
    public class PatientDirectoryController
    {
        public static int CreatePatient(int patientId, string patientName, string patientNumber, string patientAddress, string emergencyContact, string emergencyContactNumber, string doctorName, string doctorNumber, IPatientDirectoryConfigManager configManager)
        {
            string sqlConnectionString = configManager.PatientDirectoryConnection;
            int PatientId = 0;
            string insertSqlCommand = @"INSERT INTO PATIENTINFORMATION
                                                    (PATIENTNAME,
                                                     PATIENTNUMBER,
                                                     PATIENTADDRESS,
                                                     EMERGENCYCONTACT,
                                                     EMERGENCYCONTACTNUMBER,
                                                     DOCTORNAME,
                                                     DOCTORNUMBER)
                                           OUTPUT INSERTED.PATIENTID
                                           VALUES
                                                    (@PATIENTNAME,
                                                     @PATIENTNUMBER,
                                                     @PATIENTADDRESS,
                                                     @EMERGENCYCONTACT,
                                                     @EMERGENCYCONTACTNUMBER,
                                                     @DOCTORNAME,
                                                     @DOCTORNUMBER)";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(insertSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@PATIENTNAME", patientName));
                    sqlCommand.Parameters.Add(new SqlParameter("@PATIENTNUMBER", patientNumber));
                    sqlCommand.Parameters.Add(new SqlParameter("@PATIENTADDRESS", patientAddress));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMERGENCYCONTACT", emergencyContact));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMERGENCYCONTACTNUMBER", emergencyContactNumber));
                    sqlCommand.Parameters.Add(new SqlParameter("@DOCTORNAME", doctorName));
                    sqlCommand.Parameters.Add(new SqlParameter("@DOCTORNUMBER", doctorNumber));

                    sqlCommand.Connection.Open();
                    PatientId = (int)sqlCommand.ExecuteScalar();
                    sqlCommand.Connection.Close();
                }
            }
            return PatientId;
        }

        public static int UpdatePatient(int patientId, string patientName, string patientNumber, string patientAddress, string emergencyContact, string emergencyContactNumber, string doctorName, string doctorNumber, IPatientDirectoryConfigManager configManager)
        {
            string sqlConnectionString = configManager.PatientDirectoryConnection;
            string insertSqlCommand = @"UPDATE PATIENTINFORMATION
                                        SET PATIENTNAME = @PATIENTNAME,
                                            PATIENTNUMBER = @PATIENTNUMBER,
                                            PATIENTADDRESS = @PATIENTADDRESS,
                                            EMERGENCYCONTACT = @EMERGENCYCONTACT,
                                            EMERGENCYCONTACTNUMBER = @EMERGENCYCONTACTNUMBER,
                                            DOCTORNAME = @DOCTORNAME,
                                            DOCTORNUMBER = @DOCTORNUMBER
                                            WHERE PATIENTID = @PATIENTID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(insertSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@PATIENTID", patientId));
                    sqlCommand.Parameters.Add(new SqlParameter("@PATIENTNAME", patientName));
                    sqlCommand.Parameters.Add(new SqlParameter("@PATIENTNUMBER", patientNumber));
                    sqlCommand.Parameters.Add(new SqlParameter("@PATIENTADDRESS", patientAddress));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMERGENCYCONTACT", emergencyContact));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMERGENCYCONTACTNUMBER", emergencyContactNumber));
                    sqlCommand.Parameters.Add(new SqlParameter("@DOCTORNAME", doctorName));
                    sqlCommand.Parameters.Add(new SqlParameter("@DOCTORNUMBER", doctorNumber));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return  patientId;
        }
        public static bool DeletePatient(int patientID, IPatientDirectoryConfigManager configManager)
        {
            string sqlConnectionString = configManager.PatientDirectoryConnection;
            string deleteSqlCommand = @"DELETE FROM PATIENTINFORMATION WHERE PATIENTID = @PATIENTID";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(deleteSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@PATIENTID", patientID));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                return true;
            }
        }
        public static List<PatientDirectoryModel>? GetAllPatients(IPatientDirectoryConfigManager configManager)
        {
            string sqlConnectionString = configManager.PatientDirectoryConnection;
            List<PatientDirectoryModel> patientList = new List<PatientDirectoryModel>();

            string querySql = "SELECT * FROM PATIENTINFORMATION ORDER BY PATIENTID DESC";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                {
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        using (DataTable dataTable = new DataTable())
                        {
                            sqlDataAdapter.Fill(dataTable);

                            foreach (DataRow dataRow in dataTable.Rows)
                            {
                                PatientDirectoryModel patientDirectoryModel = new PatientDirectoryModel();

                                patientDirectoryModel.PatientID = Convert.ToInt32(dataRow["PATIENTID"]);
                                patientDirectoryModel.PatientName = dataRow["PATIENTNAME"]?.ToString() ?? "";
                                patientDirectoryModel.PatientNumber = dataRow["PATIENTNUMBER"]?.ToString() ?? "";
                                patientDirectoryModel.PatientAddress = dataRow["PATIENTADDRESS"]?.ToString() ?? "";
                                patientDirectoryModel.EmergencyContact = dataRow["EMERGENCYCONTACT"]?.ToString() ?? "";
                                patientDirectoryModel.EmergencyContactNumber = dataRow["EMERGENCYCONTACTNUMBER"]?.ToString() ?? "";
                                patientDirectoryModel.DoctorName = dataRow["DOCTORNAME"]?.ToString() ?? "";
                                patientDirectoryModel.DoctorNumber = dataRow["DOCTORNUMBER"]?.ToString() ?? "";

                                patientList.Add(patientDirectoryModel);
                            }
                        }
                    }
                }
            }
            return patientList;
        }
        public static PatientDirectoryModel? GetPatientByID(int PatientID, IPatientDirectoryConfigManager configManager)
        {
            string sqlConnectionString = configManager.PatientDirectoryConnection;
            PatientDirectoryModel patient = new PatientDirectoryModel();

            string querySql = "SELECT * FROM PATIENTINFORMATION WHERE PATIENTID = @PATIENTID";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@PATIENTID", PatientID));
                    sqlConnection.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            patient.PatientID = Convert.ToInt32(reader["PATIENTID"]);
                            patient.PatientName = reader["PATIENTNAME"]?.ToString() ?? "";
                            patient.PatientNumber = reader["PATIENTNUMBER"]?.ToString() ?? "";
                            patient.PatientAddress = reader["PATIENTADDRESS"]?.ToString() ?? "";
                            patient.EmergencyContact = reader["EMERGENCYCONTACT"]?.ToString() ?? "";
                            patient.EmergencyContactNumber = reader["EMERGENCYCONTACTNUMBER"]?.ToString() ?? "";
                            patient.DoctorName = reader["DOCTORNAME"]?.ToString() ?? "";
                            patient.DoctorNumber = reader["DOCTORNUMBER"]?.ToString() ?? "";
                        }
                        else
                        {
                            throw new Exception("No rows found.");
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return patient;
        }
    }
}