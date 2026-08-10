/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 10 августа 2026 06:52:06
 * Version: 1.0.381
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
