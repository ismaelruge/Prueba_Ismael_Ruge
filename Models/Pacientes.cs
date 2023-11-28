using System.ComponentModel.DataAnnotations;

namespace Prueba_Ismael_Ruge.Models
{
    public class Pacientes
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(2)]
        public string TipoDocumento { get; set; }
        public double NumeroDocumento { get; set; }

        [Required]
        private string nombres;
        public string Nombres
        {
            get { return nombres; }
            set { nombres = value.Trim().ToUpper(); }
        }

        [Required]
        private string apellidos;
        public string Apellidos
        {
            get { return apellidos; }
            set { apellidos = value.Trim().ToUpper(); }
        }


        private string correoElectronico;
        public string CorreoElectronico
        {
            get { return correoElectronico; }
            set { correoElectronico = value.Trim().ToLower(); }
        }

        public double Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool EstadoAfiliacion { get; set; }
    }
}
