using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIvsTwitch
{
    public abstract class Effect
    {
        public string EffectName { get; set; }
        public string EffectDescription { get; set; }
        public string EffectWinText { get; set; }
        public abstract void DoEffect();

        public bool CheckLoading()
        {
            UInt32 Offset = MemoryStuff.ReadUInt32((UInt32)MemoryStuff.Base, 0x12F0C88);
            Offset = MemoryStuff.ReadUInt32(Offset, 0x48);
            Offset = MemoryStuff.ReadUInt16(Offset, 0x08);
            if(Offset != 0x04)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
