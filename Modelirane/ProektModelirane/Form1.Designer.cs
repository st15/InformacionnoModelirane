namespace ProektModelirane
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tsToolstrip = new System.Windows.Forms.ToolStrip();
            this.btnStock = new System.Windows.Forms.ToolStripButton();
            this.btnFlow = new System.Windows.Forms.ToolStripButton();
            this.btnConstant = new System.Windows.Forms.ToolStripButton();
            this.btnActionConnector = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRunSpecs = new System.Windows.Forms.ToolStripButton();
            this.btnRun = new System.Windows.Forms.ToolStripButton();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpModel = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tpChart = new System.Windows.Forms.TabPage();
            this.chartResults = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tpTable = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonClickedBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buildingBlockBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.buildingBlockBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cbtnDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cbtnEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsToolstrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpModel.SuspendLayout();
            this.tpChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartResults)).BeginInit();
            this.tpTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonClickedBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingBlockBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingBlockBindingSource)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsToolstrip
            // 
            this.tsToolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStock,
            this.btnFlow,
            this.btnConstant,
            this.btnActionConnector,
            this.toolStripSeparator1,
            this.btnRunSpecs,
            this.btnRun});
            this.tsToolstrip.Location = new System.Drawing.Point(0, 0);
            this.tsToolstrip.Name = "tsToolstrip";
            this.tsToolstrip.Size = new System.Drawing.Size(631, 25);
            this.tsToolstrip.TabIndex = 0;
            this.tsToolstrip.Text = "toolStrip1";
            // 
            // btnStock
            // 
            this.btnStock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStock.Image = ((System.Drawing.Image)(resources.GetObject("btnStock.Image")));
            this.btnStock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(37, 22);
            this.btnStock.Text = "Stock";
            this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
            // 
            // btnFlow
            // 
            this.btnFlow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFlow.Image = ((System.Drawing.Image)(resources.GetObject("btnFlow.Image")));
            this.btnFlow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFlow.Name = "btnFlow";
            this.btnFlow.Size = new System.Drawing.Size(33, 22);
            this.btnFlow.Text = "Flow";
            this.btnFlow.Click += new System.EventHandler(this.btnFlow_Click);
            // 
            // btnConstant
            // 
            this.btnConstant.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnConstant.Image = ((System.Drawing.Image)(resources.GetObject("btnConstant.Image")));
            this.btnConstant.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConstant.Name = "btnConstant";
            this.btnConstant.Size = new System.Drawing.Size(55, 22);
            this.btnConstant.Text = "Constant";
            this.btnConstant.Click += new System.EventHandler(this.btnConstant_Click);
            // 
            // btnActionConnector
            // 
            this.btnActionConnector.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnActionConnector.Image = ((System.Drawing.Image)(resources.GetObject("btnActionConnector.Image")));
            this.btnActionConnector.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActionConnector.Name = "btnActionConnector";
            this.btnActionConnector.Size = new System.Drawing.Size(94, 22);
            this.btnActionConnector.Text = "Action Connector";
            this.btnActionConnector.Click += new System.EventHandler(this.btnActionConnector_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRunSpecs
            // 
            this.btnRunSpecs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRunSpecs.Image = ((System.Drawing.Image)(resources.GetObject("btnRunSpecs.Image")));
            this.btnRunSpecs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRunSpecs.Name = "btnRunSpecs";
            this.btnRunSpecs.Size = new System.Drawing.Size(61, 22);
            this.btnRunSpecs.Text = "Run Specs";
            this.btnRunSpecs.Click += new System.EventHandler(this.btnRunSpecs_Click);
            // 
            // btnRun
            // 
            this.btnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRun.Image = ((System.Drawing.Image)(resources.GetObject("btnRun.Image")));
            this.btnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(30, 22);
            this.btnRun.Text = "Run";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpModel);
            this.tabControl.Controls.Add(this.tpChart);
            this.tabControl.Controls.Add(this.tpTable);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 25);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(631, 391);
            this.tabControl.TabIndex = 1;
            // 
            // tpModel
            // 
            this.tpModel.Controls.Add(this.panel1);
            this.tpModel.Location = new System.Drawing.Point(4, 22);
            this.tpModel.Name = "tpModel";
            this.tpModel.Padding = new System.Windows.Forms.Padding(3);
            this.tpModel.Size = new System.Drawing.Size(623, 365);
            this.tpModel.TabIndex = 0;
            this.tpModel.Text = "Model";
            this.tpModel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.ContextMenuStrip = this.contextMenuStrip1;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(617, 359);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // tpChart
            // 
            this.tpChart.Controls.Add(this.chartResults);
            this.tpChart.Location = new System.Drawing.Point(4, 22);
            this.tpChart.Name = "tpChart";
            this.tpChart.Padding = new System.Windows.Forms.Padding(3);
            this.tpChart.Size = new System.Drawing.Size(623, 365);
            this.tpChart.TabIndex = 1;
            this.tpChart.Text = "Chart";
            this.tpChart.UseVisualStyleBackColor = true;
            // 
            // chartResults
            // 
            chartArea4.Name = "ChartArea1";
            this.chartResults.ChartAreas.Add(chartArea4);
            this.chartResults.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Name = "Legend1";
            this.chartResults.Legends.Add(legend4);
            this.chartResults.Location = new System.Drawing.Point(3, 3);
            this.chartResults.Name = "chartResults";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series8.Legend = "Legend1";
            series8.Name = "Series2";
            this.chartResults.Series.Add(series7);
            this.chartResults.Series.Add(series8);
            this.chartResults.Size = new System.Drawing.Size(617, 359);
            this.chartResults.TabIndex = 0;
            this.chartResults.Text = "Chart";
            // 
            // tpTable
            // 
            this.tpTable.Controls.Add(this.dataGridView1);
            this.tpTable.Location = new System.Drawing.Point(4, 22);
            this.tpTable.Name = "tpTable";
            this.tpTable.Padding = new System.Windows.Forms.Padding(3);
            this.tpTable.Size = new System.Drawing.Size(623, 365);
            this.tpTable.TabIndex = 2;
            this.tpTable.Text = "Table";
            this.tpTable.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(617, 359);
            this.dataGridView1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbtnEdit,
            this.cbtnDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(117, 48);
            // 
            // cbtnDelete
            // 
            this.cbtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cbtnDelete.Name = "cbtnDelete";
            this.cbtnDelete.Size = new System.Drawing.Size(152, 22);
            this.cbtnDelete.Text = "Delete";
            this.cbtnDelete.Click += new System.EventHandler(this.cbtnDelete_Click);
            // 
            // cbtnEdit
            // 
            this.cbtnEdit.Name = "cbtnEdit";
            this.cbtnEdit.Size = new System.Drawing.Size(152, 22);
            this.cbtnEdit.Text = "Edit";
            this.cbtnEdit.Click += new System.EventHandler(this.cbtnEdit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 416);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.tsToolstrip);
            this.Name = "Form1";
            this.Text = "Proekt Modelirane";
            this.tsToolstrip.ResumeLayout(false);
            this.tsToolstrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tpModel.ResumeLayout(false);
            this.tpChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartResults)).EndInit();
            this.tpTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonClickedBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingBlockBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingBlockBindingSource)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsToolstrip;
        private System.Windows.Forms.ToolStripButton btnFlow;
        private System.Windows.Forms.ToolStripButton btnStock;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpModel;
        private System.Windows.Forms.TabPage tpChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartResults;
        private System.Windows.Forms.ToolStripButton btnRun;
        private System.Windows.Forms.ToolStripButton btnConstant;
        private System.Windows.Forms.ToolStripButton btnActionConnector;
        private System.Windows.Forms.ToolStripButton btnRunSpecs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabPage tpTable;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource buildingBlockBindingSource;
        private System.Windows.Forms.BindingSource buildingBlockBindingSource1;
        private System.Windows.Forms.BindingSource buttonClickedBindingSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cbtnEdit;
        private System.Windows.Forms.ToolStripMenuItem cbtnDelete;
    }
}

