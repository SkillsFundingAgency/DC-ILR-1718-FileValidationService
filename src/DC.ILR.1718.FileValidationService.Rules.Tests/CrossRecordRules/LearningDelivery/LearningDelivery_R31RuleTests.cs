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
    public class LearningDelivery_R31RuleTests
    {
        public LearningDelivery_R31Rule NewRule(IValidationCrossRecordErrorHandler validationCrossRecordErrorHandler = null)
        {
            return new LearningDelivery_R31Rule(validationCrossRecordErrorHandler);
        }

       
        [Theory]
        [InlineData(null, 10)]
        [InlineData(10, null)]
        [InlineData(10, 20)]
        public void ConditionMet_True_OpenMainAim_NotMatchingStandard(long? componentStandardCode, long? mainAimStandardCode)
        {
            var mainAimLearningDelivery = new TestLearningDelivery()
            {
                AimTypeNullable = 1,
                StdCodeNullable = mainAimStandardCode
            };

            var componentAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 3,
                    StdCodeNullable = componentStandardCode
                }
            };

            var rule = NewRule(null);
            rule.ConditionMet(mainAimLearningDelivery, componentAimLearningDeliveries).Should().BeTrue();
        }

        [Theory]
        [InlineData(null, 10)]
        [InlineData(10, null)]
        [InlineData(10, 20)]
        public void ConditionMet_True_OpenMainAim_NotMatchingFramework(long? componentValue, long? mainAimValue)
        {
            var mainAimLearningDelivery = new TestLearningDelivery()
            {
                AimTypeNullable = 1,
                FworkCodeNullable = mainAimValue
            };

            var componentAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 3,
                    FworkCodeNullable = componentValue
                }
            };

            var rule = NewRule(null);
            rule.ConditionMet(mainAimLearningDelivery, componentAimLearningDeliveries).Should().BeTrue();
        }

        [Theory]
        [InlineData(null, 10)]
        [InlineData(10, null)]
        [InlineData(10, 20)]
        public void ConditionMet_True_OpenMainAim_NotMatchingPathway(long? componentValue, long? mainAimValue)
        {
            var mainAimLearningDelivery = new TestLearningDelivery()
            {
                AimTypeNullable = 1,
                PwayCodeNullable = mainAimValue
            };

            var componentAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 3,
                    PwayCodeNullable = componentValue
                }
            };

            var rule = NewRule(null);
            rule.ConditionMet(mainAimLearningDelivery, componentAimLearningDeliveries).Should().BeTrue();
        }

        [Theory]
        [InlineData(null, 10)]
        [InlineData(10, null)]
        [InlineData(10, 20)]
        public void ConditionMet_True_OpenMainAim_NotMatchingProgType(long? componentValue, long? mainAimValue)
        {
            var mainAimLearningDelivery = new TestLearningDelivery()
            {
                AimTypeNullable = 1,
                ProgTypeNullable = mainAimValue
            };

            var componentAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 3,
                    ProgTypeNullable = componentValue
                }
            };

            var rule = NewRule(null);
            rule.ConditionMet(mainAimLearningDelivery, componentAimLearningDeliveries).Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_True_NoComponentAim()
        {
            var mainAimLearningDelivery = new TestLearningDelivery()
            {
                AimTypeNullable = 1,
                StdCodeNullable = 20
            };

            var componentAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 2,
                    StdCodeNullable = 20
                }
            };

            var rule = NewRule(null);
            rule.ConditionMet(mainAimLearningDelivery, componentAimLearningDeliveries).Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_False_MatchingAims()
        {
            var mainAimLearningDelivery = new TestLearningDelivery()
            {
                AimTypeNullable = 1,
                StdCodeNullable = 20
            };

            var componentAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 3,
                    StdCodeNullable = 20
                }
            };

            var rule = NewRule(null);
            rule.ConditionMet(mainAimLearningDelivery, componentAimLearningDeliveries).Should().BeFalse();
        }

        [Fact]
        public void GetOpenMainAims_ReturnValues()
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
                    AimTypeNullable = 1,
                    StdCodeNullable = 120
                },
                new TestLearningDelivery()
                {
                    AimTypeNullable = 1,
                    StdCodeNullable = 200
                }
            };

            var rule = NewRule(null);
            rule.GetMainProgramAims(componentAimLearningDeliveries).Count().Should().Be(3);
        }

        [Fact]
        public void GetOpenComponentAims_NoneOpen()
        {
            var componentAimLearningDeliveries = new List<ILearningDelivery>()
            {
                new TestLearningDelivery()
                {
                    AimTypeNullable = 3
                },
                new TestLearningDelivery()
                {
                    AimTypeNullable = 1,
                    LearnActEndDateNullable = new DateTime(2018,10,10)
                },
                new TestLearningDelivery()
                {
                    AimTypeNullable = 1,
                    LearnActEndDateNullable = new DateTime(2018,10,10)
                }
            };

            var rule = NewRule(null);
            rule.GetMainProgramAims(componentAimLearningDeliveries).Count().Should().Be(0);
        }

        [Fact]
        public void Validate_True_NullLearningDeliveries()
        {
            var learner = new TestLearner()
            {
            };
            var validationErrorHandlerMock = new Mock<IValidationCrossRecordErrorHandler>();
            Expression<Action<IValidationCrossRecordErrorHandler>> handle = veh => veh.Handle("LearningDelivery_R31", null, null, null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);
            rule.Validate(learner);
            validationErrorHandlerMock.Verify(handle, Times.Never);
        }

        [Fact]
        public void Validate_True_NullLearner()
        {
            var validationErrorHandlerMock = new Mock<IValidationCrossRecordErrorHandler>();
            Expression<Action<IValidationCrossRecordErrorHandler>> handle = veh => veh.Handle("LearningDelivery_R31", null, null, null);
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
                    new TestLearningDelivery() { AimTypeNullable = 1, StdCodeNullable = 120},
                    new TestLearningDelivery() {  AimTypeNullable = 3, StdCodeNullable = 120},
                }
            };
            var validationErrorHandlerMock = new Mock<IValidationCrossRecordErrorHandler>();
            Expression<Action<IValidationCrossRecordErrorHandler>> handle = veh => veh.Handle("LearningDelivery_R31", null, null, null);
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
                    new TestLearningDelivery() { AimTypeNullable = 1, StdCodeNullable = 120},
                    new TestLearningDelivery() {  AimTypeNullable = 3, StdCodeNullable = 200},
                }
            };
            var validationErrorHandlerMock = new Mock<IValidationCrossRecordErrorHandler>();
            Expression<Action<IValidationCrossRecordErrorHandler>> handle = veh => veh.Handle("LearningDelivery_R31", null, null, null);
            validationErrorHandlerMock.Setup(handle);

            var rule = NewRule(validationErrorHandlerMock.Object);
            rule.Validate(learner);
            validationErrorHandlerMock.Verify(handle, Times.Once);
        }
    }
}