using Infrastructure.Helper.Config;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper.Encryption
{
    public class StringEncryption : IStringEncryption
    {
        private readonly Random _random;
        private readonly byte[] _key;
        private readonly RijndaelManaged _rm;
        private readonly UTF8Encoding _encoder;
        private IConfigCreatorHelper _configCreator;

        public StringEncryption(IConfigCreatorHelper configCreator)
        {
            _configCreator = configCreator;
            this._random = new Random();
            this._rm = new RijndaelManaged();
            this._encoder = new UTF8Encoding();
            this._key = Convert.FromBase64String(_configCreator.Get("Encryption:Key.String"));
        }

        public string Encrypt(string unencrypted)
        {
            var vector = new byte[16];
            this._random.NextBytes(vector);
            var cryptogram = vector.Concat(this.Encrypt(this._encoder.GetBytes(unencrypted), vector));
            return Convert.ToBase64String(cryptogram.ToArray()).Replace("=", "").Replace('+', '-').Replace('/', '_');
        }

        public string Decrypt(string encrypted)
        {
            encrypted = encrypted.Replace('-', '+').Replace('_', '/');
            string padding = new String('=', 3 - (encrypted.Length + 3) % 4);
            encrypted += padding;
            var cryptogram = Convert.FromBase64String(encrypted);
            if (cryptogram.Length < 17)
            {
                throw new ArgumentException("Not a valid encrypted string", "encrypted");
            }

            var vector = cryptogram.Take(16).ToArray();
            var buffer = cryptogram.Skip(16).ToArray();
            return this._encoder.GetString(this.Decrypt(buffer, vector));
        }

        public string CompressString(string uncompressedString)
        {
            var compressedStream = new MemoryStream();
            var uncompressedStream = new MemoryStream(Encoding.UTF8.GetBytes(uncompressedString));

            using (var compressorStream = new DeflateStream(compressedStream, CompressionMode.Compress, true))
            {
                uncompressedStream.CopyTo(compressorStream);
            }

            return Convert.ToBase64String(compressedStream.ToArray());
        }

        public string DecompressString(string compressedString)
        {
            var decompressedStream = new MemoryStream();
            var compressedStream = new MemoryStream(Convert.FromBase64String(compressedString));

            using (var decompressorStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            {
                decompressorStream.CopyTo(decompressedStream);
            }

            return Encoding.UTF8.GetString(decompressedStream.ToArray());
        }

        public byte[] ZipString(String str)
        {
            using (MemoryStream output = new MemoryStream())
            {
                using (DeflateStream gzip =
                  new DeflateStream(output, CompressionMode.Compress))
                {
                    using (StreamWriter writer =
                      new StreamWriter(gzip, System.Text.Encoding.UTF8))
                    {
                        writer.Write(str);
                    }
                }

                return output.ToArray();
            }
        }

        public string UnzipString(byte[] input)
        {
            using (MemoryStream inputStream = new MemoryStream(input))
            {
                using (DeflateStream gzip =
                  new DeflateStream(inputStream, CompressionMode.Decompress))
                {
                    using (StreamReader reader =
                      new StreamReader(gzip, System.Text.Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        #region Private Methode
        private byte[] Encrypt(byte[] buffer, byte[] vector)
        {
            var encryptor = this._rm.CreateEncryptor(this._key, vector);
            return this.Transform(buffer, encryptor);
        }

        private byte[] Decrypt(byte[] buffer, byte[] vector)
        {
            var decryptor = this._rm.CreateDecryptor(this._key, vector);
            return this.Transform(buffer, decryptor);
        }

        private byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            var stream = new MemoryStream();
            using (var cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
            {
                cs.Write(buffer, 0, buffer.Length);
            }

            return stream.ToArray();
        }
        #endregion
    }
}
