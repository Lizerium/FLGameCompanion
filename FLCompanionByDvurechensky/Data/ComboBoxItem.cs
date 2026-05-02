/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 02 мая 2026 19:16:20
 * Version: 1.0.280
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
