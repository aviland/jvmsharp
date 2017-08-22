using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp.native.sun.misc
{
    class Malloc
    {
        static Dictionary<long, byte[]> allocated = new Dictionary<long, byte[]>();
         long nextAddress = 64L;

         internal long allocate(long size)
        {
       //     Console.WriteLine(nextAddress);
            byte[] mem = new byte[size];
            long address = nextAddress;
            allocated[address] = mem;
            nextAddress += size;
            return address;
        }

         internal long reallocate(long address, long size)
        {
          //  Console.WriteLine(nextAddress);
            if (size == 0)
                return 0L;
            else if (address == 0)
                return allocate(size);
            else
            {
                byte[] mem = memoryAt(address);
                if (mem.Length >= size)
                    return address;
                else
                {
                    allocated.Remove(address);
                    long newAddress = allocate(size);
                    byte[] newMem = memoryAt(newAddress);
                    Array.Copy(mem, newMem, newMem.Length);
                    return newAddress;
                }
            }
        }

        static internal void free(long address)
        {
            //Console.WriteLine(nextAddress);
            if (allocated.ContainsKey(address)) { 
                allocated.Remove(address);
            //    Console.WriteLine("++++++++++++++remove");
            }
            else throw new Exception("memory was not allocated!");
        }
        static internal void setMemory(byte[] b,long address)
        {
            allocated[address] = b;
        }
        static internal byte[] memoryAt(long address)
        {
        //    Console.WriteLine(nextAddress);
            foreach (long startAddress in allocated.Keys)
            {
                byte[] mem = allocated[startAddress];
                long endAddress = startAddress + (long)mem.Length;
                if (address >= startAddress && address < endAddress)
                {
                    long offset = address - startAddress;
                    return mem.Skip((int)offset).ToArray();
                }
            }
            throw new Exception("invalid address!");
        }
    }
}
