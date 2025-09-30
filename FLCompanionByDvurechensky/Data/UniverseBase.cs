/*
 * Author: Nikolay Dvurechensky
 * Site: https://sites.google.com/view/dvurechensky
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 30 сентября 2025 06:52:08
 * Version: 1.0.63
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
