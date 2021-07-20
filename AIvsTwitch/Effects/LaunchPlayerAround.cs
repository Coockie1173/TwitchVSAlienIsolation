using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIvsTwitch.Effects
{
    public class LaunchPlayerAround : Effect
    {
        private enum Axes
        {
            X = 0xD0,
            Y = 0xD4,
            Z = 0xD8,
        }

        private Axes Axis;

        private float LaunchAmm = 100f;
        private const int MaxAmm = 200;
        private bool Inverted = false;

        public LaunchPlayerAround()
        {
            int R = ThreadSharedData.RND.Next(10 * 100, MaxAmm * 100);
            LaunchAmm = R / 100;
            int RND = ThreadSharedData.RND.Next(0, 3);
            switch (RND)
            {
                case 0:
                    {
                        Axis = Axes.X;
                        Inverted = Convert.ToBoolean(ThreadSharedData.RND.Next(0, 1));
                        break;
                    }
                case 1:
                    {
                        Axis = Axes.Y;
                        break;
                    }
                case 2:
                    {
                        Axis = Axes.Z;
                        Inverted = Convert.ToBoolean(ThreadSharedData.RND.Next(0, 1));
                        break;
                    }
            }

            EffectName = $"Annoy player in the {Axis} direction";
            EffectDescription = $"Annoy player in the {Axis} direction";
            EffectWinText = $"Annoying player in {Axis} direction.";
        }

        public override void DoEffect()
        {
            bool MovementOn = CheckLoading();
            while (MovementOn)
            {
                MovementOn = CheckLoading();
                Thread.Sleep(1000);
            }

            UInt32 AxisLockTime = (UInt32)(int.Parse(ConfigHandler.ConfigData["AxisLockTime"]) * 1000);
            UInt32 CurLockTime = 0;
            UInt32 Offset = MemoryStuff.ReadUInt32((UInt32)MemoryStuff.Base, 0x0130791C);
            Offset = MemoryStuff.ReadUInt32(Offset, 0x20);
            Offset = MemoryStuff.ReadUInt32(Offset, 0x18);
            Offset = MemoryStuff.ReadUInt32(Offset, 0x168);
            Offset = MemoryStuff.ReadUInt32(Offset, 0x8);
            Offset = MemoryStuff.ReadUInt32(Offset, 0x4);

            float mul = 1;
            if(Inverted)
            {
                mul = -1;
            }

            while (CurLockTime < AxisLockTime)
            {
                MemoryStuff.WriteFloat(Offset, (UInt32)Axis + 0xE0, LaunchAmm * mul);
                Thread.Sleep(100);
                CurLockTime += 100;
            }
            ThreadSharedData.DrawData = "";
        }
    }
}
