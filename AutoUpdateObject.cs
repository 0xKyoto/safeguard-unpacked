using System;

namespace SafeGuard
{
	// Token: 0x02000012 RID: 18
	public class AutoUpdateObject : ErrorResponse
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000023E8 File Offset: 0x000005E8
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000023F0 File Offset: 0x000005F0
		public string Version { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000023F9 File Offset: 0x000005F9
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002401 File Offset: 0x00000601
		public string Url { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000240A File Offset: 0x0000060A
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002412 File Offset: 0x00000612
		public bool Enabled { get; set; }
	}
}
