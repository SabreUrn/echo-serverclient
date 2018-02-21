using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPEchoServer {
    public static class EchoService {
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
                    while (!String.IsNullOrWhiteSpace(readMessage)) {
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
