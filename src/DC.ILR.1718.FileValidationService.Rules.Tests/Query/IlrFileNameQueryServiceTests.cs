using System;
using DC.ILR.FileValidationService.Rules.Query;
using FluentAssertions;
using Xunit;

namespace DC.ILR.FileValidationService.Rules.Tests.Query
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

        [Theory]
        [InlineData("ILR-10000532-1718-20180128-100358-10.XYZ")]
        [InlineData("ILR-0000000-0000-20180128-100358-10.zip")]
        [InlineData("ILR-0000000-0000-20180128-100358-0.xml")]
        [InlineData("ILR-10000532-1718-XYZ-100358-10.xml")]
        [InlineData("ILR-11111111111.xml")]
        public void GetUkprn_InvalidFileName(String fileName)
        {
            var service = new IlrFileNameQueryService();
            service.GetUkprn(fileName).Should().Be(0);
        }

        [Theory]
        [InlineData("ILR-10000532-1718-20180128-100358-10.xml")]
        [InlineData("ILR-10000532-1718-20180128-100358-10.zip")]
        public void GetUkprn_ValidFileName(string fileName)
        {
            var service = new IlrFileNameQueryService();
            service.GetUkprn(fileName).Should().Be(10000532);
        }

        [Theory]
        [InlineData("ILR-10000532-1718-20180128-100358-10.XYZ")]
        [InlineData("ILR-0000000-0000-20180128-100358-10.zip")]
        [InlineData("ILR-0000000-0000-20180128-100358-0.xml")]
        [InlineData("ILR-10000532-1718-XYZ-100358-10.xml")]
        [InlineData("ILR-11111111111.xml")]
        public void GetSerialNumber_InvalidFileName(String fileName)
        {
            var service = new IlrFileNameQueryService();
            service.GetSerialNumber(fileName).Should().Be(null);
        }

        [Theory]
        [InlineData("ILR-10000532-1718-20180128-100358-02.xml")]
        [InlineData("ILR-10000532-1718-20180128-100358-02.zip")]
        public void GetSerialNumber_ValidFileName(string fileName)
        {
            var service = new IlrFileNameQueryService();
            service.GetSerialNumber(fileName).Should().Be("02");
        }

        [Theory]
        [InlineData("ILR-10000532-1718-20180128-100358-02.xml",1, "10000532")]
        [InlineData("ILR-10000532-1718-20180128-100358-02.xml", 2, "1718")]
        [InlineData("ILR-10000532-1718-20180128-100358-02.xml", 3, "20180128")]
        [InlineData("ILR-10000532-1718-20180128-100358-02.xml", 4, "100358")]
        [InlineData("ILR-10000532-1718-20180128-100358-02.xml", 5, "02")]
        public void GetFilePart_ValidFileName(string fileName,int index, string expectedResult)
        {
            var service = new IlrFileNameQueryService();
            service.GetFilePart(fileName,index).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("ILR-10000532-1718-20180128-100358-02.zxml", 1)]
        [InlineData("", 2)]
        [InlineData(null,1)]
        public void GetFilePart_InValidFileName(string fileName, int index)
        {
            var service = new IlrFileNameQueryService();
            service.GetFilePart(fileName, index).Should().Be(null);
        }


        [Theory]
        [InlineData("ILR-10000532-1718-20180128-100358-10.XYZ")]
        [InlineData("ILR-10000532-1718-12345678-100358-10.xml")]
        [InlineData("ILR-10000532-1718-20180128-999999-10.zip")]
        public void GetFileDateTime_InvalidFileName(String fileName)
        {
            var service = new IlrFileNameQueryService();
            service.GetFileDateTime(fileName).Should().Be(null);
        }

        [Theory]
        [InlineData("ILR-10000532-1718-20180128-100358-02.xml")]
        [InlineData("ILR-10000532-1718-20180128-100358-02.zip")]
        public void GetFileDateTime_ValidFileName(string fileName)
        {
            var service = new IlrFileNameQueryService();
            service.GetFileDateTime(fileName).Should().Be(new DateTime(2018,01,28,10,03,58));
        }


    }
}
