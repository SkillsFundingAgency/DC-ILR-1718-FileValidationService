using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Rules.CrossRecordRules.LearningDelivery;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.ILR.Tests.Model;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace DC.ILR.FileValidationService.Rules.Tests.CrossRecordRules.LearningDelivery
{
    public class LearningDelivery_R30RuleTests
    {
        public LearningDelivery_R30Rule NewRule(IValidationCrossRecordErrorHandler validationCrossRecordErrorHandler = null)
        {
            return new LearningDelivery_R30Rule(validationCrossRecordErrorHandler);
        }

        [Theory]
        [InlineData(null, 10)]
        [InlineData(10, null)]
        [InlineData(10, 20)]
        public void ConditionMet_True_OpenMainAim_NotMatchingFramework(long? componentValue, long? mainAimValue)
        {
            var componentAimLearningDelivery = new TestLearningDelivery()
            {
                AimSeqNumberNullable = 1,
                AimTypeNullable = 3,
                FworkCodeNullable = componentValue
            };

            var mainAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 1,
                    FworkCodeNullable = mainAimValue
                }
            };

            var rule = NewRule(null);
            rule.ConditionMet(componentAimLearningDelivery, mainAimLearningDeliveries).Should().BeTrue();
        }

        [Theory]
        [InlineData(null, 10)]
        [InlineData(10, null)]
        [InlineData(10, 20)]
        public void ConditionMet_True_OpenMainAim_NotMatchingPathway(long? componentValue, long? mainAimValue)
        {
            var componentAimLearningDelivery = new TestLearningDelivery()
            {
                AimTypeNullable = 3,
                PwayCodeNullable = componentValue
            };

            var mainAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 1,
                    PwayCodeNullable = mainAimValue
                }
            };

            var rule = NewRule(null);
            rule.ConditionMet(componentAimLearningDelivery, mainAimLearningDeliveries).Should().BeTrue();
        }

        [Theory]
        [InlineData(null, 10)]
        [InlineData(10, null)]
        [InlineData(10, 20)]
        public void ConditionMet_True_OpenMainAim_NotMatchingProgType(long? componentValue, long? mainAimValue)
        {
            var componentAimLearningDelivery = new TestLearningDelivery()
            {
                AimSeqNumberNullable = 1,
                AimTypeNullable = 3,
                ProgTypeNullable = componentValue
            };

            var mainAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 1,
                    ProgTypeNullable = mainAimValue
                }
            };

            var rule = NewRule(null);
            rule.ConditionMet(componentAimLearningDelivery, mainAimLearningDeliveries).Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_True_NoMainAimComponent()
        {
            var componentAimLearningDelivery = new TestLearningDelivery()
            {
                AimSeqNumberNullable = 1,
                AimTypeNullable = 3,
                FworkCodeNullable = 20
            };

            var mainAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 2,
                    FworkCodeNullable = 20
                }
            };

            var rule = NewRule(null);
            rule.ConditionMet(componentAimLearningDelivery, mainAimLearningDeliveries).Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_False_MatchingAims()
        {
            var componentAimLearningDelivery = new TestLearningDelivery()
            {
                AimTypeNullable = 3,
                FworkCodeNullable = 20
            };

            var mainAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 1,
                    FworkCodeNullable = 20
                }
            };

            var rule = NewRule(null);
            rule.ConditionMet(componentAimLearningDelivery, mainAimLearningDeliveries).Should().BeFalse();
        }

        [Fact]
        public void GetOpenComponentAims_ReturnValues()
        {
            var componentAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 3,
                    FworkCodeNullable = 20
                },
                new TestLearningDelivery()
                {
                    AimTypeNullable = 3,
                    FworkCodeNullable = 120
                },
                new TestLearningDelivery()
                {
                    AimTypeNullable = 1,
                    FworkCodeNullable = 200
                }
            };

            var rule = NewRule(null);
            rule.GetComponentAims(componentAimLearningDeliveries).Count().Should().Be(2);
        }

        [Fact]
        public void GetOpenComponentAims_NoneAvailable()
        {
            var componentAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 1,
                    FworkCodeNullable = 120
                },
                new TestLearningDelivery()
                {
                    AimTypeNullable = 2,
                    StdCodeNullable = 200
                }
            };

            var rule = NewRule(null);
            rule.GetComponentAims(componentAimLearningDeliveries).Count().Should().Be(0);
        }

        [Fact]
        public void Exclude_True()
        {
            var componentAimLearningDelivery = new TestLearningDelivery()
            {
                ProgTypeNullable = 25
            };
            var rule = NewRule(null);
            rule.Exclude(componentAimLearningDelivery).Should().BeTrue();
        }

        [Fact]
        public void Exclude_False_Null()
        {
            var componentAimLearningDelivery = new TestLearningDelivery()
            {
                ProgTypeNullable = null
            };
            var rule = NewRule(null);
            rule.Exclude(componentAimLearningDelivery).Should().BeFalse();
        }

        [Fact]
        public void Exclude_False()
        {
            var componentAimLearningDelivery = new TestLearningDelivery()
            {
                ProgTypeNullable = 100
            };
            var rule = NewRule(null);
            rule.Exclude(componentAimLearningDelivery).Should().BeFalse();
        }

        [Fact]
        public void Validate_True_NoComponentAims()
        {
            var learner = new TestLearner()
            {
                LearnRefNumber = "Ref1",
                LearningDeliveries = new List<ILearningDelivery>()
                {
                    new TestLearningDelivery() { AimTypeNullable = 1, FworkCodeNullable = 120},
                    new TestLearningDelivery() {  AimTypeNullable = 2, FworkCodeNullable= 120},
                }
            };
            var validationErrorHandlerMock = new Mock<IValidationCrossRecordErrorHandler>();
            Expression<Action<IValidationCrossRecordErrorHandler>> handle = veh => veh.Handle("LearningDelivery_R30", null, null, null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);
            rule.Validate(learner);
            validationErrorHandlerMock.Verify(handle, Times.Never);
        }

        [Fact]
        public void Validate_True_NullLearningDeliveries()
        {
            var learner = new TestLearner()
            {
            };
            var validationErrorHandlerMock = new Mock<IValidationCrossRecordErrorHandler>();
            Expression<Action<IValidationCrossRecordErrorHandler>> handle = veh => veh.Handle("LearningDelivery_R30", null, null, null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);
            rule.Validate(learner);
            validationErrorHandlerMock.Verify(handle, Times.Never);
        }

        [Fact]
        public void Validate_True_NullLearner()
        {
            var validationErrorHandlerMock = new Mock<IValidationCrossRecordErrorHandler>();
            Expression<Action<IValidationCrossRecordErrorHandler>> handle = veh => veh.Handle("LearningDelivery_R30", null, null, null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);
            rule.Validate(null);
            validationErrorHandlerMock.Verify(handle, Times.Never);
        }

        [Fact]
        public void Validate_True()
        {
            var learner = new TestLearner()
            {
                LearnRefNumber = "Ref1",
                LearningDeliveries = new List<ILearningDelivery>()
                {
                    new TestLearningDelivery() { AimTypeNullable = 1, FworkCodeNullable = 120},
                    new TestLearningDelivery() {  AimTypeNullable = 3, FworkCodeNullable= 120},
                }
            };
            var validationErrorHandlerMock = new Mock<IValidationCrossRecordErrorHandler>();
            Expression<Action<IValidationCrossRecordErrorHandler>> handle = veh => veh.Handle("LearningDelivery_R30", null, null, null);
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
                LearningDeliveries = new List<ILearningDelivery>()
                {
                    new TestLearningDelivery() { AimTypeNullable = 3, FworkCodeNullable = 100},
                    new TestLearningDelivery() {  AimTypeNullable = 1}
                }
            };
            var validationErrorHandlerMock = new Mock<IValidationCrossRecordErrorHandler>();
            Expression<Action<IValidationCrossRecordErrorHandler>> handle = veh => veh.Handle("LearningDelivery_R30", null, null, null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);
            rule.Validate(learner);
            validationErrorHandlerMock.Verify(handle, Times.Once);
        }
    }
}