using System.ComponentModel.DataAnnotations;

namespace PracticaMVC.Models
{
    public class usuarios
    {
        [Key]
        public int id_usuario { get; set; }
        public string? nombre { get; set; }
        public string? documento { get; set; }
        public string? carnet { get; set; }
        public int? carrera_id { get; set; }
        public string? email { get; set; }
        public string? contrasenia { get; set; }
        public string? tipo_usuario { get; set; }
        public string? bloqueado { get; set; }
        public DateTime? ultimo_login { get; set; }
        public string? activo { get; set; }

    }
}
