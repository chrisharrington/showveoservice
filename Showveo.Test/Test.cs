using NUnit.Framework;
using Showveo.Container;
using Showveo.Data;

namespace Showveo.Test
{
	[TestFixture]
	public class Test
	{
		[Test]
		public void Blah()
		{
			Loader.Start();

			var repository = DR.Get<IUserRepository>();
			var user = repository.Authenticate("blah", "boo");
		}
	}
}