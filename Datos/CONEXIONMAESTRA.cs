﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SistemaAsistencias.Datos
{
    public class CONEXIONMAESTRA
    {
        public static string conexion = Convert.ToString(Logica.Desencryptacion.checkServer());
        //public static string conexion = @"Data source=DESKTOP-QR7T2GC; Initial Catalog=ORUS369; Integrated Security=true";
        public static SqlConnection conectar = new SqlConnection(conexion);
        public static void abrir()
        {
            if(conectar.State == ConnectionState.Closed)
            {
                conectar.Open();
            }
        }
        public static void cerrar()
        {
            if (conectar.State == ConnectionState.Open)
            {
                conectar.Close();
            }
        }
    }
}
