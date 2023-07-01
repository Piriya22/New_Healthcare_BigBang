using Microsoft.AspNetCore.Mvc;
using New_Healthcare_BigBang.Models;

namespace New_Healthcare_BigBang.Repository
{
    public interface IDoctorsRepo
    {
        public IEnumerable<Doctors> GetDoctor();

        public Doctors DoctorById(int Doctor_Id);
        Task<Doctors> CreateDoctor([FromForm] Doctors doctors, IFormFile imageFile);
        Task<Doctors> UpdateDoctor(int doctorid, Doctors doctor, IFormFile imageFile);
        public Doctors DeleteDoctor(int doctorid);

    }
}
