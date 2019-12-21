using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Arcturus.Time
{
    public class TimeFromTimeserver
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public static DateTime GetNetworkTime()
        {
            try
            {
                const string ntpServer = "pool.ntp.org";
                Logger.Info($"Getting Time from {ntpServer}");
                var ntpData = new byte[48];
                ntpData[0] = 0x1B; //LeapIndicator = 0 (no warning), VersionNum = 3 (IPv4 only), Mode = 3 (Client Mode)

                var addresses = Dns.GetHostEntry(ntpServer).AddressList;
                var ipEndPoint = new IPEndPoint(addresses[0], 123);
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                socket.ReceiveTimeout = 3000;
                socket.Connect(ipEndPoint);
                socket.Send(ntpData);
                socket.Receive(ntpData);
                socket.Close();

                ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
                ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];

                var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
                var networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds);

                Logger.Info($"Time gotten from {ntpServer} --> {networkDateTime.ToString("dd.MM.yyyy HH:mm:ss")}");

                return networkDateTime;
            }
            catch
            {
                Logger.Info($"Using fallback local time instead.");
                return DateTime.Now;
            }
        }


        public static DateTime GetNetworkTimeWindows()
        {
            try
            {
                //default Windows time server
                const string ntpServer = "time.windows.com";
                Logger.Info($"Getting Time from {ntpServer}");
                // NTP message size - 16 bytes of the digest (RFC 2030)
                var ntpData = new byte[48];

                //Setting the Leap Indicator, Version Number and Mode values
                ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

                var addresses = Dns.GetHostEntry(ntpServer).AddressList;

                //The UDP port number assigned to NTP is 123
                var ipEndPoint = new IPEndPoint(addresses[0], 123);
                //NTP uses UDP

                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                {
                    socket.Connect(ipEndPoint);

                    //Stops code hang if NTP is blocked
                    socket.ReceiveTimeout = 3000;

                    socket.Send(ntpData);
                    socket.Receive(ntpData);
                    socket.Close();
                }

                //Offset to get to the "Transmit Timestamp" field (time at which the reply 
                //departed the server for the client, in 64-bit timestamp format."
                const byte serverReplyTime = 40;

                //Get the seconds part
                ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

                //Get the seconds fraction
                ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

                //Convert From big-endian to little-endian
                intPart = SwapEndianness(intPart);
                fractPart = SwapEndianness(fractPart);

                var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

                //**UTC** time
                var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

                Logger.Info($"Time gotten from {ntpServer} --> {networkDateTime.ToString("dd.MM.yyyy HH:mm:ss")}");
                return networkDateTime.ToLocalTime();
            }
            catch
            {
                Logger.Info($"Using fallback local time instead.");
                return DateTime.Now;
            }
        }

        // stackoverflow.com/a/3294698/162671
        private static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                           ((x & 0x0000ff00) << 8) +
                           ((x & 0x00ff0000) >> 8) +
                           ((x & 0xff000000) >> 24));
        }

        /// <summary>
        /// Bezieht die Zeit vom Windows Zeitserver und weist den Nutzer darauf hin, falls die Zeiten zu stark abweichen sollten.
        /// </summary>
        /// <returns></returns>
        public static DateTime GetTimeAndNotifyUser()
        {
            var time = Arcturus.Time.TimeFromTimeserver.GetNetworkTimeWindows();

            if (time.Subtract(DateTime.Now) > new TimeSpan(0, 5, 0))
            {
                Logger.Info($"Notify user that time of his system is not correct.");
                Arcturus.WinApi.Message.Information("Korrigieren Sie Ihre Systemzeit!\n" +
                                                    "Diese weicht zu stark vom Windows Zeitserver ab.\n" +
                                                    $"Ihre Zeit --> {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}\n" +
                                                    $"Windows Zeit -->{time.ToString("dd.MM.yyyy HH:ss")}", "Differenz zwischen Systemzeit und Windows Zeitserver!");
            }
            else if (DateTime.Now.Subtract(time) > new TimeSpan(0, 5, 0))
            {
                Arcturus.WinApi.Message.Information("Korrigieren Sie Ihre Systemzeit!\n" +
                                                    "Diese weicht zu stark vom Windows Zeitserver ab.\n" +
                                                    $"Ihre Zeit --> {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}\n" +
                                                    $"Windows Zeit -->{time.ToString("dd.MM.yyyy HH:ss")}", "Differenz zwischen Systemzeit und Windows Zeitserver!");
            }

            Logger.Info($"Windows-Server --> {time.ToString("dd.MM.yyyy HH:ss")} || User-System --> {DateTime.Now.ToString("dd.MM.yyyy HH:ss")}");

            return time;
        }
    }
}
