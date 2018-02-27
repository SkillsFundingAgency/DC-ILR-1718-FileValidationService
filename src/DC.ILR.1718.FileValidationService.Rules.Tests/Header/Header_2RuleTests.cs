using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Model;
using DC.ILR.FileValidationService.Rules.Header;
using ESFA.DC.ILR.Tests.Model;
using FluentAssertions;
using Moq;
using System;
using System.Linq.Expressions;
using Xunit;

namespace DC.ILR.FileValidationService.Rules.Tests.Header
{
    public class Header_2RuleTests
    {
        public Header2FileRule NewRule(IValidationFileErrorHandler validationFileErrorHandler = null)
        {
            return new Header2FileRule(validationFileErrorHandler);
        }

        [Fact]
        public void ConditionMet_True_Null()
        {
            var rule = NewRule();
            rule.ConditionMet(null).Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_True()
        {
            var rule = NewRule();
            var collectionDetails = new TestCollectionDetails()
            {
                FilePreparationDate = DateTime.Now.AddDays(1)
            };
            rule.ConditionMet(collectionDetails).Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_False_Today()
        {
            var rule = NewRule();
            var collectionDetails = new TestCollectionDetails()
            {
                FilePreparationDate = DateTime.Now
            };
            rule.ConditionMet(collectionDetails).Should().BeFalse();
        }

        [Fact]
        public void ConditionMet_False_Past()
        {
            var rule = NewRule();
            var collectionDetails = new TestCollectionDetails()
            {
                FilePreparationDate = DateTime.Now.AddDays(-1)
            };
            rule.ConditionMet(collectionDetails).Should().BeFalse();
        }

        [Fact]
        public void Validate_True()
        {
            var validationErrorHandlerMock = new Mock<IValidationFileErrorHandler>();
            Expression<Action<IValidationFileErrorHandler>> handle = veh => veh.Handle("Header_2", null, null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);
            var ilrFileData = new IlrFileData()
            {
                Message = new TestMessage()
                {
                    HeaderEntity = new TestHeader()
                    {
                        CollectionDetailsEntity = new TestCollectionDetails()
                        {
                            FilePreparationDate = DateTime.Now
                        }
                    }
                }
            };

            rule.Validate(ilrFileData);
            validationErrorHandlerMock.Verify(handle, Times.Never);
        }

        [Fact]
        public void Validate_False()
        {
            var validationErrorHandlerMock = new Mock<IValidationFileErrorHandler>();
            Expression<Action<IValidationFileErrorHandler>> handle = veh => veh.Handle("Header_2", null, null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);
            var ilrFileData = new IlrFileData()
            {
                Message = new TestMessage()
                {
                    HeaderEntity = new TestHeader()
                    {
                        CollectionDetailsEntity = new TestCollectionDetails()
                        {
                            FilePreparationDate = DateTime.Now.AddDays(1)
                        }
                    }
                }
            };

            rule.Validate(ilrFileData);
            validationErrorHandlerMock.Verify(handle, Times.Once);
        }
    }
}