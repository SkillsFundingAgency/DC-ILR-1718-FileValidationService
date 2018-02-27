using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Model;
using DC.ILR.FileValidationService.Rules.CrossRecordRules.Learner;
using DC.ILR.FileValidationService.Rules.Query;
using ESFA.DC.ILR.Tests.Model;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace DC.ILR.FileValidationService.Rules.Tests.CrossRecordRules.Learner
{
    public class Learner_R06RuleTests
    {
        public LearnerR06FileRule NewRule(IValidationFileErrorHandler validationFileErrorHandler = null)
        {
            return new LearnerR06FileRule(validationFileErrorHandler);
        }

        [Fact]
        public void ConditionMet_True_OneDuplicate()
        {
            var learners = new List<TestLearner>()
            {
                new TestLearner(){LearnRefNumber = "Ref1"},
                new TestLearner(){LearnRefNumber = "Ref1"},
                new TestLearner(){LearnRefNumber = "Ref999"}
            };
            var rule = NewRule(null);
            rule.ConditionMet(learners).Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_True_MoreThanOneDuplicate()
        {
            var learners = new List<TestLearner>()
            {
                new TestLearner(){LearnRefNumber = "Ref1"},
                new TestLearner(){LearnRefNumber = "Ref1"},
                new TestLearner(){LearnRefNumber = "Ref2"},
                new TestLearner(){LearnRefNumber = "Ref2"},
                new TestLearner(){LearnRefNumber = "Ref999"}
            };
            var rule = NewRule(null);
            rule.ConditionMet(learners).Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_False_Null()
        {
            var rule = NewRule(null);
            rule.ConditionMet(null).Should().BeFalse();
        }

        [Fact]
        public void ConditionMet_False()
        {
            var learners = new List<TestLearner>()
            {
                new TestLearner()
                {
                    LearnRefNumber = "Ref1"
                },
                new TestLearner()
                {
                    LearnRefNumber = "Ref2"
                }
            };
            var rule = NewRule(null);
            rule.ConditionMet(learners).Should().BeFalse();
        }

        [Fact]
        public void Validate_True()
        {
            var ilrData = new IlrFileData()
            {
                Message = new TestMessage()
                {
                    Learners = new List<TestLearner>()
                    {
                        new TestLearner()
                        {
                            LearnRefNumber = "Ref1"
                        },
                        new TestLearner()
                        {
                            LearnRefNumber = "Ref2"
                        }
                    }
                }
            };
            var validationErrorHandlerMock = new Mock<IValidationFileErrorHandler>();
            Expression<Action<IValidationFileErrorHandler>> handle = veh => veh.Handle("Learner_R06", null, null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);

            rule.Validate(ilrData);
            validationErrorHandlerMock.Verify(handle, Times.Never);
        }

        [Fact]
        public void Validate_False()
        {
            var ilrData = new IlrFileData()
            {
                Message = new TestMessage()
                {
                    Learners = new List<TestLearner>()
                    {
                        new TestLearner()
                        {
                            LearnRefNumber = "Ref1"
                        },
                        new TestLearner()
                        {
                            LearnRefNumber = "Ref1"
                        }
                    }
                }
            };
            var validationErrorHandlerMock = new Mock<IValidationFileErrorHandler>();
            Expression<Action<IValidationFileErrorHandler>> handle = veh => veh.Handle("Learner_R06", null, null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);

            rule.Validate(ilrData);
            validationErrorHandlerMock.Verify(handle, Times.Once);
        }
    }
}