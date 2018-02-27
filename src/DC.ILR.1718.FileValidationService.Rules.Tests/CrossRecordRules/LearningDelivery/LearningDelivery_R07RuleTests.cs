using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Model;
using DC.ILR.FileValidationService.Rules.CrossRecordRules.LearningDelivery;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.ILR.Tests.Model;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace DC.ILR.FileValidationService.Rules.Tests.CrossRecordRules.LearningDelivery
{
    public class LearningDelivery_R07RuleTests
    {
        public LearningDelivery_R07Rule NewRule(IValidationCrossRecordErrorHandler validationCrossRecordErrorHandler = null)
        {
            return new LearningDelivery_R07Rule(validationCrossRecordErrorHandler);
        }

        [Fact]
        public void ConditionMet_True()
        {
            var learner = new TestLearner()
            {
                LearnRefNumber = "Ref1",
                LearningDeliveries = new List<ILearningDelivery>()
                {
                    new TestLearningDelivery() {AimSeqNumberNullable = 1},
                    new TestLearningDelivery() {AimSeqNumberNullable = 1}
                }
            };

            var rule = NewRule(null);
            rule.ConditionMet(learner).Should().BeTrue();
        }


        [Fact]
        public void ConditionMet_False_Null()
        {
            var rule = NewRule(null);
            rule.ConditionMet(null).Should().BeFalse();
        }


        [Fact]
        public void ConditionMet_False_LearningDeliveryNull()
        {
            var rule = NewRule(null);
            var learner = new TestLearner();
            rule.ConditionMet(learner).Should().BeFalse();
        }

        [Fact]
        public void ConditionMet_False()
        {

            var learner = new TestLearner()
            {
                LearnRefNumber = "Ref1",
                LearningDeliveries = new List<ILearningDelivery>()
                {
                    new TestLearningDelivery() {AimSeqNumberNullable = 1},
                    new TestLearningDelivery() {AimSeqNumberNullable = 2}
                }
            };
            
            var rule = NewRule(null);
            rule.ConditionMet(learner).Should().BeFalse();
        }

        [Fact]
        public void Validate_True()
        {
            var learner = new TestLearner()
            {
                LearnRefNumber = "Ref1",
                LearningDeliveries = new List<ILearningDelivery>()
                {
                    new TestLearningDelivery() {AimSeqNumberNullable = 1},
                    new TestLearningDelivery() {AimSeqNumberNullable = 2}
                }
            };
            var validationErrorHandlerMock = new Mock<IValidationCrossRecordErrorHandler>();
            Expression<Action<IValidationCrossRecordErrorHandler>> handle = veh => veh.Handle("LearningDelivery_R07", null,null,null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);
            rule.Validate(learner);
            validationErrorHandlerMock.Verify(handle, Times.Never);
        }

        [Fact]
        public void Validate_False()
        {
            var learner = new TestLearner()
            {
                LearnRefNumber = "Ref1",
                LearningDeliveries = new List<ILearningDelivery>()
                {
                    new TestLearningDelivery() {AimSeqNumberNullable = 1},
                    new TestLearningDelivery() {AimSeqNumberNullable = 1}
                }
            };

            var validationErrorHandlerMock = new Mock<IValidationCrossRecordErrorHandler>();
            Expression<Action<IValidationCrossRecordErrorHandler>> handle = veh => veh.Handle("LearningDelivery_R07", It.IsAny<string>(), null, null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);
            rule.Validate(learner);
            validationErrorHandlerMock.Verify(handle, Times.Once);
        }
    }
}