using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace wget
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start");

            WebRequest webr = WebRequest.Create("http://server/page");
            HttpWebResponse resp = null;

            try
            {
                resp = (HttpWebResponse)webr.GetResponse();
            }
            catch (WebException e)
            {
                resp = (HttpWebResponse)e.Response;
            }


            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding(resp.CharacterSet));

            string sReadData = sr.ReadToEnd();

            switch (resp.StatusCode)
            {
                case HttpStatusCode.OK:        //HTTP 200 - всё ОК
                    break;
                case HttpStatusCode.Forbidden: //HTTP 403 - доступ запрещён
                    break;
                case HttpStatusCode.NotFound:  //HTTP 404 - документ не найден
                    break;
                case HttpStatusCode.Moved:     //HTTP 301 - документ перемещён
                    break;
                default:                       //другие ошибки
                    break;
            }


            /*
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://hashcode.ru/");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                Console.WriteLine("Нельзя обработать ответ (404)");
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Страница загружена");
            }

            response.Close();*/

            Console.WriteLine("finish");
            Console.ReadKey();
        }
    }
}
