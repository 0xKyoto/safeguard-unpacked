using System;
using System.ComponentModel;

namespace SafeGuard
{
	// Token: 0x02000007 RID: 7
	internal class Enums
	{
		// Token: 0x02000008 RID: 8
		internal enum Messaging
		{
			// Token: 0x04000014 RID: 20
			[Description("There was an error with your login")]
			LoginError,
			// Token: 0x04000015 RID: 21
			[Description("SafeGuard will be back up shortly")]
			SafeGuardApiError,
			// Token: 0x04000016 RID: 22
			[Description("Adding time failed")]
			AddTimeError,
			// Token: 0x04000017 RID: 23
			[Description("There was an error registering")]
			RegisterError,
			// Token: 0x04000018 RID: 24
			[Description("Unable to send attack")]
			AttackError,
			// Token: 0x04000019 RID: 25
			[Description("Value is null")]
			NullError,
			// Token: 0x0400001A RID: 26
			[Description("Please download the newest update")]
			AutoUpdateInstruction,
			// Token: 0x0400001B RID: 27
			[Description("SafeGuardAuth.us")]
			AutoUpdateTitle
		}
	}
}
