using System;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace WinrtCifs.Util.Sharpen
{
    public abstract class MessageDigest
	{
	    public void Digest (byte[] buffer, int o, int len)
		{
			byte[] d = Digest ();
			d.CopyTo (buffer, o);
		}

		public byte[] Digest (byte[] buffer)
		{
			Update (buffer);
			return Digest ();
		}

		public abstract byte[] Digest ();
		public abstract int GetDigestLength ();
		public static MessageDigest GetInstance (string algorithm)
		{
			switch (algorithm.ToLower ()) {
			case "sha-1":
				return new MessageDigestSHA1();
			case "md5":
				return new MessageDigestMD5();
			}
			throw new NotSupportedException (string.Format ("The requested algorithm \"{0}\" is not supported.", algorithm));
		}

		public abstract void Reset ();
		public abstract void Update (byte[] b);
		public abstract void Update (byte b);
		public abstract void Update (byte[] b, int offset, int len);
	}

    public class MessageDigestSHA1 : MessageDigest
    {
        private CryptographicHash _hash;
        private HashAlgorithmProvider _hashAlgorithmProvider;

        public MessageDigestSHA1()
        {
            Init();
        }

        public override byte[] Digest()
        {
            byte[] hash = _hash.GetValueAndReset().ToArray();
            Reset();
            return hash;
        }

        public void Dispose()
        {

        }

        public override int GetDigestLength()
        {
            return (int)_hashAlgorithmProvider.HashLength;
            //return (_hash.HashSize / 8);
        }

        private void Init()
        {
            _hashAlgorithmProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha1);
            _hash = _hashAlgorithmProvider.CreateHash();
        }

        public override void Reset()
        {
            Dispose();
            Init();
        }

        public override void Update(byte[] input)
        {
            _hash.Append(input.AsBuffer());
        }

        public override void Update(byte input)
        {
            _hash.Append((new byte[] { 1 }).AsBuffer());
        }

        public override void Update(byte[] input, int index, int count)
        {
            if (count < 0)
                Debug.WriteLine("Argh!");
            _hash.Append(input.AsBuffer(index, count));
        }
    }
    public class MessageDigestMD5: MessageDigest 
	{
		private CryptographicHash _hash;
	    private HashAlgorithmProvider _hashAlgorithmProvider;

		public MessageDigestMD5()
		{
			Init ();
		}

		public override byte[] Digest ()
		{
            byte[] hash = _hash.GetValueAndReset().ToArray();
            Reset ();
			return hash;
		}

		public void Dispose ()
		{
            
        }

		public override int GetDigestLength ()
		{
            return (int)_hashAlgorithmProvider.HashLength;
            //return (_hash.HashSize / 8);
		}

		private void Init ()
		{
           _hashAlgorithmProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            _hash = _hashAlgorithmProvider.CreateHash();
		}

		public override void Reset ()
		{
			Dispose ();
			Init ();
		}

		public override void Update (byte[] input)
		{
            _hash.Append(input.AsBuffer());
		}

		public override void Update (byte input)
		{
            _hash.Append((new byte[] {1}).AsBuffer());
        }

		public override void Update (byte[] input, int index, int count)
		{
			if (count < 0)
				Debug.WriteLine ("Argh!");
            _hash.Append(input.AsBuffer(index,count));
        }
	}
}
