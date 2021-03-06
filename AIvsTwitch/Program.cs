using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace AIvsTwitch
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigHandler.LoadConfigFile();
            ThreadSharedData.Handler = new EffectHandler();

            //setup memory
            try
            {
                MemoryStuff.mem = new VAMemory("AI");
                Process GameProcess = Process.GetProcessesByName("AI").FirstOrDefault();
                MemoryStuff.Base = GameProcess.MainModule.BaseAddress;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("This error is usually caused by the game not being found! Please make sure the game is running. If it's still not working, try running this program as administrator.");
                Environment.Exit(1); //return error message
            }

            Thread BotThread = new Thread(new ThreadStart(StartBot));
            BotThread.Start();
            Thread TimerThread = new Thread(new ThreadStart(StartTimer));
            TimerThread.Start();

            GameOverlay.TimerService.EnableHighPrecisionTimers();

            GameOverlayAI AI = new GameOverlayAI();
            AI.Run();
        }

        public static void StartBot()
        {
            bool BotEnabled = true;
            Random RND = new Random();

            ThreadSharedData.DrawData = ConfigHandler.ConfigData["ChatUserName"];

            while(BotEnabled)
            {
                //place text through ThreadSharedData & activate through ThreadSharedData
                BotHandler Bot = new BotHandler();

                Thread.Sleep(-1);
            }
        }

        public static void StartTimer()
        {
            TimeHandler TH = new TimeHandler();
            TH.Run();
        }
    }
}
