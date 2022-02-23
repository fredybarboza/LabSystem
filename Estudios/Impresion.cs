using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioBogado.Estudios
{
    class Impresion
    {
        private string parametro;
        private string valor;
        private string unidad;
        private string valorreferencia;

        public string Parametro { get { return this.parametro; } set { this.parametro = value; } }
        public string Valor { get { return this.valor; } set { this.valor = value; } }
        public string Unidad { get { return this.unidad; } set { this.unidad = value; } }
        public string ValorReferencia { get { return this.valorreferencia; } set { this.valorreferencia = value; } }

    }
}
