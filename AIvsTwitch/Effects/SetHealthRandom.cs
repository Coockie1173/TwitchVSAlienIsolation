using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIvsTwitch.Effects
{
    public class SetHealthRandom : Effect
    {
        private int RandomAmm;
        public SetHealthRandom()
        {
            RandomAmm = ThreadSharedData.RND.Next(1, 1000);

            EffectDescription = $"Sets the health to {RandomAmm/10}%";
            EffectName = $"Set health to {RandomAmm/10}%";
            EffectWinText = $"Health has been set to {RandomAmm/10}%";
        }

        public override void DoEffect()
        {
            //0123623C -> 0x14 -> 0x320 + 0x1C
            bool MovementOn = CheckLoading();
            while(MovementOn)
            {
                MovementOn = CheckLoading();
                Thread.Sleep(1000);
            }
            UInt32 Offset = MemoryStuff.ReadUInt32((UInt32)MemoryStuff.Base, 0x0123623C);
            Offset = MemoryStuff.ReadUInt32(Offset, 0x14);
            MemoryStuff.WriteUInt16(Offset, 0x33C, (UInt16)RandomAmm); //got health
        }
    }
}
