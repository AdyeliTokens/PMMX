using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    public class AspNetUser
    {
        [Key]
        public string Id { get; set; }

        [StringLength(250)]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        [StringLength(250)]
        public string PasswordHash { get; set; }

        [StringLength(250)]
        public string SecurityStamp { get; set; }

        [StringLength(250)]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool? LockOutEndDateUTC { get; set; }
        public bool LockOutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        [StringLength(250)]
        public string UserName { get; set; }

        public ICollection<UsuariosPorPersona> PersonasConEsteUsuario { get; set; }

        public ICollection<AspNetRoles> Roles { get; set; }

    }
}
