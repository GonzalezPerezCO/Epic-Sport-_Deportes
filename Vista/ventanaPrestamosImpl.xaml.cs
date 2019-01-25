﻿using AlphaSport.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AlphaSport.Vista
{
    /// <summary>
    /// Lógica de interacción para ventanaPrestamosImpl.xaml
    /// </summary>
    public partial class VentanaPrestamosImpl : Window
    {
        private Entorno entorno;
        private static VentanaPrestamosImpl instance = null;

        private List<string> listaDisponibles; // lista de disponibles por sigla
        private List<string> listaCantDisponibles; // lista de disponibles de una sigla

        private VentanaPrestamosImpl()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();

            listaDisponibles = new List<string>();
            listaCantDisponibles = new List<string>();
        }

        public static VentanaPrestamosImpl GetInstance()
        {
            if (instance == null)
                instance = new VentanaPrestamosImpl();

            return instance;
        }

        private void Ocultar()
        {
            PrestamosImpl prestamos = PrestamosImpl.GetInstance();
            prestamos.Show();
            this.Hide();
        }  

        private void ActualizarCmbx()
        {
            cmbox_Sigla.ItemsSource = listaDisponibles;
            cmbox_Cant.ItemsSource = listaCantDisponibles;
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            Ocultar();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            Ocultar();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
