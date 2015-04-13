using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceStack.TCP
{
    public class Result : IDisposable
    {
        public Result()
        {
            mData = BufferPool.Single.Pop();
            mLineBuffer = LineBuffer.Pop();
        }

        private LineBuffer mLineBuffer;

        public object ResultData
        {
            get;
            set;
        }

        private string mHeader = null;

        private int mResultCount = 0;

        private int mItemLength = 0;

        private int mBufferOffset = 0;

        private int mBlockStartIndex = 0;

        public string Header
        {
            get
            {
                return mHeader;
            }
        }

        public IList<ArraySegment<byte>> ResultDataBlock
        {
            get;
            set;
        }

        private int mImportOffset = 0;



        internal bool Import(byte[] data, int offset, int count)
        {

            offset += mImportOffset;
            count -= mImportOffset;
            while (count > 0)
            {
                if (mHeader == null)
                {
                    if (mLineBuffer.Import(data, ref offset, ref count))
                    {
                        mHeader = mLineBuffer.GetLineString();
                        if (mHeader[0] == '+')
                        {
                            ResultData = mHeader.Substring(1, mHeader.Length - 1);
                            return true;
                        }
                        else if (mHeader[0] == '-')
                        {
                            throw new Exception(mHeader.Substring(1, mHeader.Length - 1));
                        }
                        else if (mHeader[0] == '$')
                        {
                            mResultCount = 1;
                            mItemLength = int.Parse(mHeader.Substring(1, mHeader.Length - 1));
                        }
                        else if (mHeader[0] == '*')
                        {
                            mResultCount = int.Parse(mHeader.Substring(1, mHeader.Length - 1));

                        }
                        else if (mHeader[0] == ':')
                        {
                            ResultData = mHeader.Substring(1, mHeader.Length - 1);
                            return true;
                        }
                        else
                        {
                            return true;
                        }
                        ResultDataBlock = new List<ArraySegment<byte>>(mResultCount);
                        mLineBuffer.Reset();
                    }
                }
                if (mResultCount == 0)
                    return true;
                if (mItemLength == 0 && mResultCount > 0)
                {
                    if (mLineBuffer.Import(data, ref offset, ref count))
                    {
                        string line;
                        try
                        {
                            line = mLineBuffer.GetLineString();
                            mItemLength = int.Parse(line.Substring(1, line.Length - 3));
                            mLineBuffer.Reset();
                        }
                        catch (Exception e_)
                        {
                            throw e_;
                        }
                    }

                }
                if (mItemLength == -1 || mItemLength > 0)
                    LoadData(data, ref offset, ref count);
            }

            if (count < 0)
                mImportOffset = Math.Abs(count);
            else
                mImportOffset = 0;
            return mItemLength == 0 && mResultCount == 0;
        }

        private void LoadData(byte[] data, ref int offset, ref int count)
        {
            try
            {
                if (mItemLength == -1)
                {

                    ResultDataBlock.Add(new ArraySegment<byte>(mData, mBlockStartIndex, 0));
                    mItemLength = 0;
                    mResultCount--;
                    return;
                }
                if (count >= mItemLength)
                {

                    Buffer.BlockCopy(data, offset, mData, mBufferOffset, mItemLength);
                    offset += (mItemLength + 2);
                    count -= (mItemLength + 2);
                    mBufferOffset += mItemLength;
                    ResultDataBlock.Add(new ArraySegment<byte>(mData, mBlockStartIndex, mBufferOffset - mBlockStartIndex));
                    mBlockStartIndex = mBufferOffset;
                    mItemLength = 0;
                    mResultCount--;
                }
                else
                {
                    Buffer.BlockCopy(data, offset, mData, mBufferOffset, count);
                    mBufferOffset += count;
                    offset += count;
                    mItemLength -= count;
                    count = 0;

                }
            }
            catch (Exception e_)
            {
                throw new Exception("buffer overflow buffer max size:" + BufferPool.DEFAULT_BUFFERLENGTH, e_);
            }
        }

        private string ReadLine(byte[] data, ref int offset, ref int count)
        {
            int start = offset;
            while (count > 1)
            {
                if (data[offset] == utils.Eof[0] && data[offset + 1] == utils.Eof[1])
                {
                    offset += 2;
                    count -= 2;
                    return Encoding.ASCII.GetString(data, start, offset - start - 2);
                }
                else
                {
                    offset += 1;
                    count -= 1;
                }
            }
            return null;
        }

        private byte[] mData = null;

        public void Dispose()
        {
            lock (this)
            {

                BufferPool.Single.Push(mData);
                mData = null;
                LineBuffer.Push(mLineBuffer);
                mLineBuffer = null;
            }
        }
    }
}
