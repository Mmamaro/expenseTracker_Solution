
using expenseTracker_Api.Models;
using expenseTracker_Api.Repositories;
using System.Globalization;
using System.Threading;

namespace expenseTracker_Api.Service
{
    public class monthSummaryService : IHostedService
    {
        private readonly usersRepository? _usersRepository;
        private readonly expensesRepository? _expensesRepository;
        private readonly monthlySummariesRepository? _monthlySummariesRepository;

        public monthSummaryService(usersRepository usersRepository,expensesRepository expensesRepository, 
            monthlySummariesRepository monthlySummariesRepository)
        {
           _usersRepository = usersRepository;
           _expensesRepository = expensesRepository;
           _monthlySummariesRepository = monthlySummariesRepository;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => monthlySummary(cancellationToken));

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(Timeout.Infinite, cancellationToken);
        }

        private async Task monthlySummary(CancellationToken cancellationToken)
        {
            while(!cancellationToken.IsCancellationRequested)
            {
                var users = await _usersRepository.GetAllUsersAsync();
                var expenses = await _expensesRepository.GetAllExpensesAsync();
                var monthlySummaries = await _monthlySummariesRepository.GetAllmonthlySummaryAsync();

                var groupedExpenses = expenses.GroupBy(e => new { e.userId, e.date.Year, e.date.Month });

                foreach (var user in users)
                {
                    foreach(var group in groupedExpenses.Where(g => g.Key.userId == user.userId))
                    {

                        var totalExpenses = group.Sum(e => e.amount);

                        var monthlySummary = new monthlySummary
                        {
                            userId = user.userId,
                            uniqueIdentifier = user.userId.ToString() + "_" + 
                            CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key.Month) + "_" + group.Key.Year.ToString(),
                            name = user.name,
                            year = group.Key.Year.ToString(),
                            month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key.Month),
                            salary = user.salary,
                            totalExpenses = totalExpenses,
                            remainingBal = user.salary - totalExpenses,
                            email = user.email
                        };

                           bool idExists = monthlySummaries.Any(e => e.uniqueIdentifier == monthlySummary.uniqueIdentifier);

                        if (!idExists)
                        {
                            await _monthlySummariesRepository.AddUserAsync(monthlySummary.userId, monthlySummary.uniqueIdentifier, 
                                    monthlySummary.name, monthlySummary.year,
                                    monthlySummary.month, monthlySummary.salary, 
                                    monthlySummary.totalExpenses, monthlySummary.remainingBal, monthlySummary.email);

                        }


                        //Console.WriteLine($"{monthlySummary.email} This year: {monthlySummary.year} " +
                        //    $"on this month: {monthlySummary.month} spent: {monthlySummary.totalExpenses} " +
                        //    $"and and was left with: {monthlySummary.remainingBal} " +
                        //    $"AND their UNIQUE IDENTIFIER IS: {monthlySummary.uniqueIdentifier}");
                    }
                }

                await Task.Delay(TimeSpan.FromDays(30), cancellationToken);

            }
        }
    }
}
