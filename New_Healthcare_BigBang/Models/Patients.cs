using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace New_Healthcare_BigBang.Models
{
    public class Patients
    {
        [Key]

        public int Patient_Id { get; set; }

        public int Doctor_Id { get; set; }
        [ForeignKey("Doctor_Id")]

        public Doctors? Doctors { get; set; }

        public string? Patient_Name { get; set; }

        public int Patient_Age { get; set; }

        public string? Gender { get; set; }

        public string? Medical_Treatment { get; set; }

        public string? Password { get; set; }

        public long Phone_Number { get; set; }

        public string? Patient_Address { get; set; }
    }
}
