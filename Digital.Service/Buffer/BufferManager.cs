using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Digital.Service
{
    public class BufferManager<T> : IDisposable where T : class
    {
        public BufferManager()
        {
            _semaphore = new SemaphoreSlim(0);
            _buffer = new ConcurrentQueue<T>();
        }

        private SemaphoreSlim _semaphore;

        private ConcurrentQueue<T> _buffer;

        public void Put(T item)
        {
            if (item == null)
                return;
            _buffer.Enqueue(item);
            _semaphore.Release();
        }

        //public void PutRange(T[] notifications)
        //{
        //    var length = notifications.Length;
        //    if (length > 0)
        //    {
        //        _buffer.PushRange(notifications);
        //        _semaphore.Release(length);
        //    }
        //}

        //public T Get()
        //{
        //    _semaphore.Wait();
        //    T item = null;
        //    if (_buffer.TryDequeue(out item))
        //        return item;
        //    else
        //        return null;
        //}

        public T Get(CancellationToken token)
        {
            _semaphore.Wait(token);
            T item = null;
            if (_buffer.TryDequeue(out item))
                return item;
            else
                return null;
        }

        //public Int32 GetTotalItemsCount()
        //{
        //    return _buffer.Count;
        //}

        public void Dispose()
        {
            if (_semaphore != null)
            {
                try
                {
                    _semaphore.Dispose();
                }
                finally
                {
                    _semaphore = null;
                }
            }
        }
    }
}
