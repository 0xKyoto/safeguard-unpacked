using System;

namespace SafeGuard
{
	// Token: 0x0200000D RID: 13
	public class Notification
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000022E9 File Offset: 0x000004E9
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000022F1 File Offset: 0x000004F1
		public long NotificationId { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000022FA File Offset: 0x000004FA
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002302 File Offset: 0x00000502
		public DateTime CreateDate { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000230B File Offset: 0x0000050B
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002313 File Offset: 0x00000513
		public string Message { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000231C File Offset: 0x0000051C
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002324 File Offset: 0x00000524
		public string CreatedBy { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000232D File Offset: 0x0000052D
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002335 File Offset: 0x00000535
		public bool IsActive { get; set; }
	}
}
