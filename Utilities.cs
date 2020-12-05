using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace SafeGuard
{
	// Token: 0x02000023 RID: 35
	internal static class Utilities
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00004E7C File Offset: 0x0000307C
		internal static string getJSON(string url, string programId = "", string userName = "", string password = "")
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.Proxy = null;
			httpWebRequest.Accept = "application/json; charset=utf-8";
			httpWebRequest.Method = "GET";
			httpWebRequest.Headers["programid"] = programId;
			httpWebRequest.Headers["username"] = userName;
			httpWebRequest.Headers["password"] = password;
			httpWebRequest.Headers["version"] = Assembly.GetExecutingAssembly().GetName().Version.ToString().Encrypt();
			httpWebRequest.Headers["ipaddress"] = Tools.GetClientIP().Encrypt();
			httpWebRequest.Headers["signature"] = Config.Name;
			httpWebRequest.Headers["dllmd5"] = Security.dllhash.Encrypt();
			httpWebRequest.Headers["newtonmd5"] = Security.newtonhash.Encrypt();
			httpWebRequest.Headers["hid"] = Security.GetHID("C").Encrypt();
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			string result;
			using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
			{
				string text = streamReader.ReadToEnd();
				result = text;
			}
			return result;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004FD4 File Offset: 0x000031D4
		internal static string postJSON(string url, object postObject, string programId = "", string userName = "", string password = "")
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.ContentType = "application/json; charset=utf-8";
			httpWebRequest.Accept = "application/json; charset=utf-8";
			httpWebRequest.Method = "POST";
			httpWebRequest.Proxy = null;
			httpWebRequest.Headers["programid"] = programId;
			httpWebRequest.Headers["username"] = userName;
			httpWebRequest.Headers["password"] = password;
			httpWebRequest.Headers["version"] = Assembly.GetExecutingAssembly().GetName().Version.ToString().Encrypt();
			httpWebRequest.Headers["ipaddress"] = Tools.GetClientIP().Encrypt();
			httpWebRequest.Headers["signature"] = Config.Name;
			httpWebRequest.Headers["dllmd5"] = Security.dllhash.Encrypt();
			httpWebRequest.Headers["newtonmd5"] = Security.newtonhash.Encrypt();
			httpWebRequest.Headers["hid"] = Security.GetHID("C").Encrypt();
			string value = JsonConvert.SerializeObject(postObject, new JsonSerializerSettings
			{
				NullValueHandling = (NullValueHandling)1
			});
			using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
				streamWriter.Write(value);
				streamWriter.Flush();
				streamWriter.Close();
			}
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			string result;
			using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
			{
				string text = streamReader.ReadToEnd();
				result = text;
			}
			return result;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000518C File Offset: 0x0000338C
		internal static string postJSONNoHeaders(string url, object postObject)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.ContentType = "application/json; charset=utf-8";
			httpWebRequest.Accept = "application/json; charset=utf-8";
			httpWebRequest.Method = "POST";
			string value = JsonConvert.SerializeObject(postObject, new JsonSerializerSettings
			{
				NullValueHandling = (NullValueHandling)1
			});
			using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
				streamWriter.Write(value);
				streamWriter.Flush();
				streamWriter.Close();
			}
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			string result;
			using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
			{
				string text = streamReader.ReadToEnd();
				result = text;
			}
			return result;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005260 File Offset: 0x00003460
		public static bool validateEmail(string Email)
		{
			bool result;
			try
			{
				new MailAddress(Email);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005290 File Offset: 0x00003490
		public static bool IsUrlValid(string url)
		{
			string pattern = "^(http|https|ftp|)\\://|[a-zA-Z0-9\\-\\.]+\\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\\-\\._\\?\\,\\'/\\\\\\+&amp;%\\$#\\=~])*[^\\.\\,\\)\\(\\s]$";
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
			return regex.IsMatch(url);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000052B4 File Offset: 0x000034B4
		internal static string GetEnumDescription(this Enum value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			DescriptionAttribute[] array = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
			string result;
			if (array != null && array.Length != 0)
			{
				result = array[0].Description;
			}
			else
			{
				result = value.ToString();
			}
			return result;
		}
	}
}
