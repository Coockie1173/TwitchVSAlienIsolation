using AIvsTwitch.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIvsTwitch
{
    public class EffectHandler
    {
        private enum EffectType
        {
            RandomHealth,
            RandomItem,
            LockAxis,
        }

        private Effect GetEffect(EffectType rnd)
        {
            switch (rnd)
            {
                case EffectType.RandomHealth:
                    {
                        return new SetHealthRandom();
                    }                
                case EffectType.RandomItem:
                    {
                        return new SetItemData();
                    }
                case EffectType.LockAxis:
                    {
                        return new LockAxis();
                    }
            }
            return null; //error has occured.
        }

        public EffectHandler()
        {
            //Effect Health = new SetHealthRandom();
            //Effects.Add(Health);
        }

        public Effect[] GetRandomEffects()
        {
            Effect[] _Effects = new Effect[3];
            Random rnd = new Random();
            for(int i = 0; i < 3; i++)
            {
                //_Effects[i] = Effects[rnd.Next(0, Effects.Count)];
                _Effects[i] = GetEffect((EffectType)(ThreadSharedData.RND.Next(0,Enum.GetNames(typeof(EffectType)).Length)));
            }

            return _Effects;
        }
    }
}
