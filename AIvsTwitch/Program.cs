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
            MemoryStuff.mem = new VAMemory("AI");
            Process GameProcess = Process.GetProcessesByName("AI").FirstOrDefault();
            MemoryStuff.Base = GameProcess.MainModule.BaseAddress;

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
