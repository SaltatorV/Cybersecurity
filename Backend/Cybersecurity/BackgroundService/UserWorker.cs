using Cybersecurity.Entities;
using Cybersecurity.Interfaces.Repositories;

namespace Cybersecurity.BackgroundService
{
    public class UserWorker : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private Timer? _timer = null;
        private readonly IServiceScopeFactory _factory;

        public UserWorker(IServiceScopeFactory factory)
        {
            _factory = factory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            var userRepository = _factory.CreateScope().ServiceProvider.GetRequiredService<IGenericRepository<User>>();

            var count = Interlocked.Increment(ref executionCount);

            var users = await userRepository.GetAllAsync();

            foreach(var item in users)
            {
                if (DateTime.Compare(item.PasswordExpire, DateTime.UtcNow) < 0)
                {
                    item.IsPasswordExpire = true;

                    await userRepository.UpdateAsync(item);
                    await userRepository.SaveAsync();
                }
            }

            foreach (var item in users)
            {
                if (item.IsLoginLockOn)
                {
                    if (DateTime.Compare((DateTime)item.LoginLockOnTime, DateTime.UtcNow) < 0)
                    {
                        item.IsLoginLockOn = false;
                        item.LoginLockOnTime = null;

                        await userRepository.UpdateAsync(item);
                        await userRepository.SaveAsync();
                    }
                }
            }

            Console.WriteLine(count.ToString());

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
