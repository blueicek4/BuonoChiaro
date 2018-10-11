using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blt.BuonoChiaro.BOL;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Net.Sockets;
using System.Net;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Dynamic;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string msg = String.Empty;
            dynamic obj;
            if (OpenTicket(null, out msg, out obj))
            {
                if(ValidateTicket(null, out msg, out obj))
                {
                    CancelTicket(null, out msg, out obj);
                }
            }
            Console.ReadLine();
            //IPHostEntry ipHostInfo = Dns.GetHostEntry("127.0.0.1");
            //IPAddress ipAddress = ipHostInfo.AddressList[0];

            //IPEndPoint remote = new IPEndPoint(ipAddress, 4444);

            //Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //sender.Connect(remote);
            //int byteSent = sender.Send(msg);
            //int byteRec = sender.Receive(bytes);
            //string response = Encoding.ASCII.GetString(bytes, 0, byteRec);

            //sender.Shutdown(SocketShutdown.Both);
            //sender.Close();
        }


        public static Boolean OpenTicket(string input, out string msg,  out dynamic response)
        {
            dynamic obj;
            try
            {
                var bc = new Blt.BuonoChiaro.BOL.TicketOpenReq();
                bc.NumeroScontrino = "1234";
                bc.CODDEV = "2222222222";
                bc.CODPIC = "33333333";
                bc.CODPIC = "CodPic1";
                //bc.TipoOperazione = "APERTURASCONTRINO";
                bc.Progressivo = "123456";
                bc.CodiceTransazione = "TLV";
                var s = bc.ToXML();

                Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //soc.
                System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse("127.0.0.1");
                System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 4444);
                soc.Connect(remoteEP);
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(s);
                soc.Send(byData);
                //

                //soc.Close();
                byte[] buffer = new byte[1024];
                int iRx = soc.Receive(buffer);
                char[] chars = new char[iRx];

                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
                System.String recv = new System.String(chars);

                XDocument doc = XDocument.Parse(recv);
                String jsonText = JsonConvert.SerializeXNode(doc);
                obj = JsonConvert.DeserializeObject<ExpandoObject>(jsonText);
                if (obj.CRQ.COMRES == "OK")
                {
                    msg = obj.CRQ.COMRES;
                    response = obj;
                    return true;
                }
                else
                {
                    msg = obj.CRQ.COMRES;
                    response = obj;
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                response = null;
                return false;
            }
        }

        public static Boolean ValidateTicket(string input, out string msg, out dynamic response)
        {
            dynamic obj;
            try
            {
                var bc = new Blt.BuonoChiaro.BOL.TicketValidationReq();
                bc.NumeroScontrino = "1234";
                bc.CODDEV = "2222222222";
                bc.CODPIC = "33333333";
                bc.CODPIC = "CodPic1";
                //bc.TipoOperazione = "VALIDAZIONE";
                bc.Progressivo = "123456";
                bc.CodiceTransazione = "TLV";
                bc.Rows.Add(new RowValidationRequest() { Line = "53393780000113000171200529" });
                var s = bc.ToXML();

                Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //soc.
                System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse("127.0.0.1");
                System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 4444);
                soc.Connect(remoteEP);
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(s);
                soc.Send(byData);
                //

                //soc.Close();
                byte[] buffer = new byte[1024];
                int iRx = soc.Receive(buffer);
                char[] chars = new char[iRx];

                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
                System.String recv = new System.String(chars);

                XDocument doc = XDocument.Parse(recv);
                String jsonText = JsonConvert.SerializeXNode(doc);
                obj = JsonConvert.DeserializeObject<ExpandoObject>(jsonText);
                if (obj.CRQ.COMRES == "OK")
                {
                    msg = obj.CRQ.COMRES;
                    response = obj;
                    return true;
                }
                else
                {
                    msg = obj.CRQ.COMRES;
                    response = obj;
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                response = null;
                return false;
            }
        }

        public static Boolean CancelTicket(string input, out string msg, out dynamic response)
        {
            dynamic obj;
            try
            {
                var bc = new Blt.BuonoChiaro.BOL.TicketValidationReq();
                bc.NumeroScontrino = "1234";
                bc.CODDEV = "2222222222";
                bc.CODPIC = "33333333";
                bc.CODPIC = "CodPic1";
                //bc.TipoOperazione = "STORNO";
                bc.Progressivo = "123456";
                bc.CodiceTransazione = "TLV";
                bc.Rows.Add(new RowValidationRequest() { Line = "53393780000113000171200529" });
                var s = bc.ToXML();

                Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //soc.
                System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse("127.0.0.1");
                System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 4444);
                soc.Connect(remoteEP);
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(s);
                soc.Send(byData);
                //

                //soc.Close();
                byte[] buffer = new byte[1024];
                int iRx = soc.Receive(buffer);
                char[] chars = new char[iRx];

                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
                System.String recv = new System.String(chars);

                XDocument doc = XDocument.Parse(recv);
                String jsonText = JsonConvert.SerializeXNode(doc);
                obj = JsonConvert.DeserializeObject<ExpandoObject>(jsonText);
                if (obj.CRQ.COMRES == "OK")
                {
                    msg = obj.CRQ.COMRES;
                    response = obj;
                    return true;
                }
                else
                {
                    msg = obj.CRQ.COMRES;
                    response = obj;
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                response = null;
                return false;
            }
        }

        public static Boolean CloseTicket(string input, out string msg, out dynamic response)
        {
            dynamic obj;
            try
            {
                var bc = new Blt.BuonoChiaro.BOL.TicketOpenReq();
                bc.NumeroScontrino = "1234";
                bc.CODDEV = "2222222222";
                bc.CODPIC = "33333333";
                bc.CODPIC = "CodPic1";
                //bc.TipoOperazione = "CHIUSURASCONTRINO";
                bc.Progressivo = "123456";
                bc.CodiceTransazione = "TLV";
                var s = bc.ToXML();

                Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //soc.
                System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse("127.0.0.1");
                System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 4444);
                soc.Connect(remoteEP);
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(s);
                soc.Send(byData);
                //

                //soc.Close();
                byte[] buffer = new byte[1024];
                int iRx = soc.Receive(buffer);
                char[] chars = new char[iRx];

                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
                System.String recv = new System.String(chars);

                XDocument doc = XDocument.Parse(recv);
                String jsonText = JsonConvert.SerializeXNode(doc);
                obj = JsonConvert.DeserializeObject<ExpandoObject>(jsonText);
                if (obj.CRQ.COMRES == "OK")
                {
                    msg = obj.CRQ.COMRES;
                    response = obj;
                    return true;
                }
                else
                {
                    msg = obj.CRQ.COMRES;
                    response = obj;
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                response = null;
                return false;
            }
        }

    }
}
