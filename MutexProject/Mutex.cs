using System;
using System.Threading;

namespace MutexProject
{
    public class Mutex
    {
        private int _locked;
        private int _ownerThreadId = -1;

        public void Lock()
        {
            while (Interlocked.CompareExchange(ref _locked, 1, 0) == 1)
            {
            }

            _ownerThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        public void Unlock()
        {
            if (Thread.CurrentThread.ManagedThreadId != _ownerThreadId)
            {
                if (_ownerThreadId == -1) return;
                throw new SystemException("Not owner thread trying to unlock");
            }
            Interlocked.CompareExchange(ref _locked, 0, 1);
            _ownerThreadId = -1;
        }
    }
}