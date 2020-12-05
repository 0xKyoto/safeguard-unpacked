using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace SafeGuard
{
	// Token: 0x02000018 RID: 24
	public static class Tools
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00003128 File Offset: 0x00001328
		public static string PSNResolver(string UserName)
		{
			string result;
			if (string.IsNullOrWhiteSpace(UserName))
			{
				result = "Invalid UserName";
			}
			else
			{
				try
				{
					result = Tools.psnreq("https://playstationresolver.com/php/ajax/api.php?action=get", UserName).Replace(",", " \r\n");
				}
				catch
				{
					result = "PSN API is down!";
				}
			}
			return result;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003180 File Offset: 0x00001380
		public static void EnableProxy()
		{
			string keyName = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
			Registry.SetValue(keyName, "ProxyEnable", 1);
			Registry.SetValue(keyName, "ProxyServer", "FuckYou.Cunt");
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000031B4 File Offset: 0x000013B4
		public static void DisableProxy()
		{
			string keyName = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
			Registry.SetValue(keyName, "ProxyEnable", 0);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000031D8 File Offset: 0x000013D8
		public static string GamerTagResolver(string UserName, string key)
		{
			string result;
			if (string.IsNullOrWhiteSpace(UserName))
			{
				result = "Invalid UserName";
			}
			else
			{
				GamerTagStoreObject gamerTagStoreObject = new GamerTagStoreObject();
				gamerTagStoreObject.GamerTag = UserName;
				gamerTagStoreObject.ApiKey = key;
				try
				{
					result = Tools.postRequest("https://safeguardauth.us/GamerTagSearch", gamerTagStoreObject).Replace("\"", string.Empty);
				}
				catch
				{
					result = "GT API is down!";
				}
			}
			return result;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003240 File Offset: 0x00001440
		public static string SkypeResolver(string skypename, string _username, string password, string programid)
		{
			string result;
			if (string.IsNullOrWhiteSpace(skypename))
			{
				result = "Invalid Skype Username";
			}
			else
			{
				try
				{
					result = Utilities.getJSON(string.Format("{0}/MiscFunctions?variable={1}&Event=SKYPERESOLVE", Config.MainUrl.Decrypt(), skypename), programid.Encrypt(), _username.Encrypt(), password.Encrypt());
				}
				catch (WebException ex)
				{
					if (ex.Status == WebExceptionStatus.ProtocolError)
					{
						result = ((HttpWebResponse)ex.Response).StatusDescription;
					}
					else
					{
						result = Enums.Messaging.SafeGuardApiError.GetEnumDescription();
					}
				}
			}
			return result;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000032CC File Offset: 0x000014CC
		public static string SkypeDBLookup(string skypename, string _username, string password, string programid)
		{
			string result;
			if (string.IsNullOrWhiteSpace(skypename))
			{
				result = "Invalid Skype Username";
			}
			else
			{
				try
				{
					result = Utilities.getJSON(string.Format("{0}/MiscFunctions?variable={1}&Event=SKYPEDB", Config.MainUrl.Decrypt(), skypename), programid.Encrypt(), _username.Encrypt(), password.Encrypt());
				}
				catch (WebException ex)
				{
					if (ex.Status == WebExceptionStatus.ProtocolError)
					{
						result = ((HttpWebResponse)ex.Response).StatusDescription;
					}
					else
					{
						result = Enums.Messaging.SafeGuardApiError.GetEnumDescription();
					}
				}
			}
			return result;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003358 File Offset: 0x00001558
		public static string GeoIP(string IP, string _username, string password, string programid)
		{
			string result;
			if (string.IsNullOrWhiteSpace(IP) || !Tools.validateIP(IP))
			{
				result = "Invalid IP";
			}
			else
			{
				if (Tools.validateIP(IP))
				{
					try
					{
						return Utilities.getJSON(string.Format("{0}/MiscFunctions?variable={1}&Event=GEO", Config.MainUrl.Decrypt(), IP), programid.Encrypt(), _username.Encrypt(), password.Encrypt());
					}
					catch (WebException ex)
					{
						if (ex.Status == WebExceptionStatus.ProtocolError)
						{
							return ((HttpWebResponse)ex.Response).StatusDescription;
						}
						return Enums.Messaging.SafeGuardApiError.GetEnumDescription();
					}
				}
				result = "Invalid IP";
			}
			return result;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003400 File Offset: 0x00001600
		public static string DomainToIP(string Domain, string _username, string password, string programid)
		{
			string result;
			if (string.IsNullOrWhiteSpace(Domain))
			{
				result = "Invalid Domain";
			}
			else
			{
				try
				{
					result = Utilities.getJSON(string.Format("{0}/MiscFunctions?variable={1}&Event=DOM", Config.MainUrl.Decrypt(), Domain), programid.Encrypt(), _username.Encrypt(), password.Encrypt());
				}
				catch (WebException ex)
				{
					if (ex.Status == WebExceptionStatus.ProtocolError)
					{
						result = ((HttpWebResponse)ex.Response).StatusDescription;
					}
					else
					{
						result = Enums.Messaging.SafeGuardApiError.GetEnumDescription();
					}
				}
			}
			return result;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000348C File Offset: 0x0000168C
		public static void Ping(string IP)
		{
			new Process
			{
				StartInfo = 
				{
					FileName = "ping.exe",
					Arguments = IP + " -t"
				}
			}.Start();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000034CC File Offset: 0x000016CC
		public static string PortScan(string IP, string _username, string password, string programid)
		{
			string result;
			if (string.IsNullOrWhiteSpace(IP))
			{
				result = "Invalid IP";
			}
			else
			{
				if (Tools.validateIP(IP))
				{
					try
					{
						return Utilities.getJSON(string.Format("{0}/MiscFunctions?variable={1}&Event=PORT", Config.MainUrl.Decrypt(), IP), programid.Encrypt(), _username.Encrypt(), password.Encrypt());
					}
					catch (WebException ex)
					{
						if (ex.Status == WebExceptionStatus.ProtocolError)
						{
							return ((HttpWebResponse)ex.Response).StatusDescription;
						}
						return Enums.Messaging.SafeGuardApiError.GetEnumDescription();
					}
				}
				result = "Invalid IP";
			}
			return result;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003568 File Offset: 0x00001768
		public static PullerOffsets GetOffests(string username, string password, string programid)
		{
			Tools.ProcessCheck();
			string clearText = username.ToLower();
			PullerOffsets result;
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(programid))
			{
				object arg = new
				{
					UserName = username,
					Password = password,
					ProgramId = programid
				};
				if (Tools.<>o__11.<>p__0 == null)
				{
					Tools.<>o__11.<>p__0 = CallSite<Action<CallSite, Type, object, string, string, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "LogToSlack", null, typeof(Tools), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				Tools.<>o__11.<>p__0.Target(Tools.<>o__11.<>p__0, typeof(SlackService), arg, "Tool/Login", Enums.Messaging.NullError.GetEnumDescription(), "tool-user");
				result = new PullerOffsets
				{
					Message = Enums.Messaging.NullError.GetEnumDescription(),
					Failure = true
				};
			}
			else
			{
				PullerOffsets pullerOffsets = new PullerOffsets();
				try
				{
					string json = Utilities.getJSON(string.Format("{0}/GrabOffsets?", Config.MainUrl.Decrypt()), programid.Encrypt(), clearText.Encrypt(), password.Encrypt());
					pullerOffsets = JsonConvert.DeserializeObject<PullerOffsets>(json);
					pullerOffsets.Message = "Successfully Grabbed Offsets";
				}
				catch (WebException ex)
				{
					if (ex.Status == WebExceptionStatus.ProtocolError)
					{
						return new PullerOffsets
						{
							Message = ((HttpWebResponse)ex.Response).StatusDescription,
							Failure = true
						};
					}
					return new PullerOffsets
					{
						Message = Enums.Messaging.SafeGuardApiError.GetEnumDescription(),
						Failure = true
					};
				}
				result = pullerOffsets;
			}
			return result;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003718 File Offset: 0x00001918
		public static string postRequest(string url, object postObject)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.ContentType = "application/json; charset=utf-8";
			httpWebRequest.Accept = "application/json; charset=utf-8";
			httpWebRequest.Method = "POST";
			httpWebRequest.Proxy = null;
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

		// Token: 0x060000CC RID: 204 RVA: 0x000037F4 File Offset: 0x000019F4
		internal static string psnreq(string url, string username)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			httpWebRequest.Accept = "application/x-www-form-urlencoded";
			httpWebRequest.Method = "POST";
			httpWebRequest.Proxy = null;
			string value = string.Format("psn={0}", username);
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
				string text = WebUtility.HtmlDecode(streamReader.ReadToEnd());
				result = text;
			}
			return result;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000038CC File Offset: 0x00001ACC
		public static bool validateIP(string ip)
		{
			string pattern = "^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";
			Regex regex = new Regex(pattern, RegexOptions.None);
			bool result = false;
			foreach (object obj in regex.Matches(ip))
			{
				Match match = (Match)obj;
				if (match.Success)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003940 File Offset: 0x00001B40
		public static string GetClientIP()
		{
			return Tools.getRequest("Q7UWk9IKNexshHV6e4O/GVjiNMYz5ZxbDfRLbmBjSF6Dv7uQz13ftsOQdhJeD3Pf".Decrypt());
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003960 File Offset: 0x00001B60
		internal static void ProcChecker()
		{
			bool flag = true;
			string[] source = new string[]
			{
				"fiddler",
				"wireshark",
				"charles",
				"sandboxie",
				"megadumper",
				"intercepter",
				"snpa",
				"dumcap",
				"comview",
				"netcheat",
				"cheat",
				"debugger",
				"winpcap",
				"processhacker",
				"taskmgr"
			};
			string[] source2 = new string[]
			{
				"test.exe"
			};
			while (flag)
			{
				if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Count<Process>() > 1)
				{
					Process.GetCurrentProcess().Kill();
				}
				Process[] processes = Process.GetProcesses();
				for (int i = 0; i < processes.Length; i++)
				{
					Process proc = processes[i];
					bool flag2 = source.Any((string s) => proc.MainWindowTitle.ToLower().Contains(s));
					bool flag3 = source.Any((string s) => proc.ProcessName.ToLower().Contains(s));
					bool flag4 = source2.Any((string s) => proc.MainWindowTitle.ToLower().Contains(s));
					bool flag5 = source2.Any((string s) => proc.ProcessName.ToLower().Contains(s));
					if ((flag2 || flag3) & (!flag4 && !flag5))
					{
						try
						{
							proc.Kill();
						}
						catch
						{
							Environment.Exit(0);
						}
					}
				}
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003AF0 File Offset: 0x00001CF0
		public static void ProcessCheck()
		{
			string[] array = new string[]
			{
				"solarwinds",
				"paessler",
				"fiddler",
				"cpacket",
				"Wireshark",
				"the wireshark network analyzer",
				"Ethereal",
				"sectools",
				"riverbed",
				"tcpdump",
				"Kismet",
				"EtherApe",
				"telerik",
				"glasswire",
				"dnspy",
				"dnSpy-x86",
				"dotPeek64",
				"dotPeek32"
			};
			foreach (string processName in array)
			{
				if (Process.GetProcessesByName(processName).Length != 0)
				{
					foreach (Process process in Process.GetProcessesByName(processName))
					{
						process.Kill();
					}
					MessageBox.Show("Running Suspicious Program(s). Exiting Program.", Enums.Messaging.AutoUpdateTitle.GetEnumDescription());
					Environment.Exit(0);
				}
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003C04 File Offset: 0x00001E04
		public static string getRequest(string url)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.Accept = "application/json; charset=utf-8";
			httpWebRequest.Method = "GET";
			httpWebRequest.Proxy = null;
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			string result;
			using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
			{
				string text = streamReader.ReadToEnd();
				result = text;
			}
			return result;
		}

		// Token: 0x04000064 RID: 100
		private static string key = "E3JgQZJXKU7mznFlRK/0at70griNZjWY/9CXXC7EnsTNqoGMqoIE2vEAuTwDDeV7s+eAbM/9ULRiusIgE+amfw==";
	}
}
