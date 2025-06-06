﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.PublicClasses
{
    public static class PasswordHelper
    {
        public static string EncodePasswordMd5(string pass) // Encrypt using MD5
        {
            byte[] originalBytes;
            byte[] encodedBytes;

            MD5 md5;
            //Instantiate MD5CryptoServiceProvider , get bytes for original password and compute hash (encodedpassword)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = Encoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }
    }
}
