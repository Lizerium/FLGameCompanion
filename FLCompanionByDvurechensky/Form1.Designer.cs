/*
 * Author: Nikolay Dvurechensky
 * Site: https://sites.google.com/view/dvurechensky
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 06 сентября 2025 11:46:30
 * Version: 1.0.39
 */

namespace FLCompanionByDvurechensky
{
    partial class FreelancerCompanionDvurechensky
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FreelancerCompanionDvurechensky));
            this.Map = new System.Windows.Forms.PictureBox();
            this.comboBoxSystems = new System.Windows.Forms.ComboBox();
            this.labelSystemss = new System.Windows.Forms.Label();
            this.LoggerRichTextBox = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanelNames = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxBases = new System.Windows.Forms.CheckBox();
            this.checkBoxContainers = new System.Windows.Forms.CheckBox();
            this.checkBoxAll = new System.Windows.Forms.CheckBox();
            this.checkBoxHoll = new System.Windows.Forms.CheckBox();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.textBoxY = new System.Windows.Forms.TextBox();
            this.textBoxZ = new System.Windows.Forms.TextBox();
            this.wpfHost = new System.Windows.Forms.Integration.ElementHost();
            this.but_generate = new System.Windows.Forms.Button();
            this.but_reload = new System.Windows.Forms.Button();
            this.buttonSetRoad = new System.Windows.Forms.Button();
            this.comboBoxRoadFirst = new System.Windows.Forms.ComboBox();
            this.comboBoxRoadLast = new System.Windows.Forms.ComboBox();
            this.checkBoxRusNames = new System.Windows.Forms.CheckBox();
            this.buttonCloseMap = new System.Windows.Forms.Button();
            this.comboBoxSearch = new System.Windows.Forms.ComboBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.checkBoxSearchState = new System.Windows.Forms.CheckBox();
            this.buttonSearchEquipment = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Map)).BeginInit();
            this.SuspendLayout();
            // 
            // Map
            // 
            this.Map.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Map.Location = new System.Drawing.Point(0, -2);
            this.Map.Name = "Map";
            this.Map.Size = new System.Drawing.Size(800, 800);
            this.Map.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Map.TabIndex = 1;
            this.Map.TabStop = false;
            this.Map.Paint += new System.Windows.Forms.PaintEventHandler(this.Map_Paint);
            // 
            // comboBoxSystems
            // 
            this.comboBoxSystems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxSystems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxSystems.DisplayMember = "Add";
            this.comboBoxSystems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxSystems.FormattingEnabled = true;
            this.comboBoxSystems.Location = new System.Drawing.Point(806, 1);
            this.comboBoxSystems.Name = "comboBoxSystems";
            this.comboBoxSystems.Size = new System.Drawing.Size(406, 24);
            this.comboBoxSystems.TabIndex = 2;
            this.comboBoxSystems.SelectedIndexChanged += new System.EventHandler(this.comboBoxSystems_SelectedIndexChanged);
            this.comboBoxSystems.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBoxSystems_KeyPress);
            // 
            // labelSystemss
            // 
            this.labelSystemss.AutoSize = true;
            this.labelSystemss.BackColor = System.Drawing.Color.DarkOrchid;
            this.labelSystemss.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSystemss.Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSystemss.ForeColor = System.Drawing.Color.FloralWhite;
            this.labelSystemss.Location = new System.Drawing.Point(1228, 4);
            this.labelSystemss.Name = "labelSystemss";
            this.labelSystemss.Size = new System.Drawing.Size(44, 21);
            this.labelSystemss.TabIndex = 3;
            this.labelSystemss.Text = "999";
            this.labelSystemss.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoggerRichTextBox
            // 
            this.LoggerRichTextBox.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoggerRichTextBox.Location = new System.Drawing.Point(806, 473);
            this.LoggerRichTextBox.Name = "LoggerRichTextBox";
            this.LoggerRichTextBox.Size = new System.Drawing.Size(476, 325);
            this.LoggerRichTextBox.TabIndex = 4;
            this.LoggerRichTextBox.Text = "";
            // 
            // flowLayoutPanelNames
            // 
            this.flowLayoutPanelNames.AutoScroll = true;
            this.flowLayoutPanelNames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.flowLayoutPanelNames.Location = new System.Drawing.Point(806, 185);
            this.flowLayoutPanelNames.Name = "flowLayoutPanelNames";
            this.flowLayoutPanelNames.Size = new System.Drawing.Size(476, 252);
            this.flowLayoutPanelNames.TabIndex = 5;
            // 
            // checkBoxBases
            // 
            this.checkBoxBases.AutoSize = true;
            this.checkBoxBases.Font = new System.Drawing.Font("Mongolian Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxBases.Location = new System.Drawing.Point(806, 31);
            this.checkBoxBases.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxBases.Name = "checkBoxBases";
            this.checkBoxBases.Size = new System.Drawing.Size(128, 24);
            this.checkBoxBases.TabIndex = 6;
            this.checkBoxBases.Text = "Показать базы";
            this.checkBoxBases.UseVisualStyleBackColor = true;
            this.checkBoxBases.CheckedChanged += new System.EventHandler(this.checkBoxBases_CheckedChanged);
            // 
            // checkBoxContainers
            // 
            this.checkBoxContainers.AutoSize = true;
            this.checkBoxContainers.Font = new System.Drawing.Font("Mongolian Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxContainers.Location = new System.Drawing.Point(805, 53);
            this.checkBoxContainers.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxContainers.Name = "checkBoxContainers";
            this.checkBoxContainers.Size = new System.Drawing.Size(175, 24);
            this.checkBoxContainers.TabIndex = 7;
            this.checkBoxContainers.Text = "Показать контейнеры";
            this.checkBoxContainers.UseVisualStyleBackColor = true;
            this.checkBoxContainers.CheckedChanged += new System.EventHandler(this.checkBoxContainers_CheckedChanged);
            // 
            // checkBoxAll
            // 
            this.checkBoxAll.AutoSize = true;
            this.checkBoxAll.Font = new System.Drawing.Font("Mongolian Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAll.Location = new System.Drawing.Point(1033, 27);
            this.checkBoxAll.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxAll.Name = "checkBoxAll";
            this.checkBoxAll.Size = new System.Drawing.Size(117, 24);
            this.checkBoxAll.TabIndex = 8;
            this.checkBoxAll.Text = "Показать всё";
            this.checkBoxAll.UseVisualStyleBackColor = true;
            this.checkBoxAll.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // checkBoxHoll
            // 
            this.checkBoxHoll.AutoSize = true;
            this.checkBoxHoll.Font = new System.Drawing.Font("Mongolian Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxHoll.Location = new System.Drawing.Point(1032, 53);
            this.checkBoxHoll.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxHoll.Name = "checkBoxHoll";
            this.checkBoxHoll.Size = new System.Drawing.Size(152, 24);
            this.checkBoxHoll.TabIndex = 9;
            this.checkBoxHoll.Text = "Показать порталы";
            this.checkBoxHoll.UseVisualStyleBackColor = true;
            this.checkBoxHoll.CheckedChanged += new System.EventHandler(this.checkBoxHoll_CheckedChanged);
            // 
            // textBoxX
            // 
            this.textBoxX.Location = new System.Drawing.Point(1214, 28);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(68, 20);
            this.textBoxX.TabIndex = 10;
            this.textBoxX.Text = "X";
            this.textBoxX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxY
            // 
            this.textBoxY.Location = new System.Drawing.Point(1214, 53);
            this.textBoxY.Name = "textBoxY";
            this.textBoxY.Size = new System.Drawing.Size(68, 20);
            this.textBoxY.TabIndex = 11;
            this.textBoxY.Text = "Y";
            this.textBoxY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxZ
            // 
            this.textBoxZ.Location = new System.Drawing.Point(1214, 79);
            this.textBoxZ.Name = "textBoxZ";
            this.textBoxZ.Size = new System.Drawing.Size(68, 20);
            this.textBoxZ.TabIndex = 12;
            this.textBoxZ.Text = "Z";
            this.textBoxZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // wpfHost
            // 
            this.wpfHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wpfHost.BackColor = System.Drawing.Color.White;
            this.wpfHost.Location = new System.Drawing.Point(0, -2);
            this.wpfHost.Name = "wpfHost";
            this.wpfHost.Size = new System.Drawing.Size(801, 800);
            this.wpfHost.TabIndex = 14;
            this.wpfHost.Text = "elementHost1";
            this.wpfHost.Visible = false;
            this.wpfHost.Child = null;
            // 
            // but_generate
            // 
            this.but_generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.but_generate.Image = ((System.Drawing.Image)(resources.GetObject("but_generate.Image")));
            this.but_generate.Location = new System.Drawing.Point(806, 113);
            this.but_generate.Name = "but_generate";
            this.but_generate.Size = new System.Drawing.Size(65, 67);
            this.but_generate.TabIndex = 2;
            this.but_generate.UseVisualStyleBackColor = true;
            this.but_generate.Click += new System.EventHandler(this.but_generate_Click);
            // 
            // but_reload
            // 
            this.but_reload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.but_reload.Image = ((System.Drawing.Image)(resources.GetObject("but_reload.Image")));
            this.but_reload.Location = new System.Drawing.Point(877, 115);
            this.but_reload.Name = "but_reload";
            this.but_reload.Size = new System.Drawing.Size(65, 65);
            this.but_reload.TabIndex = 15;
            this.but_reload.UseVisualStyleBackColor = true;
            this.but_reload.Click += new System.EventHandler(this.but_reload_Click);
            // 
            // buttonSetRoad
            // 
            this.buttonSetRoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetRoad.Enabled = false;
            this.buttonSetRoad.Image = ((System.Drawing.Image)(resources.GetObject("buttonSetRoad.Image")));
            this.buttonSetRoad.Location = new System.Drawing.Point(948, 115);
            this.buttonSetRoad.Name = "buttonSetRoad";
            this.buttonSetRoad.Size = new System.Drawing.Size(65, 65);
            this.buttonSetRoad.TabIndex = 16;
            this.buttonSetRoad.UseVisualStyleBackColor = true;
            this.buttonSetRoad.Click += new System.EventHandler(this.ButtonSetRoad_Click);
            // 
            // comboBoxRoadFirst
            // 
            this.comboBoxRoadFirst.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxRoadFirst.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxRoadFirst.DisplayMember = "Add";
            this.comboBoxRoadFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxRoadFirst.FormattingEnabled = true;
            this.comboBoxRoadFirst.Location = new System.Drawing.Point(1019, 115);
            this.comboBoxRoadFirst.Name = "comboBoxRoadFirst";
            this.comboBoxRoadFirst.Size = new System.Drawing.Size(263, 24);
            this.comboBoxRoadFirst.TabIndex = 17;
            // 
            // comboBoxRoadLast
            // 
            this.comboBoxRoadLast.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxRoadLast.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxRoadLast.DisplayMember = "Add";
            this.comboBoxRoadLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxRoadLast.FormattingEnabled = true;
            this.comboBoxRoadLast.Location = new System.Drawing.Point(1019, 155);
            this.comboBoxRoadLast.Name = "comboBoxRoadLast";
            this.comboBoxRoadLast.Size = new System.Drawing.Size(263, 24);
            this.comboBoxRoadLast.TabIndex = 18;
            // 
            // checkBoxRusNames
            // 
            this.checkBoxRusNames.AutoSize = true;
            this.checkBoxRusNames.Font = new System.Drawing.Font("Mongolian Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxRusNames.Location = new System.Drawing.Point(1033, 79);
            this.checkBoxRusNames.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxRusNames.Name = "checkBoxRusNames";
            this.checkBoxRusNames.Size = new System.Drawing.Size(118, 24);
            this.checkBoxRusNames.TabIndex = 19;
            this.checkBoxRusNames.Text = "Русский язык";
            this.checkBoxRusNames.UseVisualStyleBackColor = true;
            // 
            // buttonCloseMap
            // 
            this.buttonCloseMap.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.buttonCloseMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCloseMap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCloseMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCloseMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCloseMap.ForeColor = System.Drawing.Color.AliceBlue;
            this.buttonCloseMap.Location = new System.Drawing.Point(806, 83);
            this.buttonCloseMap.Name = "buttonCloseMap";
            this.buttonCloseMap.Size = new System.Drawing.Size(25, 23);
            this.buttonCloseMap.TabIndex = 20;
            this.buttonCloseMap.Text = "X";
            this.buttonCloseMap.UseVisualStyleBackColor = false;
            this.buttonCloseMap.Click += new System.EventHandler(this.buttonCloseMap_Click);
            // 
            // comboBoxSearch
            // 
            this.comboBoxSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxSearch.DisplayMember = "Add";
            this.comboBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxSearch.FormattingEnabled = true;
            this.comboBoxSearch.Location = new System.Drawing.Point(961, 443);
            this.comboBoxSearch.Name = "comboBoxSearch";
            this.comboBoxSearch.Size = new System.Drawing.Size(251, 24);
            this.comboBoxSearch.TabIndex = 21;
            this.comboBoxSearch.SelectedIndexChanged += new System.EventHandler(this.comboBoxSearch_SelectedIndexChanged);
            this.comboBoxSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBoxSearch_KeyPress);
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.BackColor = System.Drawing.Color.Cornsilk;
            this.labelSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSearch.Location = new System.Drawing.Point(807, 447);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(148, 20);
            this.labelSearch.TabIndex = 22;
            this.labelSearch.Text = "Поиск элемента";
            // 
            // checkBoxSearchState
            // 
            this.checkBoxSearchState.AutoSize = true;
            this.checkBoxSearchState.Font = new System.Drawing.Font("Mongolian Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSearchState.Location = new System.Drawing.Point(852, 83);
            this.checkBoxSearchState.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxSearchState.Name = "checkBoxSearchState";
            this.checkBoxSearchState.Size = new System.Drawing.Size(161, 24);
            this.checkBoxSearchState.TabIndex = 23;
            this.checkBoxSearchState.Text = "Алгоритм Дейкстры";
            this.checkBoxSearchState.UseVisualStyleBackColor = true;
            // 
            // buttonSearchEquipment
            // 
            this.buttonSearchEquipment.BackColor = System.Drawing.Color.Orange;
            this.buttonSearchEquipment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonSearchEquipment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSearchEquipment.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.buttonSearchEquipment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.buttonSearchEquipment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.buttonSearchEquipment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSearchEquipment.ForeColor = System.Drawing.Color.Black;
            this.buttonSearchEquipment.Location = new System.Drawing.Point(1218, 444);
            this.buttonSearchEquipment.Name = "buttonSearchEquipment";
            this.buttonSearchEquipment.Size = new System.Drawing.Size(64, 23);
            this.buttonSearchEquipment.TabIndex = 24;
            this.buttonSearchEquipment.Text = "Поиск";
            this.buttonSearchEquipment.UseVisualStyleBackColor = false;
            this.buttonSearchEquipment.Click += new System.EventHandler(this.ButtonSearchEquipment_Click);
            // 
            // FreelancerCompanionDvurechensky
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(1294, 800);
            this.Controls.Add(this.buttonSearchEquipment);
            this.Controls.Add(this.checkBoxSearchState);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.comboBoxSearch);
            this.Controls.Add(this.buttonCloseMap);
            this.Controls.Add(this.checkBoxRusNames);
            this.Controls.Add(this.comboBoxRoadLast);
            this.Controls.Add(this.comboBoxRoadFirst);
            this.Controls.Add(this.buttonSetRoad);
            this.Controls.Add(this.but_reload);
            this.Controls.Add(this.but_generate);
            this.Controls.Add(this.wpfHost);
            this.Controls.Add(this.textBoxZ);
            this.Controls.Add(this.textBoxY);
            this.Controls.Add(this.textBoxX);
            this.Controls.Add(this.checkBoxHoll);
            this.Controls.Add(this.checkBoxAll);
            this.Controls.Add(this.checkBoxContainers);
            this.Controls.Add(this.checkBoxBases);
            this.Controls.Add(this.flowLayoutPanelNames);
            this.Controls.Add(this.LoggerRichTextBox);
            this.Controls.Add(this.labelSystemss);
            this.Controls.Add(this.comboBoxSystems);
            this.Controls.Add(this.Map);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1509, 1032);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1310, 838);
            this.Name = "FreelancerCompanionDvurechensky";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FLCompanion by Dvurechensky";
            ((System.ComponentModel.ISupportInitialize)(this.Map)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox Map;
        private System.Windows.Forms.ComboBox comboBoxSystems;
        private System.Windows.Forms.Label labelSystemss;
        private System.Windows.Forms.RichTextBox LoggerRichTextBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelNames;
        private System.Windows.Forms.CheckBox checkBoxBases;
        private System.Windows.Forms.CheckBox checkBoxContainers;
        private System.Windows.Forms.CheckBox checkBoxAll;
        private System.Windows.Forms.CheckBox checkBoxHoll;
        private System.Windows.Forms.TextBox textBoxX;
        private System.Windows.Forms.TextBox textBoxY;
        private System.Windows.Forms.TextBox textBoxZ;
        private System.Windows.Forms.Integration.ElementHost wpfHost;
        private System.Windows.Forms.Button but_generate;
        private System.Windows.Forms.Button but_reload;
        private System.Windows.Forms.Button buttonSetRoad;
        private System.Windows.Forms.ComboBox comboBoxRoadFirst;
        private System.Windows.Forms.ComboBox comboBoxRoadLast;
        private System.Windows.Forms.CheckBox checkBoxRusNames;
        private System.Windows.Forms.Button buttonCloseMap;
        private System.Windows.Forms.ComboBox comboBoxSearch;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.CheckBox checkBoxSearchState;
        private System.Windows.Forms.Button buttonSearchEquipment;
    }
}

