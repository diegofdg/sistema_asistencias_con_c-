﻿using SistemaAsistencias.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SistemaAsistencias.Presentacion.AsistenteInstalacion
{
    public partial class InstalacionBd : Form
    {
        public InstalacionBd()
        {
            InitializeComponent();
        }

        private AES aes = new AES();
        string ruta;
        string nombre_del_equipo_usuario;
        public static int milisegundo;
        public static int segundos;
        public static int segundos1;
        public static int minutos1;
        public static int milisegundo1;

        private void txtusuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void InstalacionBd_Load(object sender, EventArgs e)
        {
            centrarPaneles();
            Reemplazar();
            comprobar_si_ya_hay_servidor_instalado_SQL_EXPRESS();
            Conectar();
        }

        private void Conectar()
        {
            if (btnInstalarServidor.Visible == true)
            {
                comprobar_si_ya_hay_servidor_instalado_SQL_NORMAL();
            }
        }

        private void comprobar_si_ya_hay_servidor_instalado_SQL_NORMAL()
        {
            txtservidor.Text = ".";
            ejecutar_scryt_ELIMINARBase_comprobacion_de_inicio();
            ejecutar_scryt_crearBase_comprobacion_De_inicio();
        }

        private void centrarPaneles()
        {
            Panel2.Location = new Point((Width - Panel2.Width) / 2, (Height - Panel2.Height) / 2);
            nombre_del_equipo_usuario = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            Cursor = Cursors.WaitCursor;
            Panel4.Visible = false;
            Panel4.Dock = DockStyle.None;
        }

        private void Reemplazar()
        {
            //Solo modificar este campo
            txtCrear_procedimientos.Text = txtCrear_procedimientos.Text.Replace("ORUS369", TXTbasededatos.Text);
            //***************
            txtEliminarBase.Text = txtEliminarBase.Text.Replace("BASEADACURSO", TXTbasededatos.Text);
            txtCrearUsuarioDb.Text = txtCrearUsuarioDb.Text.Replace("ada369", txtusuario.Text);
            txtCrearUsuarioDb.Text = txtCrearUsuarioDb.Text.Replace("BASEADA", TXTbasededatos.Text);
            txtCrearUsuarioDb.Text = txtCrearUsuarioDb.Text.Replace("softwarereal", lblcontraseña.Text);
            //Adjuntando al texbox que contiene los procedimientos almacenados
            txtCrear_procedimientos.Text = txtCrear_procedimientos.Text + Environment.NewLine + txtCrearUsuarioDb.Text;
        }

        private void comprobar_si_ya_hay_servidor_instalado_SQL_EXPRESS()
        {
            txtservidor.Text = @".\" + lblnombredeservicio.Text;
            ejecutar_scryt_ELIMINARBase_comprobacion_de_inicio();
            ejecutar_scryt_crearBase_comprobacion_De_inicio();
        }

        private void ejecutar_scryt_ELIMINARBase_comprobacion_de_inicio()
        {
            string str;
            SqlConnection myConn = new SqlConnection("Data source=" + txtservidor.Text + ";Initial Catalog=master;Integrated Security=True");
            str = txtEliminarBase.Text;
            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        private void ejecutar_scryt_crearBase_comprobacion_De_inicio()
        {
            SqlConnection cnn = new SqlConnection("Server=" + txtservidor.Text + "; " + "database=master; integrated security=yes");
            string s = "CREATE DATABASE " + TXTbasededatos.Text;
            SqlCommand cmd = new SqlCommand(s, cnn);
            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                SavetoXML(aes.Encrypt("Data Source=" + txtservidor.Text + ";Initial Catalog=" + TXTbasededatos.Text + ";Integrated Security=True", Desencryptacion.appPwdUnique, int.Parse("256")));
                EjecutarScript();
                Panel4.Visible = true;
                Panel4.Dock = DockStyle.Fill;
                Label1.Text = @"Instancia Encontrada...
            No Cierre esta Ventana, se cerrara Automaticamente cuando este todo Listo";
                Panel6.Visible = false;
                timer4.Start();
            }
            catch (Exception)
            {
                btnInstalarServidor.Visible = true;
                Panel6.Visible = true;
                Panel4.Visible = false;
                Panel4.Dock = DockStyle.None;
                lblbuscador_de_servidores.Text = "De click a Instalar Servidor, luego de click a SI cuando se le pida, luego presione ACEPTAR y espere por favor ";
            }
        }

        private void EjecutarScript()
        {
            ruta = Path.Combine(Directory.GetCurrentDirectory(), txtnombre_scrypt.Text + ".txt");
            StreamWriter sw;
            try
            {
                if (File.Exists(ruta) == false)
                {
                    sw = File.CreateText(ruta);
                    sw.WriteLine(txtCrear_procedimientos.Text);
                    sw.Flush();
                    sw.Close();
                }
                else if (File.Exists(ruta) == true)
                {
                    File.Delete(ruta);
                    sw = File.CreateText(ruta);
                    sw.WriteLine(txtCrear_procedimientos.Text);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception)
            {

            }
            try
            {
                Process Pross = new Process();
                Pross.StartInfo.FileName = "sqlcmd";
                Pross.StartInfo.Arguments = " -S " + txtservidor.Text + " -i " + txtnombre_scrypt.Text + ".txt";
                Pross.Start();
            }
            catch (Exception)
            {

            }
        }

        private void SavetoXML(object dbcnString)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ConnectionString.xml");
            XmlElement root = doc.DocumentElement;
            root.Attributes[0].Value = Convert.ToString(dbcnString);
            XmlTextWriter writer = new XmlTextWriter("ConnectionString.xml", null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);
            writer.Close();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            timer3.Stop();
            milisegundo += 1;
            mil3.Text = milisegundo.ToString();
            if (milisegundo == 60)
            {
                segundos += 1;
                seg3.Text = segundos.ToString();
                milisegundo = 0;
            }
            if (segundos == 15)
            {
                timer4.Stop();
                try
                {
                    File.Delete(ruta);
                }
                catch (Exception ex)
                {

                }
                Dispose();
            }
        }

        private void btnInstalarServidor_Click(object sender, EventArgs e)
        {
            try
            {
                txtArgumentosini.Text = txtArgumentosini.Text.Replace("PRUEBAFINAL22", lblnombredeservicio.Text);
                TimerCRARINI.Start();
                executa();
                timer2.Start();
                Panel4.Visible = true;
                Panel4.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void executa()
        {
            try
            {
                Process Pross = new Process();
                Pross.StartInfo.FileName = "SQLEXPR_x86_ENU.exe";
                Pross.StartInfo.Arguments = "/ConfigurationFile=ConfigurationFile.ini /ACTION=Install /IACCEPTSQLSERVERLICENSETERMS /SECURITYMODE=SQL /SAPWD=" + lblcontraseña.Text + " /SQLSYSADMINACCOUNTS=" + nombre_del_equipo_usuario;
                Pross.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                Pross.Start();
                Panel4.Visible = true;
                Panel4.Dock = DockStyle.Fill;
            }
            catch (Exception)
            {

            }
        }

        private void TimerCRARINI_Tick(object sender, EventArgs e)
        {
            string rutaPREPARAR;
            StreamWriter sw;
            rutaPREPARAR = Path.Combine(Directory.GetCurrentDirectory(), "ConfigurationFile.ini");
            rutaPREPARAR = rutaPREPARAR.Replace("ConfigurationFile.ini", @"SQLEXPR_x86_ESN\ConfigurationFile.ini");
            if (File.Exists(rutaPREPARAR) == true)
            {
                TimerCRARINI.Stop();
            }
            try
            {
                sw = File.CreateText(rutaPREPARAR);
                sw.WriteLine(txtArgumentosini.Text);
                sw.Flush();
                sw.Close();
                TimerCRARINI.Stop();
            }
            catch (Exception ex)
            {

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            milisegundo1 += 1;
            milise.Text = Convert.ToString(milisegundo1);
            if (milisegundo1 == 60)
            {
                segundos1 += 1;
                seg.Text = Convert.ToString(segundos1);
                milisegundo1 = 0;
            }
            if (segundos1 == 60)
            {
                minutos1 += 1;
                min.Text = Convert.ToString(minutos1);
                segundos1 = 0;
            }
            if (minutos1 == 6)
            {
                timer2.Stop();
                ejecutar_scryt_ELIMINARBase();
                ejecutar_scryt_crearBase();
                timer3.Start();
            }
        }
        private void ejecutar_scryt_ELIMINARBase()
        {
            string str;
            SqlConnection myConn = new SqlConnection("Data Source=" + txtservidor.Text + ";Initial Catalog=master;Integrated Security=True");
            str = txtEliminarBase.Text;
            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if ((myConn.State == ConnectionState.Open))
                {
                    myConn.Close();
                }
            }
        }

        private void ejecutar_scryt_crearBase()
        {
            var cnn = new SqlConnection("Server=" + txtservidor.Text + "; " + "database=master; integrated security=yes");
            string s = "CREATE DATABASE " + TXTbasededatos.Text;
            var cmd = new SqlCommand(s, cnn);
            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                SavetoXML(aes.Encrypt("Data Source=" + txtservidor.Text + ";Initial Catalog=" + TXTbasededatos.Text + ";Integrated Security=True", Logica.Desencryptacion.appPwdUnique, int.Parse("256")));
                EjecutarScript();
                timer4.Start();
            }
            catch (Exception ex)
            {

            }

            finally
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            milisegundo1 += 1;
            milise.Text = Convert.ToString(milisegundo1);
            if (milisegundo1 == 60)
            {
                segundos1 += 1;
                seg.Text = Convert.ToString(segundos1);

                milisegundo1 = 0;

            }

            if (segundos1 == 60)
            {
                minutos1 += 1;

                min.Text = Convert.ToString(minutos1);
                segundos1 = 0;
            }

            if (minutos1 == 1)
            {

                ejecutar_scryt_ELIMINARBase();
                ejecutar_scryt_crearBase();

            }
        }
    }
}