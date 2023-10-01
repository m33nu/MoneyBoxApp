using System;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;

namespace Moneybox.App
{
    public class Account
    {
        private INotificationService notificationService;

        public Account(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public const decimal PayInLimit = 4000m;

        public Guid Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }

        public void NotifyApproachingPayInLimit()
        {
            if (Account.PayInLimit - PaidIn < 500m)
            {
                notificationService.NotifyApproachingPayInLimit(User.Email);
            }
        }

        public void NotifyFundsLow()
        {
            if (Balance < 500m)
            {
                notificationService.NotifyFundsLow(User.Email);
            }
        }
    }
}
