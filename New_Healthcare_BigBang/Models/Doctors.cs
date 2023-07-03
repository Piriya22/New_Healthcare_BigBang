using System.ComponentModel.DataAnnotations;

namespace New_Healthcare_BigBang.Models
{
    public class Doctors
    {
        [Key]

        public int Doctor_Id { get; set; }

        public string? Doctor_Name { get; set; }

        public string? Doctor_Image { get; set; }

        public string? Gender { get; set; }

        public string? Doctor_Specialisation { get; set; }

        public int Doctor_Experience { get; set; }

        public string? Password { get; set; }

        public string? Status { get; set; }

        public string? Doc_name { get; set; }

        public string? Doc_password { get; set; }

        public ICollection<Patients>? Patients { get; set; }
    }
}
