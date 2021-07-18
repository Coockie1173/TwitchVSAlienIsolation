using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIvsTwitch.Effects
{
    public class SetItemData : Effect
    {
        /*
         * 
            ESI (pointer addr)+ X * 4 + 70

            FlarePointers:2D
            Noisemakers:A
            Smokers:5A
            Flashers:05
            Empters:23
            Molos:28
            PipeBombs:69
            Medkits:4B


            Base + 1237F84 -> 114
         * */
        private enum ItemType
        {
            Flares = 0x2D, //7
            Noisemakers = 0xA, //6
            Smokebombs = 0x5A, //5
            Flashbangs = 0x05, //4
            EMPs = 0x23, //3
            Molotovs = 0x28, //2
            Pipebombs = 0x69, //1
            Medkits = 0x48, //0
        }
        private ItemType ItemOffset;
        private const int MaxItems = 5;
        private int AmmItems;

        public SetItemData()
        {
            int RND = ThreadSharedData.RND.Next(0, 8);
            switch(RND)
            {
                case 0:
                    {
                        ItemOffset = ItemType.Medkits;
                        break;
                    }
                case 1:
                    {
                        ItemOffset = ItemType.Pipebombs;
                        break;
                    }
                case 2:
                    {
                        ItemOffset = ItemType.Molotovs;
                        break;
                    }
                case 3:
                    {
                        ItemOffset = ItemType.EMPs;
                        break;
                    }
                case 4:
                    {
                        ItemOffset = ItemType.Flashbangs;
                        break;
                    }
                case 5:
                    {
                        ItemOffset = ItemType.Smokebombs;
                        break;
                    }
                case 6:
                    {
                        ItemOffset = ItemType.Noisemakers;
                        break;
                    }
                case 7:
                    {
                        ItemOffset = ItemType.Flares;
                        break;
                    }

            }
            AmmItems = ThreadSharedData.RND.Next(0, MaxItems);
            EffectName = $"Set {ItemOffset} to {AmmItems}";
            EffectDescription = $"Set {ItemOffset} to {AmmItems}";
            EffectWinText = $"You now have {AmmItems} {ItemOffset}";
        }

        public override void DoEffect()
        {
            bool MovementOn = CheckLoading();
            while (MovementOn)
            {
                MovementOn = CheckLoading();
                Thread.Sleep(1000);
            }
            UInt32 Offset = MemoryStuff.ReadUInt32((UInt32)MemoryStuff.Base, 0x1237F84);
            MemoryStuff.WriteUInt16(Offset, (0x114 + ((UInt32)ItemOffset * 4) + 0x70), (UInt16)AmmItems);
        }
    }
}
