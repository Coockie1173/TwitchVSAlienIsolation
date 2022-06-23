using System;
using System.Collections.Generic;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace AIvsTwitch
{    
    public class BotHandler
    {
        TwitchClient client;
        
        

        public BotHandler()
        {
            //JustinFan works? Sure I'll take it
            ConnectionCredentials credentials = new ConnectionCredentials("justinfan12345", "", "wss://irc-ws.chat.twitch.tv:443");
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };

            WebSocketClient customClient = new WebSocketClient(clientOptions);
            client = new TwitchClient(customClient);
            client.Initialize(credentials, ConfigHandler.ConfigData["ChatUserName"]);

            client.OnLog += Client_OnLog;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnConnected += Client_OnConnected;

            client.Connect();
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            //Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine("Hey guys! I am a bot connected via TwitchLib!");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            Console.WriteLine(e.ChatMessage.Message);
            if(ThreadSharedData.Waiting)
            {
                ThreadSharedData.CurrentlyShow = true; //just to be sure
                //awaiting the start command
                if ((e.ChatMessage.IsBroadcaster ||e.ChatMessage.IsStaff) && e.ChatMessage.Message.ToLower() == "start")
                {
                    //STARTING
                    ThreadSharedData.Waiting = false;
                    ThreadSharedData.CurrentlyShow = false;
                }
            }
            else
            {
                //check if currently accepting input
                if (ThreadSharedData.CurrentlyShow)
                {
                    //accepting input, start counting bois!
                    if (!ThreadSharedData.PeopleVoted.Contains(e.ChatMessage.Username))
                    {
                        ThreadSharedData.PeopleVoted.Add(e.ChatMessage.Username);
                        switch (e.ChatMessage.Message)
                        {
                            case "1":
                                {
                                    ThreadSharedData.Votes[0] += 1;
                                    //ActiveEffects[0].DoEffect(); //to test my effect
                                    break;
                                }
                            case "2":
                                {
                                    ThreadSharedData.Votes[1] += 1;
                                    break;
                                }
                            case "3":
                                {
                                    ThreadSharedData.Votes[2] += 1;
                                    break;
                                }
                        }
                    }

                    ThreadSharedData.DrawData = $"{ThreadSharedData.Votes[0]} {ThreadSharedData.ActiveEffects[0].EffectName}\n" +
                        $"{ThreadSharedData.Votes[1]} {ThreadSharedData.ActiveEffects[1].EffectName}\n" +
                        $"{ThreadSharedData.Votes[2]} {ThreadSharedData.ActiveEffects[2].EffectName}";
                }
            }
        }
    }
}
