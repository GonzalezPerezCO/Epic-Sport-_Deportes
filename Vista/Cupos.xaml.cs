﻿using Deportes_WPF.Controller;
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

namespace Deportes_WPF.Vista
{
    /// <summary>
    /// Lógica de interacción para Cupos.xaml
    /// </summary>
    public partial class Cupos : Window
    {
        public Cupos()
        {
            InitializeComponent();
            Entorno entorno = Entorno.GetInstance();

        }
    }
}
