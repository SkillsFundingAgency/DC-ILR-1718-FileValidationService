using System;
using System.Collections.Generic;
using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Rules.AbstractRules;
using ESFA.DC.ILR.Model.Interface;
using System.Linq;
using System.Linq.Expressions;
using DC.ILR.FileValidationService.Rules.CrossRecordRules.LearnerEmploymentStatus;
using DC.ILR.FileValidationService.Rules.CrossRecordRules.LearningDelivery;
using ESFA.DC.ILR.Tests.Model;
using FluentAssertions;
using Moq;
using Xunit;

namespace DC.ILR.FileValidationService.Rules.Tests.CrossRecordRules.LearnerEmploymentStatus
{
    public class LearnerEmploymentStatus_R43RuleTests
    {
        public LearnerEmploymentStatus_R43Rule NewRule(IValidationCrossRecordErrorHandler validationCrossRecordErrorHandler = null)
        {
            return new LearnerEmploymentStatus_R43Rule(validationCrossRecordErrorHandler);
        }

        [Fact]
        public void ConditionMet_True()
        {
            var learner = new TestLearner()
            {
                LearnerEmploymentStatuses = new List<ILearnerEmploymentStatus>()
                {
                    new TestLearnerEmploymentStatus() {DateEmpStatAppNullable = new DateTime(2017,10,10)},
                    new TestLearnerEmploymentStatus() {DateEmpStatAppNullable = new DateTime(2017,10,10)}
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
        public void ConditionMet_False_LearnerEmploymentStatusesNull()
        {
            var rule = NewRule(null);
            var learner = new TestLearner();
            rule.ConditionMet(learner).Should().BeFalse();
        }

        [Fact]
        public void ConditionMet_False_AllNullDates()
        {
            var learner = new TestLearner()
            {
                LearnerEmploymentStatuses = new List<ILearnerEmploymentStatus>()
                {
                    new TestLearnerEmploymentStatus() {DateEmpStatAppNullable = null},
                    new TestLearnerEmploymentStatus() {DateEmpStatAppNullable = null}
                }
            };

            var rule = NewRule(null);
            rule.ConditionMet(learner).Should().BeFalse();
        }

        [Fact]
        public void ConditionMet_False()
        {
            var learner = new TestLearner()
            {
                LearnerEmploymentStatuses = new List<ILearnerEmploymentStatus>()
                {
                    new TestLearnerEmploymentStatus() {DateEmpStatAppNullable = new DateTime(2017,10,10)},
                    new TestLearnerEmploymentStatus() {DateEmpStatAppNullable = new DateTime(2018,12,12)}
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
                LearnerEmploymentStatuses = new List<ILearnerEmploymentStatus>()
                {
                    new TestLearnerEmploymentStatus() {DateEmpStatAppNullable = new DateTime(2017,10,10)},
                    new TestLearnerEmploymentStatus() {DateEmpStatAppNullable = new DateTime(2018,12,12)}
                }
            };
            var validationErrorHandlerMock = new Mock<IValidationCrossRecordErrorHandler>();
            Expression<Action<IValidationCrossRecordErrorHandler>> handle = veh => veh.Handle("LearnerEmploymentStatus_R43", null, null, null);
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
                LearnerEmploymentStatuses = new List<ILearnerEmploymentStatus>()
                {
                    new TestLearnerEmploymentStatus() {DateEmpStatAppNullable = new DateTime(2017,10,10)},
                    new TestLearnerEmploymentStatus() {DateEmpStatAppNullable = new DateTime(2017,10,10)}
                }
            };
            var validationErrorHandlerMock = new Mock<IValidationCrossRecordErrorHandler>();
            Expression<Action<IValidationCrossRecordErrorHandler>> handle = veh => veh.Handle("LearnerEmploymentStatus_R43", null, null, null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);
            rule.Validate(learner);
            validationErrorHandlerMock.Verify(handle, Times.Once);
        }
    }
}