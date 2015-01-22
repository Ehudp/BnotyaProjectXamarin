using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WomenInBible.CustomControls
{
    //internal sealed class Timer : CancellationTokenSource
    //{
    //    internal Timer(Action<object> callback, object state, int millisecondsDueTime, 
    //        int millisecondsPeriod, bool waitForCallbackBeforeNextPeriod = false)
    //    {
    //        Task.Delay(millisecondsDueTime, Token).ContinueWith(async (t, s) =>
    //        {
    //            var tuple = (Tuple<Action<object>, object>)s;

    //            while (!IsCancellationRequested)
    //            {
    //                if (waitForCallbackBeforeNextPeriod)
    //                    tuple.Item1(tuple.Item2);
    //                else
    //                    Task.Run(() => tuple.Item1(tuple.Item2));

    //                await Task.Delay(millisecondsPeriod, Token).ConfigureAwait(false);
    //            }

    //        }, Tuple.Create(callback, state), CancellationToken.None, 
    //        TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion, 
    //        TaskScheduler.Default);
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //            Cancel();

    //        base.Dispose(disposing);
    //    }
    //}

    internal class Timer : Timer<object> { }

    internal class Timer<T> : CancellationTokenSource, IDisposable
    {
        internal async void Start(Action<T> callback, T state, int dueTime, int period, Action<T> completedCallback)
        {
            if (dueTime < 0) throw new ArgumentException("dueTime cannot be negative.");
            if (callback == null) throw new ArgumentException("dueTime cannot be negative.");

            int timeLeft = period;
            var infinite = period < 0 ? true : false;

            while ((timeLeft >= 0 || infinite))
            {
                if (!this.IsCancellationRequested)
                {
                    timeLeft -= dueTime;
                    await Task.Delay(dueTime).ConfigureAwait(false);
                    callback(state);
                }
                else
                {
                    Dispose();
                    return;
                }
            }

            if (completedCallback != null)            
                completedCallback(state);            

            Dispose();
        }

        public new void Dispose() { base.Cancel(); }
    }

}
