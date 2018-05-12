using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProektModelirane
{
    public partial class Form1 : Form
    {
        static List<BuildingBlock> items = new List<BuildingBlock>();
        List<ActionConnector> connectors = new List<ActionConnector>();
        //селектиране
        int selectedIndex = -1;
        int x, y = 0; //за нова позиция при местене

        //свързване на Flow и ActionConnectors
        BuildingBlock startItem = null;
        Point startPoint = new Point(-1, -1);
        Point endPoint = new Point(-1, -1);

        //стъпка и време за симулацията
        Double time = 10;
        Double step = 2;

        public Form1()
        {
            InitializeComponent();

        }


        private void btnStock_Click(object sender, EventArgs e)
        {
            ReleaseButtons();
            btnStock.Checked = true;
        }

        private void btnFlow_Click(object sender, EventArgs e)
        {
            ReleaseButtons();
            btnFlow.Checked = true;    
        }

        private void btnConstant_Click(object sender, EventArgs e)
        {
            ReleaseButtons();
            btnConstant.Checked = true;
        }

        private void btnActionConnector_Click(object sender, EventArgs e)
        {
            ReleaseButtons();
            btnActionConnector.Checked = true;
        }

        private void btnRunSpecs_Click(object sender, EventArgs e)
        {
            ReleaseButtons();            

            FormRunSpecs subForm = new FormRunSpecs(this);
            subForm.Show();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            ReleaseButtons();            

            this.tabControl.SelectTab(tpChart);
            //Пуска симулацията и записва резултатите
            Simulation sim = new Simulation(time, step, CreateVariableList(), CreateDerivativeList());
            Dictionary<String, List<Double>> results = sim.RunSimulation();

            
            ChartCreator chartCreator = new ChartCreator(results, this.chartResults, time, step);
            chartCreator.CreateXY();
            
            //добавя таблица
            dataGridView1.DataSource = CreateTable(time, step, results);

        }

        //TODO
        public DataTable CreateTable(Double time, Double step, Dictionary<String, List<Double>> results)
        {
            DataTable dt = new DataTable();
            
            
            foreach (KeyValuePair<String, List<Double>> r in results)
            {
                object[] values = new object[r.Value.Count];
                for(int i=0; i<r.Value.Count; i++)
                {
                    dt.Columns.Add(); 
                    values[i] = r.Value[i];
                }
                dt.Rows.Add(values);
            }
            return dt;
        }


        public void ReleaseButtons()
        {
            foreach (ToolStripButton t in tsToolstrip.Items.OfType<ToolStripButton>())
            {
                t.Checked = false;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Point mouseCoord = new Point(e.X, e.Y);

            //селектираме, ако мишката е върху съществуващ итем  
            SelectClickedOnItem(mouseCoord);

            if (e.Button == MouseButtons.Left)
            {
                //добавяме конектор
                if (btnActionConnector.Checked)
                {
                    //начален итем
                    if (this.selectedIndex > -1 && this.startItem == null && items[selectedIndex] is BuildingBlock)
                    {
                        this.startItem = (BuildingBlock)items[selectedIndex];
                        return; // излизаме преди да се деселектира бутона Action Connector
                    }
                    //краен итем и създаване и добавяне в списъка
                    else if (this.selectedIndex > -1 && items[selectedIndex] is BuildingBlock)
                    {
                        if (startItem != (BuildingBlock)items[selectedIndex])
                        {
                            ActionConnector connector = new ActionConnector(startItem, (BuildingBlock)items[selectedIndex]);
                            this.connectors.Add(connector);
                            this.startItem = null;
                        }
                        //DeselectAndRefresh();
                    }
                    DeselectAndRefresh();
                }


                //добавяме flow
                else if (btnFlow.Checked)
                {
                    //начална точка
                    if (startItem == null && (startPoint.X < 0 || startPoint.Y < 0))
                    {
                        if (selectedIndex > -1 && items[selectedIndex] is Stock)
                        {
                            if (((Stock)items[selectedIndex]).getOutFlow() == null)
                                startItem = (Stock)items[selectedIndex];
                        }
                        else
                        {
                            startPoint = mouseCoord;
                        }
                        // излизаме преди да се деселектира бутона Flow
                        return;
                    }
                    //крайна точка
                    else
                    {
                        Flow flow = null;

                        //създава Flow и го добавя към Stock
                        if (selectedIndex > -1 && items[selectedIndex] is Stock && ((Stock)items[selectedIndex]).getInFlow() == null)
                        {
                            if (startItem != null)
                            {
                                flow = new Flow((Stock)startItem, (Stock)items[selectedIndex]);
                                ((Stock)startItem).AddOutFlow(flow);
                                ((Stock)items[selectedIndex]).AddInFlow(flow);
                            }
                            else
                            {
                                flow = new Flow(startPoint, (Stock)items[selectedIndex]);
                                ((Stock)items[selectedIndex]).AddInFlow(flow);
                            }
                        }
                        else if (startItem != null)
                        {
                            flow = new Flow((Stock)startItem, mouseCoord);
                            ((Stock)startItem).AddOutFlow(flow);
                        }

                        //добавя Flow към items
                        if (flow != null)
                        {
                            items.Add(flow);
                        }

                        startItem = null;
                        startPoint = new Point(-1, -1);

                        DeselectAndRefresh();
                    }

                }



                //добавяме stock
                else if (btnStock.Checked)
                {
                    Stock stock = new Stock(mouseCoord);
                    items.Add(stock);
                    DeselectAndRefresh();
                }
                //добавяме constant
                else if (btnConstant.Checked)
                {
                    Constant c = new Constant(mouseCoord);
                    items.Add(c);
                    DeselectAndRefresh();
                }
                //Редактираме при двойно кликване
                else if (e.Clicks > 1 && this.selectedIndex > -1)
                {
                    EditSelectedItem();
                }
            }
            ReleaseButtons();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (IDraw item in items)
                item.Draw(g);

            foreach (ActionConnector c in connectors)
                c.Draw(g);

            if(selectedIndex > -1) ((IDraw)items[selectedIndex]).DrawSelected(g);

            g.Dispose();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            //селектираме елемента
            DeselectAndRefresh();
            if (e.Button == MouseButtons.Right)
                SelectClickedOnItem(new Point(e.X, e.Y));
        }


        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            //местим, когато не чертаем Flow и ActionConnector
            if (!btnFlow.Checked && !btnActionConnector.Checked && (e.Button == MouseButtons.Left && selectedIndex > -1 && items[selectedIndex] is BuildingBlock))
            {
                Point newPosition = new Point();
                newPosition.X = ((BuildingBlock)items[selectedIndex]).GetBounds().X + e.X - x;
                newPosition.Y = ((BuildingBlock)items[selectedIndex]).GetBounds().Y + e.Y - y;
                x = e.X;
                y = e.Y;

                ((BuildingBlock)items[selectedIndex]).Move(newPosition);

                this.panel1.Refresh();
            }
                //
            else if (btnActionConnector.Checked)
            {
                if (this.startItem != null && startItem is BuildingBlock)
                {
                    this.Refresh();
                    Graphics g = this.panel1.CreateGraphics();
                    ActionConnector.Draw(startItem, new Point(e.X, e.Y), g);
                    g.Dispose();
                }
            }
            else if (btnFlow.Checked)
            {
                if (startItem != null)
                {
                    this.panel1.Refresh();
                    Graphics g = this.panel1.CreateGraphics();

                    //средната точка на правоъгълника на startItem
                    Point startP = new Point(startItem.GetBounds().X + startItem.GetBounds().Width / 2,
                        startItem.GetBounds().Y + startItem.GetBounds().Height / 2);

                    Flow.Draw(startP, new Point(e.X, e.Y), g);

                    g.Dispose();
                }
                   
                else if (startPoint.X > -1 && startPoint.Y > -1)
                {
                    this.panel1.Refresh();
                    Graphics g = this.panel1.CreateGraphics();

                    Flow.Draw(startPoint, new Point(e.X, e.Y), g);

                    g.Dispose();
                }
                     
            }
        }

        //Проверява дали към items вече е добавен друг item, чието име съвпада с името на параметърa buildingblock.
        //Тъй като ще викаме метода, след като в items вече имаме building block с проверяваното име, 
        //IsNameInUse ще връща true едва при второто срещане на името (т.е. първото неуникално).
        private bool IsNameInUse(BuildingBlock buildingblock)
        {
            if (items.Count(item => ((BuildingBlock)item).GetName() == buildingblock.GetName()) == 2) return true;
            else return false;
        }

        private void EditSelectedItem()
        {
            if (this.selectedIndex > -1 && items[selectedIndex] is BuildingBlock)
            {
                //отваряме форма Properties и зареждаме данните от текущия BuildingBlock
                FormProperties subForm = new FormProperties((BuildingBlock)items[selectedIndex]);
                subForm.ShowDialog();
                //ако името не е уникално, пак отваряме Properties, докато не въведем уникално име
                while (IsNameInUse((BuildingBlock)items[selectedIndex]))
                {
                    subForm.AskForName();
                    subForm.ShowDialog();
                }
            }
            DeselectAndRefresh();
        }

        private void SelectClickedOnItem(Point mouseCoord)
        {
            //селектираме, ако мишката е върху съществуващ итем  
            foreach (BuildingBlock item in items)
            {
                if (item.Contains(mouseCoord) > -1)
                {
                    selectedIndex = items.IndexOf(item);
                    x = mouseCoord.X;
                    y = mouseCoord.Y;
                }
            }
            this.panel1.Refresh();
        }

        private void DeselectAndRefresh()
        {
            selectedIndex = -1;
            this.panel1.Refresh();
        }


        //TODO delete flow & action connector
        private void DeleteSelectedItem()
        {
            if (selectedIndex >= 0)
            {
                if (items[selectedIndex] is Flow)
                {
                    ((Flow)items[selectedIndex]).RemoveStocks();
                }
                items.RemoveAt(selectedIndex);
                DeselectAndRefresh();
            }
        }

        private void cbtnEdit_Click(object sender, EventArgs e)
        {
            EditSelectedItem();
        }

        private void cbtnDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedItem();
        }

        public void SetTime(Double t)
        {
            this.time = t;
        }

        public void SetStep(Double s)
        {
            this.step = s;
        }

        private Dictionary<String, Double> CreateVariableList()
        {
            Dictionary<String, Double> variables = new Dictionary<String, Double>();

            foreach (BuildingBlock item in items)
            {
                if (item is Stock) // && ((Stock)item).CreateVariable() != Double.NaN)
                    variables.Add(item.GetName(), ((Stock)item).CreateVariable());
            }
            return variables;
        }

        private Dictionary<String, String> CreateDerivativeList()
        {
            Dictionary<String, String> derivatives = new Dictionary<String, String>();
            foreach (BuildingBlock item in items)
            {
                if (item is Stock) // && ((Stock)item).CreateDerivative().Length != 0)
                    derivatives.Add(item.GetName(), ((Stock)item).CreateDerivative());
            }

            return derivatives;
        }
    }
}
