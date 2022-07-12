using System.Text.Json;

namespace MichelMichels.DobissSharp.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly Dictionary<string, object> _data;

        public ConfigurationService(string filePath)
        {
            var content = File.ReadAllText(filePath);
            _data = JsonSerializer.Deserialize<Dictionary<string, object>>(content);
        }

        public string Get(string key) => Get<string>(key);
        public T Get<T>(string key)
        {
            _data.TryGetValue(key, out object value);

            var jsonElement = (JsonElement)value;
            return jsonElement.Deserialize<T>();
        }

        public void Set<T>(string key, T value)
        {
            if (!_data.ContainsKey(key))
            {
                _data.Add(key, value);
            }
            else
            {
                _data[key] = value;
            }
        }
    }
}
