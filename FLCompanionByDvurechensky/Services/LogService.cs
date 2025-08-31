/*
 * Author: Nikolay Dvurechensky
 * Site: https://sites.google.com/view/dvurechensky
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 31 августа 2025 07:39:56
 * Version: 1.0.33
 */

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FLCompanionByDvurechensky.Services
{
    /// <summary>
    /// Сервис логирования
    /// </summary>
    public class LogService
    {
        /// <summary>
        /// Поле вывода информации интерфейса
        /// </summary>
        private RichTextBox Logger { get; set; }
        /// <summary>
        /// Время начала логирования
        /// </summary>
        private string LogDateTime { get; set; }
        /// <summary>
        /// Директория логирования
        /// </summary>
        private string DirLog
        {
            get
            {
                var path = "Log";
                if(!Directory.Exists(path)) Directory.CreateDirectory(path);
                else
                {
                    var directoryInfo = new DirectoryInfo(path);
                    if (directoryInfo.GetFiles().Length > 50)
                    {
                        Directory.Delete(path, true); //true - если директория не пуста (удалит и файлы и папки)
                        Directory.CreateDirectory(path);
                    }
                }

                return path;
            }
        }
        /// <summary>
        /// Путь до файла лога
        /// </summary>
        private string LogPath => Path.Combine(DirLog, $"{LogDateTime}_log.txt");

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">Объект вывода интерфейса</param>
        public LogService(RichTextBox logger)
        {
            Logger = logger;
            LogDateTime = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
        }

        /// <summary>
        /// Обычное событие
        /// </summary>
        /// <param name="message">текст сообщения</param>
        public void LogEvent(string message)
        {
            Logger.BeginInvoke(new Action(() =>
            {
                Logger.BackColor = Color.LightGray;
                Logger.SelectionColor = Color.Black;
                var msg = "-----> " + message + Environment.NewLine;
                Logger.AppendText(msg);
                File.AppendAllText(LogPath, msg);
                Logger.SelectionStart = Logger.TextLength;
                Logger.ScrollToCaret();
            }));
        }

        /// <summary>
        /// Событие ошибки
        /// </summary>
        /// <param name="message">текст ошибки</param>
        public void ErrorLogEvent(string message)
        {
            Logger.BeginInvoke(new Action(() =>
            {
                Logger.BackColor = Color.LightGray;
                Logger.SelectionColor = Color.Red;
                var msg = "!~ " + message + Environment.NewLine;
                Logger.AppendText(msg);
                File.AppendAllText(LogPath, msg);
                Logger.SelectionStart = Logger.TextLength;
                Logger.ScrollToCaret();
            }));
        }

        /// <summary>
        /// Событие предупреждения
        /// </summary>
        /// <param name="message">текст предупреждения</param>
        public void ErrorWarningEvent(string message)
        {
            Logger.BeginInvoke(new Action(() =>
            {
                Logger.BackColor = Color.LightGray;
                Logger.SelectionColor = Color.DarkBlue;
                var msg = "#~ " + message + Environment.NewLine;
                Logger.AppendText(msg);
                File.AppendAllText(LogPath, msg);
                Logger.SelectionStart = Logger.TextLength;
                Logger.ScrollToCaret();
            }));
        }
    }
}
