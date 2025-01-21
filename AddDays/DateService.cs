namespace DateAdd
{
    public interface IDateService
    {
        string AddDaysToDate(string? date, int daysToAdd);
    }
    public class DateService : IDateService
    {
        private readonly int[] _daysInMonth = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

        public string AddDaysToDate(string? date, int daysToAdd)
        {
            if (string.IsNullOrEmpty(date))
            {
                return "Date Cannot Be Empty";
            }
            if (!ValidateDateFormat(date))
            {
                return "Invalid Date Format";
            }
            switch (daysToAdd)
            {
                case 0:
                    return date;
                case < 0:
                    return "Please provide number of days in positive integer";
            }

            var dateParts = date.Split('/');
            var day = int.Parse(dateParts[0]);
            var month = int.Parse(dateParts[1]);
            var year = int.Parse(dateParts[2]);

            day += daysToAdd;

            while (day > _daysInMonth[month - 1])
            {
                day -= _daysInMonth[month - 1];
                month++;

                if (month <= 12) continue;

                month = 1;
                year++;
            }

            return $"{day:D2}/{month:D2}/{year}";
        }

        private bool ValidateDateFormat(string inputDate)
        {
            var parts = inputDate.Split('/');
            if (parts.Length != 3) return false;

            if (!int.TryParse(parts[0], out var day) ||
                !int.TryParse(parts[1], out var month) ||
                !int.TryParse(parts[2], out var year)) return false;

            if (month < 1 || month > 12 || year < 1) return false;

            if (IsLeapYear(year)) _daysInMonth[1] = 29;

            return day >= 1 && day <= _daysInMonth[month - 1];
        }

        private static bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
        }
    }
}