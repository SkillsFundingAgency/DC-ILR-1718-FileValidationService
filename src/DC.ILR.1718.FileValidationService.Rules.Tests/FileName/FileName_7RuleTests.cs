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
    public class FileName_7RuleTests
    {
        public FileName7FileRule NewRule(IValidationFileErrorHandler validationFileErrorHandler = null, IIlrFileNameQueryService fileNameQueryService = null)
        {
            return new FileName7FileRule(validationFileErrorHandler, fileNameQueryService);
        }

        [Fact]
        public void ConditionMet_True()
        {
            var service = new Mock<IIlrFileNameQueryService>();
            service.Setup(x => x.GetSerialNumber("ILR-10000532-1718-20180128-100358-00.zip")).Returns("00");
            var rule = NewRule(null, service.Object);
            rule.ConditionMet("ILR-10000532-1718-20180128-100358-00.zip").Should().BeTrue();
        }

        [Theory]
        [InlineData("ILR-10000532-1718-20180128-100358-01.xml")]
        [InlineData("ILR-10000532-1718-20180128-100358-01.zip")]
        public void ConditionMet_False(string fileName)
        {
            var service = new Mock<IIlrFileNameQueryService>();
            service.Setup(x => x.GetSerialNumber(fileName)).Returns("01");
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

            var validationErrorHandlerMock = new Mock<IValidationFileErrorHandler>();
            Expression<Action<IValidationFileErrorHandler>> handle = veh => veh.Handle("Filename_7", null, null);
            validationErrorHandlerMock.Setup(handle);

            var ilrFileNameQueryService = new Mock<IIlrFileNameQueryService>();
            ilrFileNameQueryService.Setup(x => x.GetSerialNumber(It.IsAny<string>())).Returns("10");

            var rule = NewRule(validationErrorHandlerMock.Object, ilrFileNameQueryService.Object);

            rule.Validate(ilrFile);
            validationErrorHandlerMock.Verify(handle, Times.Never);
        }

        [Fact]
        public void Validate_False()
        {
            var ilrFile = new IlrFileData
            {
                FileName = "ILR-10000532-1617-20180128-100358-00.xml"
            };

            var validationErrorHandlerMock = new Mock<IValidationFileErrorHandler>();
            Expression<Action<IValidationFileErrorHandler>> handle = veh => veh.Handle("Filename_7", ilrFile.FileName, null);
            validationErrorHandlerMock.Setup(handle);

            var ilrFileNameQueryService = new Mock<IIlrFileNameQueryService>();
            ilrFileNameQueryService.Setup(x => x.GetSerialNumber(It.IsAny<string>())).Returns("00");

            var rule = NewRule(validationErrorHandlerMock.Object, ilrFileNameQueryService.Object);

            rule.Validate(ilrFile);
            validationErrorHandlerMock.Verify(handle, Times.Once);
        }
    }
}