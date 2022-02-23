using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LaboratorioBogado.Utils
{
    class ConnectionDB
    {
        MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder();

        public ConnectionDB()
        {
            this.InitConfig();
        }

        public void InitConfig()
        {
            csb.Server = "LocalHost";
            csb.Database = "laboratoriobogado";
            csb.UserID = "root";
            csb.Password = "5128575282828";
            csb.Pooling = false;
        }

        public long ExecuteSQL(string sql)
        {
            long lastId = 0;

            MySqlConnection con = new MySqlConnection(csb.ToString());

            try
            {
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = sql;
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.CommandTimeout = 0;
                lastId = cmd.LastInsertedId;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "MI ERROR 5");
            }
            finally
            {
                con.Close();
            }

            return lastId;
        }

        public MySqlDataReader ListSql(string sql)
        {
            MySqlDataReader mysqlDataReader = null;
            MySqlConnection con = new MySqlConnection(csb.ToString());
            try
            {
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandTimeout = 0;
                con.Open();

                mysqlDataReader = cmd.ExecuteReader();


            }
            catch (SystemException ex)
            {
                //con.Close();
                System.Windows.MessageBox.Show(ex.Message, "Mi Error 6");
            }
            finally
            {
                
            }



            return mysqlDataReader;
        }
    }
}
