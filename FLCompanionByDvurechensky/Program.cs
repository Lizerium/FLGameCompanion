/*
 * Author: Nikolay Dvurechensky
 * Site: https://sites.google.com/view/dvurechensky
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 31 декабря 2025 10:57:49
 * Version: 1.0.155
 */

using System;
using System.Windows.Forms;

namespace FLCompanionByDvurechensky
{
    /// <summary>
    /// Точка входа
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FreelancerCompanionDvurechensky());
        }
    }
}
