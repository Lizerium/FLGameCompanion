/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 04 июля 2026 08:47:00
 * Version: 1.0.344
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
