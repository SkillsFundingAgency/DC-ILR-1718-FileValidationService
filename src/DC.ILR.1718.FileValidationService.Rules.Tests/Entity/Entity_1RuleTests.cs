using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Rules.Entity;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.ILR.Tests.Model;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace DC.ILR.FileValidationService.Rules.Tests.Entity
{
    public class Entity_1RuleTests
    {
        public Entity1FileRule NewRule(IValidationFileErrorHandler validationFileErrorHandler = null)
        {
            return new Entity1FileRule(validationFileErrorHandler);
        }

        [Fact]
        public void ConditionMet_True_Null()
        {
            var rule = NewRule();
            rule.ConditionMet(null, null).Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_True_Empty()
        {
            var rule = NewRule();
            rule.ConditionMet(new List<ILearner>(), new List<ILearnerDestinationAndProgression>()).Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_False_ValidLearnersList()
        {
            var rule = NewRule();
            var learners = new List<ILearner>()
            {
                new TestLearner()
            };

            rule.ConditionMet(learners, null).Should().BeFalse();
        }

        [Fact]
        public void ConditionMet_False_ValidLearnerDestinationAndProgressionsList()
        {
            var rule = NewRule();

            var learnerDestinationAndProgressions = new List<ILearnerDestinationAndProgression>()
            {
                new TestLearnerDestinationAndProgression()
            };

            rule.ConditionMet(null, learnerDestinationAndProgressions).Should().BeFalse();
        }

        [Fact]
        public void Validate_True()
        {
            var validationErrorHandlerMock = new Mock<IValidationFileErrorHandler>();
            Expression<Action<IValidationFileErrorHandler>> handle = veh => veh.Handle("Entity_1", null, null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);
            var message = new TestMessage()
            {
                LearnerDestinationAndProgressions = new List<ILearnerDestinationAndProgression>()
                {
                    new TestLearnerDestinationAndProgression()
                }
            };

            rule.Validate(message);
            validationErrorHandlerMock.Verify(handle, Times.Never);
        }

        [Fact]
        public void Validate_False()
        {
            var validationErrorHandlerMock = new Mock<IValidationFileErrorHandler>();
            Expression<Action<IValidationFileErrorHandler>> handle = veh => veh.Handle("Entity_1", null, null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);
            var message = new TestMessage()
            {
            };

            rule.Validate(message);
            validationErrorHandlerMock.Verify(handle, Times.Once);
        }
    }
}