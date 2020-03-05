using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace nwc.Tarwya.Infra.Core.Helpers
{
	public static class SerializerHelper
	{
		public static T FromXml<T>(this string xml) where T : class
		{
			try
			{
				if (string.IsNullOrEmpty(xml)) return null;
				XmlSerializer xs = new XmlSerializer(typeof(T));
				using (MemoryStream memoryStream = new MemoryStream(new UTF8Encoding().GetBytes(xml)))
				{
					return xs.Deserialize(memoryStream) as T;
				}
			}
			catch (Exception)
			{

				throw;
			}

		}
		public static XDocument ToXml<T>(this T item)
		{
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				XDocument doc = new XDocument();
				using (var xmlTextWriter = doc.CreateWriter())
				{
					xmlSerializer.Serialize(xmlTextWriter, item);
					xmlTextWriter.Close();
					return doc;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
		public static T FromJson<T>(this string json) where T : class
		{
			T _result = null;
			try
			{
				if (string.IsNullOrEmpty(json)) return null;
				_result = JsonConvert.DeserializeObject<T>(json);

				if (_result == null || _result == default(T))
					return null;

				return _result;
			}
			catch (Exception)
			{
				throw;
			}

		}
		public static string ToJson<T>(this T item)
		{
			string _result = string.Empty;
			try
			{
				_result = JsonConvert.SerializeObject(item);
				if (string.IsNullOrEmpty(_result)) return null;

				return _result;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
