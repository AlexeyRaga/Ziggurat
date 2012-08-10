﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ziggurat.Contracts;
using Ziggurat.Definition.Domain.Base;

namespace Ziggurat.Definition.Domain.Project
{
    public sealed class ProjectAggregate : AggregateRootBase<ProjectState>
    {
        public void Create(Guid id, string name, string shortName)
        {
            if (State.Id != Guid.Empty) throw new InvalidOperationException("Project already created");

            EnsureShortName(shortName);

            Apply(new ProjectCreated(id, name, shortName));
        }

        private void EnsureShortName(string shortName)
        {
            if (!Regex.IsMatch(shortName, @"^\w+$")) throw new ArgumentException("shortName can only contain letters and digits", "shortName");
            if (shortName.Length > 20) throw new ArgumentException("shortName length cannot be more than 20 chars", "shortName");
        }
    }
}
