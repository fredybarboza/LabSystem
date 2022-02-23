using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioBogado.Estudios
{
    class DetallePedido
    {
        private string id_pedido;
        private string id_analisis;
        private string nombre;

        public string IdPedido { get { return this.id_pedido; } set { this.id_pedido = value; } }
        public string IdAnalisis{ get { return this.id_analisis; } set { this.id_analisis = value; } }
        public string Nombre { get { return this.nombre; } set { this.nombre = value; } }

    }
}
