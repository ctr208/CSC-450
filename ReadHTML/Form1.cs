using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using System.IO;

namespace ReadHTML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            string text = System.IO.File.ReadAllText(@"C:\Users\ctr20\Projects\VisualStudio Projects\ReadHTML\ReadHTML\TextFile1.txt");
            doc.LoadHtml(text);
            var headers = doc.DocumentNode.SelectNodes("//tr/th");
            DataTable table = new DataTable();

            foreach (HtmlNode header in headers)
                try
                {
                    if (table.Columns.Count <= 18) {
                        table.Columns.Add(header.InnerText); // create columns from th
                                                             // select rows with td elements 
                    }
                }
                catch (Exception ex)
                {
                    
                }
                foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
                    table.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText).ToArray());

            dataGridView1.DataSource = table;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver(@"C:\Users\ctr20\Desktop\")
            {
                Url = "https://seanet.uncw.edu/TEAL/twbkwbis.P_GenMenu?name=homepage"
            };

            IWebElement link;

            link = driver.FindElement(By.LinkText("Search for Courses"));

            link.Click();

            //////////////////////////////////////////////////////////
            
            IWebElement mySelectElement = driver.FindElement(By.Name("term_in"));

            SelectElement dropDown = new SelectElement(mySelectElement);

            dropDown.SelectByText("Fall 2018");


            IWebElement submitButton;

            submitButton = driver.FindElement(By.TagName("form"));

            submitButton.Submit();

            //////////////////////////////////////////////////////////

            IWebElement mySelectElement2 = driver.FindElement(By.Id("subj_id"));

            SelectElement dropDown2 = new SelectElement(mySelectElement2);

            IList<IWebElement> options2 = dropDown2.Options;

            foreach(IWebElement we in options2)
            {
                var str = we.Text;
                dropDown2.SelectByText(str);
            }

            IWebElement submitButton2;

            submitButton2 = driver.FindElement(By.TagName("form"));

            submitButton2.Submit();

            //////////////////////////////////////////////////////////

            var html = driver.PageSource;

            //StreamReader sr = new StreamReader(html);
            //StreamWriter sw = new StreamWriter(@"C:\Users\ctr20\Projects\VisualStudio Projects\ReadHTML\ReadHTML\TextFile1.txt");

            //var line = "";

            File.WriteAllText(@"C: \Users\ctr20\Projects\VisualStudio Projects\ReadHTML\ReadHTML\TextFile1.txt", html);
        }
    }
}
