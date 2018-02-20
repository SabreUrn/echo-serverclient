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
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPEchoClient
{
    class TCPEchoClient
    {
        static void Main(string[] args)
        {
            Console.Write("Enter IP: ");
            string ip = Console.ReadLine();
            TcpClient clientSocket = WaitForServer(ip);
            Console.WriteLine("Client ready");

            Stream ns = clientSocket.GetStream();  //provides a Stream
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            string message = "";
            string serverAnswer = "";


            while (true)
            {
                // enable automatic flushing

                 message = Console.ReadLine();
                try {
                    sw.WriteLine(message);
                    serverAnswer = sr.ReadLine();
                } catch(IOException) {
                    Console.WriteLine("Server connection closed.");
                    Console.WriteLine("Press any key to exit.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Server: " + serverAnswer);

            }
        }

        private static TcpClient WaitForServer(string ip) {
            TcpClient clientSocket = new TcpClient();
            bool serverFound = false;

            while (!serverFound) {
                try {
                    clientSocket = new TcpClient(ip, 6789);
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
