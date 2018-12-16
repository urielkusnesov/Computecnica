using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class User
    {
        public virtual int Id { get; set; }

        [Display(Name = "Nombre de usuario")]
        public virtual string Username { get; set; }

        [Display(Name = "Contraseña")]
        public virtual string Password { get; set; }

        public virtual string Token { get; set; }

        public virtual bool IsAdmin { get; set; }
    }
}
