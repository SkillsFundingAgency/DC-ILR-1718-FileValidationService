using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Model;
using DC.ILR.FileValidationService.Rules.FileName;
using DC.ILR.FileValidationService.Rules.Query;
using FluentAssertions;
using Moq;
using System;
using System.Linq.Expressions;
using Xunit;

namespace DC.ILR.FileValidationService.Rules.Tests.FileName
{
    public class FileName_5RuleTests
    {
        public FileName_5Rule NewRule(IValidationErrorHandler validationErrorHandler = null, IIlrFileNameQueryService fileNameQueryService = null)
        {
            return new FileName_5Rule(validationErrorHandler, fileNameQueryService);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("ILR-10000532-1718-20180128-100358-10.XYZ")]
        [InlineData("ILR-10000532-1617-20180128-100358-10.xml")]
        [InlineData("ILR-10000532-0000-20180128-100358-10.xml")]
        public void ConditionMet_True(string fileName)
        {
            var service = new Mock<IIlrFileNameQueryService>();
            service.Setup(x => x.GetYearOfCollection(fileName)).Returns((string)null);
            var rule = NewRule(null, service.Object);
            rule.ConditionMet(fileName).Should().BeTrue();
        }

        [Theory]
        [InlineData("ILR-10000532-1718-20180128-100358-10.xml")]
        [InlineData("ILR-10000532-1718-20180128-100358-10.zip")]
        public void ConditionMet_False(string fileName)
        {
            var service = new Mock<IIlrFileNameQueryService>();
            service.Setup(x => x.GetYearOfCollection(fileName)).Returns("1718");
            var rule = NewRule(null, service.Object);
            rule.ConditionMet(fileName).Should().BeFalse();
        }

        [Fact]
        public void Validate_True()
        {
            var ilrFile = new IlrFileData
            {
                FileName = "ILR-10000532-1718-20180128-100358-10.xml"
            };

            var validationErrorHandlerMock = new Mock<IValidationErrorHandler>();
            Expression<Action<IValidationErrorHandler>> handle = veh => veh.Handle("Filename_5", null, null);
            validationErrorHandlerMock.Setup(handle);

            var ilrFileNameQueryService = new Mock<IIlrFileNameQueryService>();
            ilrFileNameQueryService.Setup(x => x.GetYearOfCollection(It.IsAny<string>())).Returns("1718");

            var rule = NewRule(validationErrorHandlerMock.Object, ilrFileNameQueryService.Object);

            rule.Validate(ilrFile);
            validationErrorHandlerMock.Verify(handle, Times.Never);
        }

        [Fact]
        public void Validate_False()
        {
            var ilrFile = new IlrFileData
            {
                FileName = "ILR-10000532-1617-20180128-100358-10.xml"
            };

            var validationErrorHandlerMock = new Mock<IValidationErrorHandler>();
            Expression<Action<IValidationErrorHandler>> handle = veh => veh.Handle("Filename_5", ilrFile.FileName, null);
            validationErrorHandlerMock.Setup(handle);

            var ilrFileNameQueryService = new Mock<IIlrFileNameQueryService>();
            ilrFileNameQueryService.Setup(x => x.GetYearOfCollection(It.IsAny<string>())).Returns("1617");

            var rule = NewRule(validationErrorHandlerMock.Object, ilrFileNameQueryService.Object);

            rule.Validate(ilrFile);
            validationErrorHandlerMock.Verify(handle, Times.Once);
        }
    }
}