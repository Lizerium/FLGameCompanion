/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 25 ноября 2026 11:14:43
 * Version: 1.0.487
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
