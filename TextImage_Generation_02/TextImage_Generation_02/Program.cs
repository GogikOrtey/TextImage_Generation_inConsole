using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;

namespace TextImage_Generation_02
{
    class Program
    {
        static void Main(string[] args)
        {
            // Загрузить изображение
            //Bitmap image = new Bitmap(@"D:\Рабочий стол\Учёба 3й курс 2023\Мои проекты C# 2023\TextImage_Generation_02\Input\01.gif");
            Bitmap image = new Bitmap(@"G:\0\1\1\1\2\Облачное хранилище\Архив\Учёба\6_Учёба 3й курс 2023 Final\Мои проекты C# и JS 2023\TextImage_Generation_02\Input\01.gif");

            // Получить размер изображения
            int width = image.Width;
            int height = image.Height;

            // Установить размер маски
            int maskWidth = 100;
            //int maskHeight = 50;
            //новая высота = (старая высота * новая ширина) / старая ширина
            int maskHeight = 43;//(height * maskWidth) / width;

            // Установить размер окна консоли
            Console.WindowWidth = maskWidth;
            Console.WindowHeight = maskHeight + 2;

            // Получить размер шага для каждого пикселя в маске
            float stepX = (float)width / maskWidth;
            float stepY = (float)height / maskHeight;

            string str = @"/image turbo generate bot -rtr -1";
            //string str = @"Gogik Ortey top";
            int indStr = 0;

            List<string[]> frames = new List<string[]>();

            for (int i = 0; i < image.GetFrameCount(FrameDimension.Time); i++)
            {
                image.SelectActiveFrame(FrameDimension.Time, i);
                string[] frame = new string[maskHeight];
                for (int y = 0; y < maskHeight; y++)
                {
                    StringBuilder line = new StringBuilder();
                    for (int x = 0; x < maskWidth; x++)
                    {
                        Color pixelColor = ((Bitmap)image).GetPixel((int)(x * stepX), (int)(y * stepY));
                        char character;
                        if (pixelColor.GetBrightness() > 0.6)
                        {
                            character = str[indStr];
                        }
                        else character = ' ';

                        line.Append(character);

                        indStr++;
                        if (indStr >= str.Length) indStr = 0;
                    }
                    frame[y] = line.ToString();
                }
                frames.Add(frame);
            }

            while (true)
            {
                foreach (string[] frame in frames)
                {
                    foreach (string line in frame)
                    {
                        Console.WriteLine(line);
                    }

                    Thread.Sleep(15);
                    Console.Clear();
                }
            }
        }
    }
}