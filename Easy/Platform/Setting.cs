using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;

namespace Easy.Platform
{
    /// <summary>
    /// Represents a tpyed app setting
    /// </summary>
    /// <typeparam name="T">Type of setting</typeparam>
    public class Setting<T>
    {
        private T _defaultValue;
        private ApplicationDataContainer _container;
        private string _key;

        /// <summary>
        /// Creates a new Setting
        /// </summary>
        /// <param name="container">Data container storing the setting</param>
        /// <param name="key">Setting key</param>
        /// <returns>Setting</returns>
        public static Setting<T> FromContainer(ApplicationDataContainer container, string key)
        {
            return new Setting<T>(container, key);
        }
        
        /// <summary>
        /// Creates a new Setting
        /// </summary>
        /// <param name="container">Data container storing the setting</param>
        /// <param name="key">Setting key</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Setting</returns>
        public static Setting<T> FromContainer(ApplicationDataContainer container, string key, T defaultValue)
        {
            return new Setting<T>(container, key, defaultValue);
        }
        
        /// <summary>
        /// Creates a new local Setting
        /// </summary>
        /// <param name="key">Setting key</param>
        /// <returns>Setting</returns>
        public static Setting<T> Local(string key)
        {
            return FromContainer(ApplicationData.Current.LocalSettings, key);
        }

        /// <summary>
        /// Creates a new local Setting
        /// </summary>
        /// <param name="key">Setting key</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Setting</returns>
        public static Setting<T> Local(string key, T defaultValue)
        {
            return FromContainer(ApplicationData.Current.LocalSettings, key, defaultValue);
        }

        /// <summary>
        /// Creates a new roaming Setting
        /// </summary>
        /// <param name="key">Setting key</param>
        /// <returns>Setting</returns>
        public static Setting<T> Roaming(string key)
        {
            return FromContainer(ApplicationData.Current.RoamingSettings, key);
        }

        /// <summary>
        /// Creates a new roaming Setting
        /// </summary>
        /// <param name="key">Setting key</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Setting</returns>
        public static Setting<T> Roaming(string key, T defaultValue)
        {
            return FromContainer(ApplicationData.Current.RoamingSettings, key, defaultValue);
        }

        /// <summary>
        /// Creates a new Setting
        /// </summary>
        /// <param name="container">Data container</param>
        /// <param name="key">Setting key</param>
        /// <param name="defaultValue">Default value</param>
        private Setting(ApplicationDataContainer container, string key, T defaultValue)
        {
            _container = container;
            _key = key;
            _defaultValue = defaultValue;
        }

        /// <summary>
        /// Creates a new Setting
        /// </summary>
        /// <param name="container">Data container</param>
        /// <param name="key">Setting key</param>
        private Setting(ApplicationDataContainer container, string key)
            : this(container, key, (T)GetDefaultValue(typeof(T)))
        {
        }

        /// <summary>
        /// Gets the default value of the given type
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>Default value</returns>
        private static object GetDefaultValue(Type type)
        {
            if (type.Equals(typeof(string)))
            {
                return String.Empty;
            }
            else
            {
                return Activator.CreateInstance(type);
            }
        }

        /// <summary>
        /// Gets or sets the setting value
        /// </summary>
        public T Value
        {
            get
            {
                var result = _container.Values[_key];

                return result == null ? _defaultValue : (T)result;
            }
            set
            {
                _container.Values[_key] = value;
            }
        }
    }
}
