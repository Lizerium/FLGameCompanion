/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 23 июня 2026 15:54:22
 * Version: 1.0.332
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
