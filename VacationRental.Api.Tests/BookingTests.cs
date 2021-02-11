using System;
using System.Collections.Generic;
using System.Text;
using VacationRental.Api.Models;
using Xunit;

namespace VacationRental.Api.Tests
{
    [Collection("UnitTests")]
    public class BookingTests
    {
        [Fact]
        public void GivenDateIsEqualStartDate_ThenDateIsUnavailableShouldReturnTrue()
        {
            var booking = new Booking() { StartDate = new DateTime(2000, 01, 01) };
            var dateToCheck = new DateTime(2000, 01, 01);

            Assert.True(booking.DateIsUnavailable(dateToCheck));
        }

        [Fact]
        public void GivenDateIsInRange_ThenDateIsUnavailableShouldReturnTrue()
        {
            var booking = new Booking() { StartDate = new DateTime(2000, 01, 01), Nights = 2 };
            var dateToCheck = new DateTime(2000, 01, 02);

            Assert.True(booking.DateIsUnavailable(dateToCheck));
        }

        [Fact]
        public void GivenDateIsOutRange_ThenDateIsUnavailableShouldReturnFalse()
        {
            var booking = new Booking() { StartDate = new DateTime(2000, 01, 01), Nights = 2 };
            var dateToCheck = new DateTime(2000, 01, 03);

            Assert.False(booking.DateIsUnavailable(dateToCheck));
        }

        [Fact]
        public void GivenDateIsPreparationDate_ThenDateIsUnavailableShouldReturnTrue()
        {
            var booking = new Booking() { StartDate = new DateTime(2000, 01, 01), Nights = 2, PreparationDays = 1 };
            var dateToCheck = new DateTime(2000, 01, 03);

            Assert.True(booking.DateIsUnavailable(dateToCheck));
        }

        [Fact]
        public void GivenDateIsPreparationDate_ThenDateIsPreparationDateShouldReturnTrue()
        {
            var booking = new Booking() { StartDate = new DateTime(2000, 01, 01), Nights = 2, PreparationDays = 1 };
            var dateToCheck = new DateTime(2000, 01, 03);

            Assert.True(booking.DateIsPeparationDay(dateToCheck));
        }
        [Fact]
        public void GivenDateIsPreparationDate_ThenDateIsBookedShouldReturnFalse()
        {
            var booking = new Booking() { StartDate = new DateTime(2000, 01, 01), Nights = 2, PreparationDays = 1 };
            var dateToCheck = new DateTime(2000, 01, 03);

            Assert.False(booking.DateIsBooked(dateToCheck));
        }
        [Theory]
        [InlineData( "2000-01-01", "2000-01-06", "2000-01-02", "2000-01-06", true)]
        [InlineData( "2000-01-04", "2000-01-05", "2000-01-02", "2000-01-06", true)]
        [InlineData( "2000-01-01", "2000-01-02", "2000-01-01", "2000-01-02", true)]
        [InlineData( "2000-01-01", "2000-01-01", "2000-01-01", "2000-01-01", false)]
        [InlineData( "2000-01-01", "2000-01-02", "2000-01-02", "2000-01-04", false)]
        [InlineData( "2000-01-05", "2000-01-07", "2000-01-04", "2000-01-05", false)]

        public void DateRangeOverlaps(string t1Start, string t1End, string t2Start, string t2End, bool expectedResult)
        {
            var date1Start = DateTime.Parse(t1Start);
            var date1End = DateTime.Parse(t1End);
            var date2Start = DateTime.Parse(t2Start);
            var date2End = DateTime.Parse(t2End);

            var booking = new Booking();
            Assert.Equal(booking.DateRangesOverlaps(date1Start, date1End, date2Start, date2End),expectedResult);
        }

    }
}
