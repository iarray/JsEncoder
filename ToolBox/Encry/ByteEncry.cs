using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolBox.Encry
{
    public static class ByteEncry
    {
        public static byte[] EncryptionToBytes(byte[] param,string key)
        {
            byte[] bytes = param;
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            byte[] ebts = new byte[bytes.Length + key.Length + 3];
            Random rd = new Random();
            int k1 = rd.Next(1,5);
            int k2 = rd.Next(1,k1 * 2);
            ebts[0] = (byte)k1;
            ebts[1] = (byte)k2;
            ebts[2] = (byte)key.Length;
            int offset = 3 + key.Length;
            //复制密钥到字节数组中
            Array.Copy(keyBytes, 0, ebts,3, key.Length);
           
            for (int i = 0; i < bytes.Length; i++)
            {
                if (i % 2 == 0)
                {
                    ebts[i + offset] = (byte)(bytes[i] + i + k1);
                }
                else
                {
                    ebts[i + offset] = (byte)(bytes[i] + k2);
                }

            }
            return ebts;
        }

        public static byte[] UnEncryptionToBytes(byte[] param,string key)
        {
            byte[] bytes = param;
            int k1 = bytes[0];
            int k2 = bytes[1];
            int keyLen = bytes[2];
            int offset = 3 + keyLen;

            string keyString = Encoding.UTF8.GetString(bytes, 3, keyLen);
            if (keyString != key)
                throw new Exception("密钥不匹配");

            byte[] unebts = new byte[bytes.Length - offset];

            for (int i = 0; i < unebts.Length; i++)
            {

                if (i % 2 == 0)
                {
                    int b1 = bytes[i + offset] - i - k1;
                    //if (b1 >= 0)
                    //{
                        unebts[i] = (byte)b1;
                    //}
                    //else
                    //{
                    //    unebts[i] = 255;
                    //}
                }
                else
                {
                    int b1 = bytes[i + offset] - k2;
                    //if (b1 >= 0)
                    //{
                        unebts[i] = (byte)b1;
                    //}
                    //else
                    //{
                    //    unebts[i] = (byte)(256 + b1);
                    //}
                }

            }
            return unebts;
        }
    }
}
