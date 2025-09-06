/*
 * Author: Nikolay Dvurechensky
 * Site: https://sites.google.com/view/dvurechensky
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 06 сентября 2025 11:46:30
 * Version: 1.0.39
 */

using System;
using System.Drawing;

namespace FLCompanionByDvurechensky.Services
{
    /// <summary>
    /// Сервис для работы с картой
    /// </summary>
    public class DrawService
    {
        /// <summary>
        /// Пикселей в одном делении
        /// </summary>
        private int BlockLength { get; set; }
        /// <summary>
        /// Длинна стрелки
        /// </summary>
        private int ArrowLength { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="BlockLength"></param>
        /// <param name="ArrowLength"></param>
        public DrawService(int BlockLength,int ArrowLength)
        {
            this.BlockLength = BlockLength;
            this.ArrowLength = ArrowLength;
        }

        /// <summary>
        /// Рисование оси X
        /// </summary>
        /// <param name="start">Начало</param>
        /// <param name="end">Конец</param>
        /// <param name="map">Карта</param>
        public void DrawXAxis(Point start, Point end, Graphics map, bool arrow = true)
        {
            //Деления в положительном направлении оси
            for (int i = BlockLength; i < end.X - ArrowLength; i += BlockLength)
            {
                map.DrawLine(Pens.Black, i, -1, i, 1);
            }
            //Деления в отрицательном направлении оси
            for (int i = -BlockLength; i > start.X; i -= BlockLength)
            {
                map.DrawLine(Pens.Black, i, -1, i, 1);
            }
            //Ось
            map.DrawLine(Pens.Black, start, end);
            //Стрелка
            if(arrow) map.DrawLines(Pens.Black, GetArrow(start.X, start.Y, end.X, end.Y, ArrowLength));
        }

        /// <summary>
        /// Рисование оси Y
        /// </summary>
        /// <param name="start">Начало</param>
        /// <param name="end">Конец</param>
        /// <param name="map">Карта</param>
        public void DrawYAxis(Point start, Point end, Graphics map, bool arrow = true)
        {
            //Деления в отрицательном направлении оси
            for (int i = BlockLength; i < start.Y; i += BlockLength)
            {
                map.DrawLine(Pens.Black, -1, i, 1, i);
            }
            //Деления в положительном направлении оси
            for (int i = -BlockLength; i > end.Y + ArrowLength; i -= BlockLength)
            {
                map.DrawLine(Pens.Black, -1, i, 1, i);
            }
            //Ось
            map.DrawLine(Pens.Black, start, end);
            //Стрелка
            if (arrow) map.DrawLines(Pens.Black, GetArrow(start.X, start.Y, end.X, end.Y, ArrowLength));
        }

        /// <summary>
        /// Рисует местоположение базы
        /// </summary>
        /// <param name="X">X</param>
        /// <param name="Y">Y</param>
        /// <param name="map">Карта</param>
        public void DrawPoint(int X, int Y, int width, int height, Graphics map, Color color, int boxW = 5, int boxH = 5)
        {
            int[] coords = ResetCoords(X, Y, width, height);
            Rectangle rect = new Rectangle(coords[0], coords[1], boxW, boxH);
            map.DrawRectangle(new Pen(color, .5f), rect);
            Brush bb = new SolidBrush(color);
            map.FillRectangle(bb, rect);
        }

        /// <summary>
        /// Рисование текста
        /// </summary>
        /// <param name="point">Местоположение</param>
        /// <param name="text">Текст</param>
        /// <param name="map">Карта</param>
        /// <param name="isYAxis">Выбор оси</param>
        public void DrawText(Point point, int width, int height, string text, Graphics map, Brush color, int sizeText, bool isYAxis = false)
        {
            var fontFamily = new FontFamily("Arial");
            var font = new Font(fontFamily, sizeText, FontStyle.Bold, GraphicsUnit.Pixel);
            var size = map.MeasureString(text, font);
            var coords = ResetCoords(point.X, point.Y, width, height);
            var rect = new RectangleF(new Point(coords[0], coords[1]), size);
            map.DrawString(text, font, color, rect);
        }

        /// <summary>
        /// Вычисление стрелки оси
        /// </summary>
        /// <param name="x1">X1</param>
        /// <param name="y1">Y1</param>
        /// <param name="x2">X2</param>
        /// <param name="y2">Y2</param>
        /// <param name="height">Ширина стрелки</param>
        /// <param name="width">Длинна стрелки</param>
        /// <returns></returns>
        private PointF[] GetArrow(float x1, float y1, float x2, float y2, float height = 10, float width = 4)
        {
            PointF[] result = new PointF[3];
            //направляющий вектор отрезка
            var n = new PointF(x2 - x1, y2 - y1);
            //Длина отрезка
            var l = (float)Math.Sqrt(n.X * n.X + n.Y * n.Y);
            //Единичный вектор
            var v1 = new PointF(n.X / l, n.Y / l);
            //Длина стрелки
            n.X = x2 - v1.X * height;
            n.Y = y2 - v1.Y * height;
            //формирование элементов
            result[0] = new PointF(n.X + v1.Y * width, n.Y - v1.X * width);
            result[1] = new PointF(x2, y2);
            result[2] = new PointF(n.X - v1.Y * width, n.Y + v1.X * width);
            return result;
        }

        /// <summary>
        /// Пересчитывает координаты
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        private int[] ResetCoords(int X, int Y, int width, int height)
        {
            bool stateX = false;
            bool stateY = false;
            width /= 2;
            height /= 2;

            if (X < 0)
            {
                X += width;
                stateX = true;
            }
            if (Y < 0)
            {
                Y += height;
                stateY = true;
            }

            if (X > 0 && !stateX)
            {
                X += width;
                stateX = true;
            }
            if (Y > 0 && !stateY)
            {
                Y += height;
                stateY = true;
            }

            if (X == 0 && !stateX) X = width;
            if (Y == 0 && !stateY) Y = height;

            int[] coords = new int[2];
            coords[0] = X;
            coords[1] = Y;
            return coords;
        }
    }
}
