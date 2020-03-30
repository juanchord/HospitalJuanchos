using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace HospitalJuanchos.Models
{
    public class Pacientes
    {

        [Key]
        public int ID_Paciente{ get; set; }

        [Required]
        public string Nombre_Pac { get; set; }

        [Required]
        public string Cedula { get; set; }

        [Required]
        public bool Asegurado { get; set; }


    }
}