using AddDays;

namespace UnitTest
{
    public class DateServiceTests
    {
        private readonly DateService _dateService;
        public const string InvalidDateFormat = "Invalid Date Format";
        public DateServiceTests()
        {
            _dateService = new DateService();
        }

        [Fact]
        public void AddDaysToDate_should_add_days()
        {
            const string initialDate = "21/01/2025";
            const string expectedDate = "31/01/2025";

            var resultDate = _dateService.AddDaysToDate(initialDate, 10);

            Assert.Equal(expectedDate, resultDate);
        }

        [Fact]
        public void AddDaysToDate_should_handle_month_over_flow()
        {
            const string initialDate = "28/01/2025";
            const string expectedDate = "02/02/2025";

            var resultDate = _dateService.AddDaysToDate(initialDate, 5);

            Assert.Equal(expectedDate, resultDate);
        }

        [Fact]
        public void AddDaysToDate_should_handle_multiple_month_over_flow()
        {
            const string initialDate = "31/10/2025";
            const string expectedDate = "30/12/2025";

            var resultDate = _dateService.AddDaysToDate(initialDate, 60);

            Assert.Equal(expectedDate, resultDate);
        }

        [Fact]
        public void AddDaysToDate_should_handle_non_leap_year_handling()
        {
            const string initialDate = "29/02/2023";

            var resultDate = _dateService.AddDaysToDate(initialDate, 1);

            Assert.Equal(InvalidDateFormat, resultDate);
        }

        [Fact]
        public void AddDaysToDate_should_cross_year_boundary()
        {
            const string initialDate = "28/12/2024";
            const string expectedDate = "07/01/2025";

            var resultDate = _dateService.AddDaysToDate(initialDate, 10);

            Assert.Equal(expectedDate, resultDate);
        }

        [Fact]
        public void AddDaysToDate_should_handle_leap_year()
        {
            const string initialDate = "29/02/2024";
            const string expectedDate = "01/03/2024";

            var resultDate = _dateService.AddDaysToDate(initialDate, 1);

            Assert.Equal(expectedDate, resultDate);
        }

        [Fact]
        public void AddDaysToDate_should_handle_leap_year_divisible_by_400_valid()
        {
            const string initialDate = "29/02/2000";
            const string expectedDate = "01/03/2000";

            var resultDate = _dateService.AddDaysToDate(initialDate, 1);

            Assert.Equal(expectedDate, resultDate);
        }

        [Fact]
        public void AddDaysToDate_should_handle_non_leap_year_divisible_by_100_invalid()
        {
            const string initialDate = "29/02/1900";

            var resultDate = _dateService.AddDaysToDate(initialDate, 1);

            Assert.Equal(InvalidDateFormat, resultDate);
        }

        [Fact]
        public void AddDaysToDate_should_return_input_date_if_days_to_add_is_zero()
        {
            const string initialDate = "28/02/2024";

            var resultDate = _dateService.AddDaysToDate(initialDate, 0);

            Assert.Equal(initialDate, resultDate);
        }

        [Fact]
        public void AddDaysToDate_should_return_error_message_if_input_days_less_than_zero()
        {
            const string errorMessage = "Please provide number of days in positive integer";
            const string initialDate = "28/02/2024";

            var resultDate = _dateService.AddDaysToDate(initialDate, -1);

            Assert.Equal(errorMessage, resultDate);
        }

        [Fact]
        public void AddDaysToDate_should_return_error_message_if_input_date_is_empty()
        {
            const string errorMessage = "Date Cannot Be Empty";

            var resultDate = _dateService.AddDaysToDate("", -1);

            Assert.Equal(errorMessage, resultDate);
        }

        [Theory]
        [InlineData("2025-01-21")]
        [InlineData("-21/01/2025")]
        [InlineData("01/feb/2025")]
        [InlineData("one/02/2025")]
        [InlineData("01/02/2025a")]
        [InlineData("01/00/2025")]
        [InlineData("01/01/0")]
        [InlineData("01/13/2025")]
        public void AddDaysToDate_should_not_return_error_message_invalid_date_format(string initialDate)
        {
            var result = _dateService.AddDaysToDate(initialDate, 10);

            Assert.Equal(InvalidDateFormat, result);
        }
    }
}