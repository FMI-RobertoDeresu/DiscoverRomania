using System.ComponentModel.DataAnnotations;

namespace JRS.DR.ViewModels.Admin
{
    public class ObjectiveModel
    {
        public int? ObjectiveId { get; set; }

        [Required(ErrorMessage = "Limba obiectivului este camp obligatoriu!")]
        [Display(Name = "Limba")]
        public int? LanguageId { get; set; }

        [Required(ErrorMessage = "Categoria obiectivului este camp obligatoriu!")]
        [Display(Name = "Categoria")]
        public int? ObjectiveTypeId { get; set; }

        [Required(ErrorMessage = "Denumirea obiectivului este camp obligatoriu!")]
        [Display(Name = "Denumirea")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Descrierea obiectivului este camp obligatoriu!")]
        [Display(Name = "Descrierea")]
        public string Description { get; set; }

        [Display(Name = "Descriere ampla")]
        public string Html { get; set; }

        [Display(Name = "Selectati locatia")]
        public LocationModel Location { get; set; }
    }
}