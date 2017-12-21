using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.RespuestaGenerica;
using System.Collections.Generic;
using System.Linq;

namespace PMMX.Seguridad.Servicios
{
    public class PersonaServicio
    {
        private PMMXContext db = new PMMXContext();

        public IEnumerable<Persona> GetPersonas()
        {
            var personas = db.Personas.Select(x => x);

            return personas;
        }

        public IRespuestaServicio<Persona> GetPersona(string userId)
        {
            IRespuestaServicio<Persona> respuesta = new RespuestaServicio<Persona>();
            var persona = db.Personas.Where(x=> x.Users.FirstOrDefault().IdAspNetUser == userId ).Select(x => x).FirstOrDefault();
            if (persona == null) {
                respuesta.Mensaje = "No se encontraron registros";
            }
            else {
                respuesta.Respuesta = persona;
            }

            return respuesta;
        }
    }
}
