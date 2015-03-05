/******************************************************************************/
/*                                                                            */
/*   Program: MyThreadLock                                                    */
/*   An example how to sysconisize the access of a recourse by multiple       */
/*   threads                                                                  */
/*                                                                            */
/*   05.03.2015 0.0.0.0 uhwgmxorg Start                                       */
/*                                                                            */
/******************************************************************************/
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace MyThreadLock
{
    /// <summary>
    /// Class Program
    /// </summary>
    class Program
    {
        static public string FILE_NAME = @"C:\temp\file.txt";
        static public object threadLock = new object();

        /// <summary>
        /// CreateExportDirektory
        /// </summary>
        static public void CreateDirektory(string dir)
        {
            try
            {
                bool isExists = System.IO.Directory.Exists(dir);
                if (!isExists)
                    System.IO.Directory.CreateDirectory(dir);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int NumberOfThreads = 300;

            Console.WriteLine("Program MyThreadLock");

            // Crate file and direktory
            CreateDirektory(Path.GetDirectoryName(FILE_NAME));
            StreamWriter F = new StreamWriter(FILE_NAME);
            F.Close();

            Thread[] Threads = new Thread[NumberOfThreads];
            for (int i = 0; i < NumberOfThreads; i++)
            {
                Threads[i] = new Thread(new MyTask().Run);
                Threads[i].Name = "Thread " + i.ToString();
                Threads[i].Start();
            }

            // Wait until every thread is finished
            foreach (var thread in Threads)
                thread.Join();

            Console.WriteLine();
            Console.WriteLine("press any key to continue");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Class Task
    /// </summary>
    public class MyTask
    {
        /// <summary>
        /// Run
        /// </summary>
        public void Run()
        {
            InsertA();
            InsertB();
            InsertC();
        }

        /// <summary>
        /// InsertA
        /// </summary>
        private void InsertA()
        {
            lock (Program.threadLock)
            {
                string Message = "A from Tread " + Thread.CurrentThread.Name + "\n";
                Console.Write(Message);
                File.AppendAllText(Program.FILE_NAME, Message);
            }
        }

        /// <summary>
        /// InsertB
        /// </summary>
        private void InsertB()
        {
            lock (Program.threadLock)
            {
                string Message = "B from Tread " + Thread.CurrentThread.Name + "\n";
                Console.Write(Message);
                File.AppendAllText(Program.FILE_NAME, Message);
            }
        }

        /// <summary>
        /// InsertC
        /// </summary>
        private void InsertC()
        {
            lock (Program.threadLock)
            {
                string Message = "C from Tread " + Thread.CurrentThread.Name + "\n";
                Console.Write(Message);
                File.AppendAllText(Program.FILE_NAME, Message);
            }
        }
    }
}
