﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace CapaDatos
{
    public class Conexiones_Base_Datos
    {
        static NpgsqlConnection conexion;
        static NpgsqlCommand cmd;

        public static void Conexion()
        {
            string servidor = "localhost";
            int puerto = 5432;
            string usuario = "postgres";
            string clave = "1414250816ma";
            string baseDatos = "gestion_vuelos";

            string cadenaConexion = "Server=" + servidor + ";" + "Port=" + puerto + ";" + "User Id=" + usuario + ";" + "Password=" + clave + ";" + "Database=" + baseDatos;
            conexion = new NpgsqlConnection(cadenaConexion);
        }

        public void InsertarDatosUsuarios(int cedula, string nombre, string contraseña, string tipo_usuario)
        {
            Conexion();
            conexion.Open();
            cmd = new NpgsqlCommand("INSERT usuarios (cedula, nombre, contraseña , tipo_usuario) VALUES ('" + cedula + "', '" + nombre + "', '" + contraseña + "', '" + tipo_usuario + "')", conexion);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
    }
}