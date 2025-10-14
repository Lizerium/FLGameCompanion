/*
 * Author: Nikolay Dvurechensky
 * Site: https://sites.google.com/view/dvurechensky
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 14 октября 2025 10:15:06
 * Version: 1.0.77
 */

using System;
using System.Collections.Generic;

namespace FLCompanionByDvurechensky.Data
{
    /// <summary>
    /// Объект системы
    /// </summary>
    public class UniverseSystem
    {
        /// <summary>
        /// Консруктор
        /// </summary>
        public UniverseSystem()
        {
            Objects = new List<ObjectSystem>();
            Zones = new List<ZoneSystem>(); 
        }

        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Имя 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Адрес до INI
        /// </summary>
        public string INI { get; set; }
        /// <summary>
        /// ОТключено (Позиция системы на карте в MultiUniverse)
        /// </summary>
        public int[] Pos { get; set; }
        /// <summary>
        /// Посетил ли
        /// </summary>
        public int Visit { get; set; }
        /// <summary>
        /// Ссылка на имя в DLL
        /// </summary>
        public string DLL_Name { get; set; }
        /// <summary>
        /// Ссылка на инфоркарту DLL
        /// </summary>
        public string DLL_InfoCard { get; set; }
        /// <summary>
        /// НЕ работает(позиция на карте теперь в MultiUniverse)
        /// </summary>
        public double NavMapScale { get; set; }
        /// <summary>
        /// Итоговый радиус системы
        /// </summary>
        public int Radius
        {
            get
            {
                double val;
                if (NavMapScale != 0)
                {
                    val = 131041.0 / NavMapScale;
                }
                else val = 131041.0;
                return (int)Math.Round(val, MidpointRounding.AwayFromZero);
            }
        }
        /// <summary>
        /// НПС говорят название объекта
        /// </summary>
        public string MsgIdPrefix { get; set; }
        /// <summary>
        /// Список объектов
        /// </summary>
        public List<ObjectSystem> Objects { get; set; }
        /// <summary>
        /// Список зон
        /// </summary>
        public List<ZoneSystem> Zones { get; set; }
    }
}
