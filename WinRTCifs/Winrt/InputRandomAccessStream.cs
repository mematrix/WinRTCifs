using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;
using WinrtCifs.Util.Sharpen;

namespace WinrtCifs.Winrt
{

    /// 
    ///  WinrtInputStream 
    ///  TODO 
    ///  saki
    /// 
    public class WinrtInputStream : IInputStream
    {
        private readonly InputStream _source;


        public WinrtInputStream(InputStream inputStream)
        {
            _source = inputStream;
        }


        public InputRandomAccessStream AsInputRandomAccessStream()
        {
            return new InputRandomAccessStream(this, _source);
        }

        public void Dispose()
        {
            _source.Dispose();
        }

        public IAsyncOperationWithProgress<IBuffer, uint> ReadAsync(IBuffer buffer, uint count, InputStreamOptions options)
        {
            return AsyncInfo.Run<IBuffer, uint>(async (token, progress) =>
            {
                return await Task.Run(() =>
                {
                    try
                    {
                        progress.Report(0);
                        var bytes = new byte[buffer.Capacity];
                        var length = 0;
                        while (true)
                        {

                            if (token.IsCancellationRequested)
                            {
                                break;
                            }
                            var r = _source.Read(bytes, length, (int)count);
                            length += r;
                            if (length == count)
                            {
                                break;
                            }
                            if (r < 1)
                            {
                                break;
                            }
                        }

                        return bytes.AsBuffer();
                    }
                    finally
                    {
                        progress.Report(100);
                    }
                }, token);

            });
        }
    }
    public class InputRandomAccessStream : IRandomAccessStream
    {

        private readonly InputStream _source;
        private readonly IInputStream _stream;

        public InputRandomAccessStream(InputStream inputStream)
        {
            _source = inputStream;
            _stream = new WinrtInputStream(inputStream);
        }

        public InputRandomAccessStream(IInputStream input, InputStream sourceStream)
        {
            _source = sourceStream;
            _stream = input;
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        public IAsyncOperationWithProgress<IBuffer, uint> ReadAsync(IBuffer buffer, uint count, InputStreamOptions options)
        {
            return _stream.ReadAsync(buffer, count, options);
        }


        public IAsyncOperationWithProgress<uint, uint> WriteAsync(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<bool> FlushAsync()
        {
            return AsyncInfo.Run<bool>(async (token) => await Task.FromResult(true));
        }

        public IInputStream GetInputStreamAt(ulong position)
        {
            return _stream;
        }

        public IOutputStream GetOutputStreamAt(ulong position)
        {
            throw new NotImplementedException();
        }

        public void Seek(ulong position)
        {
            _source.Position = (long)position;
        }

        public IRandomAccessStream CloneStream()
        {
            return new InputRandomAccessStream(_source);
        }

        public bool CanRead { get; } = true;
        public bool CanWrite { get; } = false;

        public ulong Position
        {
            get { return (ulong)_source.Position; }
        }

        public ulong Size
        {
            get { return (ulong)_source.Length; }
            set
            {

            }
        }
    }
}
