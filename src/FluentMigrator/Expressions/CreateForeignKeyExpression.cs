#region License
// 
// Copyright (c) 2007-2009, Sean Chambers <schambers80@gmail.com>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System.Collections.Generic;
using System.Linq;
using FluentMigrator.Model;

namespace FluentMigrator.Expressions
{
	public class CreateForeignKeyExpression : MigrationExpressionBase
	{
		public virtual ForeignKeyDefinition ForeignKey { get; set; }

		public CreateForeignKeyExpression()
		{
			ForeignKey = new ForeignKeyDefinition();
		}

		public override void ApplyConventions(IMigrationConventions conventions)
		{
			ForeignKey.ApplyConventions(conventions);
		}

		public override void CollectValidationErrors(ICollection<string> errors)
		{
			ForeignKey.CollectValidationErrors(errors);
		}

		public override void ExecuteWith(IMigrationProcessor processor)
		{
			processor.Process(this);
		}

		public override IMigrationExpression Reverse()
		{
			return new DeleteForeignKeyExpression { ForeignKey = ForeignKey.Clone() as ForeignKeyDefinition };
		}

		public override string ToString()
		{
			return base.ToString() +
					string.Format("{0} {1}({2}) {3}({4})",
								ForeignKey.Name,
								ForeignKey.ForeignTable,
								string.Join(", ", ForeignKey.ForeignColumns.ToArray()),
								ForeignKey.PrimaryTable,
								string.Join(", ", ForeignKey.PrimaryColumns.ToArray()));
		}
	}
}