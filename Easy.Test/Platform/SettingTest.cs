using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

using Windows.Storage;

using Easy.Platform;

namespace Easy.Test.Platform
{
    // Tests the Setting class
    [TestClass]
    public class SettingTest
    {
        // A class containing some test settings
        class TestSettings
        {
            public Setting<int> IntPropertyNoDefault = Setting<int>.Local(Keys.IntPropertyNoDefault);
            public Setting<int> IntPropertyDefault = Setting<int>.Local(Keys.IntPropertyDefault, Defaults.DefaultIntValue);

            public Setting<string> StringPropertyNoDefault = Setting<string>.Local(Keys.StringPropertyDefault);
            public Setting<string> StringPropertyDefault = Setting<string>.Local(Keys.StringPropertyDefault, Defaults.DefaultStringValue);

            public class Keys
            {
                public const string IntPropertyNoDefault = "IntPropertyNoDefault";
                public const string IntPropertyDefault = "IntPropertyDefault";

                public const string StringPropertyNoDefault = "StringPropertyNoDefault";
                public const string StringPropertyDefault = "StringPropertyDefault";
            }

            public class Defaults
            {
                public const int DefaultIntValue = 20;
                public const string DefaultStringValue = "Foo";
            }
        }

        private TestSettings _settings;
        private ApplicationDataContainer _container;

        [TestInitialize]
        public void Initialize()
        {
            _settings = new TestSettings();
            _container = ApplicationData.Current.LocalSettings;
        }

        [TestCleanup]
        public void Cleanup()
        {
            _container.Values.Remove(TestSettings.Keys.IntPropertyDefault);
            _container.Values.Remove(TestSettings.Keys.IntPropertyNoDefault);
            _container.Values.Remove(TestSettings.Keys.StringPropertyDefault);
            _container.Values.Remove(TestSettings.Keys.StringPropertyNoDefault);
        }

        // Test defaults for an int setting
        [TestMethod]
        public void TestIntDefaults()
        {
            Assert.IsNull(_container.Values[TestSettings.Keys.IntPropertyDefault]);
            Assert.IsNull(_container.Values[TestSettings.Keys.IntPropertyNoDefault]);

            Assert.AreEqual(0, _settings.IntPropertyNoDefault);
            Assert.AreEqual(TestSettings.Defaults.DefaultIntValue, _settings.IntPropertyDefault);
        }

        // Test defaults for a string setting
        [TestMethod]
        public void TestStringDefaults()
        {
            Assert.IsNull(_container.Values[TestSettings.Keys.StringPropertyDefault]);
            Assert.IsNull(_container.Values[TestSettings.Keys.StringPropertyNoDefault]);

            Assert.AreEqual(String.Empty, _settings.StringPropertyNoDefault);
            Assert.AreEqual(TestSettings.Defaults.DefaultStringValue, _settings.StringPropertyDefault);
        }

        // Test setting a value and reading it directly from the data container
        [TestMethod]
        public void TestSetValue()
        {
            Assert.IsNull(_container.Values[TestSettings.Keys.IntPropertyDefault]);

            _settings.IntPropertyDefault.Value = 0xbeef;

            Assert.AreEqual(0xbeef, _container.Values[TestSettings.Keys.IntPropertyDefault]);
        }
    }
}
