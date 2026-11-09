using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiónRegistro
{
    public class Gestion
    {
        private SQLiteConnection conexion;

        public void Iniciar()
        {
            string dataSource = "Data Source=registroProyecto";
            conexion = new SQLiteConnection(dataSource);
            conexion.Open();
        }

        public void Cerrar()
        {
            conexion.Close();
        }

        //Función para ver si la cuenta introducida ya existen
        public int EncontrarCuenta(string usuario, string contraseña)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM usuarios WHERE username = '" + usuario + "' AND password = '" + contraseña + "'";
            SQLiteDataAdapter adp = new SQLiteDataAdapter(sql, conexion);
            adp.Fill(dt);
            int resultado = dt.Rows.Count;

            if (resultado == 1)
            {
                return 0;  //El usuario y contraseña ya existen
            }

            else
            {
                return -1;  //No existe esta cuenta              
            }
        }

        //Función para encontrar un usuario por su nombre de usuario, ver si el nombre de usuario ya está cogido
        public int EncontrarUsername(string username)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM usuarios WHERE username = '" + username + "'";
            SQLiteDataAdapter adp = new SQLiteDataAdapter(sql, conexion);
            adp.Fill(dt);
            int resultado = dt.Rows.Count;

            if (resultado == 1)
            {
                return 0; //El usuario ya existe
            }

            else
            {
                return -1; //El usuario no se ha encontrado
            }
        }

        public int AddUsuario(string username, string password)
        {
            int resultado = EncontrarCuenta(username, password);
            if (resultado == 0)
            {
                return -1;  //Significa que la cuenta ya está registrada
            }

            int resultado2 = EncontrarUsername(username);

            if (resultado2 == 0)
            {
                return -2;  //Significa que que ya hay un usario registrado con ese nombre de usuario
            }

            //si es cualquier otro resultado pue se añade el usuario al registrarse

            else
            {
                string sql = "INSERT INTO usuarios VALUES ( '" + username + "', '" + password + "')";
                SQLiteCommand command = new SQLiteCommand(sql, conexion);
                int i = command.ExecuteNonQuery();

                if (i == 1)
                {
                    return 0;  //Añadido correctamente
                }

                else
                {
                    return -1;  //No se ha podido añadir el usuario
                }
            }

        }
    }
    
}
