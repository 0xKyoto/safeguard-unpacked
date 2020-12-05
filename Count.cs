using System;

namespace SafeGuard
{
	// Token: 0x02000013 RID: 19
	public class Count : ErrorResponse
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000241B File Offset: 0x0000061B
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002423 File Offset: 0x00000623
		public int UserCount { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000242C File Offset: 0x0000062C
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002434 File Offset: 0x00000634
		public int TokenCount { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000243D File Offset: 0x0000063D
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002445 File Offset: 0x00000645
		public int BotnetCount { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000244E File Offset: 0x0000064E
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002456 File Offset: 0x00000656
		public int AccountCount { get; set; }
	}
}
