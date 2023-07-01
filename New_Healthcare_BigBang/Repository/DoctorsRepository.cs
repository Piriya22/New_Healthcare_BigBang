using Microsoft.AspNetCore.Mvc;
using New_Healthcare_BigBang.Models;

namespace New_Healthcare_BigBang.Repository
{
    public class DoctorsRepository : IDoctorsRepo
    {
        private readonly HealthcareContext hospitalContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DoctorsRepository(HealthcareContext con, IWebHostEnvironment webHostEnvironment)
        {
            hospitalContext = con;
            _webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<Doctors> GetDoctor()
        {
            return hospitalContext.Doctors.ToList();
        }

        public Doctors DoctorById(int Doctor_id)
        {
            try
            {
                return hospitalContext.Doctors.FirstOrDefault(x => x.Doctor_Id == Doctor_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Doctors> CreateDoctor([FromForm] Doctors doctor, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            doctor.Doctor_Image = fileName;

            hospitalContext.Doctors.Add(doctor);
            await hospitalContext.SaveChangesAsync();

            return doctor;
        }

        public async Task<Doctors> UpdateDoctor(int doctorid, Doctors doctor, IFormFile imageFile)
        {
            var existingDoctor = await hospitalContext.Doctors.FindAsync(doctorid);
            if (existingDoctor == null)
            {
                return null;
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Delete the old image file
                var oldFilePath = Path.Combine(uploadsFolder, existingDoctor.Doctor_Image);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }

                existingDoctor.Doctor_Image = fileName;
            }

            existingDoctor.Doctor_Name = doctor.Doctor_Name;
            existingDoctor.Doctor_Specialisation = doctor.Doctor_Specialisation;
            existingDoctor.Gender = doctor.Gender;
            existingDoctor.Doctor_Experience = doctor.Doctor_Experience;
            existingDoctor.Password = doctor.Password;
            await hospitalContext.SaveChangesAsync();

            return existingDoctor;
        }


        public Doctors DeleteDoctor(int doctorid)
        {
            try
            {
                Doctors doc = hospitalContext.Doctors.FirstOrDefault(x => x.Doctor_Id == doctorid);
                if (doc != null)
                {
                    hospitalContext.Doctors.Remove(doc);
                    hospitalContext.SaveChanges();
                    return doc;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Doctors DoctorbyId(int doctorid)
        {
            throw new NotImplementedException();
        }

    }
}
