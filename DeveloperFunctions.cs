using System;
using System.Net;
using Newtonsoft.Json;

namespace SafeGuard
{
	// Token: 0x02000022 RID: 34
	public class DeveloperFunctions
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00004DC8 File Offset: 0x00002FC8
		public static Count CountCall(string programid)
		{
			Tools.ProcessCheck();
			Count count = new Count();
			Count result;
			try
			{
				string json = Utilities.getJSON(string.Format("{0}/Count?", Config.MainUrl.Decrypt()), programid.Encrypt(), "", "");
				count = JsonConvert.DeserializeObject<Count>(json);
				count.Message = "Successfully grabbed information";
				result = count;
			}
			catch (WebException ex)
			{
				if (ex.Status == WebExceptionStatus.ProtocolError)
				{
					result = new Count
					{
						Message = ((HttpWebResponse)ex.Response).StatusDescription,
						Failure = true
					};
				}
				else
				{
					result = new Count
					{
						Message = "Unable to grab information.",
						Failure = true
					};
				}
			}
			return result;
		}
	}
}
