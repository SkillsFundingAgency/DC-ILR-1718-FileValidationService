using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Rules.Namespace;
using FluentAssertions;
using Moq;
using System;
using System.Linq.Expressions;
using Xunit;

namespace DC.ILR.FileValidationService.Rules.Tests.Namespace
{
    public class NamespaceMismatch_RuleTests
    {
        public NamespaceMismatch_Rule NewRule(IValidationErrorHandler validationErrorHandler = null)
        {
            return new NamespaceMismatch_Rule(validationErrorHandler);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(@"xmlns=""SFA/ILR/2016-17""")]
        [InlineData(@"xmlns=""SFA/ILR/2016-17"" <Header> ")]
        public void ConditionMet_True(string fileContent)
        {
            var rule = NewRule();
            rule.ConditionMet(fileContent).Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_False()
        {
            var rule = NewRule();
            var content = @"xmlns=""SFA/ILR/2017-18""";
            rule.ConditionMet(content).Should().BeFalse();
        }

        [Fact]
        public void Validate_True()
        {
            var validationErrorHandlerMock = new Mock<IValidationErrorHandler>();
            Expression<Action<IValidationErrorHandler>> handle = veh => veh.Handle("NamespaceMismatch", null, null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);
            var content = @"xmlns=""SFA/ILR/2017-18""";
            rule.Validate(content);
            validationErrorHandlerMock.Verify(handle, Times.Never);
        }

        [Fact]
        public void Validate_False()
        {
            var validationErrorHandlerMock = new Mock<IValidationErrorHandler>();
            Expression<Action<IValidationErrorHandler>> handle = veh => veh.Handle("NamespaceMismatch", null, null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);
            var content = @"xmlns=""SFA/ILR/2016-17""";
            rule.Validate(content);

            validationErrorHandlerMock.Verify(handle, Times.Once);
        }
    }
}