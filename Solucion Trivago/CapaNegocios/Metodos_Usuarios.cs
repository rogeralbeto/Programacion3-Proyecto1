﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace CapaNegocios
{
    public class Metodos_Usuarios
    {

        static NpgsqlConnection conexion;
        static NpgsqlCommand cmd;

        public static void Conexion()
        {
            string servidor = "localhost";
            int puerto = 5432;
            string usuario = "postgres";
            string claveAnthonny = "1414250816ma";
            string claveRoger = "Saborio17";

            string baseDatos = "gestion_vuelos";

            string cadenaConexion = "Server=" + servidor + ";" + "Port=" + puerto + ";" + "User Id=" + usuario + ";" + "Password=" + claveRoger + ";" + "Database=" + baseDatos;
            conexion = new NpgsqlConnection(cadenaConexion);

            if (conexion != null)
            {

                Console.WriteLine("Conexion con la DB nombre : " + baseDatos + " , Exitosa!!");
            }
            else
            {

                Console.WriteLine("Error en la conexion con la DB");
            }
        }




        //Este metodo llena el comboBox de nombres de paises en la seccion de alojamiento
        public void LlenarNombresPaises(ComboBox nombre_pais)
        {

            try
            {
                Conexion();
                conexion.Open();
                List<String> lista = new List<String>();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT nombre FROM paises", conexion);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        nombre_pais.Items.Add(dr.GetString(0));

                    }
                }
                conexion.Close();


            }
            catch (Exception error)
            {
                Console.WriteLine(error);

            }

        }



        //Este metodo llena el comboBox de nombres de lugares en la seccion de alojamiento
        public void LlenarNombresLugares(ComboBox nombre_lugar)
        {

            try
            {
                Conexion();
                conexion.Open();
                List<String> lista = new List<String>();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT nombre FROM lugares", conexion);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        nombre_lugar.Items.Add(dr.GetString(0));

                    }
                }
                conexion.Close();


            }
            catch (Exception error)
            {
                Console.WriteLine(error);

            }

        }


        //Este metodo llena el comboBox de nombres de paises de origen en la seccion de vuelos
        public void LlenarNombresPaisOrigen(ComboBox nombre_pais)
        {

            try
            {
                Conexion();
                conexion.Open();
                List<String> lista = new List<String>();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT pais_origen FROM rutas", conexion);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        nombre_pais.Items.Add(dr.GetString(0));

                    }
                }
                conexion.Close();


            }
            catch (Exception error)
            {
                Console.WriteLine(error);

            }

        }

        //Este metodo llena el comboBox de nombres de paises de destino en la seccion de vuelos
        public void LlenarNombresPaisDestino(ComboBox nombre_pais)
        {

            try
            {
                Conexion();
                conexion.Open();
                List<String> lista = new List<String>();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT pais_destino FROM rutas", conexion);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        nombre_pais.Items.Add(dr.GetString(0));

                    }
                }
                conexion.Close();


            }
            catch (Exception error)
            {
                Console.WriteLine(error);

            }

        }




    

        //Metodo que llena el combo de identificadores de vehiculos , en la ventana de seccion de vuelo
        public void LlenarVehiculos(ComboBox placa)
        {

            try
            {
                Conexion();
                conexion.Open();
                List<String> lista = new List<String>();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT placa FROM vehiculos", conexion);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        placa.Items.Add(dr.GetInt64(0));
                    }
                }
                conexion.Close();
                
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }

        }


        //Este metodo muestra la informacion del vehiculo seleccionado en el combobox por el usuario
        public void MostrarInformacionVehiculos(ComboBox placa, TextBox marca, TextBox modelo, TextBox tipo_vehiculo, TextBox precio)
        {

            try
            {
                Conexion();
                conexion.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT marca, modelo , tipo_vehiculo , precio FROM vehiculos WHERE placa = '" + placa.SelectedItem.ToString() + "'", conexion);
                NpgsqlDataReader leer = cmd.ExecuteReader();
                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        marca.Text = leer.GetString(0);
                        modelo.Text = leer.GetString(1);
                        tipo_vehiculo.Text = leer.GetString(2);
                        precio.Text = leer.GetDouble(3).ToString();

                    }
                    conexion.Close();
                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }

        }

        public void LlenarDtVistaPreliminarHotelsPaisASC(DataGridView agregar_hotels_pais, string pais)
        {
            try
            {
                Conexion();
                conexion.Open();
                DataSet dataset = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT  identificador , nombre, lugar , habitaciones , tarifa_hotel,foto FROM hotel WHERE pais = '" + pais + "' ORDER BY tarifa_hotel ASC", conexion);
                adapter.Fill(dataset, "rutas");
                agregar_hotels_pais.DataSource = dataset.Tables[0];
                agregar_hotels_pais.Columns[0].HeaderCell.Value = "identificador";
                agregar_hotels_pais.Columns[1].HeaderCell.Value = "nombre";
                agregar_hotels_pais.Columns[2].HeaderCell.Value = "lugar";
                agregar_hotels_pais.Columns[3].HeaderCell.Value = "habitaciones";
                agregar_hotels_pais.Columns[4].HeaderCell.Value = "tarifa_hotel";
                agregar_hotels_pais.Columns[5].HeaderCell.Value = "foto";
                //DataGridViewImageColumn imag = new DataGridViewImageColumn();
                //imag.HeaderText = "Foto_Hotel";
                //imag.Name = "Foto_Hotel";
                //dtgVuelosASC.Columns.Add(imag);

                conexion.Close();

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public void LlenarDtVistaPreliminarHotelsPaisRanking(DataGridView agregar_hotels_pais, string pais)
        {
            try
            {
                Conexion();
                conexion.Open();
                DataSet dataset = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT  identificador , nombre, lugar , habitaciones , tarifa_hotel,foto FROM hotel JOIN ranking_hoteles ON ranking_hoteles.id_hotel=hotel.identificador WHERE pais = '" + pais + "' ORDER BY calificacion ASC", conexion);
                adapter.Fill(dataset, "rutas");
                agregar_hotels_pais.DataSource = dataset.Tables[0];
                agregar_hotels_pais.Columns[0].HeaderCell.Value = "identificador";
                agregar_hotels_pais.Columns[1].HeaderCell.Value = "nombre";
                agregar_hotels_pais.Columns[2].HeaderCell.Value = "lugar";
                agregar_hotels_pais.Columns[3].HeaderCell.Value = "habitaciones";
                agregar_hotels_pais.Columns[4].HeaderCell.Value = "tarifa_hotel";
                agregar_hotels_pais.Columns[5].HeaderCell.Value = "foto";
                //DataGridViewImageColumn imag = new DataGridViewImageColumn();
                //imag.HeaderText = "Foto_Hotel";
                //imag.Name = "Foto_Hotel";
                //dtgVuelosASC.Columns.Add(imag);

                conexion.Close();

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public ArrayList CargarFotosHotelesPaisASC(string pais) {

            ArrayList v = new ArrayList();

            Conexion();
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT  foto FROM hotel WHERE pais = '" + pais + "' ORDER BY tarifa_hotel ASC", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                while (lectorConsulta.Read())
                {
                    //cedula, nombre y tipo de usuario
                    v.Add(lectorConsulta.GetString(0));
                }
            }
            conexion.Close();

            return v;
        }

        public ArrayList CargarFotosHotelesPaisDESC(string pais)
        {

            ArrayList v = new ArrayList();

            Conexion();
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT  foto FROM hotel WHERE pais = '" + pais + "' ORDER BY tarifa_hotel DESC", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                while (lectorConsulta.Read())
                {
                    //cedula, nombre y tipo de usuario
                    v.Add(lectorConsulta.GetString(0));
                }
            }
            conexion.Close();

            return v;
        }


        public void LlenarDtVistaPreliminarHotelsPaisDESC(DataGridView agregar_hotels_pais, string pais)
        {
            try
            {
                Conexion();
                conexion.Open();
                DataSet dataset = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT  identificador , nombre, lugar , habitaciones , tarifa_hotel,foto FROM hotel WHERE pais = '" + pais + "' ORDER BY tarifa_hotel DESC", conexion);
                adapter.Fill(dataset, "rutas");
                agregar_hotels_pais.DataSource = dataset.Tables[0];
                agregar_hotels_pais.Columns[0].HeaderCell.Value = "identificador";
                agregar_hotels_pais.Columns[1].HeaderCell.Value = "nombre";
                agregar_hotels_pais.Columns[2].HeaderCell.Value = "lugar";
                agregar_hotels_pais.Columns[3].HeaderCell.Value = "habitaciones";
                agregar_hotels_pais.Columns[4].HeaderCell.Value = "tarifa_hotel";
                agregar_hotels_pais.Columns[5].HeaderCell.Value = "foto";
                //DataGridViewImageColumn imag = new DataGridViewImageColumn();
                //imag.HeaderText = "Foto_Hotel";
                //imag.Name = "Foto_Hotel";
                //dtgVuelosASC.Columns.Add(imag);
                conexion.Close();

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public void LlenarDtVistaPreliminarHotelsCiudadASC(DataGridView agregar_hotels_ciudad, string lugar)
        {
            try
            {
                Conexion();
                conexion.Open();
                DataSet dataset = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT  identificador , nombre, habitaciones , tarifa_hotel,foto FROM hotel WHERE lugar = '" + lugar + "' ORDER BY tarifa_hotel ASC", conexion);
                adapter.Fill(dataset, "hotel");
                agregar_hotels_ciudad.DataSource = dataset.Tables[0];
                agregar_hotels_ciudad.Columns[0].HeaderCell.Value = "identificador";
                agregar_hotels_ciudad.Columns[1].HeaderCell.Value = "nombre";
                agregar_hotels_ciudad.Columns[2].HeaderCell.Value = "habitaciones";
                agregar_hotels_ciudad.Columns[3].HeaderCell.Value = "tarifa_hotel";
                agregar_hotels_ciudad.Columns[4].HeaderCell.Value = "foto";

                conexion.Close();

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public void LlenarDtVistaPreliminarHotelsCiudadDESC(DataGridView agregar_hotels_ciudad, string lugar)
        {
            try
            {
                Conexion();
                conexion.Open();
                DataSet dataset = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT  identificador , nombre, habitaciones , tarifa_hotel , foto FROM hotel WHERE lugar = '" + lugar + "' ORDER BY tarifa_hotel DESC", conexion);
                adapter.Fill(dataset, "hotel");
                agregar_hotels_ciudad.DataSource = dataset.Tables[0];
                agregar_hotels_ciudad.Columns[0].HeaderCell.Value = "identificador";
                agregar_hotels_ciudad.Columns[1].HeaderCell.Value = "nombre";
                agregar_hotels_ciudad.Columns[2].HeaderCell.Value = "habitaciones";
                agregar_hotels_ciudad.Columns[3].HeaderCell.Value = "tarifa_hotel";
                agregar_hotels_ciudad.Columns[4].HeaderCell.Value = "foto";
                conexion.Close();

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public void LlenarDtVistaPreliminarBusquedaHotel(DataGridView agregar_busqueda_hotel, string nombreHotel)
        {
            try
            {
                Conexion();
                conexion.Open();
                DataSet dataset = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT  identificador , nombre, pais , lugar , habitaciones , tarifa_hotel,foto FROM hotel WHERE nombre = '" + nombreHotel + "'", conexion);
                adapter.Fill(dataset, "hotel");
                agregar_busqueda_hotel.DataSource = dataset.Tables[0]; 
                agregar_busqueda_hotel.Columns[0].HeaderCell.Value = "identificador";
                agregar_busqueda_hotel.Columns[1].HeaderCell.Value = "nombre";
                agregar_busqueda_hotel.Columns[2].HeaderCell.Value = "pais";
                agregar_busqueda_hotel.Columns[3].HeaderCell.Value = "lugar";
                agregar_busqueda_hotel.Columns[4].HeaderCell.Value = "habitaciones";
                agregar_busqueda_hotel.Columns[5].HeaderCell.Value = "tarifa_hotel";
                agregar_busqueda_hotel.Columns[6].HeaderCell.Value = "foto";
                conexion.Close();

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public bool BanderaHotel(String nombreHotel)
        {
            bool bandera;

            Conexion();
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT * FROM hotel WHERE nombre='" + nombreHotel + "'", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                bandera = true;
            }
            else {

                bandera = false;
            }
            conexion.Close();

            return bandera;
        }

        public void InsertarDatosReservacionHotel(int idHotel,string fechaLlegada,string fechaSalida, int cantidadHabitaciones, int cantidadPersonas, int idCliente, int cantidadPersonasMayores, int cantidadPersonasMenores,double precioTotalHospedaje)
        {
            try
            {

                Conexion();
                conexion.Open();
                cmd = new NpgsqlCommand("INSERT INTO informacion_reservaciones_hoteles (id_hotel,fecha_llegada,fecha_salida,cantidad_habitaciones,cantidad_personas,id_cliente,personas_mayores,personas_menores,precio_total_reservacion) VALUES ('" + idHotel+"','"+fechaLlegada+"','"+fechaSalida+"','"+cantidadHabitaciones+"','"+cantidadPersonas+"','"+idCliente+"','"+cantidadPersonasMayores+"','"+cantidadPersonasMenores+"','"+precioTotalHospedaje+"')", conexion);
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Error--- \n" + error);
            }

        }

        public void InsertarDatosReservacionHotelDesdePrereservacion(int idHotel, DateTime fechaLlegada, DateTime fechaSalida, int cantidadHabitaciones, int cantidadPersonas, int idCliente, int cantidadPersonasMayores, int cantidadPersonasMenores, double precioTotalHospedaje)
        {
            try
            {

                Conexion();
                conexion.Open();
                cmd = new NpgsqlCommand("INSERT INTO informacion_reservaciones_hoteles (id_hotel,fecha_llegada,fecha_salida,cantidad_habitaciones,cantidad_personas,id_cliente,personas_mayores,personas_menores,precio_total_reservacion) VALUES ('" + idHotel + "','" + fechaLlegada + "','" + fechaSalida + "','" + cantidadHabitaciones + "','" + cantidadPersonas + "','" + idCliente + "','" + cantidadPersonasMayores + "','" + cantidadPersonasMenores + "','" + precioTotalHospedaje + "')", conexion);
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Error--- \n" + error);
            }

        }

        public void InsertarDatosPreReservacionHotel(int idHotel, string fechaLlegada, string fechaSalida, int cantidadHabitaciones, int cantidadPersonas, int idCliente, int cantidadPersonasMayores, int cantidadPersonasMenores, double precioTotalHospedaje)
        {
            try
            {

                Conexion();
                conexion.Open();
                cmd = new NpgsqlCommand("INSERT INTO informacion_prereservaciones_hoteles (id_hotel,fecha_llegada,fecha_salida,cantidad_habitaciones,cantidad_personas,id_cliente,personas_mayores,personas_menores,precio_total_prereservacion) VALUES ('" + idHotel + "','" + fechaLlegada + "','" + fechaSalida + "','" + cantidadHabitaciones + "','" + cantidadPersonas + "','" + idCliente + "','" + cantidadPersonasMayores + "','" + cantidadPersonasMenores + "','" + precioTotalHospedaje + "')", conexion);
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Error--- \n" + error);
            }

        }
        

        public void LlenarDtVistaPrereservacionesHotelPais(DataGridView agregar_busqueda_hotel, int cedulaUsuario)
        {
            try
            {
                Conexion();
                conexion.Open();
                DataSet dataset = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT  id_hotel, fecha_llegada, fecha_salida,cantidad_habitaciones, cantidad_personas,id_cliente,personas_menores,personas_mayores,precio_total_prereservacion FROM informacion_prereservaciones_hoteles WHERE id_cliente = '" + cedulaUsuario + "'", conexion);
                adapter.Fill(dataset, "rutas");
                agregar_busqueda_hotel.DataSource = dataset.Tables[0];
                agregar_busqueda_hotel.Columns[0].HeaderCell.Value = "id_hotel";
                agregar_busqueda_hotel.Columns[1].HeaderCell.Value = "fecha_llegada";
                agregar_busqueda_hotel.Columns[2].HeaderCell.Value = "fecha_salida";
                agregar_busqueda_hotel.Columns[3].HeaderCell.Value = "cantidad_habitaciones";
                agregar_busqueda_hotel.Columns[4].HeaderCell.Value = "cantidad_personas";
                agregar_busqueda_hotel.Columns[5].HeaderCell.Value = "id_cliente";
                agregar_busqueda_hotel.Columns[6].HeaderCell.Value = "personas_menores";
                agregar_busqueda_hotel.Columns[7].HeaderCell.Value = "personas_mayores";
                agregar_busqueda_hotel.Columns[8].HeaderCell.Value = "precio_total_prereservacion ";
                conexion.Close();

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }
        

        public void EliminarDatosPreReservacionHotel(int idHotel, int cantidadHabitaciones, int cantidadPersonas, int idCliente)
        {
            try
            {
                Conexion();
                conexion.Open();
                cmd = new NpgsqlCommand("DELETE FROM informacion_prereservaciones_hoteles WHERE id_hotel='"+idHotel+"' AND cantidad_habitaciones='"+cantidadHabitaciones+"' AND cantidad_personas='"+cantidadPersonas+"' AND id_cliente='"+idCliente+"'", conexion);
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception error)
            {   MessageBox.Show("Error--- \n" + error);
            }
        }

        public int DiferenciaDiasFechas(String fechaLlegada,String fechaSalida) {

            int dias=0;
            DateTime f1 = DateTime.Parse(fechaLlegada);
            DateTime f2 = DateTime.Parse(fechaSalida);
            TimeSpan ts = f2 - f1;
            dias = ts.Days;
            if (dias < 0)
            {
                dias = 0;
            }          
            return dias;
        }

        public void ActualizarCantidadHabitacionesHotel(int nuevaCantidad,int  idHotel)
        {
            try
            {
                Conexion();
                conexion.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("UPDATE hotel SET habitaciones='"+nuevaCantidad+"' WHERE identificador='"+idHotel+"'", conexion);
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error--- \n" + error);
            }
        }

        //Este metodo llena el comboBox de nombres de paises en la seccion de alojamiento
        public void LlenarHotelesPaisDestino(ComboBox hotelesPais,string pais)
        {

            try
            {
                Conexion();
                conexion.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT nombre FROM hotel WHERE pais='"+pais+"'", conexion);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        hotelesPais.Items.Add(dr.GetString(0));

                    }
                }
                conexion.Close();


            }
            catch (Exception error)
            {
                Console.WriteLine(error);

            }
        }
        
        public bool BanderaRutaSinEscala(String paisOrigen,String  paisDestino)
        {
            bool bandera;

            Conexion();
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT pais_origen , pais_destino FROM rutas WHERE pais_origen='" + paisOrigen + "' AND pais_destino='"+paisDestino+"'", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                bandera = true;
            }
            else
            {

                bandera = false;
            }
            conexion.Close();

            return bandera;
        }

        public bool BanderaRutaConEscalas(String paisDestino)
        {
            bool bandera;

            Conexion();
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT pais_destino FROM rutas WHERE pais_destino='" + paisDestino + "'", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                bandera = true;
            }
            else
            {

                bandera = false;
            }
            conexion.Close();

            return bandera;
        }

        public int ActualCantidadHabitaciones(int idHotel)
        {
            int dias = 0;
            Conexion();
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT habitaciones FROM hotel WHERE identificador='" + idHotel + "'", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                dias = Convert.ToInt16(lectorConsulta.GetString(0));
            }         
            conexion.Close();
            return dias;
        }

        public void LlenarDtVistaPreliminarVuelosASC(DataGridView dtgVuelosASC, string paisOrigen, string paisDestino)
        {
            try
            {
                Conexion();
                conexion.Open();
                DataSet dataset = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT identificador_ruta,pais_origen,pais_destino,duracion,precio FROM tarifas_vuelos JOIN rutas ON tarifas_vuelos.ruta=rutas.identificador_ruta WHERE pais_origen='"+paisOrigen+"' AND pais_destino='"+paisDestino+"' ORDER BY precio ASC", conexion);
                adapter.Fill(dataset, "rutas");
                dtgVuelosASC.DataSource = dataset.Tables[0];
                dtgVuelosASC.Columns[0].HeaderCell.Value = "identificador_ruta";
                dtgVuelosASC.Columns[1].HeaderCell.Value = "pais_origen";
                dtgVuelosASC.Columns[2].HeaderCell.Value = "pais_destino";
                dtgVuelosASC.Columns[3].HeaderCell.Value = "duracion";
                dtgVuelosASC.Columns[4].HeaderCell.Value = "precio";
                
                //dtgVuelosASC.Columns.Add("vueloDirectoEscala", "Vuelo Directo o Escala");
                conexion.Close();
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public void LlenarDtVistaPreliminarVuelosDESC(DataGridView dtgVuelosASC, string paisOrigen, string paisDestino)
        {
            try
            {
                Conexion();
                conexion.Open();
                DataSet dataset = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT identificador_ruta,pais_origen,pais_destino,duracion,precio FROM tarifas_vuelos JOIN rutas ON tarifas_vuelos.ruta=rutas.identificador_ruta WHERE pais_origen='" + paisOrigen + "' AND pais_destino='" + paisDestino + "' ORDER BY precio DESC", conexion);
                adapter.Fill(dataset, "rutas");
                dtgVuelosASC.DataSource = dataset.Tables[0];
                dtgVuelosASC.Columns[0].HeaderCell.Value = "identificador_ruta";
                dtgVuelosASC.Columns[1].HeaderCell.Value = "pais_origen";
                dtgVuelosASC.Columns[2].HeaderCell.Value = "pais_pastino";
                dtgVuelosASC.Columns[3].HeaderCell.Value = "duracion";
                dtgVuelosASC.Columns[4].HeaderCell.Value = "precio";
                conexion.Close();
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public void RetornarPrecioHabitacionesHotel(ComboBox NombreHotel,TextBox precioH) {

            try
            {
                Conexion();
                conexion.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT tarifa_hotel FROM hotel WHERE nombre = '" + NombreHotel.SelectedItem.ToString()+"'", conexion);
                NpgsqlDataReader leer = cmd.ExecuteReader();
                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        precioH.Text = leer.GetDouble(0).ToString();
                    }
                    conexion.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public void InsertarDatosCalifacionHoteles(int idHotel, int calificacionAcutual)
        {
            try
            {

                Conexion();
                conexion.Open();
                cmd = new NpgsqlCommand("INSERT INTO ranking_hoteles (id_hotel,calificacion_actual) VALUES ('" + idHotel + "','" + calificacionAcutual + "')", conexion);
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Error--- \n" + error);
            }

        }

        public int RetornarIDVueloDirecto(string paisOrigen, string paisDestino)
        {
            int IDVuelo = 0;
            try
            {
               
                Conexion();
                conexion.Open();
                NpgsqlCommand consulta = new NpgsqlCommand("SELECT identificador_ruta FROM rutas WHERE pais_origen='" + paisOrigen + "' AND pais_destino ='"+ paisDestino +"'", conexion);
                NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
                if (lectorConsulta.HasRows)
                {
                    while (lectorConsulta.Read())
                    {
                        IDVuelo = lectorConsulta.GetInt16(0);
                    }
                }
                conexion.Close();

            }
            catch (Exception erroe)
            {
                MessageBox.Show("Error \n " + erroe);
            }
      
            return IDVuelo;
        }

        public int RetornarDuracionTotalVueloDirecto(String paisOrigen)
        {
            int duracionVuelo = 0;

            try
            {
                Conexion();
                conexion.Open();
                NpgsqlCommand consulta = new NpgsqlCommand("SELECT duracion FROM  rutas  WHERE pais_origen ='" + paisOrigen + "'", conexion);
                NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
                if (lectorConsulta.HasRows)
                {
                    while (lectorConsulta.Read())
                    {
                        duracionVuelo = lectorConsulta.GetInt16(0);
                    }
                }
                conexion.Close();
            }
            catch (Exception erroe)
            {

                MessageBox.Show("Error \n " + erroe);
            }
            
            return duracionVuelo;
        }

        public int RetornarDuracionTotalVueloEscala(string paisOrigen,string paisDestino)
        {
            int duracionVuelo = 0;

            try
            {
                Conexion();
                conexion.Open();
                NpgsqlCommand consulta = new NpgsqlCommand("SELECT duracion FROM  rutas  WHERE pais_origen='" + paisOrigen + "' AND pais_destino='"+paisDestino+"'", conexion);
                NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
                if (lectorConsulta.HasRows)
                {
                    while (lectorConsulta.Read())
                    {
                        duracionVuelo = lectorConsulta.GetInt16(0);
                    }
                }
                conexion.Close();
            }
            catch (Exception erroe)
            {

                MessageBox.Show("Error \n " + erroe);
            }

            return duracionVuelo;
        }

        public double RetornarPrecioTotalVueloDirecto(int id_ruta)
        {
            double precioVuelo=0;
            try
            {
                Conexion();
                conexion.Open();
                NpgsqlCommand consulta = new NpgsqlCommand("SELECT precio FROM tarifas_vuelos JOIN rutas ON tarifas_vuelos.ruta=rutas.identificador_ruta WHERE identificador_ruta='" +id_ruta+"'", conexion);
                NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
                if (lectorConsulta.HasRows)
                {
                    while (lectorConsulta.Read())
                    {
                        precioVuelo = lectorConsulta.GetDouble(0);
                    }
                }

                conexion.Close();
            }
            catch (Exception erroe)
            {
                MessageBox.Show("Error \n " + erroe);
            }

            return precioVuelo;
        }

        public ArrayList LlenarDtVistaPreliminarVuelosASCString(string paisO,string paisD)
        {
            ArrayList lista = new ArrayList();
            string datos = string.Empty;
            try
            {
                Conexion();
                conexion.Open();
                NpgsqlCommand consulta = new NpgsqlCommand("SELECT pais_origen,pais_destino,duracion,precio FROM tarifas_vuelos  JOIN rutas ON tarifas_vuelos.ruta=rutas.identificador_ruta WHERE pais_origen='"+paisO+"' AND pais_destino='"+paisD+"' ORDER BY precio ASC", conexion);
                NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
                if (lectorConsulta.HasRows)
                {
                    while (lectorConsulta.Read())
                    {
                        //datos = lectorConsulta.GetDouble(0).ToString();
                        datos = lectorConsulta.GetString(0)+";"+ lectorConsulta.GetString(1) + ";"+ lectorConsulta.GetInt16(2) + ";"+ lectorConsulta.GetDouble(3);
                        lista.Add(datos);                   
                    }
                }

                //dtgVuelosASC.Columns.Add("vueloDirectoEscala", "Vuelo Directo o Escala");
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error \n"+error);
            }
            return lista;
        }

        public ArrayList LlenarDtVistaPreliminarVuelosDESCString(string paisO, string paisD)
        {
            ArrayList lista = new ArrayList();
            string datos = string.Empty;
            try
            {
                Conexion();
                conexion.Open();
                NpgsqlCommand consulta = new NpgsqlCommand("SELECT pais_origen,pais_destino,duracion,precio FROM tarifas_vuelos  JOIN rutas ON tarifas_vuelos.ruta=rutas.identificador_ruta WHERE pais_origen='" + paisO + "' AND pais_destino='" + paisD + "' ORDER BY precio DESC", conexion);
                NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
                if (lectorConsulta.HasRows)
                {
                    while (lectorConsulta.Read())
                    {
                        //datos = lectorConsulta.GetDouble(0).ToString();
                        datos = lectorConsulta.GetString(0) + ";" + lectorConsulta.GetString(1) + ";" + lectorConsulta.GetInt16(2) + ";" + lectorConsulta.GetDouble(3);
                        lista.Add(datos);
                    }
                }

                //dtgVuelosASC.Columns.Add("vueloDirectoEscala", "Vuelo Directo o Escala");
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error \n" + error);
            }
            return lista;
        }

        public String RetornarPaisEscala(String paisDestino)
        {
            String paisEscala = string.Empty;

            try
            {
                Conexion();
                conexion.Open();
                NpgsqlCommand consulta = new NpgsqlCommand("SELECT pais_origen FROM rutas WHERE pais_destino='"+ paisDestino +"'", conexion);
                NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
                if (lectorConsulta.HasRows)
                {
                    while (lectorConsulta.Read())
                    {
                        paisEscala = lectorConsulta.GetString(0);
                    }
                }

                conexion.Close();
            }
            catch (Exception erroe)
            {
                MessageBox.Show("Error \n " + erroe);
            }

            return paisEscala;
        }


        public double RetornarPrecioTotalVueloDirectoOrigenEscala(string paisOrigen)
        {
            double precioVuelo = 0;
            try
            {
                Conexion();
                conexion.Open();
                NpgsqlCommand consulta = new NpgsqlCommand("SELECT precio FROM tarifas_vuelos JOIN rutas ON tarifas_vuelos.ruta=rutas.identificador_ruta WHERE pais_origen='" + paisOrigen + "'", conexion);
                NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
                if (lectorConsulta.HasRows)
                {
                    while (lectorConsulta.Read())
                    {
                        precioVuelo = lectorConsulta.GetDouble(0);
                    }
                }

                conexion.Close();
            }
            catch (Exception erroe)
            {
                MessageBox.Show("Error \n " + erroe);
            }

            return precioVuelo;
        }

        public double RetornarPrecioTotalVueloDirectoOrigenDestinoVueloEscala(string paisOrigen,string paisDestino)
        {
            double precioVuelo = 0;
            try
            {
                Conexion();
                conexion.Open();
                NpgsqlCommand consulta = new NpgsqlCommand("SELECT precio FROM tarifas_vuelos JOIN rutas ON tarifas_vuelos.ruta=rutas.identificador_ruta WHERE pais_origen='" + paisOrigen + "' AND pais_destino='"+paisDestino+"'", conexion);
                NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
                if (lectorConsulta.HasRows)
                {
                    while (lectorConsulta.Read())
                    {
                        precioVuelo = lectorConsulta.GetDouble(0);
                    }
                }

                conexion.Close();
            }
            catch (Exception erroe)
            {
                MessageBox.Show("Error \n " + erroe);
            }

            return precioVuelo;
        }

        public ArrayList LlenarDtVistaPreliminarVuelosDuracionASCtring(string paisO, string paisD)
        {
            ArrayList lista = new ArrayList();
            string datos = string.Empty;
            try
            {
                Conexion();
                conexion.Open();
                NpgsqlCommand consulta = new NpgsqlCommand("SELECT pais_origen,pais_destino,duracion,precio FROM tarifas_vuelos  JOIN rutas ON tarifas_vuelos.ruta=rutas.identificador_ruta WHERE pais_origen='" + paisO + "' AND pais_destino='" + paisD + "' ORDER BY duracion ASC", conexion);
                NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
                if (lectorConsulta.HasRows)
                {
                    while (lectorConsulta.Read())
                    {
                        //datos = lectorConsulta.GetDouble(0).ToString();
                        datos = lectorConsulta.GetString(0) + ";" + lectorConsulta.GetString(1) + ";" + lectorConsulta.GetInt16(2) + ";" + lectorConsulta.GetDouble(3);
                        lista.Add(datos);
                    }
                }

                //dtgVuelosASC.Columns.Add("vueloDirectoEscala", "Vuelo Directo o Escala");
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error \n" + error);
            }
            return lista;
        }

        /// <summary>
        /// Meetodo que Guarda la Informacion de las Reservaciones en la Seccion VUELOS
        /// </summary>
        /// <param name="id_Vuelo"></param>
        /// <param name="id_Hotel"></param>
        /// <param name="placa_vehiculo"></param>
        /// <param name="pasajeros_adultos"></param>
        /// <param name="pasajeros_niños"></param>
        /// <param name="habitaciones_hotel"></param>
        /// <param name="fecha_Inicio"></param>
        /// <param name="fecha_Fin"></param>
        /// <param name="pais_Origen"></param>
        /// <param name="pais_Destino"></param>
        /// <param name="duracion_Total"></param>
        /// <param name="precio_Total"></param>
        /// <param name="vuelo_escala"></param>
        /// <param name="id_Cliente"></param>
        public void InsertarDatosReservacionesVuelos(Reservaciones_Vuelos reservacion){
            try
            {
                Conexion();
                conexion.Open();
                cmd = new NpgsqlCommand("INSERT INTO informacion_reservaciones_vuelos (id_vuelo,id_hotel,placa_vehiculo,pasajeros_adultos,pasajeros_niños,habitaciones_hotel,fecha_inicio,fecha_fin,pais_origen,pais_destino,duracion_total,precio_total,vuelo_escala,id_cliente) VALUES ('" + reservacion.id_vuelo + "','" + reservacion.id_hotel + "','" + reservacion.placa_vehiculo + "','" + reservacion.pasajeros_adultos + "','" + reservacion.pasajeros_niños + "','" + reservacion.habitaciones_hotel + "','" + reservacion.fecha_inicio + "','" + reservacion.fecha_fin + "','" + reservacion.pais_origen + "','" + reservacion.pais_destino + "','" + reservacion.duracion_total + "','" + reservacion.precio_total + "','" + reservacion.vuelo_escala + "','" + reservacion.id_cliente + "')", conexion);
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error--- \n" + error);
            }

            MessageBox.Show("Se ha Registrado la Reservacon Correctamente.");
        }


        /// <summary>
        /// Meetodo que Guarda la Informacion de las Reservaciones en la Seccion VUELOS
        /// </summary>
        /// <param name="id_Vuelo"></param>
        /// <param name="id_Hotel"></param>
        /// <param name="placa_vehiculo"></param>
        /// <param name="pasajeros_adultos"></param>
        /// <param name="pasajeros_niños"></param>
        /// <param name="habitaciones_hotel"></param>
        /// <param name="fecha_Inicio"></param>
        /// <param name="fecha_Fin"></param>
        /// <param name="pais_Origen"></param>
        /// <param name="pais_Destino"></param>
        /// <param name="duracion_Total"></param>
        /// <param name="precio_Total"></param>
        /// <param name="vuelo_escala"></param>
        /// <param name="id_Cliente"></param>
        public void InsertarDatosPreReservacionesVuelos(Reservaciones_Vuelos reservacion)
        {
            try
            {
                Conexion();
                conexion.Open();
                cmd = new NpgsqlCommand("INSERT INTO informacion_prereservaciones_vuelos (id_vuelo,id_hotel,placa_vehiculo,pasajeros_adultos,pasajeros_niños,habitaciones_hotel,fecha_inicio,fecha_fin,pais_origen,pais_destino,duracion_total,precio_total,vuelo_escala,id_cliente) VALUES ('" + reservacion.id_vuelo + "','" + reservacion.id_hotel + "','" + reservacion.placa_vehiculo + "','" + reservacion.pasajeros_adultos + "','" + reservacion.pasajeros_niños + "','" + reservacion.habitaciones_hotel + "','" + reservacion.fecha_inicio + "','" + reservacion.fecha_fin + "','" + reservacion.pais_origen + "','" + reservacion.pais_destino + "','" + reservacion.duracion_total + "','" + reservacion.precio_total + "','" + reservacion.vuelo_escala + "','" + reservacion.id_cliente + "')", conexion);
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error--- \n" + error);
            }

            MessageBox.Show("Se ha Registrado la Pre Reservacon Correctamente.");
        }

        public int ConsultarIDHotel(String nombreHotel)
        {
            int IDHotel = 0;

            Conexion();
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT identificador FROM hotel WHERE nombre='" + nombreHotel + "'", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                while (lectorConsulta.Read())
                {
                    //cedula, nombre y tipo de usuario
                    //informacionUsuario = lectorConsulta.GetString(0) + ";" + lectorConsulta.GetString(1)  + ";"+ lectorConsulta.GetString(3);
                    IDHotel = int.Parse(lectorConsulta.GetString(0));

                }
            }
            conexion.Close();

            return IDHotel;
        }

        public void LlenarDtVistaPreReservacionesVuelos(DataGridView datos_reservaciones_vuelos, int cedulaUsuario)
        {
            try
            {
                Conexion();
                conexion.Open();
                DataSet dataset = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT id_vuelo,id_hotel,placa_vehiculo,pasajeros_adultos,pasajeros_niños,habitaciones_hotel,fecha_inicio,fecha_fin,pais_origen,pais_destino,duracion_total,precio_total,vuelo_escala,id_cliente FROM informacion_prereservaciones_vuelos WHERE id_cliente = '" + cedulaUsuario + "'", conexion);
                adapter.Fill(dataset, "rutas");
                datos_reservaciones_vuelos.DataSource = dataset.Tables[0];
                datos_reservaciones_vuelos.Columns[0].HeaderCell.Value = "id_vuelo";
                datos_reservaciones_vuelos.Columns[1].HeaderCell.Value = "id_hotel";
                datos_reservaciones_vuelos.Columns[2].HeaderCell.Value = "placa_vehiculo";
                datos_reservaciones_vuelos.Columns[3].HeaderCell.Value = "pasajeros_adultos";
                datos_reservaciones_vuelos.Columns[4].HeaderCell.Value = "pasajeros_niños";
                datos_reservaciones_vuelos.Columns[5].HeaderCell.Value = "habitaciones_hotel";
                datos_reservaciones_vuelos.Columns[6].HeaderCell.Value = "fecha_inicio";
                datos_reservaciones_vuelos.Columns[7].HeaderCell.Value = "fecha_fin";
                datos_reservaciones_vuelos.Columns[8].HeaderCell.Value = "pais_origen";
                datos_reservaciones_vuelos.Columns[9].HeaderCell.Value = "pais_destino";
                datos_reservaciones_vuelos.Columns[10].HeaderCell.Value = "duracion_total";
                datos_reservaciones_vuelos.Columns[11].HeaderCell.Value = "precio_total";
                datos_reservaciones_vuelos.Columns[13].HeaderCell.Value = "vuelo_escala";
                datos_reservaciones_vuelos.Columns[13].HeaderCell.Value = "id_cliente";
                conexion.Close();

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public int ConsultarCantidadHabitacionesHotel(int idHotel)
        {
            int habitacionesHotel = 0;

            Conexion();
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT habitaciones FROM hotel WHERE identificador='" + idHotel + "'", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                while (lectorConsulta.Read())
                {
                    //cedula, nombre y tipo de usuario
                    //informacionUsuario = lectorConsulta.GetString(0) + ";" + lectorConsulta.GetString(1)  + ";"+ lectorConsulta.GetString(3);
                    habitacionesHotel = int.Parse(lectorConsulta.GetString(0));

                }
            }
            conexion.Close();

            return habitacionesHotel;
        }

        public void EliminarPreReservacionesVuelos(string pasajeros_Adultos,string pasajeros_Niños,string pais_Origen,string pais_Destino,string id_cliente)
        {
            try
            {
                Conexion();
                conexion.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM informacion_prereservaciones_vuelos WHERE  pasajeros_adultos= '" + pasajeros_Adultos + "' AND pasajeros_niños='"+pasajeros_Niños+"' AND pais_origen='"+pais_Origen+"' AND pais_destino='"+pais_Destino+"' AND id_cliente='"+id_cliente+"'", conexion);
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error--- \n" + error);
            }

        }

    }
    
}