/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 25 июня 2026 16:30:11
 * Version: 1.0.334
 */

using System.Collections.Generic;

namespace FLCompanionByDvurechensky.Data
{
    /// <summary>
    /// Сюрпризик
    /// </summary>
    public class Loadout
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Loadout()
        {
            Cargo = new List<Cargo>();
        }

        /// <summary>
        /// Имя сюрприза
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Тип сюрприза
        /// </summary>
        public string Archetype { get; set; }
        /// <summary>
        /// Груз сюрприза
        /// </summary>
        public List<Cargo> Cargo { get; set; }
    }

    /// <summary>
    /// Объект данных груза
    /// </summary>
    public class Cargo
    {
        /// <summary>
        /// Имя груза
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Количество груза
        /// </summary>
        public int Count { get; set; }
    }
}
