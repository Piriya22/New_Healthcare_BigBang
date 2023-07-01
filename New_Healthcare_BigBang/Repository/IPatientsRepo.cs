using New_Healthcare_BigBang.Models;

namespace New_Healthcare_BigBang.Repository
{
    public interface IPatientsRepo
    {
        Task<IEnumerable<Patients>> GetAllPatients();
        Task<Patients> GetPatientById(int patientId);
        Task<Patients> PostPatient(Patients patient);
        Task<int> UpdatePatient(int id, Patients patient);
        Task<int> DeletePatient(int patientId);
    }
}
