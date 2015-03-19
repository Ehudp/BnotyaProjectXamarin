using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WomenInBible.Interfaces
{
    public interface ISettings
    {
        T GetValueOrDefault<T>(string key, T defaultValue = default(T));

        bool AddOrUpdateValue<T>(string key, T value);

        [Obsolete("This method is now obsolete, please use generic version as this may be removed in the future.")]
        bool AddOrUpdateValue(string key, Object value);

        void Remove(string key);

        [Obsolete("Save is deprecated and settings are automatically saved when AddOrUpdateValue is called.")]
        void Save();
    }
}
