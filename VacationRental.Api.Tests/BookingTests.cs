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
        public void GivenDateIsEqualStartDate_ThenShouldReturnTrue()
        {
            var booking = new Booking() { StartDate = new DateTime(2000, 01, 01) };
            var dateToCheck = new DateTime(2000, 01, 01);

            Assert.True(booking.DateIsBooked(dateToCheck));
        }

        [Fact]
        public void GivenDateIsInRange_ThenShouldReturnTrue()
        {
            var booking = new Booking() { StartDate = new DateTime(2000, 01, 01),Nights=2 };
            var dateToCheck = new DateTime(2000, 01, 02);

            Assert.True(booking.DateIsBooked(dateToCheck));
        }

        [Fact]
        public void GivenDateIsOutRange_ThenShouldReturnFalse()
        {
            var booking = new Booking() { StartDate = new DateTime(2000, 01, 01),Nights=2 };
            var dateToCheck = new DateTime(2000, 01, 03);

            Assert.False(booking.DateIsBooked(dateToCheck));
        }
    }
}
