using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using HtmlAgilityPack;

namespace craiglistscrapper
{
    public partial class Form1 : Form
    {
       // private const string pattern = "<div class=\"anonemail\"> * </div>";
      //  private const string pattern = "<div class=\"anonemail\"> * </div>";
        //<div class="anonemail">td7jz-4876315202@pers.craigslist.org</div>
        
        //"(<div class=\"anonemail\" .*>)(.*)(<\\/div>)";

        //[^<>]* any amount of characters
        List<string> list = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            // SimpleGET("test");

          //  Extract("test");
            getUrls(txtLink.Text);

        }
        public void getUrls(string url)
        {//<p class="row"</p>
           /* string pattern = "(<p[^<>]*class=\"row\"[^<>]*>)(.*)(<\\/p>)";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            Stream responseStream = ((HttpWebResponse)request.GetResponse()).GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string str = reader.ReadToEnd();
            reader.Close();
            responseStream.Flush();
            responseStream.Close();*/
            list.Clear();
            var document = new HtmlWeb().Load(url);
            HtmlNodeCollection rowNodes = document.DocumentNode.SelectNodes("//p[@class='row']/a[@href][1]");
foreach (HtmlNode row in rowNodes)
{

    string link1 = row.Attributes["href"].Value;
     link1 = link1.Replace(".html", "");
   
    string test = txtLink.Text.Substring(txtLink.Text.IndexOf("search"));//grabs search/pet
    string test2 = txtLink.Text.Replace(test, "") + "reply" + link1; // gets rid of search/pet
    var document2 = new HtmlWeb().Load(test2);
    if (document2.DocumentNode.SelectSingleNode("//a[@href][@class='mailapp']") != null)
    {
        string meow = document2.DocumentNode.SelectSingleNode("//a[@href][@class='mailapp']").InnerText;
        list.Add(meow);
    }
 
}
//txtOutputBox.Text = node[0].InnerHtml;//.SelectNodes("//a")[0].OuterHtml;

          //  txtOutputBox.Text = str;
          /* // MatchCollection matches = Regex.Matches(str, pattern);
            foreach (Match match in Regex.Matches(str, pattern))
            {
                string str3 = match.Groups[1].Value;
              //  string test = "hello";
            }*/


        }
        public void Extract(string index)
        {
            string pattern = "(<div[^<>]*class=\"anonemail\"[^<>]*>)(.*)(<\\/div>)";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(index);
            request.Method = "GET";
            Stream responseStream = ((HttpWebResponse)request.GetResponse()).GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string str = reader.ReadToEnd();
            reader.Close();
            responseStream.Flush();
            responseStream.Close();
            MatchCollection matches = Regex.Matches(str, pattern);
            if (matches.Count > 0)
            {
                foreach (Match m in matches)
                {
                    // m.Groups[0]
                    //  Console.WriteLine("Inner DIV: {0}", );

                  // txtOutputBox.Text =  m.Groups[2].Value;//1 gives u anonymousemail, 0 whole thing, 2 inside div

                }
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {

            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt) | *.txt"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(dialog.FileName))
                {
                    foreach (string str in list)
                    {
                        writer.WriteLine(str);
                    }
                }
            }
        }

    }
}
