/*
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
using System.Threading;
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
                //Task.Run(() => EchoService.DoClient(client));
                Thread t = new Thread(() => EchoService.DoClient(client));
                t.Start();
            }
        }
    }
}
