using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ProvidersConsumers
{
    public class Buffer<TValue>
    {
        private readonly Queue<TValue> _data;

        private readonly Semaphore _readAccess;
        private readonly Semaphore _writeAccess;

        private readonly Mutex _dataAccess;

        public Buffer(Int32 capacity)
        {
            _data = new Queue<TValue>(capacity);
            
            _dataAccess = new Mutex();

            _readAccess = new Semaphore(capacity, capacity);
            _writeAccess = new Semaphore(0, capacity);
        }

        public void Write(TValue value)
        {
            _writeAccess.WaitOne();

            _dataAccess.WaitOne();
            _data.Enqueue(value);
            _dataAccess.ReleaseMutex();
 
            _readAccess.Release();
        }

        public TValue Read()
        {
            _readAccess.WaitOne();

            _dataAccess.WaitOne();
            TValue value = _data.Dequeue();
            _dataAccess.ReleaseMutex();

            _writeAccess.Release();

            return value;
        }
    }
}
