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
    public class FileName_8RuleTests
    {
        public FileName_8Rule NewRule(IValidationErrorHandler validationErrorHandler = null, IIlrFileNameQueryService fileNameQueryService = null)
        {
            return new FileName_8Rule(validationErrorHandler, fileNameQueryService);
        }

        [Fact]
        public void ConditionMet_True_Null()
        {
            var service = new Mock<IIlrFileNameQueryService>();
            service.Setup(x => x.GetSerialNumber(It.IsAny<string>())).Returns((string)null);
            var rule = NewRule(null, service.Object);
            rule.ConditionMet("ILR-10000532-1718-20180128-100358-01.zip").Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_True()
        {
            var service = new Mock<IIlrFileNameQueryService>();
            service.Setup(x => x.GetFileDateTime(It.IsAny<string>())).Returns(DateTime.Now.AddMinutes(1));
            var rule = NewRule(null, service.Object);
            rule.ConditionMet(It.IsAny<string>()).Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_False_Now()
        {
            var service = new Mock<IIlrFileNameQueryService>();
            service.Setup(x => x.GetFileDateTime(It.IsAny<string>())).Returns(DateTime.Now);
            var rule = NewRule(null, service.Object);
            rule.ConditionMet(It.IsAny<string>()).Should().BeFalse();
        }

        [Fact]
        public void ConditionMet_False_MinuteBefore()
        {
            var service = new Mock<IIlrFileNameQueryService>();
            service.Setup(x => x.GetFileDateTime(It.IsAny<string>())).Returns(DateTime.Now.AddMinutes(-1));
            var rule = NewRule(null, service.Object);
            rule.ConditionMet(It.IsAny<string>()).Should().BeFalse();
        }

        [Fact]
        public void Validate_True()
        {
            var ilrFile = new IlrFileData
            {
                FileName = $"ILR-10000532-1718-{DateTime.Now.ToString("yyyyMMdd", null)}-{DateTime.Now.ToString("HHmmss", null)}-10.xml"
            };

            var validationErrorHandlerMock = new Mock<IValidationErrorHandler>();
            Expression<Action<IValidationErrorHandler>> handle = veh => veh.Handle("Filename_8", null, null);
            validationErrorHandlerMock.Setup(handle);

            var ilrFileNameQueryService = new Mock<IIlrFileNameQueryService>();
            ilrFileNameQueryService.Setup(x => x.GetFileDateTime(It.IsAny<string>())).Returns(DateTime.Now);

            var rule = NewRule(validationErrorHandlerMock.Object, ilrFileNameQueryService.Object);

            rule.Validate(ilrFile);
            validationErrorHandlerMock.Verify(handle, Times.Never);
        }

        [Fact]
        public void Validate_False()
        {
            var ilrFile = new IlrFileData
            {
                FileName = It.IsAny<string>()
            };

            var validationErrorHandlerMock = new Mock<IValidationErrorHandler>();
            Expression<Action<IValidationErrorHandler>> handle = veh => veh.Handle("Filename_8", null, null);
            validationErrorHandlerMock.Setup(handle);

            var ilrFileNameQueryService = new Mock<IIlrFileNameQueryService>();
            ilrFileNameQueryService.Setup(x => x.GetFileDateTime(It.IsAny<string>())).Returns(DateTime.Now.AddMinutes(1));

            var rule = NewRule(validationErrorHandlerMock.Object, ilrFileNameQueryService.Object);

            rule.Validate(ilrFile);
            validationErrorHandlerMock.Verify(handle, Times.Once);
        }
    }
}