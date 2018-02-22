using System;
using DC.ILR._1718.FileValidationService.Rules.Query;
using FluentAssertions;
using Xunit;

namespace DC.ILR._1718.FileValidationService.Rules.Tests.Query
{
    public class IlrFileNameQueryServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetYearOfCollection_NullOrEmpty(string fileName)
        {
            var service = new IlrFileNameQueryService();
            service.GetYearOfCollection(fileName).Should().Be(null);
        }

        [Theory]
        [InlineData("ILR-10000532-1718-20180128-100358-10.XYZ")]
        [InlineData("ILR-0000000-0000-20180128-100358-10.zip")]
        [InlineData("ILR-0000000-0000-20180128-100358-0.xml")]
        [InlineData("ILR-10000532-1718-XYZ-100358-10.xml")]
        [InlineData("ILR-11111111111.xml")]
        public void GetYearOfCollection_InvalidFileName(String fileName)
        {
            var service = new IlrFileNameQueryService();
            service.GetYearOfCollection(fileName).Should().Be(null);
        }

        [Theory]
        [InlineData("ILR-10000532-1718-20180128-100358-10.xml")]
        [InlineData("ILR-10000532-1718-20180128-100358-10.zip")]
        public void GetYearOfCollection_ValidFileName(string fileName)
        {
            var service = new IlrFileNameQueryService();
            service.GetYearOfCollection(fileName).Should().Be("1718");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("ILR-10000532-1718-20180128-100358-10.XYZ")]
        [InlineData("ILR-0000000-0000-20180128-100358-10.zip")]
        [InlineData("ILR-0000000-0000-20180128-100358-0.xml")]
        [InlineData("ILR-10000532-1718-XYZ-100358-10.xml")]
        [InlineData("ILR-11111111111.xml")]
        public void IsValidFileName_False(string fileName)
        {
            var service = new IlrFileNameQueryService();
            service.IsValidFileName(fileName).Should().BeFalse();
        }

        [Theory]
        [InlineData("ILR-10000532-1718-20180128-100358-10.xml")]
        [InlineData("ILR-10000532-1718-20180128-100358-10.zip")]
        public void IsValidFileName_True(string fileName)
        {
            var service = new IlrFileNameQueryService();
            service.IsValidFileName(fileName).Should().BeTrue();
        }
    }
}
