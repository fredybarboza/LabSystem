using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioBogado.Estudios
{
    class Pedido
    {
        private string id;
        private string orden;
        private string ci;
        private string nombre;
        private string apellido;
        private string servicio;

        public string Id { get { return this.id; } set { this.id = value; } }
        public string Orden { get { return this.orden; } set { this.orden = value; } }
        public string Ci { get { return this.ci; } set { this.ci = value; } }
        public string Nombre { get { return this.nombre; } set { this.nombre = value; } }
        public string Apellido { get { return this.apellido; } set { this.apellido = value; } }
        public string Servicio { get { return this.servicio; } set { this.servicio = value; } }
    }
}
