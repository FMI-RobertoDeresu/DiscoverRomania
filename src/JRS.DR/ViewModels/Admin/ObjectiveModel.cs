using System.ComponentModel.DataAnnotations;

namespace JRS.DR.ViewModels.Admin
{
    public class ObjectiveModel
    {
        public int? ObjectiveId { get; set; }

        [Required(ErrorMessage = "Limba obiectivului este camp obligatoriu!")]
        [Display(Name = "Limba obiectivului")]
        public int? LanguageId { get; set; }

        [Required(ErrorMessage = "Categoria obiectivului este camp obligatoriu!")]
        [Display(Name = "Categoria obiectivului")]
        public int? ObjectiveTypeId { get; set; }

        [Required(ErrorMessage = "Denumirea obiectivului este camp obligatoriu!")]
        [Display(Name = "Denumirea obiectivului")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Descrierea obiectivului este camp obligatoriu!")]
        [Display(Name = "Descrierea obiectivului")]
        public string Description { get; set; }

        [Required(ErrorMessage = "URL Imagine obiectiv este camp obligatoriu!")]
        [Display(Name = "URL Imagine obiectiv")]
        public string Picture { get; set; }

        [Display(Name = "Descriere ampla")]
        public string Html { get; set; }

        [Display(Name = "Locatie")]
        public LocationModel Location { get; set; }
    }
}