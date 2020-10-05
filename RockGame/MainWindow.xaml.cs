using Microsoft.Win32;
using RockGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RockGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filePath { get; set; }
        OpenFileDialog fileDialog = new OpenFileDialog();
        public Cell sizeCell;
        public Image[,] renderMap;
        public int step = 0;
        public Cell heroPosition;
        public Timer timer;
        public string path;
        public Map _map;
        public Hero _hero;
        public List<Cell> listKey;

        public MainWindow()
        {
            InitializeComponent();
            AddFileDialog();

        }

        private void AddFileDialog()
        {
            fileDialog.InitialDirectory = Assembly.GetEntryAssembly().Location;
            fileDialog.RestoreDirectory = true;
            fileDialog.Title = "Open map file";
            fileDialog.DefaultExt = "txt";
            fileDialog.Filter = "txt files (*.txt)|*.txt";
            fileDialog.FileOk += OpenFileDialog_FileOk;
        }

        private void MapRender(Map map)
        {
            //Closed += GameWindow_Closed;

            _map = map;
            sizeCell = new Cell(500.0 / map.Width, 500.0 / map.Height);
            renderMap = new Image[map.Height, map.Width];

            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    renderMap[i, j] = new Image();
                    renderMap[i, j].Height = sizeCell.y;
                    renderMap[i, j].Width = sizeCell.x;
                    renderMap[i, j].Stretch = Stretch.Fill;
                    game.Children.Add(renderMap[i, j]);
                    Canvas.SetLeft(renderMap[i, j], j * sizeCell.x);
                    Canvas.SetTop(renderMap[i, j], i * sizeCell.y); 
                    

                    renderMap[i, j].Source = map.MapImages[i, j];
                }
            }

            Hero hero = new Hero(map.MapString);
            _hero = hero;
            hero.SaveToFile("moves.txt");
            path = string.Join("", hero.Path);
            heroPosition = map.startPoint;
            listKey = map.listKey;
            var a = hero.Health;

            renderMap[(int)heroPosition.y, (int)heroPosition.x].Source = hero.imagesHero;


            timer = new Timer(500);
            timer.Elapsed += OnPaint;
            timer.Start();
        }

        private void btnOpenMap_Click(object sender, RoutedEventArgs e)
        {
            fileDialog.ShowDialog();
            Map mapGame = new Map(filePath);
            MapRender(mapGame);
        }

        private void OpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            game.Children.Clear();
            filePath = null;
            sizeCell = null;
            renderMap = null;
            step = 0;
            heroPosition = null;
            timer = null;
            path = null;
            _map = null;
            _hero = null;
            listKey = null;
            filePath = fileDialog.FileName;
        }

        public void Step()
        {
            if (step >= path.Length)
            {
                timer.Dispose();
                WinDialog winDialog = new WinDialog(this, step);
                winDialog.Show();
            }
            else
            {
                switch (path[step])
                {
                    case 'R':
                        heroPosition += new Cell(1, 0);
                        break;
                    case 'D':
                        heroPosition += new Cell(0, 1);
                        break;
                    case 'L':
                        heroPosition += new Cell(-1, 0);
                        break;
                    case 'U':
                        heroPosition += new Cell(0, -1);
                        break;
                    default:
                        break;
                }
                step++;
                //StepsStat.Text = "Шаги: " + curStep;
            }
        }

        public void OnPaint(object source, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                for (int i = 0; i < _map.Height; i++)
                    for (int j = 0; j < _map.Width; j++)
                    {
                        renderMap[i, j].Source = _map.MapImages[i, j];
                    }

                foreach(var item in listKey)
                {
                    if (item.x == (int)heroPosition.x && item.y == (int)heroPosition.y)
                    {
                        _map.MapImages[(int)heroPosition.y, (int)heroPosition.x] = _map.images[0];
                    }
                }



                renderMap[(int)heroPosition.y, (int)heroPosition.x].Source = _hero.imagesHero;
                

                Step();
            });
        }

    }
}
