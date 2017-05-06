using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace WinrtCifs.Util.Sharpen
{
    public class Thread : IRunnable
    {
        private int? _id = null;
        private Task _task = null;
        private CancellationTokenSource _canceller = null;

        private bool _completed = false;

        private IRunnable _runnable;

        [ThreadStatic]
        private static Thread _wrapperThread;

        public Thread() : this(null, null)
        {
        }

        public Thread(string name) : this(null, name)
        {
        }


        public Thread(IRunnable runnable) : this(runnable, null)
        {
        }


        public String Name;


        Thread(IRunnable runnable, string name)
        {
            this._runnable = runnable ?? this;
            if (!string.IsNullOrEmpty(name))
            {
                this.Name = name;
            }
            _wrapperThread = this;
        }

        private Thread(int threadId)
        {
            this._id = threadId;
        }

        public static Thread CurrentThread()
        {
            if (_wrapperThread == null)
            {
                _wrapperThread = new Thread(Environment.CurrentManagedThreadId);
            }
            return _wrapperThread;
        }

        public string GetName()
        {
            return Name;
        }

        public int? GetId()
        {
            return _id;
        }
        public bool IsComplete()
        {
            return _completed;
        }


        private void InternalRun()
        {
            try
            {
                _runnable.Run();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            finally
            {
            }
        }

        public virtual void Run()
        {
        }

        public void SetDaemon(bool daemon)
        {

        }

        public static void Sleep(long milis)
        {
            Task.Delay((int)milis).Wait();
        }

        public void Join()
        {
            this._task?.Wait();
        }

        public void Join(long timeout)
        {
            this._task?.Wait((int)timeout);
        }

        public void SetName(string name)
        {
            Name = name;
        }



        public void Start()
        {
            this._canceller = new CancellationTokenSource();

            this._task = Task.Factory.StartNew(() =>
                {
                    _wrapperThread = this;
                    this._id = Environment.CurrentManagedThreadId;
                    _runnable.Run();
                }, this._canceller.Token, TaskCreationOptions.LongRunning, TaskScheduler.Current);

            this._task.ContinueWith((e) =>
            {
                _completed = true;
                try
                {
                    Monitor.PulseAll(this);
                }
                catch (Exception)
                {
                }
            });
        }

        public void Abort()
        {
            this._canceller?.Cancel(true);
        }


        public bool Equals(Thread thread)
        {
            if (thread == null || this._id == null || thread.GetId() == null)
            {
                return false;
            }
            return this._id == thread.GetId();
        }

    }

}
