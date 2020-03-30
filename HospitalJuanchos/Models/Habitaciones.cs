using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HospitalJuanchos.Models
{
    public class Habitaciones
    {
        [Key]
        public int ID_Habitacion { get; set; }

        [Required]
        public string Numero_Hab{ get; set; }

        [Required]
        public Tipo Tipo_Hab { get; set; }

        [Required]
        public int Precio { get; set; }



        public enum Tipo
        {
            Doble,Privada,Suite
        }
    }

}