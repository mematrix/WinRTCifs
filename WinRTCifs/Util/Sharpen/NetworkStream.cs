using System;
using System.IO;

namespace WinrtCifs.Util.Sharpen
{
    public class NetworkStream : Stream 
    {
        SocketEx _socket;

        public NetworkStream(SocketEx socket)
        {
            _socket = socket;
        }
        
        public override bool CanRead
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CanSeek
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CanWrite
        {
            get { throw new NotImplementedException(); }
        }

        public override void Flush()
        {
           // throw new NotImplementedException();
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            //NEW SOCKET
            return _socket.Receive(buffer, offset, count,System.Net.Sockets.SocketFlags.None);
           // return _socket.Receive(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            //NEW SOCKET
             _socket.Send(buffer, offset, count, System.Net.Sockets.SocketFlags.None);
           // _socket.Send(buffer, offset, count);
        }
    }
}
