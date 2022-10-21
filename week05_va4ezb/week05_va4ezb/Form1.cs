using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using week05_va4ezb.Entities;
using week05_va4ezb.mnbservicereference;

namespace week05_va4ezb
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();
        public Form1()
        {
            InitializeComponent();

            
            refresh();

        }

        private void refresh()
        {
            Rates.Clear();
            comboBox1.DataSource = Currencies;
            dataGridView1.DataSource = Rates;
            lekerd();
            diagram();
        }
        private string lekerd()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.Text,
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()
            };
            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;

            dataGridView1.DataSource = Rates;


            var xml = new XmlDocument();
            xml.LoadXml(result);

            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                var child = (XmlElement)element.ChildNodes[0];
                rate.Currency = child.GetAttribute("curr");

                var unit = decimal.Parse(child.GetAttribute("unit"));
                var value = decimal.Parse(child.InnerText);
                if (unit != 0)
                    rate.value = value / unit;



                Rates.Add(rate);    
            }
            return result;
        }
        private void xml()
            {           
            }


            
        private void diagram()
            {
            chartRateData.DataSource = Rates;
            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;
            var legend = chartRateData.Legends[0];
            legend.Enabled = false;
            var chartarea = chartRateData.ChartAreas[0];
            chartarea.AxisX.MajorGrid.Enabled = false;
            chartarea.AxisY.MajorGrid.Enabled = false;
            chartarea.AxisY.IsStartedFromZero = false;




        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh();
        }
    }
}
