using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concentus.Oggfile
{
    internal class OpusHeader
    {
        byte version;
        byte channel_count;
        ushort pre_skip;
        internal uint input_sample_rate;
        short output_gain;
        byte mapping_family;
        byte stream_count;
        byte coupled_count;
    
        internal static OpusHeader ParsePacket(byte[] packet, int packetLength)
        {
            if (packetLength < 19)
                return null;

            if (!"OpusHead".Equals(Encoding.UTF8.GetString(packet, 0, 8)))
                return null;

            OpusHeader returnVal = new OpusHeader();
            
            returnVal.version = packet[8];
            returnVal.channel_count = packet[9];
            if (!BitConverter.IsLittleEndian) {
                Array.Reverse(packet, 10, 2);
                Array.Reverse(packet, 12, 4);
                Array.Reverse(packet, 16, 2);
            }
            returnVal.pre_skip = BitConverter.ToUInt16(packet, 10);
            returnVal.input_sample_rate = BitConverter.ToUInt32(packet, 12);
            returnVal.output_gain = BitConverter.ToInt16(packet, 16);
            returnVal.mapping_family = packet[18];

            return returnVal;
        }
    }
}
