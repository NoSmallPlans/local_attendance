using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _5051.Models
{
    public class CredentialsViewModel
    {
        [Required]
        [Display(Name = "Username")]

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
    }

 
}