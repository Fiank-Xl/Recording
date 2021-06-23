using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.IO;

namespace Recording
{
    public partial class Form1 : Form
    {
        private Process _process = null;
        private System.Timers.Timer _timer = new System.Timers.Timer();
        private DateTime _span = DateTime.MinValue;
        public Form1()
        {
            InitializeComponent();
            _timer.Interval = 1000;
            _timer.Elapsed += _timer_Elapsed;
            SetTimeVal(_span.ToString("HH:mm:ss") + System.Environment.NewLine);
        }
        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _span = _span.AddSeconds(1);
            SetTimeVal(_span.ToString("HH:mm:ss")+System.Environment.NewLine);
        }

        public delegate void del_SetText(string s);
        private void SetVal(string s) 
        {
            if (tb_Message.InvokeRequired)
            {
                del_SetText del = new del_SetText(SetVal);
                tb_Message.Invoke(del, s);
            }
            else
            {
                tb_Message.AppendText(s);
            }
        }
        private void SetTimeVal(string s)
        {
            if (lb_time.InvokeRequired)
            {
                del_SetText del = new del_SetText(SetTimeVal);
                lb_time.Invoke(del, s);
            }
            else
            {
                lb_time.Text = "录制时间：" + s;
            }
        }
        private async void bt_Start_Click(object sender, EventArgs e)
        {
            try
            {
                if (_process != null)
                {
                    MessageBox.Show("正在录制中，请勿重复录制。");
                    return;
                }
                string url = tb_httpUrl.Text;
                string path = tb_Save.Text;
                if (string.IsNullOrWhiteSpace(url))
                {
                    MessageBox.Show("URL为空");
                    tb_httpUrl.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(path))
                {
                    MessageBox.Show("路径为空");
                    tb_Save.Focus();
                    return;
                }
                string ffmpeg = System.Environment.CurrentDirectory + @"\ffmpeg\ffmpeg.exe";
                if (string.IsNullOrWhiteSpace(ffmpeg))
                {
                    MessageBox.Show("ffmpeg配置错误");
                    return;
                }
                tb_Message.Text += "正在解析流地址..." + System.Environment.NewLine;
                HttpClient httpClient = new HttpClient();
                var urlstring = url;
                HttpResponseMessage response = httpClient.GetAsync(urlstring).Result;
                var content = response.Content.ReadAsStringAsync().Result;
                string[] strArr = content.Split(new string[] { @"</script>", "<script>" }, StringSplitOptions.RemoveEmptyEntries);
                string liveUrl = "";
                foreach (var item in strArr)
                {
                    if (item.Contains("window.__INIT_PROPS__"))
                    {
                        int len = item.IndexOf(':');
                        string str = item.Substring(len + 5).Trim();
                        str = str.Substring(0, str.Length - 1);
                        var serializer = new JavaScriptSerializer();
                        serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

                        dynamic obj = serializer.Deserialize(str, typeof(object));
                        //liveUrl = obj.room.stream_url.hls_pull_url;\
                        liveUrl = obj.room.stream_url.rtmp_pull_url;
                        string _userName = obj.room.owner.nickname;
                        path += _userName + @"\";
                    }
                }
                tb_Message.Text += "录制链接：" + liveUrl + System.Environment.NewLine;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                DateTime time = DateTime.Now;
                path += time.ToString("yyyyMMddHHmmss") + ".mp4";
                path = Utiles.GetUTF8(path);
                await Task.Run(() =>
                {
                    _process = new Process();
                    _process.StartInfo.FileName = ffmpeg;
                    _process.StartInfo.Arguments = @" -i " + liveUrl + " -c:v copy -c:a copy -bsf:a aac_adtstoasc \"" + path + "\"";
                    _process.StartInfo.UseShellExecute = false;
                    _process.StartInfo.RedirectStandardInput = true;
                    _process.StartInfo.RedirectStandardError = true;
                    _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    _process.StartInfo.RedirectStandardOutput = true;
                    _process.StartInfo.CreateNoWindow = true;
                    _process.ErrorDataReceived += new DataReceivedEventHandler(Output);
                    SetVal("准备录制" + System.Environment.NewLine);
                    _timer.Start();
                    _process.Start();
                    _process.BeginErrorReadLine();
                    _process.WaitForExit();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Output(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    string message = e.Data;
                    if (message.ToUpper().Contains("SERVER RETURNED 404 NOT FOUND"))
                    {
                        SetVal("录制错误：地址错误或未开始直播" + System.Environment.NewLine);
                        StopRecord();
                    }
                    //else 
                    //{
                    //    SetVal(message + System.Environment.NewLine);
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_Stop_Click(object sender, EventArgs e)
        {
                    StopRecord();
        }

        private void bt_Select_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = fbd.SelectedPath + @"\";
            }
            tb_Save.Text = path;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否退出?", "提示:", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                StopRecord();
                e.Cancel = false;               
            }
            else if (dr == DialogResult.Cancel)
            {
                e.Cancel = true;                 
            }
        }
        private void StopRecord() 
        {

            try
            {
                if (_process != null)
                {
                    _process.StandardInput.WriteLine("q");
                    _process.Close();
                    _process.Dispose();
                    _process = null;
                    _timer.Stop();
                    _span = DateTime.MinValue;
                    tb_Message.Text += "已停止录制！" + System.Environment.NewLine;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
