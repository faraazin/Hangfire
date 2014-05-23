﻿// This file is part of HangFire.
// Copyright © 2013-2014 Sergey Odinokov.
// 
// HangFire is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as 
// published by the Free Software Foundation, either version 3 
// of the License, or any later version.
// 
// HangFire is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public 
// License along with HangFire. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Globalization;
using HangFire.Common;

namespace HangFire.States
{
    public class ProcessingState : IState
    {
        public static readonly string StateName = "Processing";

        public ProcessingState(string serverId, int workerNumber)
        {
            if (String.IsNullOrWhiteSpace(serverId)) throw new ArgumentNullException("serverId");

            ServerId = serverId;
            StartedAt = DateTime.UtcNow;
            WorkerNumber = workerNumber;
        }

        public DateTime StartedAt { get; set; }
        public string ServerId { get; set; }
        public int WorkerNumber { get; set; }

        public string Name { get { return StateName; } }
        public string Reason { get; set; }
        public bool IsFinal { get { return false; } }
        public bool IgnoreExceptions { get { return false; } }

        public Dictionary<string, string> SerializeData()
        {
            return new Dictionary<string, string>
            {
                { "StartedAt", JobHelper.ToStringTimestamp(StartedAt) },
                { "ServerId", ServerId },
                { "WorkerNumber", WorkerNumber.ToString(CultureInfo.InvariantCulture) }
            };
        }
    }
}
