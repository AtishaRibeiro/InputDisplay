using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This code was translated from Thakis' c++ Yaz0 decoder into c#

namespace InputDisplay
{
    class Yaz1dec
    {

        public List<byte> DecodeAll(byte[] src)
        {
            int readBytes = 0;
            int srcSize = src.Length;
            Console.WriteLine("input file size: 0x{0:X}\n", srcSize);
            List<byte> decodedBytes = new List<byte>();

            while (readBytes < srcSize)
            {
                //search yaz1 block
                while (readBytes + 3 < srcSize
                  && (src[readBytes] != 'Y'
                  || src[readBytes + 1] != 'a'
                  || src[readBytes + 2] != 'z'
                  || src[readBytes + 3] != '1'))
                    ++readBytes;

                if (readBytes + 3 >= srcSize)
                    return decodedBytes; //nothing left to decode

                readBytes += 4;

                byte[] original = new byte[4];
                Array.Copy(src, readBytes, original, 0, 4);
                Array.Reverse(original); //swap endianness
                uint Size = BitConverter.ToUInt32(original, 0); 
                byte[] Dst = new byte[Size + 0x1000];

                readBytes += 12; //4 byte size, 8 byte unused

                var r = decodeYaz1(src, readBytes, srcSize - readBytes, ref Dst, Size);
                readBytes += r.srcPos;
                Console.WriteLine("Read 0x{0:X} bytes from input\n", readBytes);

                decodedBytes.AddRange(Dst);
            }

            return decodedBytes;
        }

        private (int srcPos, int dstPos) decodeYaz1(in byte[] src, int offset, int srcSize, ref byte[] dst, uint uncompressedSize)
        {
            var r = (srcPos: 0, dstPos: 0);
            //int srcPlace = 0, dstPlace = 0; //current read/write positions

            int validBitCount = 0; //number of valid bits left in "code" byte
            byte currCodeByte = src[offset + r.srcPos];
            while (r.dstPos < uncompressedSize)
            {
                //read new "code" byte if the current one is used up
                if (validBitCount == 0)
                {
                    currCodeByte = src[offset + r.srcPos];
                    ++r.srcPos;
                    validBitCount = 8;
                }

                if ((currCodeByte & 0x80) != 0)
                {
                    //straight copy
                    dst[r.dstPos] = src[offset + r.srcPos];
                    r.dstPos++;
                    r.srcPos++;
                    //if(r.srcPos >= srcSize)
                    //  return r;
                }
                else
                {
                    //RLE part
                    byte byte1 = src[offset + r.srcPos];
                    byte byte2 = src[offset + r.srcPos + 1];
                    r.srcPos += 2;
                    //if(r.srcPos >= srcSize)
                    //  return r;

                    int dist = ((byte1 & 0xF) << 8) | byte2;
                    int copySource = r.dstPos - (dist + 1);

                    int numBytes = byte1 >> 4;
                    if (numBytes == 0)
                    {
                        numBytes = src[offset + r.srcPos] + 0x12;
                        r.srcPos++;
                        //if(r.srcPos >= srcSize)
                        //  return r;
                    }
                    else
                        numBytes += 2;

                    //copy run
                    for (int i = 0; i < numBytes; ++i)
                    {
                        dst[r.dstPos] = dst[copySource];
                        copySource++;
                        r.dstPos++;
                    }
                }

                //use next bit from "code" byte
                currCodeByte <<= 1;
                validBitCount -= 1;
            }

            return r;
        }
    }
}
