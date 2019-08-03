using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GlobalMemoryStatusExApp
{
    class Program
    {
        private const double OneGigabyte = 1024 * 1024 * 1024;

        [StructLayout(LayoutKind.Sequential)]
        public class MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;

            public MEMORYSTATUSEX()
            {
                this.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

        static void Main(string[] args)
        {
            var memoryStatus = new MEMORYSTATUSEX();
            if (!GlobalMemoryStatusEx(memoryStatus))
            {
                Console.WriteLine(Marshal.GetLastWin32Error());
            }
            else
            {
                Console.WriteLine("dwLength: {0}", memoryStatus.dwLength);
                Console.WriteLine("dwMemoryLoad: {0}", memoryStatus.dwMemoryLoad);
                Console.WriteLine("ullTotalPhys: {0}", memoryStatus.ullTotalPhys / OneGigabyte);
                Console.WriteLine("ullAvailPhys: {0}", memoryStatus.ullAvailPhys / OneGigabyte);
                Console.WriteLine("ullTotalPageFile: {0}", memoryStatus.ullTotalPageFile / OneGigabyte);
                Console.WriteLine("ullAvailPageFile: {0}", memoryStatus.ullAvailPageFile / OneGigabyte);
                Console.WriteLine("ullTotalVirtual: {0}", memoryStatus.ullTotalVirtual / OneGigabyte);
                Console.WriteLine("ullAvailVirtual: {0}", memoryStatus.ullAvailVirtual / OneGigabyte);
                Console.WriteLine("ullAvailExtendedVirtual: {0}", memoryStatus.ullAvailExtendedVirtual / OneGigabyte);
            }
        }
    }
}
