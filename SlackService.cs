using System;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;

namespace SafeGuard
{
	// Token: 0x02000016 RID: 22
	internal class SlackService
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00002F90 File Offset: 0x00001190
		public static void LogToSlack(dynamic data, string controller, string action, string channel)
		{
			object arg = new
			{
				Controller = controller,
				Action = action,
				Data = data
			};
			if (SlackService.<>o__0.<>p__1 == null)
			{
				SlackService.<>o__0.<>p__1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(SlackService)));
			}
			Func<CallSite, object, string> target = SlackService.<>o__0.<>p__1.Target;
			CallSite <>p__ = SlackService.<>o__0.<>p__1;
			if (SlackService.<>o__0.<>p__0 == null)
			{
				SlackService.<>o__0.<>p__0 = CallSite<Func<CallSite, Type, object, Formatting, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "SerializeObject", null, typeof(SlackService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			string arg2 = target(<>p__, SlackService.<>o__0.<>p__0.Target(SlackService.<>o__0.<>p__0, typeof(JsonConvert), arg, 1));
			object arg3 = new
			{
				text = string.Format("Log Entry: {0}", arg2),
				username = "SafeGuardBot",
				icon_emoji = ":grimacing:",
				channel = string.Format("#{0}", channel)
			};
			if (SlackService.<>o__0.<>p__3 == null)
			{
				SlackService.<>o__0.<>p__3 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(SlackService)));
			}
			Func<CallSite, object, string> target2 = SlackService.<>o__0.<>p__3.Target;
			CallSite <>p__2 = SlackService.<>o__0.<>p__3;
			if (SlackService.<>o__0.<>p__2 == null)
			{
				SlackService.<>o__0.<>p__2 = CallSite<Func<CallSite, Type, string, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "postJSONNoHeaders", null, typeof(SlackService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			target2(<>p__2, SlackService.<>o__0.<>p__2.Target(SlackService.<>o__0.<>p__2, typeof(Utilities), "https://hooks.slack.com/services/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", arg3));
		}
	}
}
