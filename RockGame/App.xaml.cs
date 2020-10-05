using RockGame.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1;

namespace RockGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		public static bool isConsole = false;

		void App_Startup(object sender, StartupEventArgs e)
		{
			if (e.Args.Length == 2)
			{
				try
				{
					if (e.Args[0] == "-console")
					{
						isConsole = true;
						ConsoleManager.Show();
						Map mapGame = new Map(e.Args[1]);
						Hero hero = new Hero(mapGame.MapString);
						hero.SaveToFile("moves.txt");
						Shutdown();
					}
					else
					{
						isConsole = false;
					}
				}
				catch (Exception)
				{
					MessageBox.Show("Use: ./program.exe -console <path to file>", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
					isConsole = false;
				}

			}
			else if (e.Args.Length == 0)
			{
				isConsole = false;
			}
			else
			{
				MessageBox.Show("Use: ./program.exe -console <path to file>", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
				isConsole = false;
			}
			if (!isConsole)
			{
				ConsoleManager.Hide();
				MainWindow mainWindow = new MainWindow();
				mainWindow.Show();
			}
		}
	}
}

