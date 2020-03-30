using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalJuanchos.Models
{
    public class Medicos
    {
        [Key]
        public int ID_Medico { get; set; }

        [Required]
        public string Nombre_Med { get; set; }

        [Required]
        public string Exequatur { get; set; }

        [Required]
        public string Especialidad { get; set; }


    }
}