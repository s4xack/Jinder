using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MyWaitAll
{
    public class WaitAll : IDisposable
    {
        private readonly List<Boolean> _atoms;
        private readonly ManualResetEvent _event;
        private readonly Mutex _atomsAccessMutex;

        public WaitAll(Int32 atomsNumber)
        {
            _atoms = Enumerable.Repeat(false, atomsNumber).ToList();
            _event = new ManualResetEvent(false);
            _atomsAccessMutex = new Mutex();
        }

        public void SetAtomSignaled(Int32 atomId)
        {
            if (atomId < 0 || atomId >= _atoms.Count)
                throw new ArgumentException("Wrong atom id!");

            _atomsAccessMutex.WaitOne();

            _atoms[atomId] = true;

            if (CheckAtomsForSignal())
                _event.Set();

            _atomsAccessMutex.ReleaseMutex();
        }

        public void SetAtomNoSignaled(Int32 atomId)
        {
            if (atomId < 0 || atomId >= _atoms.Count)
                throw new ArgumentException("Wrong atom id!");

            _atomsAccessMutex.WaitOne();

            _atoms[atomId] = false;

            if (!CheckAtomsForSignal())
                _event.Reset();

            _atomsAccessMutex.ReleaseMutex();
        }

        private Boolean CheckAtomsForSignal()
        {
            return _atoms.Aggregate((a, b) => a && b);
        }

        public void Wait()
        {
            _event.WaitOne();
        }

        public bool Wait(int millisecondsTimeout)
        {
            return _event.WaitOne(millisecondsTimeout);
        }

        public void Dispose()
        {
            _event?.Dispose();
            _atomsAccessMutex?.Dispose();
        }
    }
}
