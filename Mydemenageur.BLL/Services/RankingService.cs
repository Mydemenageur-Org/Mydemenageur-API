using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mydemenageur.BLL.Services.Interfaces;

namespace Mydemenageur.BLL.Services
{
    public class RankingService : IHostedService
    {

        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;

        public RankingService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            TimeSpan interval = TimeSpan.FromHours(24);
            var nextRunTime = DateTime.Today.AddDays(1).AddHours(3);
            var curTime = DateTime.Now;
            var firstInterval = nextRunTime.Subtract(curTime);

            Action action = () =>
            {
                var t1 = Task.Delay(firstInterval);
                t1.Wait();
                _timer = new Timer(
                    CalculatRanking,
                    null,
                    TimeSpan.Zero,
                    interval
                );
            };

            // no need to await this call here because this task is scheduled to run much much later.
            Task.Run(action);
            return Task.CompletedTask;	
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void CalculatRanking(object state)
        {
            using(var scope = _serviceProvider.CreateScope()) {
                var grosBrasService = scope.ServiceProvider.GetService<IGrosBrasService>();
                grosBrasService.CalculatRanking();
            }
        }
    }
}