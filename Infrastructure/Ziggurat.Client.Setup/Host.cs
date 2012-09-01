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

        public Host()
        {
            Cancellation = new CancellationTokenSource();
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
            var allTasks = _hostTasks
                .Select(x => x.GetTask(Cancellation.Token))
                .ToArray();

            foreach (var task in allTasks.Where(x => x.Status == TaskStatus.Created))
            {
                task.Start();
            }

            return Task.Factory.StartNew(() =>
            {
                Task.WaitAll(allTasks);
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
