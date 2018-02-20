﻿/*
 * TCPEchoServer
 *
 * Author Michael Claudius, ZIBAT Computer Science
 * Version 1.0. 2014.02.10
 * Copyright 2014 by Michael Claudius
 * Revised 2014.09.01
 * All rights reserved
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TCPEchoServer {
    class TCPEchoServer {

        public static void Main(string[] args) {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = new TcpListener(IPAddress.Any, 6789);
            
            serverSocket.Start();
            Console.WriteLine("Server started");
            
            while (true) {
                TcpClient client = serverSocket.AcceptTcpClient();
                Task.Run(() => DoClient(client));
            }

            /*
            //TcpClient connectionSocket = serverSocket.AcceptTcpClient();
            //Socket connectionSocket = serverSocket.AcceptSocket();
            //Console.WriteLine("Server activated");
            Stream ns = connectionSocket.GetStream();
            // Stream ns = new NetworkStream(connectionSocket);

            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string message = sr.ReadLine();
            string answer = "";
            while (message != null && message != "")
            {

                Console.WriteLine("Client: " + message);
                answer = message.ToUpper();
                sw.WriteLine(answer);
                message = sr.ReadLine();

            }
            //ns.Close();
            //connectionSocket.Close();
            //serverSocket.Stop();
            */
        }

        public static void DoClient(TcpClient client) {
            using (NetworkStream ns = client.GetStream())
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns)) {
                sw.AutoFlush = true;
                while (true) {
                    //string readMessage = sr.ReadLine();
                    string readMessage = "";
                    try {
                        readMessage = sr.ReadLine();
                    } catch (IOException) {
                        Console.WriteLine("Client connection closed.");
                        return;
                    }
                    string writeMessage = "";

                    Console.WriteLine(readMessage);
                    while(!String.IsNullOrWhiteSpace(readMessage)) {
                        Console.WriteLine("Client: " + readMessage);
                        writeMessage = readMessage.ToUpper();
                        sw.WriteLine(writeMessage);
                        try {
                            readMessage = sr.ReadLine();
                        } catch (IOException) {
                            Console.WriteLine("Client connection closed.");
                            return;
                        }
                    }
                }
            }
        }
    }

}


