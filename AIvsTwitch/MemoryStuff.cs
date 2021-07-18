using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AIvsTwitch
{
    public class MemoryStuff
    {
        public static VAMemory mem;
        public static IntPtr Base;
        
        public static UInt32 GetBaseAddr()
        {
            return (UInt32)mem.getBaseAddress;
        }

        public static UInt32 ReadUInt32(UInt32 BaseAddr, UInt32 Offset)
        {
            return mem.ReadUInt32((IntPtr)(BaseAddr + Offset));
        }
        
        public static UInt32 ReadUInt16(UInt32 BaseAddr, UInt32 Offset)
        {
            return mem.ReadUInt16((IntPtr)(BaseAddr + Offset));
        }

        public static float ReadFloat(UInt32 BaseAddr, UInt32 Offset)
        {
            return mem.ReadFloat((IntPtr)(BaseAddr + Offset));
        }

        public static void WriteUInt32(UInt32 BaseAddr, UInt32 Offset, UInt32 Value)
        {
            mem.WriteUInt32((IntPtr)(BaseAddr + Offset), Value);
        }

        public static void WriteUInt16(UInt32 BaseAddr, UInt32 Offset, UInt16 Value)
        {
            mem.WriteUInt16((IntPtr)(BaseAddr + Offset), Value);
        }

        public static void WriteFloat(UInt32 BaseAddr, UInt32 Offset, float Value)
        {
            mem.WriteFloat((IntPtr)(BaseAddr + Offset), Value);
        }
    }
}
