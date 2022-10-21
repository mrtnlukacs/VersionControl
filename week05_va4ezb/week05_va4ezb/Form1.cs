using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using week05_va4ezb.Entities;
using week05_va4ezb.mnbservicereference;

namespace week05_va4ezb
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        public Form1()
        {
            InitializeComponent();
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
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

        }
    }
}
