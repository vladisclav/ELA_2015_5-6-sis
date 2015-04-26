using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Messaging;

namespace ImageProcessingWorker
{
    class Program
    {
        static void Main(string[] args)
        { Console.WriteLine("Worker is running");
            while (true)
            {
                MessageQueue messageQueue = null;

                using (messageQueue = new MessageQueue(@".\Private$\Images")) //Подключение к очереди на локальном компьютере.
                {
                    System.Messaging.Message[] messages = messageQueue.GetAllMessages(); //Заполнение массива с копиями всех сообщений в очереди.

                    foreach (System.Messaging.Message message in messages) //Цикл по сообщениям
                    {
                        messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(Task) });// Установоление Formatter для того чтобы указать что тело содержит Order
                        Task t = (Task)messageQueue.Receive().Body; //получаем и форматируем сообщение.
                        Console.WriteLine("New task "+t.name); //Отображаем сообщение.
                        string preparedArguments = null;
                        if(t.height.Equals("") || t.width.Equals(""))
                        {
                            if (t.degrees.Equals("")) 
                            {
                                if (t.blackwhite.Equals("true")) //тольео белое/черное
                                {
                                    Console.WriteLine("//only b/w");
                                    preparedArguments = @"-colorSpace Gray D:\sisgui\sisgui\uploads\" + t.name + @" D:\sisgui\sisgui\results\" + t.name;
                                }
                                if (t.blackwhite.Equals("false"))//копируем
                                {

                                    Console.WriteLine("just copy");
                                    preparedArguments = @" D:\sisgui\sisgui\uploads\" + t.name + @" D:\sisgui\sisgui\results\" + t.name;
                                }
                            }
                            else
                            {
                                if (t.blackwhite.Equals("true"))//поворот и белое/черное
                                    {

                                        Console.WriteLine("//rotate and b/w");
                                        preparedArguments = @"-rotate " + t.degrees + @" -colorSpace Gray D:\sisgui\sisgui\uploads\" + t.name + @" D:\sisgui\sisgui\results\" + t.name;
                                    }
                                    if (t.blackwhite.Equals("false"))//поворачиваем
                                    {

                                        Console.WriteLine("//just rotate");
                                        preparedArguments = @"-rotate " + t.degrees + @" D:\sisgui\sisgui\uploads\" + t.name + @" D:\sisgui\sisgui\results\" + t.name;
                                    }
                            }
                        }
                        else
                        {
                            if (t.degrees.Equals(""))
                            {
                                if (t.blackwhite.Equals("true")) //меняем размер и белое/черное
                                {

                                    Console.WriteLine("resize and b/w");
                                    preparedArguments = @"-resize  " + t.height + "x" + t.width + @" -colorSpace Gray D:\sisgui\sisgui\uploads\" + t.name + @" D:\sisgui\sisgui\results\" + t.name;
                                }
                                if (t.blackwhite.Equals("false"))//меняем размер
                                {

                                    Console.WriteLine("//resize");
                                    preparedArguments = @"-resize  " + t.height + "x" + t.width + @" D:\sisgui\sisgui\uploads\" + t.name + @" D:\sisgui\sisgui\results\" + t.name;
                                }
                            }
                            else
                            {
                                if (t.blackwhite.Equals("true"))//размер, поворот и белое/черное
                                {

                                    Console.WriteLine("//resize, rotate and b/w");
                                    preparedArguments = @"-resize  " + t.height + "x" + t.width + @" -rotate " + t.degrees + @" -colorSpace Gray D:\sisgui\sisgui\uploads\" + t.name + @" D:\sisgui\sisgui\results\" + t.name;
                                }
                                if (t.blackwhite.Equals("false"))//размер и поворот
                                {
                                    Console.WriteLine("//resize & rotate");

                                    preparedArguments = @"-resize  " + t.height + "x" + t.width + @" -rotate " + t.degrees + @" D:\sisgui\sisgui\uploads\" + t.name + @" D:\sisgui\sisgui\results\" + t.name;
                                }

                            }

                        }
                        var proc = new Process // создаем процесc
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = @"C:\Program Files\ImageMagick-6.9.0-Q16\convert.exe", // расположение нашего Worker ImageMagick
                                Arguments = preparedArguments,
                                UseShellExecute = false,
                                RedirectStandardError = true,
                                CreateNoWindow = true
                            }
                        };

                        proc.Start();
                        string error = proc.StandardError.ReadToEnd();
                        proc.WaitForExit();
                        
                }
            }

        }
       }
    }
}
