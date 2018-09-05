using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SiteServer.Plugin
{
    /// <summary>
    /// 系统支持的数据库数据类型
    /// </summary>
    [JsonConverter(typeof(DataTypeConverter))]
    public class DataType : IEquatable<DataType>, IComparable<DataType>
    {
        /// <summary>
        /// 布尔值数据类型
        /// </summary>
        public static readonly DataType Boolean = new DataType(nameof(Boolean));

        /// <summary>
        /// 日期数据类型
        /// </summary>
        public static readonly DataType DateTime = new DataType(nameof(DateTime));

        /// <summary>
        /// 小数数据类型
        /// </summary>
        public static readonly DataType Decimal = new DataType(nameof(Decimal));

        /// <summary>
        /// 整数数据类型
        /// </summary>
        public static readonly DataType Integer = new DataType(nameof(Integer));

        /// <summary>
        /// 长文本数据类型
        /// </summary>
        public static readonly DataType Text = new DataType(nameof(Text));

        /// <summary>
        /// 字符串数据类型
        /// </summary>
        public static readonly DataType VarChar = new DataType(nameof(VarChar));

        internal DataType(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            Value = value;
        }

        /// <summary>
        /// 数据类型的值。
        /// </summary>
        public string Value { get; }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Equals(obj as DataType);
        }

        /// <summary>
        /// 比较两个数据类型是否一致。
        /// </summary>
        /// <param name="a">需要比较的数据类型。</param>
        /// <param name="b">需要比较的数据类型。</param>
        /// <returns>如果一致，则为true；否则为false。</returns>
        public static bool operator ==(DataType a, DataType b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if ((object) a == null || (object) b == null)
            {
                return false;
            }

            return a.Equals(b);
        }

        /// <summary>
        /// 比较两个数据类型是否不一致。
        /// </summary>
        /// <param name="a">需要比较的数据类型。</param>
        /// <param name="b">需要比较的数据类型。</param>
        /// <returns>如果不一致，则为true；否则为false。</returns>
        public static bool operator !=(DataType a, DataType b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 比较两个数据类型是否一致。
        /// </summary>
        /// <param name="other">需要比较的数据类型。</param>
        /// <returns>如果一致，则为true；否则为false。</returns>
        public bool Equals(DataType other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return
                Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 比较两个数据类型是否一致。
        /// </summary>
        /// <param name="other">需要比较的数据类型。</param>
        /// <returns>如果一致，则为0；否则为1。</returns>
        public int CompareTo(DataType other)
        {
            if (other == null)
            {
                return 1;
            }

            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            return StringComparer.OrdinalIgnoreCase.Compare(Value, other.Value);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return EqualityComparer<string>.Default.GetHashCode(Value);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Value;
        }
    }

    /// <inheritdoc />
    public class DataTypeConverter : JsonConverter
    {
        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DataType);
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dataType = value as DataType;
            serializer.Serialize(writer, dataType != null ? dataType.Value : null);
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var value = (string)reader.Value;
            return string.IsNullOrEmpty(value) ? null : new DataType(value);
        }
    }
}