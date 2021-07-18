using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace AIvsTwitch
{
    public class TimeHandler
    {
        public void Run()
        {
            bool TimerRunning = true;

            ThreadSharedData.CurrentlyShow = false;

            while (TimerRunning)
            {
                Thread.Sleep(TimeSpan.FromSeconds(int.Parse(ConfigHandler.ConfigData["EffectInterval"])));

                //enable drawing of overlay
                ThreadSharedData.CurrentlyShow = true;
                ThreadSharedData.ActiveEffects = ThreadSharedData.Handler.GetRandomEffects();
                ThreadSharedData.DrawData = $"{ThreadSharedData.Votes[0]} {ThreadSharedData.ActiveEffects[0].EffectName}\n" +
                    $"{ThreadSharedData.Votes[1]} {ThreadSharedData.ActiveEffects[1].EffectName}\n" +
                    $"{ThreadSharedData.Votes[2]} {ThreadSharedData.ActiveEffects[2].EffectName}";

                Thread.Sleep(TimeSpan.FromSeconds(int.Parse(ConfigHandler.ConfigData["VoteTime"])));

                Effect WinEffect = GetWinningVote();
                ThreadSharedData.DrawData = $"{WinEffect.EffectWinText}";
                WinEffect.DoEffect();                
                Thread.Sleep(TimeSpan.FromSeconds(int.Parse(ConfigHandler.ConfigData["VictorTime"])));
                ThreadSharedData.CurrentlyShow = false;
                ThreadSharedData.PeopleVoted.Clear();
                for (int i = 0; i < 3; i++)
                {
                    ThreadSharedData.Votes[i] = 0;
                }
            }
        }

        private Effect GetWinningVote()
        {
            int MaxVotes = 0;
            int MaxVotesID = 0;
            for(int i = 0; i < 3; i++)
            {
                if(ThreadSharedData.Votes[i] > MaxVotes)
                {
                    MaxVotes = i;
                    MaxVotesID = i;
                }
            }

            return ThreadSharedData.ActiveEffects[MaxVotesID];
        }
    }
}
