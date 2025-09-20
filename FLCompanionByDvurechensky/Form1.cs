/*
 * Author: Nikolay Dvurechensky
 * Site: https://sites.google.com/view/dvurechensky
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 20 сентября 2025 14:15:34
 * Version: 1.0.53
 */

using FLCompanionByDvurechensky.Data;
using FLCompanionByDvurechensky.Services;
using GraphX.Common.Enums;
using GraphX.Controls.Models;
using GraphX.Controls;
using GraphX.Logic.Algorithms.OverlapRemoval;
using GraphX.Logic.Models;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsProject;
using System.Reflection;
using System.Threading.Tasks;

namespace FLCompanionByDvurechensky
{
    /// <summary>
    /// Главный класс
    /// </summary>
    public partial class FreelancerCompanionDvurechensky : Form
    {
        public LogService LogService { get; set; }
        private DrawService DrawService { get; set; }
        private SystemService SystemService { get; set; }
        private List<ObjectSystem> ObjectPoints { get; set; } 
        private Bitmap ImageMap { get; set; }
        private double KeyResize { get; set; }
        private double KeyOverSize { get; set; }
        private string CurrentSystem { get; set; }
        private ZoomControl Zoomctrl { get; set; }
        private GraphAreaExample GArea { get; set; }
        private GXLogicCore<DataVertex, DataEdge, BidirectionalGraph<DataVertex, DataEdge>> gXLogic { get; set; }
        private string[] VerticleRoad { get; set; }
        private string[] GererateRoads { get; set; }

        public FreelancerCompanionDvurechensky()
        {
            //строительство формы
            InitializeComponent();

            //старт сервиса логирования системы
            LogService = new LogService(LoggerRichTextBox);

            //инициализация приближения и отдаления карты колесом мыши
            Map.MouseWheel += Map_MouseWheel;

            //ожидание открытия 
            this.Shown += FreelancerCompanionDvurechensky_Show;
        }

        /// <summary>
        /// Событие после первого отображения формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FreelancerCompanionDvurechensky_Show(object sender, EventArgs e)
        {
            //загрузка данных
            InitializeSystems();
        }

        /// <summary>
        /// Доп. операции над картой колёсиком мыши (приближение/отдаление)
        /// </summary>
        /// <param name="sender">#</param>
        /// <param name="e">Мышь</param>
        private void Map_MouseWheel(object sender, MouseEventArgs e)
        {
            if(e.Delta > 0)
            {
                Map.Location = new Point(Map.Location.X - 10, Map.Location.Y - 10);
                Map.Width += 10;
                Map.Height += 10;
            }
            else
            {
                Map.Location = new Point(Map.Location.X + 10, Map.Location.Y + 10);
                Map.Width -= 10;
                Map.Height -= 10;
            }
        }

        /// <summary>
        /// Загрузка данных систем
        /// </summary>
        private void InitializeSystems()
        {
            checkBoxRusNames.Checked = true;
            SystemService = new SystemService(isRussian: checkBoxRusNames.Checked, logService: LogService);
            DrawService = new DrawService(5, 3);
            SystemService.GetInfo(comboBoxSystems, comboBoxRoadFirst, comboBoxRoadLast, comboBoxSearch, LogService);
            labelSystemss.Text = comboBoxSystems.Items.Count.ToString();
        }

        /// <summary>
        /// Отрисовка первоначального вида карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Map_Paint(object sender, PaintEventArgs e)
        {
            int w = Map.ClientSize.Width / 2;
            int h = Map.ClientSize.Height / 2;
            //Смещение начала координат в центр PictureBox
            e.Graphics.TranslateTransform(w, h);
        }

        //первая координата это X отрицательно влево - положительно вправо
        //вторая коордианат это Y отрицательно вверх - положительно вниз
        //третья координата это Z отрицательно вниз - положительно вверх
        private void comboBoxSystems_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var comboBox = (ComboBox)sender;
            Map.Location = new Point(0, 0);
            Map.Width = 800;
            Map.Height = 800;
            flowLayoutPanelNames.Controls.Clear();
            ObjectPoints = new List<ObjectSystem>();
            CurrentSystem = SystemService.SystemsID[comboBox.SelectedIndex];
            KeyResize = (double)400 / SystemService.UniverseSystemsData[CurrentSystem].Radius;
            ImageMap = new Bitmap(Map.Width, Map.Height);
            Map.Image = ImageMap;
            checkBoxContainers.Checked = false;
            checkBoxBases.Checked = false;

            var listMaxTmp = new List<int>();
            var radius = SystemService.UniverseSystemsData[CurrentSystem].Radius;

            foreach (var bases in SystemService.UniverseSystemsData[CurrentSystem].Objects.FindAll((objectEl) => !objectEl.ID.ToLower().Contains("zone_") && !objectEl.ID.ToLower().Contains("_sun")))
            {
                listMaxTmp.Add(Math.Abs(bases.Pos[0]));
                listMaxTmp.Add(Math.Abs(bases.Pos[1]));
                listMaxTmp.Add(Math.Abs(bases.Pos[2]));
            }

            var maxCoordSystem = listMaxTmp.Max();
            if (maxCoordSystem > radius)
            {
                KeyOverSize = Math.Round((double)maxCoordSystem / radius, 5);
                KeyResize /= KeyOverSize;
                KeyResize = Math.Round(KeyResize, 5);
            }
            else KeyOverSize = 0;

            RepaintAxis();
            LogService.LogEvent($"Open [{comboBox.SelectedIndex + 1}] {comboBox.Text}");
        }

        private void RepaintAxis()
        {
            var gr = Graphics.FromImage(Map.Image);
            ClearMap(gr);
            //отрисовка осей
            int w = Map.ClientSize.Width / 2;
            int h = Map.ClientSize.Height / 2;
            //Смещение начала координат в центр PictureBox
            gr.TranslateTransform(w, h);
            int newSizeW = 0;
            int newSizeH = 0;
            if (KeyOverSize > 0) newSizeW = (int)Math.Round((double)(w / KeyOverSize), MidpointRounding.AwayFromZero);
            else newSizeW = w;
            if (KeyOverSize > 0) newSizeH = (int)Math.Round((double)(double)(h / KeyOverSize), MidpointRounding.AwayFromZero);
            else newSizeH = h;
            //X
            DrawService.DrawXAxis(new Point(-newSizeW, newSizeW), new Point(newSizeW, newSizeW), gr, false);
            DrawService.DrawXAxis(new Point(-newSizeW, 0), new Point(newSizeW, 0), gr, false);
            DrawService.DrawXAxis(new Point(-newSizeW, newSizeW - newSizeW / 2), new Point(newSizeW, newSizeW - newSizeW / 2), gr, false);
            DrawService.DrawXAxis(new Point(-newSizeW, newSizeW - (newSizeW / 2 + newSizeW / 4)), new Point(newSizeW, newSizeW - (newSizeW / 2 + newSizeW / 4)), gr, false);
            DrawService.DrawXAxis(new Point(-newSizeW, newSizeW - newSizeW / 4), new Point(newSizeW, newSizeW - newSizeW / 4), gr, false);
            DrawService.DrawXAxis(new Point(-newSizeW, -newSizeW), new Point(newSizeW, -newSizeW), gr, false);
            DrawService.DrawXAxis(new Point(-newSizeW, -(newSizeW - newSizeW / 2)), new Point(newSizeW, -(newSizeW - newSizeW / 2)), gr, false);
            DrawService.DrawXAxis(new Point(-newSizeW, -(newSizeW - (newSizeW / 2 + newSizeW / 4))), new Point(newSizeW, -(newSizeW - (newSizeW / 2 + newSizeW / 4))), gr, false);
            DrawService.DrawXAxis(new Point(-newSizeW, -(newSizeW - newSizeW / 4)), new Point(newSizeW, -(newSizeW - newSizeW / 4)), gr, false);
            //Y
            DrawService.DrawYAxis(new Point(-newSizeH, newSizeH), new Point(-newSizeH, -newSizeH), gr, false);
            DrawService.DrawYAxis(new Point(0, newSizeH), new Point(0, -newSizeH), gr, false);
            DrawService.DrawYAxis(new Point(newSizeW - newSizeW / 2, newSizeH), new Point(newSizeW - newSizeW / 2, -newSizeH), gr, false);
            DrawService.DrawYAxis(new Point(newSizeW - (newSizeW / 2 + newSizeW / 4), newSizeH), new Point(newSizeW - (newSizeW / 2 + newSizeW / 4), -newSizeH), gr, false);
            DrawService.DrawYAxis(new Point(newSizeW - newSizeW / 4, newSizeH), new Point(newSizeW - newSizeW / 4, -newSizeH), gr, false);
            DrawService.DrawYAxis(new Point(newSizeH, newSizeH), new Point(newSizeH, -newSizeH), gr, false);
            DrawService.DrawYAxis(new Point(-(newSizeW - newSizeW / 2), newSizeH), new Point(-(newSizeW - newSizeW / 2), -newSizeH), gr, false);
            DrawService.DrawYAxis(new Point(-(newSizeW - (newSizeW / 2 + newSizeW / 4)), newSizeH), new Point(-(newSizeW - (newSizeW / 2 + newSizeW / 4)), -newSizeH), gr, false);
            DrawService.DrawYAxis(new Point(-(newSizeW - newSizeW / 4), newSizeH), new Point(-(newSizeW - newSizeW / 4), -newSizeH), gr, false);

            checkBoxAll.Checked = false;
            checkBoxBases.Checked = false;
            checkBoxContainers.Checked = false;
            checkBoxHoll.Checked = false;
        }

        private void ClearMap(Graphics graphics)
        {
            DrawService.DrawPoint(-Map.Width, -Map.Height, Map.Width, Map.Height, graphics, Color.White, Map.Width * 2, Map.Height * 2);
        }

        private void checkBoxBases_CheckedChanged(object sender, EventArgs e)
        {
            var checkBoxBases = (CheckBox)sender;
            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                var counter = 0;
                foreach (var baseID in SystemService.UniverseSystemsData[CurrentSystem].Objects.FindAll((baseId) => baseId.BaseID != null).ToArray())
                {
                    int x = (int)Math.Round(KeyResize * baseID.Pos[0], MidpointRounding.AwayFromZero);
                    int y = (int)Math.Round(KeyResize * baseID.Pos[2], MidpointRounding.AwayFromZero);
                    int[] mapPos = new int[3];
                    mapPos[0] = x;
                    mapPos[1] = y;
                    mapPos[2] = baseID.Pos[1];
                    baseID.MapPos = mapPos;
                    if (!ObjectPoints.Contains(baseID))
                        ObjectPoints.Add(baseID);

                    //рисую или стираю точки на карте
                    if (checkBoxBases.Checked == true)
                    {
                        counter++;
                        //Формирую вывод UI
                        var button = new Button();
                        button.Width = 231;
                        button.Height = 30;
                        var tooltip = new ToolTip();
                        tooltip.SetToolTip(button, $"Z: [{baseID.Pos[1]}] X: [{baseID.Pos[0]}] Y: [{baseID.Pos[2]}]\n" + ((baseID.Archetype != null) ? baseID.Archetype : string.Empty));
                        button.MouseEnter += Base_MouseEnter;
                        button.MouseLeave += Base_MouseLeave;
                        button.Click += OpenPos_Click;
                        button.Name = baseID.BaseID;
                        var nameTmp = baseID.BaseID.ToLower();
                        button.Text = (!string.IsNullOrEmpty(SystemService.UniverseBasesData[nameTmp].Name)) ? "[" + counter + "]" + SystemService.UniverseBasesData[nameTmp].Name : "[" + counter + "]" + baseID.ID;
                        flowLayoutPanelNames.Controls.Add(button);
                    }
                    else flowLayoutPanelNames.Controls.Clear();

                    //рисую или стираю точки на карте
                    if (checkBoxBases.Checked == true) DrawService.DrawPoint(x, y, Map.Width, Map.Height, gr, Color.Blue, 6, 6);
                    else RepaintAxis();
                }
                Map.Image = ImageMap;
            }
        }

        private void OpenPos_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            textBoxX.Text = ObjectPoints.Find((obj) => obj.ID.Contains(btn.Name) || (obj.BaseID != null && obj.BaseID.Contains(btn.Name))).Pos[0].ToString();
            textBoxY.Text = ObjectPoints.Find((obj) => obj.ID.Contains(btn.Name) || (obj.BaseID != null && obj.BaseID.Contains(btn.Name))).Pos[1].ToString();
            textBoxZ.Text = ObjectPoints.Find((obj) => obj.ID.Contains(btn.Name) || (obj.BaseID != null && obj.BaseID.Contains(btn.Name))).Pos[2].ToString();
        }


        private void Holl_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                var obj = ObjectPoints.Find((t) => t.ID.Contains(btn.Name));
                var index = Array.IndexOf(SystemService.ArraySystemsCombobox, obj.GotoID);
                comboBoxSystems.SelectedIndex = index;
            }
            else
            {
                textBoxX.Text = ObjectPoints.Find((obj) => obj.ID.Contains(btn.Name) || (obj.BaseID != null && obj.BaseID.Contains(btn.Name))).Pos[0].ToString();
                textBoxY.Text = ObjectPoints.Find((obj) => obj.ID.Contains(btn.Name) || (obj.BaseID != null && obj.BaseID.Contains(btn.Name))).Pos[1].ToString();
                textBoxZ.Text = ObjectPoints.Find((obj) => obj.ID.Contains(btn.Name) || (obj.BaseID != null && obj.BaseID.Contains(btn.Name))).Pos[2].ToString();
            }
        }


        private void checkBoxContainers_CheckedChanged(object sender, EventArgs e)
        {
            var checkBoxContainers = (CheckBox)sender;

            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                var counter = 0;
                foreach (var objectElement in SystemService.UniverseSystemsData[CurrentSystem].Objects)
                {
                    var id = objectElement.ID.ToLower();
                    bool ok = false;
                    if (objectElement.Loadout != null && SystemService.Loadouts.Find((l) => l.Name.Contains(objectElement.Loadout.ToLower())) != null) ok = true;

                    if (!ok) continue;

                    int x = (int)Math.Round(KeyResize * objectElement.Pos[0], MidpointRounding.AwayFromZero);
                    int y = (int)Math.Round(KeyResize * objectElement.Pos[2], MidpointRounding.AwayFromZero);
                    int[] mapPos = new int[3];
                    mapPos[0] = x;
                    mapPos[1] = y;
                    mapPos[2] = objectElement.Pos[1];
                    objectElement.MapPos = mapPos;
                    if (!ObjectPoints.Contains(objectElement))
                        ObjectPoints.Add(objectElement);

                    //рисую или стираю точки на карте
                    if (checkBoxContainers.Checked == true)
                    {
                        counter++;
                        //Формирую вывод UI
                        var button = new Button();
                        button.Width = 231;
                        button.Height = 30;
                        var tooltip = new ToolTip();
                        tooltip.SetToolTip(button, $"Z: [{objectElement.Pos[1]}] X: [{objectElement.Pos[0]}] Y: [{objectElement.Pos[2]}]\n" + ((objectElement.Archetype != null) ? objectElement.Archetype : string.Empty));
                        button.MouseEnter += Container_MouseEnter;
                        button.MouseLeave += Container_MouseLeave;
                        button.Click += OpenPos_Click;
                        button.Name = objectElement.ID;
                        var nameTmp = objectElement.ID.ToLower();
                        button.Text = "[" + counter + "]" + objectElement.ID;
                        flowLayoutPanelNames.Controls.Add(button);
                    }
                    else flowLayoutPanelNames.Controls.Clear();

                    //рисую или стираю точки на карте
                    if (checkBoxContainers.Checked == true) DrawService.DrawPoint(x, y, Map.Width, Map.Height, gr, Color.DarkCyan);
                    else RepaintAxis();
                }
                Map.Image = ImageMap;
            }
        }

        private void checkBoxHoll_CheckedChanged(object sender, EventArgs e)
        {
            var checkBoxHoll = (CheckBox)sender;

            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                var counter = 0;
                foreach (var objectElement in SystemService.UniverseSystemsData[CurrentSystem].Objects.FindAll((objectEl) => (objectEl.GotoID != null)))//objectEl.ID.Contains("_hole") || objectEl.ID.ToLower().Contains(CurrentSystem.ToLower()+"_to")) && !objectEl.ID.Contains("Zone_"))
                {
                    int x = (int)Math.Round(KeyResize * objectElement.Pos[0], MidpointRounding.AwayFromZero);
                    int y = (int)Math.Round(KeyResize * objectElement.Pos[2], MidpointRounding.AwayFromZero);
                    int[] mapPos = new int[3];
                    mapPos[0] = x;
                    mapPos[1] = y;
                    mapPos[2] = objectElement.Pos[1];
                    objectElement.MapPos = mapPos;
                    if (!ObjectPoints.Contains(objectElement))
                        ObjectPoints.Add(objectElement);

                    //рисую или стираю точки на карте
                    if (checkBoxHoll.Checked == true)
                    {
                        counter++;
                        //Формирую вывод UI
                        var button = new Button();
                        button.Width = 231;
                        button.Height = 30;
                        var tooltip = new ToolTip();
                        tooltip.SetToolTip(button, $"Z: [{objectElement.Pos[1]}] X: [{objectElement.Pos[0]}] Y: [{objectElement.Pos[2]}]\n" + ((objectElement.Archetype != null) ? objectElement.Archetype : string.Empty));
                        button.MouseEnter += All_MouseEnter;
                        button.MouseLeave += All_MouseLeave;
                        button.Click += Holl_Click;
                        button.Name = objectElement.ID;
                        var nameTmp = objectElement.ID.ToLower();
                        button.Text = "[" + counter + "]" + (!string.IsNullOrEmpty(objectElement.Goto) ? objectElement.Goto : objectElement.ID);
                        flowLayoutPanelNames.Controls.Add(button);
                    }
                    else flowLayoutPanelNames.Controls.Clear();

                    //рисую или стираю точки на карте
                    if (checkBoxHoll.Checked == true) DrawService.DrawPoint(x, y, Map.Width, Map.Height, gr, Color.DarkOrchid);
                    else RepaintAxis();
                }
                Map.Image = ImageMap;
            }
        }

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            var checkBoxAll = (CheckBox)sender;
            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                var counter = 0;
                foreach (var objectEl in SystemService.UniverseSystemsData[CurrentSystem].Objects)
                {
                    int x = (int)Math.Round(KeyResize * objectEl.Pos[0], MidpointRounding.AwayFromZero);
                    int y = (int)Math.Round(KeyResize * objectEl.Pos[2], MidpointRounding.AwayFromZero);
                    int[] mapPos = new int[3];
                    mapPos[0] = x;
                    mapPos[1] = y;
                    mapPos[2] = objectEl.Pos[1];
                    objectEl.MapPos = mapPos;
                    if (!ObjectPoints.Contains(objectEl))
                        ObjectPoints.Add(objectEl);

                    //рисую или стираю точки на карте
                    if (checkBoxAll.Checked == true)
                    {
                        //Формирую вывод UI
                        var button = new Button();
                        button.Width = 231;
                        button.Height = 30;
                        var tooltip = new ToolTip();
                        tooltip.SetToolTip(button, $"Z: [{objectEl.Pos[1]}] X: [{objectEl.Pos[0]}] Y: [{objectEl.Pos[2]}]\n" + ((objectEl.Archetype != null) ? objectEl.Archetype : string.Empty));
                        button.MouseEnter += All_MouseEnter;
                        button.MouseLeave += All_MouseLeave;
                        button.Click += OpenPos_Click;
                        button.Name = objectEl.ID;
                        var nameTmp = objectEl.ID.ToLower();
                        button.Text = "[" + counter + "]" + objectEl.ID;
                        flowLayoutPanelNames.Controls.Add(button);
                    }
                    else flowLayoutPanelNames.Controls.Clear();

                    //рисую или стираю точки на карте
                    if (checkBoxAll.Checked == true) DrawService.DrawPoint(x, y, Map.Width, Map.Height, gr, Color.DarkOrange);
                    else RepaintAxis();
                }
                Map.Image = ImageMap;
            }
        }


        private void ComboBoxSystems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                int index = comboBoxSystems.FindStringExact(comboBoxSystems.Text);
                comboBoxSystems.SelectedIndex = index;
            }
        }

        private async void ComboBoxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                await SearchEquipments();
            }
        }
        private async void ButtonSearchEquipment_Click(object sender, EventArgs e)
        {
            await SearchEquipments();
        }

        /// <summary>
        /// Поиск совпадений в списке оборудования
        /// </summary>
        private async Task SearchEquipments()
        {
            comboBoxSearch.Items.Clear();
            var searchListEq = new List<Equipment>();
            searchListEq.AddRange(SystemService.Equipments.FindAll((eq) => (string.IsNullOrEmpty(eq.Name) ? eq.Id.Contains(comboBoxSearch.Text): eq.Name.Contains(comboBoxSearch.Text))));
            if (searchListEq.Count <= 0) return;
            var count = (searchListEq.Count <= 1000) ? searchListEq.Count : 1000;
            for (int i = 0; i < count; i++)
            {
                var eq = searchListEq[i];
                var equipmentsData = new ComboBoxItem
                {
                    Text = (string.IsNullOrEmpty(eq.Name) ? eq.Id : eq.Name),
                    ID = eq.Id
                };
                comboBoxSearch.Items.Add(equipmentsData);
            }
            comboBoxSearch.DroppedDown = true;
        }

        private void Base_MouseLeave(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var obj = ObjectPoints.Find((baseEl) => baseEl.BaseID == button.Name);
            var name = (string.IsNullOrEmpty(obj.NameBase)) ? button.Name : obj.NameBase;
            //сделать точку обычной
            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                DrawService.DrawText(new Point(obj.MapPos[0] + 15, obj.MapPos[1] + 15), Map.Width, Map.Height, name, gr, Brushes.White, 15);
                DrawService.DrawPoint(obj.MapPos[0], obj.MapPos[1], Map.Width, Map.Height, gr, Color.Black, 10, 10);
                Map.Image = ImageMap;
            }
        }

        private void Base_MouseEnter(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var obj = ObjectPoints.Find((baseEl) => baseEl.BaseID == button.Name);
            var name = (string.IsNullOrEmpty(obj.NameBase)) ? button.Name : obj.NameBase;
            //сделать точку крупнее
            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                DrawService.DrawPoint(obj.MapPos[0], obj.MapPos[1], Map.Width, Map.Height, gr, Color.Red, 10, 10);
                DrawService.DrawText(new Point(obj.MapPos[0] + 15, obj.MapPos[1] + 15), Map.Width, Map.Height, name, gr, Brushes.Black, 15);
                Map.Image = ImageMap;
            }
        }

        private void Container_MouseLeave(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var obj = ObjectPoints.Find((baseEl) => baseEl.ID == button.Name);
            var name = button.Name;
            //сделать точку обычной
            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                DrawService.DrawText(new Point(obj.MapPos[0] + 15, obj.MapPos[1] + 15), Map.Width, Map.Height, name, gr, Brushes.White, 15);
                DrawService.DrawPoint(obj.MapPos[0], obj.MapPos[1], Map.Width, Map.Height, gr, Color.Red);
                Map.Image = ImageMap;
            }
        }

        private void Container_MouseEnter(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var obj = ObjectPoints.Find((objectEl) => objectEl.ID == button.Name);
            var name = button.Name;
            //сделать точку крупнее
            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                DrawService.DrawPoint(obj.MapPos[0], obj.MapPos[1], Map.Width, Map.Height, gr, Color.LightGreen);
                DrawService.DrawText(new Point(obj.MapPos[0] + 15, obj.MapPos[1] + 15), Map.Width, Map.Height, name, gr, Brushes.Black, 15);
                Map.Image = ImageMap;
            }
        }

        private void All_MouseLeave(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var obj = ObjectPoints.Find((baseEl) => baseEl.ID == button.Name);
            var name = button.Name;
            if (obj == null) return;
            //сделать точку обычной
            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                DrawService.DrawText(new Point(obj.MapPos[0] + 15, obj.MapPos[1] + 15), Map.Width, Map.Height, name, gr, Brushes.White, 15);
                DrawService.DrawPoint(obj.MapPos[0], obj.MapPos[1], Map.Width, Map.Height, gr, Color.Brown);
                Map.Image = ImageMap;
            }
        }

        private void All_MouseEnter(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var obj = ObjectPoints.Find((objectEl) => objectEl.ID == button.Name);
            var name = button.Name;
            //сделать точку крупнее
            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                DrawService.DrawPoint(obj.MapPos[0], obj.MapPos[1], Map.Width, Map.Height, gr, Color.LightGreen);
                DrawService.DrawText(new Point(obj.MapPos[0] + 15, obj.MapPos[1] + 15), Map.Width, Map.Height, name, gr, Brushes.Black, 15);
                Map.Image = ImageMap;
            }
        }
        private void comboBoxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = (ComboBox)sender;

            LogService.LogEvent("Ищем элемент " + comboBoxSearch.Text + "(" + (combo.SelectedItem as ComboBoxItem).ID + ")" + "...");
            LogService.LogEvent("------------------");
            LogService.LogEvent("----Астероиды----");

            //Ищу совпадения в астероидах
            foreach (var ast in SystemService.SysAsteroids)
            {
                var val = ast.Value.FindAll((zone) => zone.LootId.Contains((combo.SelectedItem as ComboBoxItem).ID));
                bool ok = false;
                string zoneName = string.Empty;
                foreach (var v in val)
                {
                    if (v.ZoneName != null && v.ZoneName.Length > 0)
                    {
                        zoneName = zoneName.ToLower();
                        ok = true;
                        break;
                    }
                }

                if (val != null && val.Count > 0 && ok)
                {
                    foreach (var sys in SystemService.UniverseSystemsData)
                    {
                        var obj = sys.Value.Zones?.FindAll((el) => el.ID.ToLower().Contains(zoneName));
                        if (obj != null && obj.Count > 0)
                        {
                            LogService.LogEvent(SystemService.SystemNamesID[ast.Key.ToLower()] + " - " +
                                " POS: X: " + obj[0].Pos[0] + " Y: " + obj[0].Pos[1] + " Z: " + obj[0].Pos[2]);
                        }
                    }
                }
                if (val != null && val.Count > 0 && !ok) //EXCLUSION ZONE в ASTEROIDS
                {
                    LogService.LogEvent(SystemService.SystemNamesID[ast.Key.ToLower()] + " - " + val[0].LootId);
                }
            }

            LogService.LogEvent("------------------");
            LogService.LogEvent("----контейнеры----");

            //Ищу совпадения в контейнерах
            foreach (var ast in SystemService.Loadouts)
            {
                var val = ast.Cargo.Find((zone) => zone.Name.Contains((combo.SelectedItem as ComboBoxItem).ID.ToLower()));
                if (val != null)
                {
                    foreach (var sys in SystemService.UniverseSystemsData)
                    {
                        var obj = sys.Value.Objects?.FindAll((el) => el.Loadout != null && el.Loadout.ToLower().Contains(ast.Name));
                        if (obj != null && obj.Count > 0)
                        {
                            LogService.LogEvent("SYSTEM: " + SystemService.SystemNamesID[sys.Key.ToLower()] + " L: " + ast.Name + " - " + val.Name + " / " + val.Count);
                        }
                    }
                }
            }

            LogService.LogEvent("------------------");
        }

        #region GraphMap
        private System.Windows.UIElement GenerateWpfVisuals(bool Custom = false)
        {
            Zoomctrl = new ZoomControl();
            ZoomControl.SetViewFinderVisibility(Zoomctrl, System.Windows.Visibility.Visible);
            gXLogic = new GXLogicCore<DataVertex, DataEdge, BidirectionalGraph<DataVertex, DataEdge>>();
            GArea = new GraphAreaExample
            {
                LogicCore = gXLogic,
                EdgeLabelFactory = new DefaultEdgelabelFactory()
            };
            GArea.ShowAllEdgesLabels(true);
            gXLogic.Graph = (!Custom) ? GenerateGraph() : GenerateRoad();
            gXLogic.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.LinLog;//KK - неплохо
            gXLogic.DefaultLayoutAlgorithmParams = gXLogic.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.LinLog);
            gXLogic.DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            gXLogic.DefaultOverlapRemovalAlgorithmParams = gXLogic.AlgorithmFactory.CreateOverlapRemovalParameters(OverlapRemovalAlgorithmTypeEnum.OneWayFSA);
            ((OverlapRemovalParameters)gXLogic.DefaultOverlapRemovalAlgorithmParams).HorizontalGap = 100;
            ((OverlapRemovalParameters)gXLogic.DefaultOverlapRemovalAlgorithmParams).VerticalGap = 100;
            gXLogic.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.None;
            gXLogic.AsyncAlgorithmCompute = false;
            Zoomctrl.Content = GArea;
            GArea.RelayoutFinished += gArea_RelayoutFinished;
            var myResourceDictionary = new System.Windows.ResourceDictionary { Source = new Uri("Templates\\template.xaml", UriKind.Relative) };
            Zoomctrl.Resources.MergedDictionaries.Add(myResourceDictionary);
            return Zoomctrl;
        }

        private void gArea_RelayoutFinished(object sender, EventArgs e)
        {
            Zoomctrl.ZoomToFill();
        }

        private GraphExample GenerateGraph()
        {
            //загрузка систем
            var dataGraph = new GraphExample();
            for(int i = 0; i < SystemService.ArraySystemsCombobox.Length; i++)
            {
                var dataVertex = new DataVertex();
                if (checkBoxRusNames.Checked == true)
                {
                    var rusName = SystemService.UniverseSystemsData[SystemService.ArraySystemsCombobox[i]].Name;
                    //dataVertex = new DataVertex("[" + i + "] " + rusName);
                    dataVertex = new DataVertex(rusName);
                }
                else dataVertex = new DataVertex("[" + i + "] " + SystemService.ArraySystemsCombobox[i]);
                dataGraph.AddVertex(dataVertex);
            }
            var vlist = dataGraph.Vertices.ToList();

            //создание связей
            if(SystemService.HollRoads != null)
            {
                foreach(var road in SystemService.HollRoads)
                {
                    var roadFirstSys = road.Substring(0, road.IndexOf('='));
                    var roadLastSys = road.Substring((road.IndexOf('=') + 1));
                    var index_1 = Array.IndexOf(SystemService.ArraySystemsCombobox, roadFirstSys);
                    var index_2 = Array.IndexOf(SystemService.ArraySystemsCombobox, roadLastSys);
                    if (index_1 == -1 || index_2 == -1) continue;
                    var dataEdge = new DataEdge(vlist[index_1], vlist[index_2]) { Text = string.Format("{0} -> {1}", vlist[index_1], vlist[index_2]) };
                    dataGraph.AddEdge(dataEdge);
                }
            }

            return dataGraph;
        }

        private GraphExample GenerateRoad()
        {
            //загрузка систем
            var dataGraph = new GraphExample();
            LogService.LogEvent($"Создаю путь от {comboBoxRoadFirst.Text} до {comboBoxRoadLast.Text}");


            for (int i = 0; i < VerticleRoad.Length; i++)
            {
                var dataVertex = new DataVertex();
                dataVertex = new DataVertex(VerticleRoad[i]);
                dataGraph.AddVertex(dataVertex);
                if(i > 0)
                {
                    var dataEdge = new DataEdge(dataGraph.Vertices.ToList()[i-1], dataGraph.Vertices.ToList()[i]) { Text = string.Format("{0} -> {1}", dataGraph.Vertices.ToList()[i-1], dataGraph.Vertices.ToList()[i]) };
                    dataGraph.AddEdge(dataEdge);
                }
            }
            var vlist = dataGraph.Vertices.ToList();
            return dataGraph;
        }

        private void but_generate_Click(object sender, EventArgs e)
        {
            wpfHost.Visible = true;
            wpfHost.Child = GenerateWpfVisuals(Custom: false);
            GArea.GenerateGraph(true);
            GArea.SetVerticesDrag(true, true);
            Zoomctrl.ZoomToFill();
            buttonSetRoad.Enabled = true;
        }

        private void but_reload_Click(object sender, EventArgs e)
        {
            wpfHost.Visible = true;
            GArea.RelayoutGraph();
        }

        private void ButtonSetRoad_Click(object sender, EventArgs e)
        {
            var id1 = string.Empty;
            var id2 = string.Empty;
            if (!checkBoxRusNames.Checked)
            {   // определяю английское с русского наименования
                id1 = SystemService.SystemsNameId[comboBoxRoadFirst.Text];
                id2 = SystemService.SystemsNameId[comboBoxRoadLast.Text];
            }
            else
            {
                id1 = comboBoxRoadFirst.Text;
                id2 = comboBoxRoadLast.Text;
            }

            var pth = string.Empty;

            if (checkBoxSearchState.Checked)
            {
                //чистим от  однонаправленных связей
                var edges = SystemService.HollRoads.FindAll((road) =>
                {
                    var road_1 = road.Substring(0, road.IndexOf('=')); //[Оптимум] какой-то Оптимум=Неизвестный сектор
                    var road_2 = road.Substring(road.IndexOf('=') + 1); //[Неизвестный сектор] какой-то Оптимум=Неизвестный сектор
                    if (SystemService.HollRoads.Contains(road_2 + '=' + road_1)) return true; //Неизвестный сектор=Оптимум
                    return false;
                });

                var g = new Graph();
                gXLogic.Graph = GenerateGraph();
                foreach (var vertice in gXLogic.Graph.Vertices)
                    g.AddVertex(vertice.Text);
                foreach (var edge in edges)
                {
                    var roadFirstSys = SystemService.SystemNamesID[edge.Substring(0, edge.IndexOf('='))];
                    var roadLastSys = SystemService.SystemNamesID[edge.Substring(edge.IndexOf('=') + 1)];

                    g.AddEdge(roadFirstSys, roadLastSys, 1);
                }
                    
                var dijkstra = new Dijkstra(g);
                pth = dijkstra.FindShortestPath(id1, id2);
                LogService.LogEvent(pth);
            }
            else
            {
                GraphC g = new GraphC();
                for (int i = 0; i < comboBoxSystems.Items.Count; i++)
                {
                    Keyss.Add(i);
                    g.AddKey(i);
                }

                SystemService.HollRoads.Distinct();

                foreach(var road in SystemService.HollRoads)
                {
                    var roadFirstSys = road.Substring(0, road.IndexOf('='));
                    var roadLastSys = road.Substring((road.IndexOf('=') + 1));
                    var index_1 = Array.IndexOf(SystemService.ArraySystemsCombobox, roadFirstSys);
                    var index_2 = Array.IndexOf(SystemService.ArraySystemsCombobox, roadLastSys);
                    string rs = index_1 + "-" + index_2;
                    Edge.Add(rs);
                    g.AddEdge(rs);
                }
                Paths.Clear();
                FindPath(g, Array.IndexOf(SystemService.ArraySystemsCombobox, (comboBoxRoadLast.SelectedItem as ComboBoxItem).ID));

                var pathsResult = Paths.FindAll((res) =>
                {
                    res = res.Trim();
                    if (res.IndexOf(' ') == -1) return false;
                    if (res.LastIndexOf(' ') == -1) return false;

                    var start = res.Substring(0, res.IndexOf(' '));
                    int INDEXStop = res.LastIndexOf(' ');
                    var stop = res.Substring(INDEXStop);
                    if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(stop) && int.Parse(start.Trim()) == Array.IndexOf(SystemService.ArraySystemsCombobox, (comboBoxRoadFirst.SelectedItem as ComboBoxItem).ID)
                        && int.Parse(stop.Trim()) == Array.IndexOf(SystemService.ArraySystemsCombobox, (comboBoxRoadLast.SelectedItem as ComboBoxItem).ID))
                    {
                        return true;
                    }
                    else return false;
                });
                foreach(var path in pathsResult)
                {
                    var str = path.Trim();
                    string[] resM = str.Split(' ');
                    foreach(string s in resM)
                    {
                        LogService.LogEvent(SystemService.SystemsID[int.Parse(s)]);
                        pth += SystemService.SystemNamesID[SystemService.SystemsID[int.Parse(s.Trim())]] + "=";
                    }
                    LogService.LogEvent(str);
                }
            }

            VerticleRoad = pth.Split('=');

            bool startNameState = false;
            string startName = string.Empty;
            string stopName = string.Empty;
            GererateRoads = new string[VerticleRoad.Length - 1];
            int counter = 0;
            for (int i = 0; i < VerticleRoad.Length; i++)
            {
                VerticleRoad[i] = VerticleRoad[i].Trim();
                if (startNameState)
                {
                    stopName = VerticleRoad[i].Trim();
                    startNameState = false;
                    var road = startName + "=" + stopName;
                    GererateRoads[counter] = road;
                    counter++;
                }
                else
                {
                    if (string.IsNullOrEmpty(stopName))
                    {
                        startName = VerticleRoad[i];
                    }
                    else
                    {
                        startName = stopName;
                        stopName = VerticleRoad[i];
                        var road = startName + "=" + stopName;
                        GererateRoads[counter] = road;
                        counter++;
                        startName = VerticleRoad[i];
                    }

                    startNameState = true;
                }
            }
            wpfHost.Visible = true;
            wpfHost.Child = GenerateWpfVisuals(Custom: true);
            GArea.GenerateGraph(true);
            GArea.SetVerticesDrag(true, true);
            Zoomctrl.ZoomToFill();
        }

        private void buttonCloseMap_Click(object sender, EventArgs e)
        {
            wpfHost.Visible = false;
        }
        #endregion

        public void FindPath(GraphC gr, int start) //gr - граф, start - номер вершины, от которой нужно найти пути до остальных
        {
            marks = new Dictionary<int, bool>();
            path = new Stack<int>();
            source = gr;
            foreach (int i in gr.Keys)
                marks.Add(i, false);

            DFS(start);
        }

        public List<int> Keyss = new List<int>();
        public List<string> Edge = new List<string>();
        Dictionary<int, bool> marks;
        Stack<int> path; //<-- это стек
        GraphC source; //<-- это моя реализация графа, что там внутри - не важно
        public List<string> Paths = new List<string>();

        //функция поиска
        public void DFS(int v)
        {
            marks[v] = true;
            path.Push(v); // сохраняем в стек текущую вершину
            foreach (int i in path) // выводим путь до текущей
            {
                Console.Write(i.ToString() + " ");
            }

            Console.WriteLine();

            foreach (int i in source[v, Keyss, Edge].NodeLinks)
                if (marks[i] == false)
                {
                    DFS(i);
                    path.Pop(); // не забываем извлекать уже проверенные вершины
                }

            string paths = string.Empty;
            foreach (int i in path) // выводим путь до текущей
            {
                paths += i.ToString() + " ";
            }
            Paths.Add(paths);
        }
    }

    public class GraphC
    {
        public List<int> Keys = new List<int>();

        public List<string> Edge = new List<string>();

        public List<int> NodeLinks { get; set; }

        public GraphC()
        {
            NodeLinks = new List<int>();
        }

        public GraphC(int index, List<int> keys, List<string> edge)
        {
            Keys.AddRange(keys);
            Edge.AddRange(edge);

            NodeLinks = new List<int>();

            var Edges = Edge.FindAll((ed) =>
            {
                var start = ed.Substring(0, ed.IndexOf('-'));
                int startVal = int.Parse(start);
                if (startVal == index)
                {
                    return true;
                }
                else return false;
            });

            foreach (var Edge in Edges)
            {
                var stop = int.Parse(Edge.Substring(Edge.IndexOf('-') + 1, Edge.Length - (Edge.IndexOf('-') + 1)));
                NodeLinks.Add(stop);
            }

            //Console.WriteLine($"Y verchini {index} svyazey {NodeLinks.Count}");
        }

        public GraphC this[int index, List<int> keys, List<string> edges]
        {
            get
            {
                return new GraphC(index, keys, edges);
            }
        }

        public void AddKey(int key)
        {
            Keys.Add(key);
        }

        public void AddEdge(string edge)
        {
            Edge.Add(edge);
        }
    }
}
