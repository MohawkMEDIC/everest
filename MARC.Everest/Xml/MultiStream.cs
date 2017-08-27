using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MARC.Everest.Xml
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class MultiStream : Stream
    {
        private List<Stream> streams = new List<Stream>();

        long position = 0;
        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Length
        {
            get
            {
                return streams.Select(s => s.Length).Aggregate((long)0, (acc, x) => acc + x);
            }
        }

        public override long Position
        {
            get { return position; }
            set { Seek(value, SeekOrigin.Begin); }
        }

        public override void Flush() { }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long len = Length;
            switch (origin)
            {
                case SeekOrigin.Begin:
                    position = offset;
                    break;
                case SeekOrigin.Current:
                    position += offset;
                    break;
                case SeekOrigin.End:
                    position = len + offset;
                    break;
            }
            if (position > len)
            {
                position = len;
            }
            if (position < 0)
            {
                position = 0;
            }
            return position;
        }

        public override void SetLength(long value) { }

        public void AddStream(Stream stream)
        {
            streams.Add(stream);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            long len = 0;
            int result = 0;
            int buf_pos = offset;
            int bytesRead;
            foreach (Stream stream in streams)
            {
                if (position < (len + stream.Length))
                {
                    stream.Position = position - len;
                    bytesRead = stream.Read(buffer, buf_pos, count);
                    result += bytesRead;
                    buf_pos += bytesRead;
                    position += bytesRead;
                    if (bytesRead < count)
                    {
                        count -= bytesRead;
                    }
                    else
                    {
                        break;
                    }
                }
                len += stream.Length;
            }
            return result;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            // unused
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}