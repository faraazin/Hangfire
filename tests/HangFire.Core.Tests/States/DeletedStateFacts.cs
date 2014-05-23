﻿using System;
using HangFire.Common;
using HangFire.States;
using Xunit;

namespace HangFire.Core.Tests.States
{
    public class DeletedStateFacts
    {
        [Fact]
        public void StateName_ReturnsDeleted()
        {
            var result = DeletedState.StateName;
            Assert.Equal("Deleted", result);
        }

        [Fact]
        public void NameProperty_ReturnsStateName()
        {
            var state = CreateState();

            var result = state.Name;

            Assert.Equal(DeletedState.StateName, result);
        }

        [Fact]
        public void IsFinalProperty_ReturnsTrue()
        {
            var state = CreateState();

            var result = state.IsFinal;

            Assert.True(result);
        }

        [Fact]
        public void IgnoreExceptions_ReturnsTrue()
        {
            var state = CreateState();

            var result = state.IgnoreExceptions;

            Assert.True(result);
        }

        [Fact]
        public void DeletedAtProperty_ReturnsCurrentUtcDate()
        {
            var state = CreateState();

            Assert.True(DateTime.UtcNow.AddMinutes(-1) < state.DeletedAt);
            Assert.True(state.DeletedAt < DateTime.UtcNow.AddMinutes(1));
        }

        [Fact]
        public void SerializeData_ReturnsSerializedStateData()
        {
            var state = CreateState();

            var data = state.SerializeData();

            Assert.Equal(1, data.Count);
            Assert.True(JobHelper.FromStringTimestamp(data["DeletedAt"]) != default(DateTime));
        }

        private static DeletedState CreateState()
        {
            return new DeletedState();
        }
    }
}
