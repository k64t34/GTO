using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wget
{
    class Program
    {
        //[STAThread] // need for WebBrowser in console app
        static void Main(string[] args)
        {
            //string url = "http://hashcode.ru";
            //string url = "https://sd.cdu.so/sd/services/rest/check-status";
            //string url = "http://yafg.fgru";
            string url = "http://ya.ru/";
            string correctAnswer = "Operation completed successfully";
            Console.WriteLine("Start check service REST API ");
            Console.WriteLine("Connect to {0}",url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response;// = (HttpWebResponse)request.GetResponse();            
            try
            {                
                response = (HttpWebResponse)request.GetResponse();
            switch (response.StatusCode)
               {
           case HttpStatusCode.OK:        //HTTP 200 - всё ОК
               Console.WriteLine("HTTP 200 OK");
               break;
           case HttpStatusCode.Forbidden: //HTTP 403 - доступ запрещён
               Console.WriteLine("HTTP 403 доступ запрещён");
               break;
           case HttpStatusCode.NotFound:  //HTTP 404 - документ не найден
               Console.WriteLine("HTTP 404 документ не найден");
               break;
           case HttpStatusCode.Moved:     //HTTP 301 - документ перемещён
               Console.WriteLine("HTTP 301  документ перемещён");
               break;
           default:                       //другие ошибки
               Console.WriteLine("HTTP XXX другие ошибки");
               break;
            }                
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream, Encoding.GetEncoding(response.CharacterSet));
                string sReadData = sr.ReadToEnd();
                Console.WriteLine("Get {0} bytes {1} charset {2}", sReadData.Length, response.ContentType, response.CharacterSet);
                int Start_Position = 0, End_Position = sReadData.Length;
                int linewidth = 77;
                if (End_Position>linewidth)
                    Console.WriteLine("{0}...",sReadData.Substring(0, linewidth));
                else
                    Console.WriteLine(sReadData);

                #region RegExpression
                //Использование регулярных выражений
                //[^>] =любой символ кроме > Жадная и ленивая квантификация https://ru.wikipedia.org/wiki/%D0%A0%D0%B5%D0%B3%D1%83%D0%BB%D1%8F%D1%80%D0%BD%D1%8B%D0%B5_%D0%B2%D1%8B%D1%80%D0%B0%D0%B6%D0%B5%D0%BD%D0%B8%D1%8F
                // * любой симовл ноль или более
                // ? любой симовл ноль или один
                //https://regex101.com/
                //https://ru.wikipedia.org/wiki/%D0%A0%D0%B5%D0%B3%D1%83%D0%BB%D1%8F%D1%80%D0%BD%D1%8B%D0%B5_%D0%B2%D1%8B%D1%80%D0%B0%D0%B6%D0%B5%D0%BD%D0%B8%D1%8F
                //https://ru.stackoverflow.com/questions/189364/%D0%92%D1%8B%D1%80%D0%B5%D0%B7%D0%B0%D1%82%D1%8C-%D1%82%D0%B5%D0%BA%D1%81%D1%82-%D0%BC%D0%B5%D0%B6%D0%B4%D1%83-%D1%82%D0%B5%D0%B3%D0%B0%D0%BC%D0%B8-%D1%80%D0%B5%D0%B3%D1%83%D0%BB%D1%8F%D1%80%D0%BA%D0%BE%D0%B9
                //https://github.com/forcewake/Benchmarks/blob/master/src/Benchmarks.HtmlParsers/Benchmarks/TableBenchmark.cs#L207-L261
                //Regex rowRegex = new Regex("<tr[^>]*?>(?<rowContent>((?!</tr>).)*)</tr>",                
                //my work Regex body = new Regex(@"<body[^>]*>([^<]*<[^>]*>[^<]*)*<\/body>", RegexOptions.IgnoreCase);


                Regex bodyRegex = new Regex(@"<body[^>]*>", RegexOptions.IgnoreCase); //https://learn.microsoft.com/ru-ru/dotnet/api/system.text.regularexpressions.matchcollection?view=net-7.0
                MatchCollection matches = bodyRegex.Matches(sReadData);
                string bodyText = sReadData; 
                if (matches.Count > 0)
                {                    
                    Start_Position = matches[0].Index + matches[0].Length;
                    Console.WriteLine("start body tag found in position {0}/{1}", Start_Position,sReadData.Length);
                    bodyRegex = new Regex(@"<\/body>", RegexOptions.IgnoreCase);
                    matches = bodyRegex.Matches(sReadData);
                    if (matches.Count > 0)
                    {
                        Console.WriteLine("end body tag found in position   {0}/{1}", matches[0].Index, sReadData.Length);
                        End_Position = matches[0].Index - Start_Position;
                    }
                    else
                        Console.WriteLine("end body tag not found");
                        End_Position = sReadData.Length - Start_Position;
                    
                    if (End_Position > 0)
                    {
                        bodyText = sReadData.Substring(Start_Position, End_Position);
                    }
                }
                else
                    Console.WriteLine("start body tag not found ");
                #endregion
                Console.WriteLine("Correct answer = {0}", correctAnswer);
                Console.Write("Compare text   = ");
                if (bodyText.Length > linewidth)
                    Console.WriteLine("{0}...", bodyText.Substring(0, linewidth));
                else
                    Console.WriteLine(bodyText);

                
                if (string.Compare(bodyText, correctAnswer, StringComparison.OrdinalIgnoreCase) == 0) //https://learn.microsoft.com/ru-ru/dotnet/api/system.string.compare?view=net-7.0#system-string-compare(system-string-system-string)
                {
                    Console.WriteLine("Service REST API is OK");
                }
                else
                {
                    Console.WriteLine("Service REST API is FAIL");
                }





                #region
                //WebBrowser webBrowser = new WebBrowser();
                //webBrowser.AllowNavigation = true;
                //webBrowser.Navigate(url);
                //System.Threading.Thread.Sleep(10000);
                //webBrowser.DocumentStream = stream;
                //HtmlDocument htmlDoc = webBrowser.Document;
                //webBrowser.DocumentStream = response.GetResponseStream();                
                //webBrowser.DocumentText = "<html><body>Please enter your name:<br/>" +        "<input type='text' name='userName'/><br/>" +        "<a href='http://www.microsoft.com'>continue</a>" +        "</body></html>";//                sReadData;                */
                //System.Threading.Thread.Sleep(3000);
                //Console.WriteLine(webBrowser.DocumentText.Length);
                //Console.WriteLine(webBrowser.DocumentText);
                //Console.WriteLine(webBrowser.Document.Body.InnerText.Length);
                //Console.WriteLine(webBrowser.Document.Body.InnerText);
                //Console.WriteLine("---------------------------------------");                 
                //Console.WriteLine(htmlDoc.Body.ToString());
                //Console.WriteLine("---------------------------------------");
                #endregion
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
                response = (HttpWebResponse)e.Response;
            }
            /*switch (response.StatusCode)
            {
                case HttpStatusCode.OK:        //HTTP 200 - всё ОК
                    Console.WriteLine("HTTP 200 OK");
                    break;
                case HttpStatusCode.Forbidden: //HTTP 403 - доступ запрещён
                    Console.WriteLine("HTTP 403 доступ запрещён");
                    break;
                case HttpStatusCode.NotFound:  //HTTP 404 - документ не найден
                    Console.WriteLine("HTTP 404 документ не найден");
                    break;
                case HttpStatusCode.Moved:     //HTTP 301 - документ перемещён
                    Console.WriteLine("HTTP 301  документ перемещён");
                    break;
                default:                       //другие ошибки
                    Console.WriteLine("HTTP XXX другие ошибки");
                    break;
            }*/            
            Console.WriteLine("Finish check");
            Console.ReadKey();
        }
    }
}
