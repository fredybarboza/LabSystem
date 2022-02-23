using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioBogado.Estudios
{
    class Servicios
    {
        private string id;
        private string nombre;

        public string Id { get { return this.id; } set { this.id = value; } }
        public string Nombre { get { return this.nombre; } set { this.nombre = value; } }
    }
}
