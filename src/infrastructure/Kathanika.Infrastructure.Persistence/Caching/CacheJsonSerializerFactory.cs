using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Extensions.Caching.Hybrid;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kathanika.Infrastructure.Persistence.Caching;

internal class CacheJsonSerializerFactory : IHybridCacheSerializerFactory
{
    private static readonly JsonSerializerSettings JsonSettings = new()
    {
        ContractResolver = new PrivateSetterContractResolver(),
        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
    };

    public bool TryCreateSerializer<T>([NotNullWhen(true)] out IHybridCacheSerializer<T>? serializer)
    {
        serializer = new NewtonsoftJsonSerializer<T>();
        return true;
    }

    private sealed class NewtonsoftJsonSerializer<T> : IHybridCacheSerializer<T>
    {
        public T Deserialize(ReadOnlySequence<byte> source)
        {
            var json = System.Text.Encoding.UTF8.GetString(source.ToArray());
            return JsonConvert.DeserializeObject<T>(json, JsonSettings)!;
        }

        public void Serialize(T value, IBufferWriter<byte> target)
        {
            using StringWriter stringWriter = new();

            JsonSerializer serializer = JsonSerializer.Create(JsonSettings);
            serializer.Serialize(stringWriter, value);

            var jsonBytes = System.Text.Encoding.UTF8.GetBytes(stringWriter.ToString());
            target.Write(jsonBytes);
        }
    }

    private sealed class PrivateSetterContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.Writable) return property;

            PropertyInfo? propertyInfo = member as PropertyInfo;
            if (propertyInfo == null) return property;

            var hasPrivateSetter = propertyInfo.GetSetMethod(true) != null;
            property.Writable = hasPrivateSetter;

            return property;
        }
    }
}