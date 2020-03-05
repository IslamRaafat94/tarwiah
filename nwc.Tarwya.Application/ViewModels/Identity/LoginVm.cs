using System.ComponentModel.DataAnnotations;

namespace nwc.Tarwya.Application.ViewModels.Identity
{
    public class LoginVm
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
