/*
 * TCPEchoClient
 *
 * Author Michael Claudius, ZIBAT Computer Scienc
 * Version 1.0. 2014.02.10
 * Copyright 2014 by Michael Claudius
 * Revised 2014.09.01, 2016.09.14
 * All rights reserved
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPEchoClient
{
    class TCPEchoClient
    {
        static void Main(string[] args)
        {
            //Console.ReadLine();
            TcpClient clientSocket = WaitForServer();
            Console.WriteLine("Client ready");

            Stream ns = clientSocket.GetStream();  //provides a Stream
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            string message = "";


            while (true)
            {
                // enable automatic flushing

                 message = Console.ReadLine();
                sw.WriteLine(message);
                string serverAnswer = sr.ReadLine();

                Console.WriteLine("Server: " + serverAnswer);

            }
        }

        private static TcpClient WaitForServer() {
            TcpClient clientSocket = new TcpClient();
            bool serverFound = false;

            while (!serverFound) {
                try {
                    clientSocket = new TcpClient("localhost", 6789);
                    serverFound = true;
                } catch (SocketException) {
                    Console.WriteLine("Cannot find server. Check if server is running.");
                    Console.WriteLine("Retrying in 5 seconds.");
                    System.Threading.Thread.Sleep(5000);
                }
            }

            return clientSocket;
        }
    }

}
