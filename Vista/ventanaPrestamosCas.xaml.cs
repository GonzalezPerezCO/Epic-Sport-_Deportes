﻿using AlphaSport.Controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Lógica de interacción para ventanaPrestamosCas.xaml
    /// </summary>
    public partial class VentanaPrestamosCas : Window
    {

        private Entorno entorno;
        private static VentanaPrestamosCas instance = null;
        private static List<string> lista = new List<string>();

        private VentanaPrestamosCas()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();

            codigo.Focus();
        }        

        public static VentanaPrestamosCas GetInstance()
        {
            if (instance == null)
                instance = new VentanaPrestamosCas();

            return instance;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private List<string> SepararIds(List<string> lista)
        {
            // a,b,c,...,x

            List<string> result = new List<string>();

            string[] separadas;
            separadas = lista[0].Split(',');

            foreach (var item in separadas)
            {
                result.Add(item);
                Debug.WriteLine("<< id a lista: " + item);
            }

            return result;
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            if (codigo.Text == "" || !UInt64.TryParse(codigo.Text, out UInt64 abc))
            {
                MessageBox.Show("Ingrese un número de carnet valido!");
            }
            else
            {
                // Lista: nombre, codigo, casillero, disponible{0:no, 1:si}, entrada, salida
                UInt64 codigoEs = Convert.ToUInt64(codigo.Text);
                int codigoCas = Convert.ToInt32(cmbox.SelectedValue);

                List<string> busCod = entorno.BuscarCasilleroEstu(codigoEs); // valida y devuelve datos si tiene casillero
                Debug.WriteLine("<<<<<< ++++++ "+busCod[0] + busCod[1]);
                if (busCod.Count != 0 && busCod[0] == entorno.INFOSQL) // cuando si esta pero no tiene casillero
                {
                    if (cmbox.Text == "")
                    {
                        MessageBox.Show("Agregue un casillero primero.");
                    }
                    else
                    {
                        entorno.AgregarEstudianteCasillero(codigoCas, codigoEs);
                        ActualizarListaDisp(); // actualiza lista y oculta esta ventana
                        Debug.WriteLine("<<< Prestamo: id_c = " + codigoCas + ", codigoEst = " + codigoEs + ".");
                        MessageBox.Show("Casillero asignado!");
                    }
                }
                else if (busCod.Count != 0 && busCod[0] == entorno.ERRORSQL) // cuando el estudiante no existe o presenta errores
                {
                    MessageBox.Show(busCod[1]);
                }
                else
                {
                    entorno.QuitarEstudianteCasillero(codigoEs);
                    ActualizarListaDisp();
                    MessageBox.Show("Estudiante encontrado! Casillero liberado.");
                    Debug.WriteLine("<<< Prestamo liberado: codigoEst = " + codigoEs + ".");
                } 

                /*if (busCod.Count != 0)
                {
                    if (cmbox.SelectedValue != null)
                    {
                        MessageBox.Show("El estudiante ya tiene un casillero asignado!");
                    }
                    else
                    {
                        MessageBox.Show("Estudiante encontrado! Casillero liberado.");
                        Debug.WriteLine("<<< Prestamo liberado: codigoEst = " + codigoEs + ".");
                        entorno.QuitarEstudianteCasillero(codigoEs);
                        ActualizarListaDisp();
                    }
                }
                else if (!estudiante)
                {
                    MessageBox.Show("Estudiante no encontrado!");
                }
                else if (estudiante)
                {
                    if (cmbox.Text == "")
                    {
                        MessageBox.Show("Agregue un casillero primero.");
                    }
                    else
                    {
                        entorno.AgregarEstudianteCasillero(codigoCas, codigoEs);
                        Debug.WriteLine("<<< Prestamo: id_c = " + codigoCas + ", codigoEst = " + codigoEs + ".");
                        MessageBox.Show("Casillero asignado!");
                        ActualizarListaDisp(); // actualiza lista y oculta esta ventana
                    }
                }*/
            }

            Limpiar();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            Ocultar();
        }

        private void Limpiar() {
            codigo.Text = "";
            cmbox.SelectedValue = null;
            //this.Hide();
        }

        private void Ocultar() {
            Casilleros casilleros = Casilleros.GetInstance();
            casilleros.Show();
            this.Hide();
        }

        private void ActualizarListaDisp()
        {
            lista = entorno.DisponiblesCasilleros();

            if (lista.Count != 0 && (lista[0] == entorno.ERRORSQL || lista[0] == entorno.INFOSQL))
            {
                cmbox.ItemsSource = new List<string>();
                if(this.IsVisible) MessageBox.Show(lista[1]);
            }
            else 
            {   lista = SepararIds(lista);
                cmbox.ItemsSource = lista;                
            }
            //Ocultar();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ActualizarListaDisp();
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
