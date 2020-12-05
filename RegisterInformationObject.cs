using System;

namespace SafeGuard
{
	// Token: 0x0200000B RID: 11
	public class RegisterInformationObject : ErrorResponse
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002204 File Offset: 0x00000404
		// (set) Token: 0x06000032 RID: 50 RVA: 0x0000220C File Offset: 0x0000040C
		public string Token { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002215 File Offset: 0x00000415
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000221D File Offset: 0x0000041D
		public string Email { get; set; }
	}
}
