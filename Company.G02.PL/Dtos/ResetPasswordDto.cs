using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Password Is Required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password Is Required!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Confirm Password dose not match Password")]
        public string NewPassword { get; set; }





    }
}
