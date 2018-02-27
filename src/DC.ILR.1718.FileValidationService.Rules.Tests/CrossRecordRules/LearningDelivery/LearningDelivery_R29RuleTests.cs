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
    public class LearningDelivery_R29RuleTests
    {
        public LearningDelivery_R29Rule NewRule(IValidationCrossRecordErrorHandler validationCrossRecordErrorHandler = null)
        {
            return new LearningDelivery_R29Rule(validationCrossRecordErrorHandler);
        }

        [Theory]
        [InlineData(null, null, null, 10)]
        [InlineData(null, null, 10, null)]
        [InlineData(null, 10, null, null)]
        [InlineData(10, null, null, null)]
        [InlineData(10, 20, 30, 40)]
        [InlineData(null, null, null, null)]
        [InlineData(10, 10, null, 10)]
        [InlineData(null, null, 10, 10)]
        [InlineData(10, 10, null, null)]
        public void ConditionMet_True_ClosedMainAim(long? frameworkCode, long? pathwayCode, long? progType, long? standardCode)
        {
            var componentAimLearningDelivery = new TestLearningDelivery()
            {
                AimSeqNumberNullable = 1,
                AimTypeNullable = 3,
                FworkCodeNullable = frameworkCode,
                ProgTypeNullable = progType,
                StdCodeNullable = standardCode,
                PwayCodeNullable = pathwayCode
            };

            var mainAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 1,
                    FworkCodeNullable = frameworkCode,
                    ProgTypeNullable = progType,
                    StdCodeNullable = standardCode,
                    PwayCodeNullable = pathwayCode,
                    LearnActEndDateNullable = new DateTime(2017,10,10)
                }
            };

            var rule = NewRule(null);
            rule.ConditionMet(componentAimLearningDelivery, mainAimLearningDeliveries).Should().BeTrue();
        }

        [Theory]
        [InlineData(null, 10)]
        [InlineData(10, null)]
        [InlineData(10, 20)]
        public void ConditionMet_True_OpenMainAim_NotMatchingStandard(long? componentStandardCode, long? mainAimStandardCode)
        {
            var componentAimLearningDelivery = new TestLearningDelivery()
            {
                AimSeqNumberNullable = 1,
                AimTypeNullable = 3,
                StdCodeNullable = componentStandardCode
            };

            var mainAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 1,
                    StdCodeNullable = mainAimStandardCode
                }
            };

            var rule = NewRule(null);
            rule.ConditionMet(componentAimLearningDelivery, mainAimLearningDeliveries).Should().BeTrue();
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
                AimSeqNumberNullable = 1,
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
                StdCodeNullable = 20
            };

            var mainAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 2,
                    StdCodeNullable = 20
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
                AimSeqNumberNullable = 1,
                AimTypeNullable = 3,
                StdCodeNullable = 20
            };

            var mainAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 1,
                    StdCodeNullable = 20
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
                    StdCodeNullable = 20
                },
                new TestLearningDelivery()
                {
                    AimTypeNullable = 3,
                    StdCodeNullable = 120
                },
                new TestLearningDelivery()
                {
                    AimTypeNullable = 3,
                    StdCodeNullable = 200,
                    LearnActEndDateNullable = new DateTime(2018,10,10)
                }
            };

            var rule = NewRule(null);
            rule.GetOpenComponentAims(componentAimLearningDeliveries).Count().Should().Be(2);
        }

        [Fact]
        public void GetOpenComponentAims_NoneOpen()
        {
            var componentAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 1,
                    StdCodeNullable = 20
                },
                new TestLearningDelivery()
                {
                    AimTypeNullable = 3,
                    StdCodeNullable = 120,
                    LearnActEndDateNullable = new DateTime(2018,10,10)
                },
                new TestLearningDelivery()
                {
                    AimTypeNullable = 1,
                    StdCodeNullable = 200,
                    LearnActEndDateNullable = new DateTime(2018,10,10)
                }
            };

            var rule = NewRule(null);
            rule.GetOpenComponentAims(componentAimLearningDeliveries).Count().Should().Be(0);
        }

        [Fact]
        public void Validate_True()
        {
            var learner = new TestLearner()
            {
                LearnRefNumber = "Ref1",
                LearningDeliveries = new List<ILearningDelivery>()
                {
                    new TestLearningDelivery() { AimTypeNullable = 1, StdCodeNullable = 120},
                    new TestLearningDelivery() {  AimTypeNullable = 3, StdCodeNullable = 120},
                }
            };
            var validationErrorHandlerMock = new Mock<IValidationCrossRecordErrorHandler>();
            Expression<Action<IValidationCrossRecordErrorHandler>> handle = veh => veh.Handle("LearningDelivery_R29", null, null, null);
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
                    new TestLearningDelivery() { AimTypeNullable = 3, StdCodeNullable = 120},
                    new TestLearningDelivery() {  AimTypeNullable = 1, StdCodeNullable = 120, LearnActEndDateNullable = new DateTime(2018,10,10)},
                }
            };
            var validationErrorHandlerMock = new Mock<IValidationCrossRecordErrorHandler>();
            Expression<Action<IValidationCrossRecordErrorHandler>> handle = veh => veh.Handle("LearningDelivery_R29", null, null, null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);
            rule.Validate(learner);
            validationErrorHandlerMock.Verify(handle, Times.Once);
        }
    }
}