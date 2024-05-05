using Infrastructure.Helper.Config;
using Service.Executor;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AppConfig
{
    public class Services
    {
        private readonly IQueuedExecutor _queuedExecutor;
        private readonly IConfigCreatorHelper _configCreator;
        private int _instanceRunning;
        private Timer _timer;
        private List<bool> _queueStop;

        public Services(
            IQueuedExecutor queuedExecutor,
            IConfigCreatorHelper configCreator
           // IApplicationLifetime applicationLifetime
            )
        {
            _queuedExecutor = queuedExecutor;
            _configCreator = configCreator;
            _instanceRunning = Convert.ToInt32(_configCreator.GetInteger("Service:Service.Instance.Int"));
            _queueStop = new List<bool>();
        }

        public void Start()
        {
            for (int i = 0; i < _instanceRunning; i++) _queueStop.Add(true);

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(_configCreator.GetDouble("Service:Service.Interval.Seconds.Double")));
        }

        private void DoWork(object state)
        {
            for (int i = 0; i < _queueStop.Count; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    var data = _queuedExecutor.Run();

                    _queueStop.Add(data);

                    Task.Delay(TimeSpan.FromMilliseconds(10));

                }, TaskCreationOptions.LongRunning | TaskCreationOptions.PreferFairness);

                if (i + 1 == _queueStop.Count)
                    _queueStop = new List<bool>();
            }
        }
    }
}
