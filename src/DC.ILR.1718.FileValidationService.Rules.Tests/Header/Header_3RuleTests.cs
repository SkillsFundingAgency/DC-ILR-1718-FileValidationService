using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Model;
using DC.ILR.FileValidationService.Rules.Header;
using ESFA.DC.ILR.Tests.Model;
using FluentAssertions;
using Moq;
using System;
using System.Linq.Expressions;
using DC.ILR.FileValidationService.Rules.Query;
using Xunit;

namespace DC.ILR.FileValidationService.Rules.Tests.Header
{
    public class Header_3RuleTests
    {
        public Header3FileRule NewRule(IValidationFileErrorHandler validationFileErrorHandler = null, IIlrFileNameQueryService fileNameQueryService = null)
        {
            return new Header3FileRule(validationFileErrorHandler,fileNameQueryService);
        }

        [Fact]
        public void ConditionMet_True()
        {
            var service = new Mock<IIlrFileNameQueryService>();
            service.Setup(x => x.GetUkprn("ILR-10000532-1718-20180128-100358-10.xml")).Returns(10000532);
            var rule = NewRule(null, service.Object);
            rule.ConditionMet("ILR-10000532-1718-20180128-100358-10.xml",11111111).Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_False()
        {
            var service = new Mock<IIlrFileNameQueryService>();
            service.Setup(x => x.GetUkprn("ILR-10000532-1718-20180128-100358-10.xml")).Returns(10000532);
            var rule = NewRule(null, service.Object);
            rule.ConditionMet("ILR-10000532-1718-20180128-100358-10.xml", 10000532).Should().BeFalse();
        }

      

        [Fact]
        public void Validate_True()
        {
            var validationErrorHandlerMock = new Mock<IValidationFileErrorHandler>();
            Expression<Action<IValidationFileErrorHandler>> handle = veh => veh.Handle("Header_3", null, null);
            validationErrorHandlerMock.Setup(handle);

            var fileQueryServiceMock = new Mock<IIlrFileNameQueryService>();
            fileQueryServiceMock.Setup(x => x.GetUkprn(It.IsAny<string>())).Returns(10000532);

            var rule = NewRule(validationErrorHandlerMock.Object, fileQueryServiceMock.Object);
            var ilrFileData = SetupIlrFileDataObject(10000532);

            rule.Validate(ilrFileData);
            validationErrorHandlerMock.Verify(handle, Times.Never);
        }



        [Fact]
        public void Validate_False()
        {
            var validationErrorHandlerMock = new Mock<IValidationFileErrorHandler>();
            Expression<Action<IValidationFileErrorHandler>> handle = veh => veh.Handle("Header_3", "ILR-10000532-1718-20180128-100358-10.xml", null);
            validationErrorHandlerMock.Setup(handle);

            var fileQueryServiceMock = new Mock<IIlrFileNameQueryService>();
            fileQueryServiceMock.Setup(x => x.GetUkprn(It.IsAny<string>())).Returns(10000532);

            var rule = NewRule(validationErrorHandlerMock.Object, fileQueryServiceMock.Object);
            var ilrFileData = SetupIlrFileDataObject(999999);

            rule.Validate(ilrFileData);
            validationErrorHandlerMock.Verify(handle, Times.Once);
        }

        private IlrFileData SetupIlrFileDataObject(long ukprn)
        {
            var ilrFileData = new IlrFileData()
            {
                FileName = "ILR-10000532-1718-20180128-100358-10.xml",
                Message = new TestMessage()
                {
                    HeaderEntity = new TestHeader()
                    {
                        SourceEntity = new TestSource()
                        {
                            UKPRN = (int)ukprn
                        }
                    }
                }
            };
            return ilrFileData;
        }
    }
}