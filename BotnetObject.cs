using System;

namespace SafeGuard
{
	// Token: 0x02000011 RID: 17
	public class BotnetObject
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000023A4 File Offset: 0x000005A4
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000023AC File Offset: 0x000005AC
		public string AttkIp { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000023B5 File Offset: 0x000005B5
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000023BD File Offset: 0x000005BD
		public string AttkPort { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000023C6 File Offset: 0x000005C6
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000023CE File Offset: 0x000005CE
		public string AttkMethod { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000023D7 File Offset: 0x000005D7
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000023DF File Offset: 0x000005DF
		public string AttkTime { get; set; }
	}
}
