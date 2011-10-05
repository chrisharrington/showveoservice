using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using ShowveoService.Service.Configuration;
using ShowveoService.Service.Encoding;

namespace ShowveoService.Test.Service.Encoding
{
	/// <summary>
	/// Tests the EncoderFactory class.
	/// </summary>
	[TestFixture]
	public class EncoderFactoryTest
	{
		#region Data Members
		/// <summary>
		/// The subject under test.
		/// </summary>
		private IEncoderFactory _sut;

		/// <summary>
		/// The mock IConfigurationProvider object.
		/// </summary>
		private Mock<IConfigurationProvider> _mockConfigurationProvider;
		#endregion

		#region Setup
		/// <summary>
		/// Sets up the tests.
		/// </summary>
		[SetUp]
		public void Setup()
		{
			_mockConfigurationProvider = new Mock<IConfigurationProvider>();
			_sut = new EncoderFactory(_mockConfigurationProvider.Object);
		}
		#endregion

		#region Tests
		/// <summary>
		/// Tests that the constructor fails gracefully when given an invalid configuration provider.
		/// </summary>
		[Test]
		public void ShouldFailWithInvalidConfigurationProviderOnConstruction()
		{
			Assert.Throws<ArgumentNullException>(() => new EncoderFactory(null));
		}

		/// <summary>
		/// Tests that the CreateAll method creates three distinct encoders.
		/// </summary>
		[Test]
		public void ShouldCreateThreeEncodersOnCreateAll()
		{
			var encoders = _sut.CreateAll().ToArray();
			Assert.AreEqual(encoders.Count(), 3);
			Assert.AreEqual(encoders.Where(x => x.GetType() == typeof(TabletHandbrakeEncoder)).Count(), 1);
			Assert.AreEqual(encoders.Where(x => x.GetType() == typeof(TVHandbrakeEncoder)).Count(), 1);
			Assert.AreEqual(encoders.Where(x => x.GetType() == typeof(PhoneHandbrakeEncoder)).Count(), 1);
		}

		/// <summary>
		/// Tests that the Create method creates a phone encoder when given the phone preset.
		/// </summary>
		[Test]
		public void ShouldCreatePhoneEncoderOnCreate()
		{
			Assert.AreEqual(_sut.Create(EncodingPreset.Phone).GetType(), typeof (PhoneHandbrakeEncoder));
		}

		/// <summary>
		/// Tests that the Create method creates a TV encoder when given the TV preset.
		/// </summary>
		[Test]
		public void ShouldCreateTVEncoderOnCreate()
		{
			Assert.AreEqual(_sut.Create(EncodingPreset.TV).GetType(), typeof (TVHandbrakeEncoder));
		}

		/// <summary>
		/// Tests that the Create method creates a tablet encoder when given the tablet preset.
		/// </summary>
		[Test]
		public void ShouldCreateTabletEncoderOnCreate()
		{
			Assert.AreEqual(_sut.Create(EncodingPreset.Tablet).GetType(), typeof (TabletHandbrakeEncoder));
		}
		#endregion
	}
}