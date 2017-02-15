using System;

namespace WebSuiteDDD.SharedKernel.Tests.Mocks
{
    public class EntityBaseMock : EntityBase<Guid>
    {
        public EntityBaseMock(Guid id) : base(id)
        {
        }
    }
}