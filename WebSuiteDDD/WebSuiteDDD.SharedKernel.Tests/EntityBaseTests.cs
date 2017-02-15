using System;
using WebSuiteDDD.SharedKernel.Tests.Mocks;
using Xunit;

namespace WebSuiteDDD.SharedKernel.Tests
{
    public class EntityBaseTests
    {
        [Fact]
        public void Equals_Should_ReturnFalse_When_TheObjectIsNull()
        {
            var entityBase = new EntityBaseMock(Guid.NewGuid());

            Assert.False(entityBase.Equals(null));
        }

        [Fact]
        public void Equals_Should_ReturnFalse_When_TwoObjectsAreNotEqual()
        {
            var entityBase1 = new EntityBaseMock(Guid.NewGuid());
            var entityBase2 = new EntityBaseMock(Guid.NewGuid());

            Assert.False(entityBase1.Equals(entityBase2));
        }

        [Fact]
        public void Equals_Should_ReturnTrue_When_TwoObjectsAreEqual()
        {
            var id = Guid.NewGuid();
            var entityBase1 = new EntityBaseMock(id);
            var entityBase2 = new EntityBaseMock(id);

            Assert.True(entityBase1.Equals(entityBase2));
        }

        [Fact]
        public void Equals_Should_ReturnFalse_When_DifferentEntityIsPassedAsObject()
        {
            var entityBase1 = new EntityBaseMock(Guid.NewGuid());
            var entityBase2 = new EntityBaseMock(Guid.NewGuid());

            Assert.False(entityBase1.Equals((object)entityBase2));
        }

        [Fact]
        public void Equals_Should_ReturnTrue_When_EqualEntityIsPassedAsObject()
        {
            var id = Guid.NewGuid();
            var entityBase1 = new EntityBaseMock(id);
            var entityBase2 = new EntityBaseMock(id);

            Assert.True(entityBase1.Equals((object)entityBase2));
        }
    }
}