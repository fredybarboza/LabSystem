using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioBogado.Estudios
{
    class Hemograma
    {
        private string fecha;
        private string id;
        private string hemoglobina;
        private string hematocrito;
        private string gr;
        private string gb;
        private string plaquetas;
        private string eritro1h;
        private string eritro2h;
        private string neutrofilos;
        private string linfocitos;
        private string monocitos;
        private string eosinofilos;
        private string basofilos;
        private string observacion;

        public string Fecha { get { return this.fecha; } set { this.fecha = value; } }
        public string Id { get { return this.id; } set { this.id = value; } }

        public string Hemoglobina { get { return this.hemoglobina; } set { this.hemoglobina = value; } }

        public string Hematocrito { get { return this.hematocrito; } set { this.hematocrito = value; } }

        public string Gr { get { return this.gr; } set { this.gr = value; } }

        public string Gb { get { return this.gb; } set { this.gb = value; } }

        public string Plaquetas { get { return this.plaquetas; } set { this.plaquetas = value; } }

        public string Eritro1h { get { return this.eritro1h; }set { this.eritro1h = value; } }

        public string Eritro2h { get { return this.eritro2h; } set { this.eritro2h = value; } }

        public string Neutrofilos { get { return this.neutrofilos; } set { this.neutrofilos = value; } }

        public string Linfocitos { get { return this.linfocitos; } set { this.linfocitos = value; } }

        public string Monocitos { get { return this.monocitos; } set { this.monocitos = value; } }

        public string Eosinofilos { get { return this.eosinofilos; } set { this.eosinofilos = value; } }

        public string Basofilos { get { return this.basofilos; } set { this.basofilos = value; } }

        public string Observacion { get { return this.observacion; } set { this.observacion = value; } }


    }
}
