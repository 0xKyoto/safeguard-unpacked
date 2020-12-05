using System;

namespace SafeGuard
{
	// Token: 0x0200000F RID: 15
	public class GamerTagStoreObject
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000234F File Offset: 0x0000054F
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002357 File Offset: 0x00000557
		public string GamerTag { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002360 File Offset: 0x00000560
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002368 File Offset: 0x00000568
		public string IpAddress { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002371 File Offset: 0x00000571
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002379 File Offset: 0x00000579
		public string AddedBy { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002382 File Offset: 0x00000582
		// (set) Token: 0x06000062 RID: 98 RVA: 0x0000238A File Offset: 0x0000058A
		public string ApiKey { get; set; }
	}
}
