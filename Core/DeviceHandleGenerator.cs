using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RbxProxiless.Core
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class DeviceHandleGenerator
    {
        private static Random rand = new Random();

        private static ulong teaEncrypt(ulong BrowserID)
        {

            uint[] keys = { 0x5A96E9DE, 0x7B80D8E4, 0xCC969B58, 0x2B99050C };

            uint v0 = (uint)(BrowserID & 4294967295);
            uint v1 = (uint)(BrowserID >> ((326656 >> 10) - 287));

            const uint delta = 0xD203172E;
            uint sum = 0;

            for (int i = 0; i < 32; ++i)
            {
                sum += delta;
                v0 += ((v1 << 9) + keys[0]) ^ (v1 + sum) ^ ((v1 >> 5) + keys[1]);
                v1 += ((v0 << 9) + keys[2]) ^ (v0 + sum) ^ ((v0 >> 5) + keys[3]);
            }

            ulong b = v1;
            b = (b << 32) | v0;

            return b;

        }

        public static string GrabDeviceHandle()
        {
            ulong browserId = (ulong)rand.Next(0, 10000000);
            ulong deviceHandle = teaEncrypt(browserId);

            return $"{browserId}:{deviceHandle}";
        }
    }
}
