using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalJuanchos.Models
{
    public class Citas
    {
        [Key]
        public int ID_Cita { get; set; }

        [Required]
        public int ID_Paciente { get; set; }
        [ForeignKey("ID_Paciente")]
        public Pacientes Paciente { get; set; }

        [Required]
        public string Fecha_De_Cita { get; set; }

        [Required]
        public string Hora_De_Cita { get; set; }

        [Required]
        public int ID_Medico { get; set; }
        [ForeignKey("ID_Medico")]
        public Medicos Medico { get; set; }
    }
}