using Newtonsoft.Json;
using ProjectWebFreeFire.Enums;
using ProjectWebFreeFire.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ProjectWebFreeFire
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "HttpWebRequest Freefire Evento";
            Logs.WriteBlue("@author: https://github.com/WesleyVale99");

            Logs.WriteCyan("Digite a URL do evento: ");
            string WebLink = Console.ReadLine();

            if (!string.IsNullOrEmpty(WebLink) && (WebLink.Contains("http://") || WebLink.Contains("https://")))
            {
                try
                {
                    clearLine(0, 1, 4);
                    Console.CursorTop = 1;

                    Logs.WriteYellow("Aguarde...");

                    HttpWebRequest web = WebRequest.CreateHttp(WebLink);
                    web.Method = TagsMethods.GET.ToString();
                    web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36 Edg/91.0.864.54";
                    HttpWebResponse response = web.GetResponse() as HttpWebResponse;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine(web.Address.Authority);
                        string[] token = WebLink.Split('=', '&');
                        if (!string.IsNullOrEmpty(token[1]))
                        {

                            Logs.WriteYellow("Token separado do jogador...");
                            Logs.WriteYellow("Agora Algumas informações do servidor...");
                            Logs.WriteYellow(JsonConvert.SerializeObject(web, Formatting.Indented));


                            Thread thread = new Thread(OpenEvent);
                            thread.SetApartmentState(ApartmentState.STA);
                            thread.Start();

                            StreamReader reader = new StreamReader(response.GetResponseStream());

                            MainUsers.WebLink = WebLink;
                            MainUsers.Token = token[1];
                            MainUsers.Header = reader.ReadToEnd();
                            MainUsers.Host = web.Address.Host;
                        }
                        else
                        {
                            Usage.MessageBox((IntPtr)0, WebLink, " Token Incorreto [ ! ]", 0);
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        Logs.WriteRed("site não atende os requisitos. " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    Usage.MessageBox((IntPtr)0, ex.ToString(), "Exception", 0);
                }

            }
            else
            {
                Usage.MessageBox((IntPtr)0, WebLink, " URL Incorreta [ ! ]", 0);
                Environment.Exit(0);
            }

            Process.GetCurrentProcess().WaitForExit();
        }
        public static void OpenEvent()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Event());
        }
        private static void clearLine(int left, int startindex,int finishindex)
        {
            for(int i = startindex; i < finishindex; i++)
            {
                int pLeft = Console.CursorLeft;
                int pTop = Console.CursorTop;

                Console.SetCursorPosition(left, i);
                Console.Write(new string(' ', Console.BufferWidth - Console.CursorLeft));

                Console.SetCursorPosition(pLeft, pTop);
            }
        }
    }
}
