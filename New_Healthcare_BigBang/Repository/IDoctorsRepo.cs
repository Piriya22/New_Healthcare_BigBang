using Microsoft.AspNetCore.Mvc;
using New_Healthcare_BigBang.Models;
using New_Healthcare_BigBang.Models.DTO;
using System.Numerics;

namespace New_Healthcare_BigBang.Repository
{
    public interface IDoctorsRepo
    {
        public IEnumerable<Doctors> GetDoctor();

        public Doctors DoctorById(int Doctor_Id);
        Task<Doctors> CreateDoctor([FromForm] Doctors doctors, IFormFile imageFile);
        Task<Doctors> UpdateDoctor(int doctorid, Doctors doctor, IFormFile imageFile);
        public Doctors DeleteDoctor(int doctorid);

        Task<UpdateStatus> UpdateStatus(UpdateStatus status);
        Task<UpdateStatus> DeclineDoctorStatus(UpdateStatus status);

        public Task<ICollection<Doctors>> RequestedDoctor();
        public Task<ICollection<Doctors>> AcceptedDoctor();

    }
}
