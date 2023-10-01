namespace AppUnittest;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneybox.App;
using Moneybox.App.Domain.Services;
using Moq;

[TestClass]
public class AccountTests
{
    [TestMethod]
    public void NotifyFundsLow_LowFunds()
    {
        var notificationServiceMock = new Mock<INotificationService>();
        var account = new Account(notificationServiceMock.Object)
        {
            User = new User { Email = "moneyboxapp@test.com" },
            Balance = 10.0m
        };

        account.NotifyFundsLow();

        notificationServiceMock.Verify(
            ns => ns.NotifyFundsLow("moneyboxapp@test.com"),
            Times.Once
            );
    }

    [TestMethod]
    public void NotifyFundsLow_EnoughFunds()
    {
        var notificationServiceMock = new Mock<INotificationService>();
        var account = new Account(notificationServiceMock.Object)
        {
            User = new User { Email = "moneyboxapp@test.com" },
            Balance = 501.0m

        };

        account.NotifyFundsLow();

        notificationServiceMock.Verify(
            ns => ns.NotifyFundsLow("moneyboxapp@test.com"),
            Times.Never
        );
    }

    [TestMethod]
    public void NotifyApproachingPayInLimit_ShouldNotify()
    {
        // Notify when Funds is low
        var notificationServiceMock = new Mock<INotificationService>();
        var account = new Account(notificationServiceMock.Object)
        {
            User = new User { Email = "moneyboxapp@test.com" },
            PaidIn = 3900.0m
        };
        account.NotifyApproachingPayInLimit();
        notificationServiceMock.Verify(
            ns => ns.NotifyApproachingPayInLimit("moneyboxapp@test.com"), Times.Once);
    }


    [TestMethod]
    public void NotifyApproachingPayInLimit_ShouldNotNotify()
    {
        // Do not notify when fund is not low
        var notificationServiceMock = new Mock<INotificationService>();
        var account = new Account(notificationServiceMock.Object)
        {
            User = new User { Email = "moneyboxapp@test.com" },
            PaidIn = 500.0m,
        };
        account.NotifyApproachingPayInLimit();
        notificationServiceMock.Verify(
            ns => ns.NotifyApproachingPayInLimit("moneyboxapp@test.com"), Times.Never);
    }
}
