using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Windows.Forms;

namespace Conexiones
{
    
    public  class conexion
    {
        public SqlConnection conectar()
        {
            SqlConnection scn;

            scn = new SqlConnection("SERVER=SERVER;database=DBFacturador;Integrated security=true");
            return scn;
        }
    }
}
