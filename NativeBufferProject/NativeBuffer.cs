using System;
using System.Runtime.InteropServices;

namespace NativeBufferProject
{
    public class NativeBuffer : IDisposable
    {
        private bool _disposed;
        public IntPtr Handle { get; }

        public NativeBuffer(int cb)
        {
            Handle = Marshal.AllocHGlobal(cb);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Marshal.FreeHGlobal(Handle);
                }
                _disposed = true;
            }
        }

        ~NativeBuffer()
        {
            Dispose(false);
        }
    }
}