/*
 * Author: Nikolay Dvurechensky
 * Site: https://sites.google.com/view/dvurechensky
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 23 сентября 2025 17:15:43
 * Version: 1.0.56
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
