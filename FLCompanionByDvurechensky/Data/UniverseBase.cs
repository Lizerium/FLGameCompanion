/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:38:00
 * Version: 1.0.27
 */

namespace FLCompanionByDvurechensky.Data
{
    /// <summary>
    /// Объект базы
    /// </summary>
    public class UniverseBase
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// ID системы
        /// </summary>
        public string System { get; set; }
        /// <summary>
        /// Ссылка на имя Базы
        /// </summary>
        public string DLL_Name { get; set; }
        /// <summary>
        /// Имя базы
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Адрес до INI
        /// </summary>
        public string INI { get; set; }
    }
}
