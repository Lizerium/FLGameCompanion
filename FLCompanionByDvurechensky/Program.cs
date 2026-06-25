/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 25 июня 2026 16:30:11
 * Version: 1.0.334
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
