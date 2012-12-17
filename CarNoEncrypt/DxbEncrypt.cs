using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace CarNoEncrypt
{
    class DxbEncrypt
    {
        private static string key ="!@#$"; 
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str">待加密的明文字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptString(string str)
        {
            byte[] bStr = (new UnicodeEncoding()).GetBytes(str);
            byte[] bKey = (new UnicodeEncoding()).GetBytes(key);
            for (int i = 0; i < bStr.Length; i += 2)
            {
                for (int j = 0; j < bKey.Length; j += 2)
                {
                    bStr[i] = Convert.ToByte(bStr[i] ^ bKey[j]);
                }
            }
            return (new UnicodeEncoding()).GetString(bStr).TrimEnd('\0');
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="str">待解密的密文字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>解密后的明文</returns>
        public static string DecryptString(string str)
        {
            return EncryptString(str);
        }
    }
}
