using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Hemogoblin.ChatBot;

namespace Hemogoblin.WeatherBot
{
    /// <summary>
    /// WeatherBot's implementation of IChatBotConfig
    /// </summary>
    [Serializable]
    public class WeatherBotConfig : IChatBotConfig
    {
        public string[] Addresses { get; private set; }
        public int Port { get; private set; }
        public bool AutoHandleNickCollision { get; private set; }
        public bool AutoJoinOnInvite { get; private set; }
        public bool AutoReconnect { get; private set; }
        public bool AutoRejoin { get; private set; }
        public bool AutoRetryConnection { get; private set; }
        public int AutoRetryDelay { get; private set; }
        public int AutoRetryLimit { get; private set; }
        public bool ReceiveWallops { get; private set; }
        public bool SupportNonRFC { get; private set; }
        public bool SyncChannelsOnJoin { get; private set; }
        public bool UseSSL { get; private set; }
        public string VersionResponse { get; private set; }
        public string[] Nicks { get; private set; }
        public string Username { get; private set; }
        public string RealName { get; private set; }
        public string[] PostConnectionCommands { get; private set; }
        public string[] Channels { get; private set; } 

        public static WeatherBotConfig GenerateConfig()
        {
            // Get the server address from the user
            var addresses = new string[1];
            Console.Write("Enter the address of the server to connect to:\r\n> ");
            addresses[0] = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(addresses[0]))
            {
                Console.Write("\r\nNothing entered. Enter the address of the server to connect to.\r\n");
                addresses[0] = Console.ReadLine();
            }
            
            // Get the port from the user
            Console.Write("\r\nEnter the port of the server to connect to (6667):\r\n> ");
            var portStr = Console.ReadLine();
            var port = 6667;
            if (string.IsNullOrWhiteSpace(portStr))
            {
                Console.Write("\r\nNothing entered. Defaulting to port 6667.\r\n");
            }
            else if (null != portStr)
            {
                port = Int32.Parse(portStr);
            }

            // Ask if to use SSL
            Console.Write("\r\nUse SSL? [y/(n)]\r\n> ");
            var useSSLstr = Console.ReadLine();
            bool useSSL;
            if (useSSLstr == "Y" || useSSLstr == "y")
            {
                useSSL = true;
            }
            else
            {
                if (useSSLstr != "N" && useSSLstr != "n")
                {
                    Console.Write("\r\nInvalid entry. Defaulting to no SSL.\r\n");
                }
                useSSL = false;
            }

            // Get the nick from the user
            var nicks = new string[2];
            Console.Write("\r\nEnter the desired nick. (WeatherBot)\r\n> ");
            nicks[0] = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(nicks[0]))
            {
                Console.Write("\r\nNothing entered. Defaulting to nick \"WeatherBot\"\r\n");
                nicks[0] = "WeatherBot";
            }

            // Get the alternate nick from the user
            Console.Write("\r\nEnter the desired alternate nick. (Weather_Bot)\r\n> ");
            nicks[1] = Console.ReadLine();
            if(String.IsNullOrWhiteSpace(nicks[1]))
            {
                Console.Write("\r\nNothing entered. Defaulting to nick \"Weather_Bot\"\r\n");
                nicks[1] = "Weather_Bot";
            }

            // Get the username from the user
            Console.Write("\r\nEnter the desired username. (WeatherBot)\r\n> ");
            var username = Console.ReadLine();
            if(String.IsNullOrWhiteSpace(username))
            {
                Console.Write("\r\nNothing entered. Defaulting to \"WeatherBot\"\r\n");
                username = "WeatherBot";
            }

            // Get the real name from the user
            Console.Write("\r\nEnter the desired real name. (WeatherBot)\r\n> ");
            var realName = Console.ReadLine();
            if(String.IsNullOrWhiteSpace(realName))
            {
                Console.Write("\r\nNothing entered. Defaulting to \"WeatherBot\"\r\n");
                realName = "WeatherBot";
            }

            // Get the list of channels the user wishes to join.
            Console.Write("\r\nEnter the list of channels, separated by spaces.\r\n> ");
            var channelStr = Console.ReadLine();
            var channels = new string[0];
            if (String.IsNullOrWhiteSpace(channelStr))
            {
                Console.Write("\r\nNothing entered. Defaulting to no channels.\r\n");
            }
            else if(null != channelStr)
            {
                channels = channelStr.Split(' ');
            }

            return new WeatherBotConfig()
            {
                Addresses = addresses,
                Port = port,

                AutoHandleNickCollision = true,
                AutoJoinOnInvite = true,
                AutoReconnect = true,
                AutoRejoin = true,
                AutoRetryConnection = true,
                AutoRetryDelay = 10,
                AutoRetryLimit = 3,
                ReceiveWallops = true,
                SupportNonRFC = false,
                SyncChannelsOnJoin = true,
                UseSSL = useSSL,
                VersionResponse = "WeatherBot v0.01",
                Nicks = nicks,
                Username = username,
                RealName = realName,
                Channels = channels,
                PostConnectionCommands = new string[0]
            };
        }

        /// <summary>
        /// Tries to open config.dat
        /// </summary>
        /// <returns>A WeatherBotConfig object generated from config.dat</returns>
        public static WeatherBotConfig OpenConfigFile()
        {
            return (WeatherBotConfig)(new BinaryFormatter().Deserialize(new FileStream("config.dat", FileMode.Open)));
        }

        /// <summary>
        /// Tries to save config.dat
        /// </summary>
        public void SaveConfigFile()
        {
            new BinaryFormatter().Serialize(new FileStream("config.dat", FileMode.CreateNew), this);
        }

        private WeatherBotConfig(){}
    }
}
