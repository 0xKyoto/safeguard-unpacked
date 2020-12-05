using System;
using System.Collections.Generic;

namespace SafeGuard
{
	// Token: 0x0200000C RID: 12
	public class LoginResponse : ErrorResponse
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000222E File Offset: 0x0000042E
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002236 File Offset: 0x00000436
		public long Id { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000223F File Offset: 0x0000043F
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002247 File Offset: 0x00000447
		public string ProgramName { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002250 File Offset: 0x00000450
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002258 File Offset: 0x00000458
		public string UserName { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002261 File Offset: 0x00000461
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002269 File Offset: 0x00000469
		public string Email { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002272 File Offset: 0x00000472
		// (set) Token: 0x0600003F RID: 63 RVA: 0x0000227A File Offset: 0x0000047A
		public DateTime ExpirationDate { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002283 File Offset: 0x00000483
		// (set) Token: 0x06000041 RID: 65 RVA: 0x0000228B File Offset: 0x0000048B
		public int Level { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002294 File Offset: 0x00000494
		// (set) Token: 0x06000043 RID: 67 RVA: 0x0000229C File Offset: 0x0000049C
		public bool Banned { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000022A5 File Offset: 0x000004A5
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000022AD File Offset: 0x000004AD
		public string ProgramId { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000022B6 File Offset: 0x000004B6
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000022BE File Offset: 0x000004BE
		public string FullName { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000022C7 File Offset: 0x000004C7
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000022CF File Offset: 0x000004CF
		public string HID { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000022D8 File Offset: 0x000004D8
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000022E0 File Offset: 0x000004E0
		public List<Notification> Notifications { get; set; }
	}
}
