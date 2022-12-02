using mikroszimulacio.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mikroszimulacio
{

    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        Random rng = new Random(1234);


        public Form1()
        {
            InitializeComponent();
            Population = GetPopulation(textBox1.Text);
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");
            
            
        }
        private void Simualation()
        {
            for (int year = 2025; year <= numericUpDown1.Value; year++)
            {
                for (int i = 0; i < Population.Count; i++)
                {
                    SimStep(year, Population[i]);
                }
                int ferfiakszama = (from x in Population
                                    where x.Neme == Gender.Male && x.IsAlive
                                    select x).Count();
                int nokszama = (from x in Population
                                where x.Neme == Gender.Female && x.IsAlive
                                select x).Count();
                Console.WriteLine(
                    string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, ferfiakszama, nokszama));
            }
        }

        private void SimStep(int year, Person person)
        {
            
            if (!person.IsAlive) return;

          
            byte age = (byte)(year - person.Szulido);

            
            double pDeath = (from x in DeathProbabilities
                             where x.Neme == person.Neme && x.kor == age
                             select x.halalvaloszinuseg).FirstOrDefault();
           
            if (rng.NextDouble() <= pDeath)
                person.IsAlive = false;

            
            if (person.IsAlive && person.Neme == Gender.Female)
            {
               
                double pBirth = (from x in BirthProbabilities
                                 where x.kor == age
                                 select x.szulvaloszinusege).FirstOrDefault();
               
                if (rng.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.Szulido = year;
                    újszülött.Gyerekekszama = 0;
                    újszülött.Neme = (Gender)(rng.Next(1, 3));
                    Population.Add(újszülött);
                }
            }
        }
        

        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        Szulido = int.Parse(line[0]),
                        Neme = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        Gyerekekszama = int.Parse(line[2])
                    });
                }
            }

            return population;
        }

        public List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbability> bp = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    bp.Add(new BirthProbability()
                    {
                        kor = int.Parse(line[0]),
                        gyerekekszama = int.Parse(line[1]),
                        szulvaloszinusege = int.Parse(line[2])
                    });
                }
            }

            return bp;
        }

        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            List<DeathProbability> dp = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    dp.Add(new DeathProbability()
                    {
                        Neme = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        kor = int.Parse(line[1]),
                        halalvaloszinuseg = double.Parse(line[2])
                    });
                }
            }

            return dp;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            Simualation();
            DisplayResult();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK) return;
            textBox1.Text = ofd.FileName;

        }
    }
}
