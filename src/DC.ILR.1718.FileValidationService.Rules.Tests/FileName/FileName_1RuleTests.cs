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
    public class FileName_1RuleTests
    {
        public FileName_1Rule NewRule(IValidationErrorHandler validationErrorHandler = null, IIlrFileNameQueryService fileNameQueryService = null)
        {
            return new FileName_1Rule(validationErrorHandler, fileNameQueryService);
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
        public void ConditionMet_True(string fileName)
        {
            var service = new Mock<IIlrFileNameQueryService>();
            service.Setup(x => x.IsValidFileName(fileName)).Returns(false);
            var rule = NewRule(null, service.Object);
            rule.ConditionMet(fileName).Should().BeTrue();
        }

        [Theory]
        [InlineData("ILR-10000532-1718-20180128-100358-10.xml")]
        [InlineData("ILR-10000532-1718-20180128-100358-10.zip")]
        public void ConditionMet_False(string fileName)
        {
            var service = new Mock<IIlrFileNameQueryService>();
            service.Setup(x => x.IsValidFileName(fileName)).Returns(true);
            var rule = NewRule(null, service.Object);
            rule.ConditionMet(fileName).Should().BeFalse();
        }

        [Fact]
        public void Validate_True()
        {
            var ilrFile = new IlrFileData();
            ilrFile.FileName = "ILR-10000532-1718-20180128-100358-10.xml";

            var validationErrorHandlerMock = new Mock<IValidationErrorHandler>();
            Expression<Action<IValidationErrorHandler>> handle = veh => veh.Handle("Filename_1", null, null);
            validationErrorHandlerMock.Setup(handle);

            var ilrFileNameQueryService = new Mock<IIlrFileNameQueryService>();
            ilrFileNameQueryService.Setup(x => x.IsValidFileName(It.IsAny<string>())).Returns(true);

            var rule = NewRule(validationErrorHandlerMock.Object, ilrFileNameQueryService.Object);

            rule.Validate(ilrFile);
            validationErrorHandlerMock.Verify(handle, Times.Never);
        }

        [Fact]
        public void Validate_False()
        {
            var ilrFile = new IlrFileData();
            ilrFile.FileName = "ILR-10000532-1718-20180128-100358-10.XYZ";

            var validationErrorHandlerMock = new Mock<IValidationErrorHandler>();
            Expression<Action<IValidationErrorHandler>> handle = veh => veh.Handle("Filename_1", ilrFile.FileName, null);
            validationErrorHandlerMock.Setup(handle);

            var ilrFileNameQueryService = new Mock<IIlrFileNameQueryService>();
            ilrFileNameQueryService.Setup(x => x.IsValidFileName(It.IsAny<string>())).Returns(false);

            var rule = NewRule(validationErrorHandlerMock.Object, ilrFileNameQueryService.Object);

            rule.Validate(ilrFile);
            validationErrorHandlerMock.Verify(handle, Times.Once);
        }
    }
}