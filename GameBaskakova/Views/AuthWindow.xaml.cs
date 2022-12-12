﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameBaskakova
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reg_Auth.SelectedIndex= 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Reg_Auth.SelectedIndex= 0;
        }

        private void LoginName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (LoginName.Text == "Логин")
                LoginName.Text = "";
        }

        private void LoginPass_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (LoginPass.Text == "Пароль")
                LoginPass.Text = "";
        }
    }
}
