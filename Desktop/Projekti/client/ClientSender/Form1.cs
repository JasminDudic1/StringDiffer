using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Imaging;

using WebSocketSharp;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace ClientSender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string path = "config.json";
        bool connected;
        bool sharing = false;

        WebSocket ws;
        int amount = 0;
        int pongMiss = 0;
        //string server = "si-grupa5.herokuapp.com";
        string server = "109.237.47.100:25565";


        // eba54ce1-1df9-49ca-b104-801a8827f911

        //9003a70a-afcd-11eb-8529-0242ac130003

        private void Form1_Load(object sender, EventArgs e)
        {



        }

        private Task OnError(WebSocketSharp.ErrorEventArgs errorEventArgs)
        {
            Console.Write("Error: {0}, Exception: {1}", errorEventArgs.Message, errorEventArgs.Exception);
            return Task.FromResult(0);
        }

     
        private Task OnMessage(MessageEventArgs messageEventArgs)
        {
            string text = messageEventArgs.Text.ReadToEnd();
            JObject json = JObject.Parse(text);

            if (json["type"].ToString() == "Connected")
            {
                statusLbl.Invoke(new Action(() => statusLbl.BackColor = Color.Green));
                statusLbl.Invoke(new Action(() => statusLbl.Text = "Connected"));
                connected = true;
                recieveBox.Invoke(new Action(() => recieveBox.Text = text + amount));
            }
            else if (json["type"].ToString() == "Disconnected")
            {
                statusLbl.Invoke(new Action(() => statusLbl.BackColor = Color.Red));
                statusLbl.Invoke(new Action(() => statusLbl.Text = "Disconnected"));
                connected = false;
            }
            else if (json["type"].ToString() == "command")
            {
                recieveBox.Invoke(new Action(() => recieveBox.Text = text));
                sendMessage("command_result", "radi", nameBox.Text, locationBox.Text);
            }
            else if (json["type"].ToString() == "getScreenshot")
            {

                recieveBox.Invoke(new Action(() => recieveBox.Text = "Trazi image" + amount));
                sendScreenshot();

            }
            else if (json["type"].ToString() == "getFileDirect")
            {
                recieveBox.Invoke(new Action(() => recieveBox.Text = "Trazi direct file " + json["fileName"].ToString()));
                sendFile(json["fileName"].ToString(),"sendFileDirect");
            }
            else if (json["type"].ToString() == "getFile")
            {
                recieveBox.Invoke(new Action(() => recieveBox.Text = "Trazi file" + json["fileName"].ToString()));
                sendFile(json["fileName"].ToString(),"sendFile");
            }
            else if (json["type"].ToString() == "putFile")
            {
                recieveBox.Invoke(new Action(() => recieveBox.Text = "Stavlja file" + json["fileName"].ToString()));
                saveFile(json["fileName"].ToString(), json["data"].ToString());
            }
            else if (json["type"].ToString() == "putFiles")
            {
                saveFiles(json["files"].ToString());
            }
            else if (json["type"].ToString() == "sendInfo")
            {
                ws.Send("{ \"type\":\"" + "savedFile" + "\", \"message\":\"" + "Success" + "\", \"deviceUid\":\"" + uidbox.Text +  "\"}");

            }
            else if (json["type"].ToString() != "ping")
            {
               // recieveBox.Invoke(new Action(() => recieveBox.Text = text));
                sendMessage("Empty", "komanda nije cd", nameBox.Text, locationBox.Text);
            }
            return Task.FromResult(0);

        }

        private void saveFile(string fileName, string data)
        {

            File.WriteAllBytes(fileName, Convert.FromBase64String(data));
            ws.Send("{ \"type\":\"" + "savedFile" + "\", \"message\":\"" + "Success" + "\", \"name\":\"" + nameBox.Text + "\", \"location\":\"" + locationBox.Text + "\", \"deviceUid\":\"" + uidbox.Text + "\"}");


        }

        private void saveFiles(string files)
        {

            JArray json = JArray.Parse(files);

            int length = json.Count;
            string toReturn = "{ \"type\":\"" + "savedFiles" + "\", \"deviceUid\":\"" + uidbox.Text + "\", \"message\":" + " [ ";


            foreach(JObject o in json)
            {
                try
                {
                   File.WriteAllBytes(o.GetValue("fileName").ToString(), Convert.FromBase64String(o.GetValue("data").ToString()));
                    toReturn += "{ \"message\":\"" + o.GetValue("fileName").ToString() + " Written"+ "\"},";
                }
                catch(Exception e)
                {
                    toReturn += "{ \"message\":\"" + o.GetValue("fileName").ToString() + " Not Written" + "\"},";
                }
            }

            
            toReturn += "{ " + "\"Message\":\"" + "DONE" + "\"} ] }";

            ws.Send(toReturn);
          

        }

        private void sendMessage(string type, string message, string name, string location)
        {
            ws.Send("{ \"type\":\"" + type + "\", \"message\":\"" + message + "\", \"name\":\"" + name + "\", \"location\":\"" + location + "\", \"deviceUid\":\"" + uidbox.Text + "\"}");
        }

        public void Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                textBox1.Text = reader.ReadToEnd();
            }


        }

        private void Post()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://"+server+"/login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            CookieContainer cok = new CookieContainer();
            httpWebRequest.CookieContainer = cok;
            httpWebRequest.AllowAutoRedirect = false;
            httpWebRequest.Headers.Add("Authorization", uidbox.Text);

            using (var streamWriter = new

            StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{ \"id\":\"" + uidbox.Text + "\"}";

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            reconnect();

        }

        private void sendScreenshot()
        {

            amount++;
            Bitmap captureBitmap = new Bitmap(1920, 1080, PixelFormat.Format32bppArgb);



            Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);
            captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
      
            System.IO.MemoryStream ms = new MemoryStream();
            captureBitmap.Save(ms, ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            var base64String = Convert.ToBase64String(byteImage); // Get Base64

            sendMessage("sendScreenshot", base64String, nameBox.Text, locationBox.Text);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!connected) return;

            byte[] bytes = System.IO.File.ReadAllBytes(path);
            /* ws.Send(bytes);
             sendMessage("sendFile", bytes, nameBox.Text, locationBox.Text);*/


            string base64Config = Convert.ToBase64String(bytes);

            recieveBox.Text = " i sent it ";
            ws.Send("{ \"type\":\"" + "sendFile" + "\", \"config\":\"" + base64Config + "\", \"name\":\"" +
               nameBox.Text + "\", \"location\":\"" + locationBox.Text + "\", \"deviceUid\":\"" + uidbox.Text +
               "\", \"fileName\":\"" + "config.json" + "\"}");


        }

        private void sendFile(string fileName,string type = "sendFile")
        {
            byte[] bytes = System.IO.File.ReadAllBytes(fileName);
            string base64Config = Convert.ToBase64String(bytes);
            ws.Send("{ \"type\":\"" + type + "\", \"message\":\"" + base64Config + "\", \"data\":\"" + base64Config + "\", \"name\":\"" +
               nameBox.Text + "\", \"location\":\"" + locationBox.Text + "\", \"deviceUid\":\"" + uidbox.Text +
               "\", \"fileName\":\"" + fileName+ "\"}");
        }

        private void statusLbl_Click(object sender, EventArgs e)
        {
            if (nameBox.Text.Length == 0 || locationBox.Text.Length == 0) return;
            statusLbl.Text = "Disconnected";
            statusLbl.BackColor = Color.Red;
            reconnect();
        }

        private void reconnect()
        {
            ws = new WebSocket(url: "ws://"+server, onMessage: OnMessage, onError: OnError,onClose: OnClose);
            //ws = new WebSocket(url: "ws://si-grupa5.herokuapp.com", onMessage: OnMessage, onError: OnError);
            ws.SetCookie(new WebSocketSharp.Net.Cookie("cookie", uidbox.Text));
            ws.Connect().Wait();

            sendMessage("sendCredentials", "nesto", nameBox.Text, locationBox.Text);

            recieveBox.Text = "";
            timer2.Enabled = true;
            timer2.Start();
        }

        private Task OnClose(CloseEventArgs arg)
        {

            //ovdje timer koji se vrti svakih 10sec
            //i ugasi se ako conn vrati true
            //stavis da conn vraca true ako se spoji

            return Task.FromResult(0);
        }

        private void disconenct()
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (nameBox.Text.Length == 0 || locationBox.Text.Length == 0) return;
            statusLbl.Text = "Disconnected";
            statusLbl.BackColor = Color.Blue;
            // reconnect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Post();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
                //if (ws.IsAlive().Result == true)
                //{
                    ws.Send("{ \"type\":\"" + "pong" + "\"}");
                /*statusLbl.Invoke(new Action(() => recieveBox.Text = "Ponged"));
                pongMiss = 0;
                }
                else if (pongMiss++ >= 3)
                {

                    statusLbl.Invoke(new Action(() => statusLbl.BackColor = Color.Red));
                    statusLbl.Invoke(new Action(() => statusLbl.Text = "Disconnected"));
                    connected = false;
            }
            else
            {
                statusLbl.Invoke(new Action(() => recieveBox.Text = "No response "+pongMiss));
            }*/
        }
    }
}
