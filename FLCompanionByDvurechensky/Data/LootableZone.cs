/*
 * Author: Nikolay Dvurechensky
 * Site: https://sites.google.com/view/dvurechensky
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 14 октября 2025 10:15:06
 * Version: 1.0.77
 */

namespace FLCompanionByDvurechensky.Data
{
    /// <summary>
    /// Зона добычи груза
    /// </summary>
    public class LootableZone
    {
        /// <summary>
        /// Имя зоны астероидов
        /// </summary>
        public string ZoneName { get; set; }
        /// <summary>
        /// ID груза внутри этой зоны
        /// </summary>
        public string LootId { get; set; }
    }
}
