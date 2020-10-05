using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Media.Imaging;

namespace RockGame.Core
{
    public class Map
    {
		public BitmapImage[] images;
		private BitmapImage[,] _map;
		private string[,] _mapString;

		public BitmapImage[,] MapImages
		{
			get
			{
				return _map;
			}
			set
			{
				_map = value;
			}
		}

		public string[,] MapString
		{
			get
			{
				return _mapString;
			}
			set
			{
				_mapString = value;
			}
		}
        
        public int Height 
        { 
            get 
            { 
                return MapImages.GetLength(0); 
            } 
        }

        public int Width 
        { 
            get 
            { 
				return MapImages.GetLength(1); 
            } 
        }

		public Cell startPoint { get; private set; }
		public Cell finishPoint { get; private set; }
		public List<Cell> listKey { get; private set; }

		public Map(string fileMap)
        {
			string[] linesMap = File.ReadAllLines(fileMap);
			int[] size = linesMap[0].Split(' ').Select((w) => int.Parse(w)).ToArray();
			MapImages = new BitmapImage[size[0], size[1]];
			MapString = new string[size[0], size[1]];
			listKey = new List<Cell>();

			var rootFolder = Directory.GetCurrentDirectory();
			rootFolder = rootFolder.Substring(0,rootFolder.IndexOf(@"\RockGame\", StringComparison.Ordinal) + @"\RockGame".Length);
			rootFolder = rootFolder + @"\RockGame\Image\";

			images = new BitmapImage[14];
			images[0] = new BitmapImage(new Uri(rootFolder + "floor.png"));
			images[1] = new BitmapImage(new Uri(rootFolder + "wall.png"));
			images[2] = new BitmapImage(new Uri(rootFolder + "DoorB.png"));
			images[3] = new BitmapImage(new Uri(rootFolder + "KeyCardB.png"));

			images[4] = new BitmapImage(new Uri(rootFolder + "fire.png"));
			images[5] = new BitmapImage(new Uri(rootFolder + "medicine.png"));
			images[6] = new BitmapImage(new Uri(rootFolder + "PointAppearance.png"));
			images[7] = new BitmapImage(new Uri(rootFolder + "Exit.png"));

			images[8] = new BitmapImage(new Uri(rootFolder + "DoorA.png"));
			images[9] = new BitmapImage(new Uri(rootFolder + "KeyCardA.png"));

			images[10] = new BitmapImage(new Uri(rootFolder + "DoorC.png"));
			images[11] = new BitmapImage(new Uri(rootFolder + "KeyCardC.png"));

			//images[12] = new BitmapImage(new Uri(rootFolder + "DoorB.png"));
			//images[13] = new BitmapImage(new Uri(rootFolder + "KeyCardB.png"));


			for (int i = 0; i < size[0]; i++)
			{
				string[] cells = linesMap[1 + i].Split(' ');
				for (int j = 0; j < size[1]; j++)
				{
					MapString[i, j] = cells[j];
					switch (cells[j])
					{
						case ".":
							MapImages[i, j] = images[0];
							break;
						case "X":
							MapImages[i, j] = images[1];
							break;
						case "S":
							MapImages[i, j] = images[6];
							startPoint = new Cell(j, i);
							break;
						case "Q":
							MapImages[i, j] = images[7];
							finishPoint = new Cell(j, i);
							break;


						case "A":
							MapImages[i, j] = images[8];
							break;
						case "B":
							MapImages[i, j] = images[2];
							break;
						case "C":
							MapImages[i, j] = images[10];
							break;
						case "D":
							MapImages[i, j] = images[2];
							break;


						case "a":
							MapImages[i, j] = images[9];
							listKey.Add(new Cell(j, i));
							break;
						case "b":
							MapImages[i, j] = images[3];
							listKey.Add(new Cell(j, i));
							break;
						case "c":
							MapImages[i, j] = images[11];
							listKey.Add(new Cell(j, i));
							break;
						case "d":
							MapImages[i, j] = images[3];
							listKey.Add(new Cell(j, i));
							break;

						case "1":
						case "2":
						case "3":
						case "4":
						case "5":
							MapImages[i, j] = images[4];
							break;
						case "H":
							MapImages[i, j] = images[5];
							break;
						default:
							break;
					}
				}
			}
		}

		public string this[int i, int j]
		{
			get
			{
				return _mapString[i, j];
			}
		}

	}
}
