using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectWebFreeFire.Utils;
using System;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ProjectWebFreeFire
{
    public partial class Event : Form
    {
        public Event()
        {
            InitializeComponent();

        }
        private void OpenWeb()
        {
            webBrowser1.Navigate(MainUsers.WebLink);
            url.Text = MainUsers.Host;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (WebClient web = new WebClient())
                {
                    string API = "https://" + MainUsers.Host + "/api/info?access_token=" + MainUsers.Token;
                    JObject myApi = JObject.Parse(web.DownloadString(API));
      
                    if ((string)myApi["status"] == "success")
                    {
                        //Lugar que sempre tem que ser alterado dependendo do evento.
                        if (myApi.ToString().Contains("player")){
                            pID.Text = "Jogador id: " + (string)myApi["data"]["player"]["uid"];
                            Diamount.Text = "Diamantes: " + (string)myApi["data"]["player"]["gem"];
                        }
                        else
                        {
                            pID.Text = "Jogador id: " + (string)myApi["data"]["uid"];
                            Diamount.Text = "Diamantes: " + (string)myApi["data"]["gems"];
                        }

                        button1.Enabled = false;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;

                        OpenWeb();

                        MessageBox.Show("informação do jogador carregada.");
                    }
                    else
                    {
                        MessageBox.Show("Algo de errado acontenceu, motivo: " + (string)myApi["status"]);
                    }
                }
            }
            catch (Exception ex)
            {
                webBrowser1.Dispose();
                MessageBox.Show("Error ao carregar a informação do jogador. \n " + ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Logs.WriteYellow(MainUsers.Header);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (WebClient web = new WebClient())
            {
                string API = "https://" + MainUsers.Host + "/api/info?access_token=" + MainUsers.Token;
                JObject myApi = JObject.Parse(web.DownloadString(API));
                Logs.WriteYellow(myApi);
            }
        }

        private void Event_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenWeb();
        }

        private void pID_Click(object sender, EventArgs e)
        {
            string id = pID.Text.Remove(0, 12);
            if(id != "0")
            {
                Clipboard.SetText(id);
                MessageBox.Show("ID Copiado com sucesso.");
            }
        }
    }
}
