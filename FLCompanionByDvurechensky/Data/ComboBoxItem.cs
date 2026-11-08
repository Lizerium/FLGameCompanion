/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 08 ноября 2026 13:29:44
 * Version: 1.0.470
 */

namespace FLCompanionByDvurechensky.Data
{
    /// <summary>
    /// Кастомизированный объект Combobox
    /// </summary>
    public class ComboBoxItem
    {
        /// <summary>
        /// Текстовое поле
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string ID { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
