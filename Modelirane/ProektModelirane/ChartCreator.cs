using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProektModelirane
{
    class ChartCreator
    {
        private Dictionary<String, List<Double>> results;
        private Chart chartResults;
        //time, step
        private List<Double> xValues;

        public ChartCreator(Dictionary<String, List<Double>> results, Chart chartResults)
        {
            this.results = results;
            this.chartResults = chartResults;
        }

        public ChartCreator(Dictionary<String, List<Double>> results, Chart chartResults, Double time, Double step)
        {
            this.results = results;
            this.chartResults = chartResults;

            this.xValues = new List<Double>();
            for (Double i = 0; i <= time; i += step)
                xValues.Add(i);
        }

        public void Create()
        {
            //изтриваме графиката на предишните резултати
            chartResults.Series.Clear();

            foreach (KeyValuePair<String, List<Double>> result in results)
            {
                //Добавяме нова серия в графиката (chart) chartResults, с име ключа на резултата

                String seriesName = result.Key.ToString();
                chartResults.Series.Add(seriesName);

                //Правим графиката да е от тип Spline (по подразбиране е Columns)
                chartResults.Series[seriesName].ChartType = SeriesChartType.Spline;

                //Свързваме добавената серия с данните от резултата - result.Value
                //X стойността се генерира като номер на стъпката.
                chartResults.Series[seriesName].Points.DataBindY(result.Value);
            }
        }

        public void CreateXY()
        {
            //изтриваме графиката на предишните резултати
            chartResults.Series.Clear();

            chartResults.ChartAreas[0].AxisX.Minimum = 0;

            foreach (KeyValuePair<String, List<Double>> result in results)
            {
                //Добавяме нова серия в графиката (chart) chartResults, с име ключа на резултата

                String seriesName = result.Key.ToString();
                chartResults.Series.Add(seriesName);

                //Правим графиката да е от тип Spline (по подразбиране е Columns)
                chartResults.Series[seriesName].ChartType = SeriesChartType.Spline;

                //Свързваме добавената серия с данните от резултата - result.Value
                //X стойността е единица време
                chartResults.Series[seriesName].Points.DataBindXY(xValues, result.Value);
            }
        }
    }
}
