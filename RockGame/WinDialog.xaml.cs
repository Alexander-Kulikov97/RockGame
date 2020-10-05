using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RockGame
{
    /// <summary>
    /// Логика взаимодействия для WinDialog.xaml
    /// </summary>
    public partial class WinDialog : Window
    {
        public MainWindow _gameWindow;
        public WinDialog(MainWindow gameWindow, int steps)
        {
            InitializeComponent();
            stepsWin.Text = "STEPS: " + steps;
            this._gameWindow = gameWindow;
        }
    }
}
