using System;

namespace SafeGuard
{
	// Token: 0x02000014 RID: 20
	public class AccountGen : ErrorResponse
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000245F File Offset: 0x0000065F
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002467 File Offset: 0x00000667
		public string Username { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002470 File Offset: 0x00000670
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002478 File Offset: 0x00000678
		public string Password { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002481 File Offset: 0x00000681
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002489 File Offset: 0x00000689
		public bool IsUsed { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002492 File Offset: 0x00000692
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000249A File Offset: 0x0000069A
		public string UsedBy { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000024A3 File Offset: 0x000006A3
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000024AB File Offset: 0x000006AB
		public long type { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000024B4 File Offset: 0x000006B4
		// (set) Token: 0x0600008B RID: 139 RVA: 0x000024BC File Offset: 0x000006BC
		public Guid Program { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000024C5 File Offset: 0x000006C5
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000024CD File Offset: 0x000006CD
		public int Id { get; set; }
	}
}
