using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIvsTwitch
{
    public class ThreadSharedData
    {
        public static int[] Votes = new int[3];

        public static bool CurrentlyShow = true;
        public static string DrawData = "";


        public static Random RND = new Random();
        public static EffectHandler Handler;

        public static Effect[] ActiveEffects;

        public static List<string> PeopleVoted = new List<string>();
    }
}
