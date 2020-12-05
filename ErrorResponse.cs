using System;

namespace SafeGuard
{
	// Token: 0x02000009 RID: 9
	public class ErrorResponse
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000021D1 File Offset: 0x000003D1
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000021D9 File Offset: 0x000003D9
		public string Message { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000021E2 File Offset: 0x000003E2
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000021EA File Offset: 0x000003EA
		public bool Failure { get; set; }
	}
}
