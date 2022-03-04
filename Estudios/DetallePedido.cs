using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaboratorioBogado.Utils;
using MySql.Data.MySqlClient;
using LaboratorioBogado.Estudios;
using System.Collections.ObjectModel;


namespace LaboratorioBogado.Estudios
{
    class DetallePedido
    {
        public string id_pedido;
        public string id_analisis;
        public string nombre;

        public string IdPedido { get { return this.id_pedido; } set { this.id_pedido = value; } }
        public string IdAnalisis{ get { return this.id_analisis; } set { this.id_analisis = value; } }
        public string Nombre { get { return this.nombre; } set { this.nombre = value; } }

    }
}
