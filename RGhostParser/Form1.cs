using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;

namespace RGhostParser
{
    public partial class Form1 : Form
    {
        int i,s=0,f=0;
        string link,mask;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int start = int.Parse(textBox_start.Text);
                int end = int.Parse(textBox_end.Text);
                for (i = start; i <= end; i += 1)
                {
                    try
                    {
                        if (radioButton1.Checked)
                        {
                            link = "http://rghost.ru/";
                            mask = "<li data-share42-locale='ru' data-share42-text='(.*?)' id='share42'></li>";
                        }
                        if (radioButton2.Checked)
                        {
                            link = "http://zalil.ru/";
                            mask = "(.*?)&nbsp;&nbsp;&nbsp;(.*?)<br><br>";
                        }
                        progressBar1.Minimum = start;
                        progressBar1.Maximum = end;
                        progressBar1.Value = i;
                        button_start.Enabled = false;
                        if (i == end)
                            button_start.Enabled = true;
                        int url = i;
                        s += 1;
                        label5.Text = Convert.ToString(s);
                        WebClient web = new WebClient();
                        web.Encoding = System.Text.Encoding.UTF8;
                        web.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2) AppleWebKit/537.27 (KHTML, like Gecko) Chrome/26.0.1386.0 Safari/537.27");
                        web.Headers.Add("Accept", "*/*");
                        web.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4");
                        web.Headers.Add("Accept-Charset", "windows-1251,utf-8;q=0.7,*;q=0.3");
                        String html = web.DownloadString(link + Convert.ToString(url));
                        MatchCollection m1 = Regex.Matches(html, @mask);
                        if (radioButton1.Checked)
                        {
                            foreach (Match m in m1)
                            {
                                textBox_logs.Text += (m.Groups[1].Value + " - rghost.ru/" + Convert.ToString(url) + Environment.NewLine);
                                f++;
                                label6.Text = Convert.ToString(f);
                                break;
                            }
                        }
                        if (radioButton2.Checked)
                        {
                            foreach (Match m in m1)
                            {
                                textBox_logs.Text += (m.Groups[1].Value + " (" + m.Groups[2].Value + ") - zalil.ru/" + Convert.ToString(url) + Environment.NewLine);
                                f++;
                                label6.Text = Convert.ToString(f);
                                break;
                            }
                        }
                    }
                    catch (WebException)
                    {
                        //MessageBox.Show("403 ошибка");
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка ввода!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox_start.Text = "34369440";
            textBox_end.Text = "34369458";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            s = 0; label6.Text = "0";
            f = 0; label5.Text = "0";
            textBox_logs.Text = "";
        }
    }
}
