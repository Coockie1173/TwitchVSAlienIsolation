using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AIvsTwitch.Effects
{
    public class LockAxis : Effect
    {
        // 10 -> E0 + X/Y/Z
        private enum Axes
        {
            X = 0x60,
            Y = 0x64,
            Z = 0x68,
        }

        private float LockedVal;
        private Axes Axis;

        public LockAxis()
        {
            int RND = ThreadSharedData.RND.Next(0, 3);
            switch(RND)
            {
                case 0:
                    {
                        Axis = Axes.X;
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
                        break;
                    }
            }

            //UInt32 Offset = MemoryStuff.ReadUInt32((UInt32)MemoryStuff.Base, 0x0130791C);
            //Offset = MemoryStuff.ReadUInt32(Offset, 0x20);
            //Offset = MemoryStuff.ReadUInt32(Offset, 0x18);
            //Offset = MemoryStuff.ReadUInt32(Offset, 0x168);
            //Offset = MemoryStuff.ReadUInt32(Offset, 0x8);
            //Offset = MemoryStuff.ReadUInt32(Offset, 0x4);

            EffectName = $"Lock {Axis} at current position";
            EffectDescription = $"Lock {Axis} at current position";
            EffectWinText = $"Your {Axis} is now locked.";
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
            LockedVal = MemoryStuff.ReadFloat(Offset, 0xE0 + (UInt32)Axis);

            while (CurLockTime < AxisLockTime)
            {
                MemoryStuff.WriteFloat(Offset, (UInt32)Axis + 0xE0, LockedVal);
                Thread.Sleep(10);
                CurLockTime += 10;
            }
            ThreadSharedData.DrawData = "";
        }
    }
}
