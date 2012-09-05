using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ziggurat.Client.Setup
{
    public sealed class Host : IDisposable
    {
        public CancellationTokenSource Cancellation { get; private set; }
        private readonly List<HostTask> _hostTasks = new List<HostTask>();
        private readonly List<HostTask> _startupTasks = new List<HostTask>();

        public Host()
        {
            Cancellation = new CancellationTokenSource();
        }

        public void AddStartupTask(Func<CancellationToken, Task> taskFactory)
        {
            if (taskFactory == null) throw new ArgumentNullException("taskFactory");
            _startupTasks.Add(new HostTask(taskFactory));
        }

        public void AddStartupTask(Action<CancellationToken> task)
        {
            if (task == null) throw new ArgumentNullException("task");
            AddStartupTask(c => Task.Factory.StartNew(() => task(c)));
        }

        public void AddTask(Func<CancellationToken, Task> taskFactory)
        {
            if (taskFactory == null) throw new ArgumentNullException("taskFactory");
            _hostTasks.Add(new HostTask(taskFactory));
        }

        public void AddTask(Action<CancellationToken> task)
        {
            if (task == null) throw new ArgumentNullException("task");
            AddTask(c => Task.Factory.StartNew(() => task(c)));
        }

        public Task Run()
        {
            var startupTask = RunGroupOfTasks(_startupTasks);
            return startupTask.ContinueWith(task => RunGroupOfTasks(_hostTasks), Cancellation.Token);
        }

        private Task RunGroupOfTasks(IList<HostTask> hostTasks)
        {
            var tplTasks = hostTasks
                .Select(x => x.GetTask(Cancellation.Token))
                .ToArray();

            foreach (var task in tplTasks.Where(x => x.Status == TaskStatus.Created))
            {
                task.Start();
            }

            return Task.Factory.StartNew(() =>
            {
                Task.WaitAll(tplTasks);
            }, Cancellation.Token);
        }


        public void Dispose()
        {
            Cancellation.Cancel();
        }

        private class HostTask
        {
            private readonly Func<CancellationToken, Task> _taskFactory;
            public HostTask(Func<CancellationToken, Task> taskFactory)
            {
                _taskFactory = taskFactory;
            }

            public Task GetTask(CancellationToken token)
            {
                return _taskFactory(token);
            }
        }
    }
}
