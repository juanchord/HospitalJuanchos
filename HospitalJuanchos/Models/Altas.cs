using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalJuanchos.Models
{
    public class Altas
    {

        [Key]
        public int ID_ALtas { get; set; }

        public string Nombre_De_Paciente { get; set; }

        public string Fecha_De_Ingreso { get; set; }

        public string Fecha_De_Salida { get; set; }

        public string Habitacion { get; set; }

        public double Monto { get; set; }

        public int ID_Ingreso { get; set; }
        [ForeignKey("ID_Ingreso")]
        public Ingresos Ingresos { get; set; }

        public Pacientes Paciente { get; set; }

        public Habitaciones Habitaciones { get; set; }
    }
}