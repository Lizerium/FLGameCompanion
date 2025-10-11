/*
 * Author: Nikolay Dvurechensky
 * Site: https://sites.google.com/view/dvurechensky
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 11 октября 2025 08:49:40
 * Version: 1.0.74
 */

using FLCompanionByDvurechensky.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FLCompanionByDvurechensky.Services
{
    /// <summary>
    /// Сервис глобальной работы системы
    /// </summary>
    public class SystemService
    {
        /// <summary>
        /// Все данные системы
        /// </summary>
        public Dictionary<string, UniverseSystem> UniverseSystemsData { get; set; }
        /// <summary>
        /// Список баз 
        /// </summary>
        public Dictionary<string, UniverseBase> UniverseBasesData { get; set; }
        /// <summary>
        /// ID Систнем
        /// </summary>
        public List<string> SystemsID { get; set; }
        /// <summary>
        /// Список предметов в игре
        /// </summary>
        public List<Equipment> Equipments { get; set; }
        /// <summary>
        /// ID системы - Список зон добычи ископаемых
        /// </summary>
        public Dictionary<string, List<LootableZone>> SysAsteroids { get; set; }
        /// <summary>
        /// ID - Name системы
        /// </summary>
        public Dictionary<string, string> SystemNamesID { get; set; }
        /// <summary>
        /// Name - Id систем
        /// </summary>
        public Dictionary<string, string> SystemsNameId { get; set; }
        /// <summary>
        /// Cписок контейнеров
        /// </summary>
        public List<Loadout> Loadouts { get; set; }
        /// <summary>
        /// Список путей от систем до систем
        /// </summary>
        public List<string> HollRoads { get; set; } 
        /// <summary>
        /// Массив очищенных айдишников систем для ComboBox
        /// </summary>
        public string[] ArraySystemsCombobox { get; set; }
        /// <summary>
        /// Обрабатывать русские наименования в алгоритмах
        /// </summary>
        public bool IsRussian { get; set; }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        private static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int LoadString(IntPtr hInstance, int ID, StringBuilder lpBuffer, int nBufferMax);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeLibrary(IntPtr hModule);
        private LogService LogService { get; set; }
        private string BaseId { get; set; }
        private string SystemID { get; set; }
        private int SurptiseNick_ID { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="isRussian">использование русского языка(TODO: по умолчанию только он)</param>
        /// <param name="logService">экземпляр сервиса логирования</param>
        public SystemService(bool isRussian, LogService logService)
        {
            UniverseSystemsData = new Dictionary<string, UniverseSystem>();
            UniverseBasesData = new Dictionary<string, UniverseBase>();
            SystemNamesID = new Dictionary<string, string>();
            SystemsNameId = new Dictionary<string, string>();
            SysAsteroids = new Dictionary<string, List<LootableZone>>();
            Equipments = new List<Equipment>();
            SystemsID = new List<string>();
            Loadouts = new List<Loadout>();
            HollRoads = new List<string>();
            IsRussian = isRussian;
            LogService = logService;
        }

        /// <summary>
        /// Получение и обработка информации
        /// </summary>
        /// <param name="systems">Список систем</param>
        /// <param name="roadStart">Выбор пути старта</param>
        /// <param name="roadStop">Выбор точки остановки</param>
        /// <param name="equipments">Выбор оборудования для поиска</param>
        /// <param name="logService">Экземпляр сервиса логирования</param>
        public void GetInfo(ComboBox systems, ComboBox roadStart, ComboBox roadStop, ComboBox equipments, LogService logService)
        {
            try
            {   
                var stopwatch = new Stopwatch();
                Task.Factory.StartNew(delegate ()
                {
                    Parallel.Invoke(() => GetAllSystems(),
                       () => GetAllEquipments(equipments),
                       () => GetAllAsteroids(),
                       () => GetAllLoadouts(),
                       () => GetAllBases(roadStart, roadStop, systems));
                });
            }
            catch (Exception exception)
            {
                logService.ErrorLogEvent(exception.Message);
            }
        }

        /// <summary>
        /// Получение списка грузов
        /// </summary>
        /// <param name="line"></param>
        /// <param name="logService"></param>
        private void GetLoadout(string line)
        {
            if (line.Contains("nickname ="))
            {
                SurptiseNick_ID++;
                var nickName = (line.Substring(10, line.Length - 10)).Trim().ToLower();
                Loadouts.Add(new Loadout() { Name = nickName });
            }
            if (line.Contains("archetype ="))
            {
                var arch = (line.Substring(11, line.Length - 11)).Trim().ToLower();
                Loadouts[SurptiseNick_ID - 1].Archetype = arch;
            }
            if (line.Contains("cargo ="))
            {
                var cargoNameCount = (line.Substring(7, line.Length - 7)).Trim().ToLower();
                var name = cargoNameCount.Substring(0, cargoNameCount.IndexOf(','));
                var count = cargoNameCount.Substring(cargoNameCount.IndexOf(',') + 1, cargoNameCount.Length - (cargoNameCount.IndexOf(',') + 1));
                int.TryParse(count, out int res);
                Loadouts[SurptiseNick_ID - 1].Cargo.Add(new Cargo()
                {
                    Name = name,
                    Count = res
                });
            }
        }

        /// <summary>
        /// Получение информации о системе
        /// </summary>
        /// <param name="line">строка с данными</param>
        private void GetSystemDataToFile(string line)
        {
            if (line.Contains("nickname"))
            {
                SystemID = (line.Substring(10, line.Length - 10)).Trim().ToLower();
                UniverseSystemsData.Add(SystemID, new UniverseSystem()
                {
                    Id = SystemID
                });
            }
            if (line.Contains("strid_name"))
            {
                var dll_name = (line.Substring(12, line.Length - 12)).Trim();
                UniverseSystemsData[SystemID].DLL_Name = dll_name;
                var id = SystemID.ToLower();
                string name = string.Empty;
                if (SystemNamesID.ContainsKey(id)) name = SystemNamesID[id];
                else name = id;
                UniverseSystemsData[SystemID].Name = name;
            }
            if (line.Contains("visit"))
            {
                var visit = (line.Substring(7, line.Length - 7)).Trim();
                UniverseSystemsData[SystemID].Visit = int.Parse(visit);
            }
            if (line.Contains("ids_info"))
            {
                var dll_ids_name = (line.Substring(10, line.Length - 10)).Trim();
                UniverseSystemsData[SystemID].DLL_InfoCard = dll_ids_name;
            }
            if (line.Contains("file"))
            {
                var file = (line.Substring(6, line.Length - 6)).Trim();
                UniverseSystemsData[SystemID].INI = file;
                GetSystemInfo(SystemID);
            }
            if (line.Contains("NavMapScale"))
            {
                var nav = (line.Substring(13, line.Length - 13)).Trim();
                IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                UniverseSystemsData[SystemID].NavMapScale = double.Parse(nav, formatter);
            }
        }

        /// <summary>
        /// Получение списка баз системы
        /// </summary>
        /// <param name="line">строка с информацией</param>
        private void GetBaseDataToFile(string line)
        {
            if (line.Contains("nickname"))
            {
                BaseId = (line.Substring(10, line.Length - 10)).Trim().ToLower();
                UniverseBasesData.Add(BaseId, new UniverseBase()
                {
                    Id = BaseId
                });
            }
            if (line.Contains("strid_name"))
            {
                var dll_name = (line.Substring(12, line.Length - 12)).Trim();
                if (dll_name.Contains(";"))
                    dll_name = dll_name.Substring(0, dll_name.IndexOf(';'));
                UniverseBasesData[BaseId].DLL_Name = dll_name;
                var names = GetNameSystem(int.Parse(dll_name), BaseId);
                foreach (var name in names)
                {
                    UniverseBasesData[BaseId].Name += name + " | ";
                }
            }
        }

        /// <summary>
        /// Читает конфигурационный файл системы
        /// </summary>
        /// <param name="systemId">ID системы</param>

        private void GetSystemInfo(string systemId)
        {
            var sr = new StreamReader(UniverseSystemsData[systemId].INI);
            var data = sr.ReadLine();
            var Object = false;
            var Zone = false;
            while (data != null)
            {
                if (data.Contains("Object"))
                {
                    Object = true;
                    Zone = false;
                    if (UniverseSystemsData[systemId].Objects == null)
                        UniverseSystemsData[systemId].Objects = new List<ObjectSystem>();
                }
                if (data.Contains("Zone"))
                {
                    Object = false;
                    Zone = true;
                }
                if (Object)
                {
                    if (data.Contains("nickname"))
                    {
                        var id_name = (data.Substring(10, data.Length - 10)).Trim();
                        UniverseSystemsData[systemId].Objects.Add(new ObjectSystem()
                        {
                            ID = id_name
                        });
                    }
                    if (data.Contains("pos ="))
                    {
                        var position = (data.Substring(5, data.Length - 5)).Trim();
                        if (position.Contains(";"))
                            position = (position.Substring(0, position.IndexOf(';')).Trim());
                        int[] pos = position.Split(',').Select(n =>
                        {
                            int val = 0;
                            n = n.Trim();
                            var state = int.TryParse(n, out val);
                            if (state == false)
                            {
                                double td = 0;
                                if (n.Contains('.'))
                                {
                                    IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                                    td = double.Parse(n, formatter);
                                    int i = (int)Math.Round(td, MidpointRounding.AwayFromZero);
                                    val = i;
                                    return val;
                                }
                                else
                                {
                                    File.AppendAllText("log.txt", "Error Parse Position - " + position + " - " + UniverseSystemsData[systemId].INI + "\n");
                                    return 0;
                                }
                            }
                            else return val;
                        }).ToArray();
                        if(pos == null)
                        {
                            LogService.ErrorLogEvent(systemId);
                        }
                        else UniverseSystemsData[systemId].Objects[UniverseSystemsData[systemId].Objects.Count - 1].Pos = pos;
                    }
                    if (data.Contains("base ="))
                    {
                        var baseID = (data.Substring(6, data.Length - 6)).Trim();
                        UniverseSystemsData[systemId].Objects[UniverseSystemsData[systemId].Objects.Count - 1].BaseID = baseID;
                    }
                    if (data.Contains("ids_name ="))
                    {
                        var idsName = (data.Substring(10, data.Length - 10)).Trim();
                        UniverseSystemsData[systemId].Objects[UniverseSystemsData[systemId].Objects.Count - 1].IdsName = idsName;
                    }
                    if (data.Contains("archetype ="))
                    {
                        var archetype = (data.Substring(11, data.Length - 11)).Trim();
                        UniverseSystemsData[systemId].Objects[UniverseSystemsData[systemId].Objects.Count - 1].Archetype = archetype;
                    }
                    if (data.Contains("loadout ="))
                    {
                        var loadout = (data.Substring(9, data.Length - 9)).Trim();
                        UniverseSystemsData[systemId].Objects[UniverseSystemsData[systemId].Objects.Count - 1].Loadout = loadout;
                    }
                    if (data.Contains("goto =") && !data.Contains(';'))
                    {
                        var loadout = (data.Substring(6, data.Length - 6)).Trim().ToLower();
                        var idS = loadout.Substring(0, loadout.IndexOf(','));
                        var nameS = SystemNamesID[idS];
                        UniverseSystemsData[systemId].Objects[UniverseSystemsData[systemId].Objects.Count - 1].Goto = nameS;
                        UniverseSystemsData[systemId].Objects[UniverseSystemsData[systemId].Objects.Count - 1].GotoID = idS;
                    }
                }
                if(Zone)
                {
                    if (data.Contains("nickname ="))
                    {
                        var id_name = (data.Substring(10, data.Length - 10)).Trim();
                        UniverseSystemsData[systemId].Zones.Add(new ZoneSystem()
                        {
                            ID = id_name
                        });
                    }
                    if (data.Contains("pos ="))
                    {
                        var position = (data.Substring(5, data.Length - 5)).Trim();
                        if (position.Contains(";"))
                            position = (position.Substring(0, position.IndexOf(';')).Trim());
                        int[] pos = position.Split(',').Select(n =>
                        {
                            var val = 0;
                            n = n.Trim();
                            var state = int.TryParse(n, out val);
                            if (state == false)
                            {
                                double td = 0;
                                if (n.Contains('.'))
                                {
                                    IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                                    td = double.Parse(n, formatter);
                                    int i = (int)Math.Round(td, MidpointRounding.AwayFromZero);
                                    val = i;
                                    return val;
                                }
                                else
                                {
                                    File.AppendAllText("log.txt", "Error Parse Position - " + position + " - " + UniverseSystemsData[systemId].INI + "\n");
                                    return 0;
                                }
                            }
                            else return val;
                        }).ToArray();
                        if (pos == null)
                        {
                            LogService.ErrorLogEvent(systemId);
                        }
                        else UniverseSystemsData[systemId].Zones[UniverseSystemsData[systemId].Zones.Count - 1].Pos = pos;
                    }
                }
                data = sr.ReadLine();
            }
        }

        /// <summary>
        /// Получение человекочитаемого наименования
        /// </summary>
        /// <param name="id">идентификатор элемента объекта</param>
        /// <param name="baseId">идентификатор объекта</param>
        /// <returns>список наименований</returns>
        public List<string> GetNameSystem(int id, string baseId)
        {
            string[] dlls = new string[] { "NameResources.dll", "SBM.dll", "SBM2.dll", "SBM3.dll" };
            var names = new List<string>();
            foreach(string dll in dlls)
            {
                string name = ExtractStringFromDLL(dll, id);
                if (!string.IsNullOrEmpty(name))
                {
                    if(!names.Contains(name))
                    {
                        names.Add(name);
                    }
                }
            }
            if(names.Count == 0)
            {
                names.Add("НЕТ НАЗВАНИЙ");
                LogService.ErrorLogEvent($"[Имя объекта системы][{baseId}] id: " + id + " - не содержит названия");
            }
            return names;
        }

        /// <summary>
        /// Обновление содержимого ключевых элементов Combobox
        /// </summary>
        private void LoadComboboxData(ComboBox roadStart, ComboBox roadStop, ComboBox systems)
        {
            int resultCountSystem = 0;
            var countCurrSys = 0;
            ArraySystemsCombobox = new string[UniverseSystemsData.Count];
            var dirInfoSystems = new DirectoryInfo("SYSTEMS");
            var dirInfoArray = dirInfoSystems.GetDirectories();
            //формирую список идентификаторов систем
            foreach (var dirInfo in dirInfoArray)
            {
                var dirName = dirInfo.ToString().ToLower();
                if (UniverseSystemsData.ContainsKey(dirName))
                {
                    resultCountSystem++;
                    var roadStartData = new ComboBoxItem();
                    roadStartData.Text = (string.IsNullOrEmpty(UniverseSystemsData[dirName].Name) ? UniverseSystemsData[dirName].Id : UniverseSystemsData[dirName].Name);
                    roadStartData.ID = UniverseSystemsData[dirName].Id;
                    roadStart.BeginInvoke(new Action(() =>
                    {
                        roadStart.Items.Add(roadStartData);
                        roadStart.SelectedIndex = 0;
                    }));
                    var roadStopData = new ComboBoxItem();
                    roadStopData.Text = (string.IsNullOrEmpty(UniverseSystemsData[dirName].Name) ? UniverseSystemsData[dirName].Id : UniverseSystemsData[dirName].Name);
                    roadStopData.ID = UniverseSystemsData[dirName].Id;
                    roadStop.BeginInvoke(new Action(() =>
                    {
                        roadStop.Items.Add(roadStopData);
                        if(roadStop.Items.Count >= 2) roadStop.SelectedIndex = 1;
                    }));

                    if (UniverseSystemsData[dirName].Name.Length == 0)
                    {
                        systems.BeginInvoke(new Action(() =>
                        {
                            systems.Items.Add(UniverseSystemsData[dirName].Id);
                        }));
                    }
                    else
                    {
                        systems.BeginInvoke(new Action(() =>
                        {
                            systems.Items.Add(UniverseSystemsData[dirName].Name + " | " + UniverseSystemsData[dirName].Id);
                            systems.SelectedIndex = 0;
                        }));
                    }
                    ArraySystemsCombobox[countCurrSys] = dirName;
                    countCurrSys++;
                    SystemsID.Add(UniverseSystemsData[dirName].Id);
                }
                else
                {
                    resultCountSystem++;
                    LogService.ErrorWarningEvent("[" + resultCountSystem + "]  " + dirName + " - не является системой...");
                }
            }
            ArraySystemsCombobox = ArraySystemsCombobox.Where(x => x != null).ToArray();
            LogService.LogEvent("Обновление данных в интерфейсе о системах завершено");

            LoadRoute();
        }

        /// <summary>
        /// Обработка маршрутов между системами (все гиперпереходы)
        /// </summary>
        private void LoadRoute()
        {
            foreach (var sys in ArraySystemsCombobox)
            {
                foreach (var elem in UniverseSystemsData[sys].Objects.FindAll((el) => !el.ID.Contains('=') && el.ID.ToLower().Contains(sys.ToLower() + "_to")))
                {
                    var name = elem.ID;
                    var destiny = name.Substring(name.IndexOf('_') + 4, name.Length - 4 - sys.Length);
                    if (destiny.IndexOf('_') != -1)
                        destiny = destiny.Substring(0, destiny.IndexOf('_'));
                    //проверяем goto
                    if (elem.GotoID == null || elem.GotoID.ToLower().Contains(sys.ToLower())) continue;

                    var res = sys.ToLower() + "=" + elem.GotoID.ToLower();
                    if (!res.Contains("police01"))
                        HollRoads.Add(res);
                }
                HollRoads.Add("aod01=hu04"); //система ангелов тьмы
                HollRoads.Add("hu04=aod01"); //система ангелов тьмы
                HollRoads.Add("dream_system01=hi03"); //система грёз
                HollRoads.Add("hi03=dream_system01"); //система грёз
            }

            //формирование словаря для маршрутизации в русском переводе
            for (int i = 0; i < ArraySystemsCombobox.Length; i++)
            {
                if (!SystemsNameId.ContainsKey(UniverseSystemsData[ArraySystemsCombobox[i]].Name))
                    SystemsNameId.Add(UniverseSystemsData[ArraySystemsCombobox[i]].Name, ArraySystemsCombobox[i]);
                else
                    LogService.LogEvent(UniverseSystemsData[ArraySystemsCombobox[i]].Name);
            }

            LogService.LogEvent("Список маршрутов определён и обновлён в интерфейсе");
        }

        /// <summary>
        /// Чтение списка баз и информации о системе
        /// </summary>
        private void GetAllBases(ComboBox roadStart, ComboBox roadStop, ComboBox systems)
        {
            using (var reader = new StreamReader("universe.ini"))
            {
                var line = string.Empty;
                bool systemState = false, baseState = false;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.Contains("[system]"))
                        {
                            systemState = true;
                            baseState = false;
                        }
                        if (line.Contains("[Base]"))
                        {
                            systemState = false;
                            baseState = true;
                        }
                        if (systemState)
                            GetSystemDataToFile(line);
                        if (baseState)
                            GetBaseDataToFile(line);
                    }
                }
            }

            LogService.LogEvent($"Вcя информация о {UniverseSystemsData.Count} системах игры прочитана");
            LogService.LogEvent($"Все базы игры прочитаны: {UniverseBasesData.Count}");

            LoadComboboxData(roadStart, roadStop, systems);
        }

        /// <summary>
        /// Получает список грузов
        /// </summary>
        private void GetAllLoadouts()
        {
            //получаю список грузов
            using (var reader = new StreamReader("loadouts.ini"))
            {
                var line = string.Empty;
                bool load = false;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        line = line.ToLower();
                        if (line.Contains("[loadout]"))
                            load = true;
                        if (load) GetLoadout(line);
                    }
                }
            }

            LogService.LogEvent($"Все грузы игры прочитаны: {Loadouts.Count}");
        }

        /// <summary>
        /// Получение списка систем
        /// </summary>
        private void GetAllSystems()
        {
            using (var reader = new StreamReader("systems.ini"))
            {
                var line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var idS = line.Substring(0, line.IndexOf('='));
                        var nameS = line.Substring(line.IndexOf('=') + 1);
                        SystemNamesID.Add(idS, nameS);
                    }
                }
            }

            LogService.LogEvent($"Все системы игры прочитаны: {SystemNamesID.Count}");
        }

        /// <summary>
        /// Получаение всех наименований оборудования в игре
        /// </summary>
        private void GetAllEquipments(ComboBox equipments)
        {
            using (var reader = new StreamReader("equipments.ini"))
            {
                var line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var tmp1 = line.Substring(line.IndexOf(',') + 1, line.Length - (line.IndexOf(',') + 1));
                        var tmp2 = tmp1.Substring(tmp1.IndexOf(',') + 1, tmp1.Length - (tmp1.IndexOf(',') + 1));
                        var tmp3 = tmp2.Substring(tmp2.IndexOf(',') + 1, tmp2.Length - (tmp2.IndexOf(',') + 1));
                        var tmp4 = tmp3.Substring(tmp3.IndexOf(',') + 1, tmp3.Length - (tmp3.IndexOf(',') + 1));
                        var Id = tmp4.Substring(0, tmp4.IndexOf(',')).Trim();
                        var tmp5 = tmp4.Substring(tmp4.IndexOf(',') + 1, tmp4.Length - (tmp4.IndexOf(',') + 1));
                        var Name = tmp5.Substring(0, tmp5.IndexOf(',')).Trim();
                        Equipments.Add(new Equipment() { Id = Id, Name = Name });
                    }
                }
            }

            LogService.LogEvent($"Всё оброрудование игры прочитано: {Equipments.Count}");
        }

        /// <summary>
        /// Получаение всех данных по астероидным полям
        /// </summary>
        private void GetAllAsteroids()
        {
            var dirInfoSystems = new DirectoryInfo("ASTEROIDS");
            var files = dirInfoSystems.GetFiles();

            foreach(var file in files)
            {
                if (!file.Name.Contains('_')) continue;

                var idSys = file.Name.Substring(0, file.Name.IndexOf('_'));
                var path = Path.Combine("ASTEROIDS", file.Name);

                //получаю список груза
                using (var reader = new StreamReader(path))
                {
                    var line = string.Empty;
                    bool loot = false;
                    var nameZone = string.Empty;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            if (line.Contains("[LootableZone]")) loot = true;
                            if (line.Contains("[TexturePanels]")) loot = false;
                            if (loot)
                            {
                                //Id зоны
                                if(line.Contains("zone ="))
                                {
                                    nameZone = line.Substring(line.IndexOf('=') + 1, line.Length - (line.IndexOf('=') + 1)).Trim();
                                }
                                //Груз астероида
                                if(line.Contains("asteroid_loot_commodity ="))
                                {
                                    var lootes = line.Substring(line.IndexOf('=') + 1, line.Length - (line.IndexOf('=') + 1)).Trim();

                                    //Если система с таким ID существует, то заполняем инфу в неё
                                    if (SysAsteroids.ContainsKey(idSys))
                                    {
                                        SysAsteroids[idSys].Add(new LootableZone()
                                        {
                                            LootId = lootes.ToLower(),
                                            ZoneName = nameZone.ToLower()
                                        });
                                    }
                                    else //создаём такую систему со своими зонами астероидов
                                    {

                                        SysAsteroids.Add(idSys, new List<LootableZone>() { new LootableZone()
                                        {
                                            LootId = lootes.ToLower(),
                                            ZoneName = nameZone.ToLower()
                                        }});
                                    }
                                }
                            }
                        }
                    }
                }
            }

            LogService.LogEvent($"Все астероидные поля прочитаны: {SysAsteroids.Count}");
        }

        /// <summary>
        /// Чтение элемента из DLL
        /// </summary>
        /// <param name="file">Адрес до файла</param>
        /// <param name="number">Номер элемента</param>
        /// <returns>строка с данными</returns>
        private string ExtractStringFromDLL(string file, int number)
        {
            var lib = LoadLibrary(file);
            var resultBuilder = new StringBuilder(2048);
            LoadString(lib, number, resultBuilder, resultBuilder.Capacity);
            FreeLibrary(lib);
            return resultBuilder.ToString();
        }
    }
}
