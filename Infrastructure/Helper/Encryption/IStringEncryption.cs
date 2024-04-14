using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper.Encryption
{
    public interface IStringEncryption
    {
        string Encrypt(string unencrypted);

        string Decrypt(string encrypted);

        string CompressString(string uncompressedString);

        string DecompressString(string compressedString);

        byte[] ZipString(String str);

        string UnzipString(byte[] input);
    }
}
