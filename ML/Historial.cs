using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Historial
    {
        public int IdHistorial { get; set; }
        public DateTime HoraFecha { get; set; }
        public int Resultado { get; set; }
        public string Numero { get; set; }
        public ML.Usuario Usuario { get; set; }
        public List<object> Historiales { get; set; }
    }
}
