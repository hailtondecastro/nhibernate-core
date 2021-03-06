﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Collections.Generic;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH1796Generic
{
	using System.Threading.Tasks;
	[TestFixture]
	public class FixtureAsync: BugTestCase
	{
		[Test]
		public async Task MergeAsync()
		{
			var entity = new Entity { Name = "Vinnie Luther" };
			using (ISession s = OpenSession())
			using (ITransaction t = s.BeginTransaction())
			{
				await (s.SaveAsync(entity));
				await (t.CommitAsync());
			}

			entity.DynProps = new Dictionary<string, object>();
			entity.DynProps["StrProp"] = "Modified";
			using (ISession s = OpenSession())
			using (ITransaction t = s.BeginTransaction())
			{
				await (s.MergeAsync(entity));
				await (t.CommitAsync());
			}

			using (ISession s = OpenSession())
			using (ITransaction t = s.BeginTransaction())
			{
				await (s.CreateQuery("delete from Entity").ExecuteUpdateAsync());
				await (t.CommitAsync());
			}
		}

		[Test]
		public async Task SaveOrUpdateAsync()
		{
			var entity = new Entity { Name = "Vinnie Luther" };
			using (ISession s = OpenSession())
			using (ITransaction t = s.BeginTransaction())
			{
				await (s.SaveOrUpdateAsync(entity));
				await (t.CommitAsync());
			}

			entity.DynProps = new Dictionary<string, object>();
			entity.DynProps["StrProp"] = "Modified";
			using (ISession s = OpenSession())
			using (ITransaction t = s.BeginTransaction())
			{
				await (s.SaveOrUpdateAsync(entity));
				await (t.CommitAsync());
			}

			using (ISession s = OpenSession())
			using (ITransaction t = s.BeginTransaction())
			{
				await (s.CreateQuery("delete from Entity").ExecuteUpdateAsync());
				await (t.CommitAsync());
			}
		}
	}
}