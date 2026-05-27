/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 27 мая 2026 13:03:52
 * Version: 1.0.305
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
