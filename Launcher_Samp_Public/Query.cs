using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Launcher_Samp_Public
{
    class Api
    {
        public static string ServerName { get; internal set; }
        public static string Player { get; internal set; }
        public static string MaxPlayer { get; internal set; }
        public static string ServerPassword { get; internal set; }
        public static string Gamemode { get; internal set; }
        public static string Language { get; internal set; }
        public static string IP { get; internal set; }
        public static int PORT { get; internal set; }

        public static string Pagesize { get; internal set; }
        public static string Fpslimit { get; internal set; }
        public static string Disableheadmove { get; internal set; }
        public static string Ime { get; internal set; }
        public static string Multicore { get; internal set; }
        public static string Directmode { get; internal set; }
        public static string Audiomsgoff { get; internal set; }
        public static string Nonametagstatus { get; internal set; }
        public static string Fontweight { get; internal set; }
        public static string Audioproxyoff { get; internal set; }
        public static string Timestamp { get; internal set; }

        public class Query
        {
            Socket qSocket;
            IPAddress address;
            int _port = 0;
            string[] results;
            int _count = 0;
            DateTime[] timestamp = new DateTime[2];

            public Query(string IP, int port)
            {
                qSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
                {
                    SendTimeout = 5000,
                    ReceiveTimeout = 5000
                };

                try
                {
                    address = Dns.GetHostAddresses(IP)[0];
                }

                catch
                {

                }

                _port = port;
            }

            public bool Send(char opcode)
            {
                try
                {
                    EndPoint endpoint = new IPEndPoint(address, _port);

                    using (MemoryStream stream = new MemoryStream())
                    {
                        using (BinaryWriter writer = new BinaryWriter(stream))
                        {
                            writer.Write("SAMP".ToCharArray());

                            string[] SplitIP = address.ToString().Split('.');

                            writer.Write(Convert.ToByte(Convert.ToInt32(SplitIP[0])));
                            writer.Write(Convert.ToByte(Convert.ToInt32(SplitIP[1])));
                            writer.Write(Convert.ToByte(Convert.ToInt32(SplitIP[2])));
                            writer.Write(Convert.ToByte(Convert.ToInt32(SplitIP[3])));

                            writer.Write((ushort)_port);

                            writer.Write(opcode);

                            if (opcode == 'p')
                                writer.Write("8493".ToCharArray());

                            timestamp[0] = DateTime.Now;
                        }

                        if (qSocket.SendTo(stream.ToArray(), endpoint) > 0)
                            return true;
                    }
                }

                catch
                {
                    return false;
                }

                return false;
            }

            public int Receive()
            {
                try
                {
                    _count = 0;

                    EndPoint endpoint = new IPEndPoint(address, _port);

                    byte[] rBuffer = new byte[3402];
                    qSocket.ReceiveFrom(rBuffer, ref endpoint);

                    timestamp[1] = DateTime.Now;

                    using (MemoryStream stream = new MemoryStream(rBuffer))
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            if (stream.Length <= 10)
                                return _count;

                            reader.ReadBytes(10);

                            switch (reader.ReadChar())
                            {
                                case 'i': // Information
                                    {
                                        results = new string[6];

                                        results[_count++] = Convert.ToString(reader.ReadByte());

                                        results[_count++] = Convert.ToString(reader.ReadInt16());

                                        results[_count++] = Convert.ToString(reader.ReadInt16());

                                        int hostnamelen = reader.ReadInt32();
                                        results[_count++] = new string(reader.ReadChars(hostnamelen));

                                        int gamemodelen = reader.ReadInt32();
                                        results[_count++] = new string(reader.ReadChars(gamemodelen));

                                        int mapnamelen = reader.ReadInt32();
                                        results[_count++] = new string(reader.ReadChars(mapnamelen));

                                        return _count;
                                    }

                                case 'r': // Rules
                                    {
                                        int rulecount = reader.ReadInt16();

                                        results = new string[rulecount * 2];

                                        for (int i = 0; i < rulecount; i++)
                                        {
                                            int rulelen = reader.ReadByte();
                                            results[_count++] = new string(reader.ReadChars(rulelen));

                                            int valuelen = reader.ReadByte();
                                            results[_count++] = new string(reader.ReadChars(valuelen));
                                        }

                                        return _count;
                                    }

                                case 'c': // Client list
                                    {
                                        int playercount = reader.ReadInt16();

                                        results = new string[playercount * 2];

                                        for (int i = 0; i < playercount; i++)
                                        {
                                            int namelen = reader.ReadByte();
                                            results[_count++] = new string(reader.ReadChars(namelen));

                                            results[_count++] = Convert.ToString(reader.ReadInt32());
                                        }

                                        return _count;
                                    }

                                case 'd': // Detailed player information
                                    {
                                        int playercount = reader.ReadInt16();

                                        results = new string[playercount * 4];

                                        for (int i = 0; i < playercount; i++)
                                        {
                                            results[_count++] = Convert.ToString(reader.ReadByte());

                                            int namelen = reader.ReadByte();
                                            results[_count++] = new string(reader.ReadChars(namelen));

                                            results[_count++] = Convert.ToString(reader.ReadInt32());
                                            results[_count++] = Convert.ToString(reader.ReadInt32());
                                        }

                                        return _count;
                                    }

                                case 'p': // Ping
                                    {
                                        results = new string[1];

                                        results[_count++] = timestamp[1].Subtract(timestamp[0]).Milliseconds.ToString();

                                        return _count;
                                    }

                                default:
                                    return _count;
                            }
                        }
                    }
                }

                catch
                {
                    return _count;
                }
            }

            public string[] Store(int count)
            {
                string[] rString = new string[count];

                for (int i = 0; i < count && i < _count; i++)
                    rString[i] = results[i];

                _count = 0;

                return rString;
            }
        }
    }
}
