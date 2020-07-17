using System;
using System.Collections.Generic;
using System.Threading;

namespace BeerBees
{
    public class AutoSender<TValue>
    {
        private readonly Queue<TValue> _data;

        private readonly ISender<TValue> _sender;

        private readonly Int32 _limit;

        private readonly Mutex _dataAccess;
        private readonly AutoResetEvent _sendAccess;
        private readonly Semaphore _addAccess;

        public AutoSender(Int32 limit, ISender<TValue> sender)
        {
            _limit = limit;
            _data = new Queue<TValue>(limit);

            _sender = sender ?? throw new ArgumentNullException(nameof(sender));

            _dataAccess = new Mutex();
            _sendAccess = new AutoResetEvent(false);
            _addAccess = new Semaphore(0, limit);
        }

        public void Add(TValue value)
        {
            _addAccess.WaitOne();

            _dataAccess.WaitOne();
            _data.Enqueue(value);
            _dataAccess.ReleaseMutex();

            if (_limit == _data.Count)
                _sendAccess.Set();
        }

        private void Send()
        {
            _sendAccess.WaitOne();

            _sender.Send(_data);

            _data.Clear();

            _addAccess.Release(_limit);
        }

        public void StartSending()
        {
            Thread thread = new Thread(() =>
            {
                while(true) Send();
            });
        }
    }
}
