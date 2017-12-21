using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.RespuestaGenerica
{
    public class RespuestaServicio<T> : IRespuestaServicio<T>
    {

        private bool ejecucionCorrecta;

        private string mensaje;

        public RespuestaServicio()
        {

            this.Mensaje = "";

            this.ejecucionCorrecta = true;

        }

        public bool EjecucionCorrecta

        {

            get

            {

                return this.ejecucionCorrecta;

            }

        }

        public string Mensaje

        {

            get

            {

                return this.mensaje;

            }


            set

            {

                if (value == null || value.Length == 0)
                {

                    this.ejecucionCorrecta = true;

                }
                else
                {

                    this.ejecucionCorrecta = false;

                }


                this.mensaje = value;

            }

        }

        public T Respuesta { get; set; }

    }

}
