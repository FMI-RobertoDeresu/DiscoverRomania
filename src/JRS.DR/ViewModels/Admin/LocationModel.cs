using System.ComponentModel.DataAnnotations;

namespace JRS.DR.ViewModels.Admin
{
    public class LocationModel
    {
        [Required(ErrorMessage = "Va rugam sa alegeti locatia - X!")]
        public decimal? X { get; set; }

        [Required(ErrorMessage = "Va rugam sa alegeti locatia - Y!")]
        public decimal? Y { get; set; }
    }
}