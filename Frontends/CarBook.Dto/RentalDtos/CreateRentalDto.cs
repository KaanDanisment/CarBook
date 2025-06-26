using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.RentalDtos
{
    public class CreateRentalDto
    {
        public int CarId { get; set; }

        [Required(ErrorMessage ="Lütfen adınızı giriniz")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage ="Lütfen mail adresinizi giriniz")]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage ="Lütfen telefon numaranızı giriniz")]
        public string CustomerPhoneNumber { get; set; }

        [Required(ErrorMessage = "Lütfen kiralama tarihini giriniz.")]
        public DateOnly RentDate { get; set; }

        [Required(ErrorMessage = "Lütfen iade tarihini giriniz.")]
        public DateOnly ReturnDate { get; set; }

        [Required(ErrorMessage = "Lütfen kiralama saatini giriniz.")]
        public TimeOnly RentTime { get; set; }

        [Required(ErrorMessage = "Lütfen iade saatini giriniz.")]
        public TimeOnly ReturnTime { get; set; }

        [Required(ErrorMessage = "Lütfen kiralama konumunu giriniz.")]
        [Range(1, int.MaxValue, ErrorMessage = "Konum bilgisi olmadan kiralama yapılamaz. Lütfen daha sonra tekrar deneyin")]
        public int PickupLocationId { get; set; }

        [Required(ErrorMessage = "Lütfen iade konumunu giriniz.")]
        [Range(1, int.MaxValue, ErrorMessage = "Konum bilgisi olmadan kiralama yapılamaz. Lütfen daha sonra tekrar deneyin")]
        public int DropoffLocationId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
