using System.ComponentModel.DataAnnotations;

namespace PracticaMVC.Models
{
    public class marcas
    {
        [Key]
        [Display(Name ="ID de Marca")]
        public int id_marcas { get; set; }

        [Display(Name = "Nombre de la Marca")]
        [Required(ErrorMessage = "El nombre de la marca NO es opcional")]
        public string? nombre_marca { get; set; }

        [Display(Name = "Estado")]
        public string? estados { get; set; }


    }
}
