﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Linq;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using NUnit.Framework;
using NHibernate.Test.MappingByCode.IntegrationTests.NH3041;

namespace NHibernate.Test.MappingByCode.IntegrationTests.NH3657
{
	using System.Threading.Tasks;
	[TestFixture]
	public class OneToOneToPropertyReferenceWithExplicitClassSetAsync : TestCaseMappingByCode
	{
		protected override void OnSetUp()
		{
			using (var session = OpenSession())
			using (var tx = session.BeginTransaction())
			{
				var person1 = new Person { FirstName = "Jack" };
				session.Save(person1);

				var person2 = new Person { FirstName = "Robert" };
				session.Save(person2);

				var personDetail = new PersonDetail { LastName = "Smith", Person = person1 };
				session.Save(personDetail);

				tx.Commit();
			}
		}

		protected override void OnTearDown()
		{
			using (var session = OpenSession())
			using (var tx = session.BeginTransaction())
			{
				session.Delete("from System.Object");
				tx.Commit();
			}
		}

		protected override HbmMapping GetMappings()
		{
			var mapper = new ModelMapper();
			mapper.Class<PersonDetail>(m =>
				{
					m.Id(t => t.PersonDetailId, a => a.Generator(Generators.Identity));
					m.Property(t => t.LastName,
							   c =>
								   {
									   c.NotNullable(true);
									   c.Length(32);
								   });
					m.ManyToOne(t => t.Person,
								c =>
									{
										c.Column("PersonId");
										c.Unique(true);
										c.NotNullable(false);
										c.NotFound(NotFoundMode.Ignore);
									});
				});

			mapper.Class<Person>(m =>
				{
					m.Id(t => t.PersonId, a => a.Generator(Generators.Identity));
					m.Property(t => t.FirstName,
							   c =>
								   {
									   c.NotNullable(true);
									   c.Length(32);
								   });
					m.OneToOne(t => t.PersonDetail,
							   oo =>
								   {
									   //NH-3657
									   oo.Class(typeof(PersonDetail));
									   oo.PropertyReference(x => x.Person);
									   oo.Cascade(Mapping.ByCode.Cascade.All);
								   });
				});

			return mapper.CompileMappingForAllExplicitlyAddedEntities();
		}

		[Test]
		public async Task ShouldConfigureSessionCorrectlyAsync()
		{
			using (var session = OpenSession())
			{
				var person1 = await (session.GetAsync<Person>(1));
				var person2 = await (session.GetAsync<Person>(2));
				var personDetail = await (session.Query<PersonDetail>().SingleAsync());

				Assert.IsNull(person2.PersonDetail);
				Assert.IsNotNull(person1.PersonDetail);
				Assert.AreEqual(person1.PersonDetail.LastName, personDetail.LastName);
				Assert.AreEqual(person1.FirstName, personDetail.Person.FirstName);
			}
		}
	}
}
