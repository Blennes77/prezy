using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace prezy
{
    public class Denon
    {
        private static string _ip = "172.16.94.133";
        private static int _port = 23;
        private static Socket _socket;
        private static bool _isOpen = false;
        private static byte _cr = 0x0d;

        public static void OpenSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(_ip), _port);
            try
            {
                _socket.Connect(endPoint);
                _isOpen = true;
                Console.WriteLine("Connected to DENON.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[DENON] - Connexion error :" + ex.Message);
            }
        }

        public static void CloseSocket()
        {
            _isOpen = false;
            _socket.Close();
        }

        private static void SendCommand(string pCommand)
        {
            OpenSocket();
            if (_isOpen)
            {
                byte[] buffer = Encoding.Default.GetBytes(pCommand);
                buffer = AddByteToBuffer(buffer, _cr);
                _socket.Send(buffer, 0, buffer.Length, SocketFlags.None);
                //buffer = new byte[255];
                //int recept = _socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                //Console.WriteLine("[DENON] - ACK Received : " + recept.ToString());
                CloseSocket();
            }
            else
            {
                Console.WriteLine("[DENON] - Socket not open.");
            }

        }

        private static byte[] AddByteToBuffer(byte[] pBuffer, byte pByte)
        {
            byte[] newArray = new byte[pBuffer.Length + 1];
            pBuffer.CopyTo(newArray, 0);
            newArray[pBuffer.Length] = pByte;
            return newArray;
        }

        public static void PowerOn()
        {
            SendCommand(CommandType.PowerOn.Value);
        }

        public static void PowerOff()
        {
            SendCommand(CommandType.PowerOff.Value);
        }

        public static void SelectSource(CommandType pCmdType)
        {
            SendCommand(pCmdType.Value);
        }
    }

    public sealed class CommandType
    {
        public static readonly CommandType PowerOn = new CommandType("PWON");
        public static readonly CommandType PowerOff = new CommandType("PWSTANDBY");
        public static readonly CommandType SelectPhono = new CommandType("SIPHONO");
        public static readonly CommandType SelectCD = new CommandType("SICD");
        public static readonly CommandType SelectTuner = new CommandType("SITUNER");
        public static readonly CommandType SelectServer = new CommandType("SISERVER");

        private CommandType(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}