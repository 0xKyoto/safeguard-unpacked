using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace SafeGuard
{
	// Token: 0x02000024 RID: 36
	internal static class Security
	{
		// Token: 0x060000EF RID: 239 RVA: 0x0000530C File Offset: 0x0000350C
		internal static string Encrypt(this string clearText)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(clearText);
			using (Aes aes = Aes.Create())
			{
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(Security.EncryptionKey, new byte[]
				{
					73,
					118,
					97,
					110,
					32,
					77,
					101,
					100,
					118,
					101,
					100,
					101,
					118
				});
				aes.Key = rfc2898DeriveBytes.GetBytes(32);
				aes.IV = rfc2898DeriveBytes.GetBytes(16);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cryptoStream.Write(bytes, 0, bytes.Length);
						cryptoStream.Close();
					}
					clearText = Convert.ToBase64String(memoryStream.ToArray());
				}
			}
			return clearText;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000053EC File Offset: 0x000035EC
		internal static string Decrypt(this string cipherText)
		{
			cipherText = cipherText.Replace(" ", "+");
			byte[] array = Convert.FromBase64String(cipherText);
			using (Aes aes = Aes.Create())
			{
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(Security.EncryptionKey, new byte[]
				{
					73,
					118,
					97,
					110,
					32,
					77,
					101,
					100,
					118,
					101,
					100,
					101,
					118
				});
				aes.Key = rfc2898DeriveBytes.GetBytes(32);
				aes.IV = rfc2898DeriveBytes.GetBytes(16);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
					{
						cryptoStream.Write(array, 0, array.Length);
						cryptoStream.Close();
					}
					cipherText = Encoding.Unicode.GetString(memoryStream.ToArray());
				}
			}
			return cipherText;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000054E0 File Offset: 0x000036E0
		internal static string ComputeHash(string s)
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				using (FileStream fileStream = File.OpenRead(s))
				{
					byte[] array = md.ComputeHash(fileStream);
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < array.Length; i++)
					{
						stringBuilder.Append(array[i].ToString("X2"));
					}
					result = stringBuilder.ToString();
				}
			}
			return result;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005574 File Offset: 0x00003774
		internal static string GetHID(string drive)
		{
			if (drive == string.Empty)
			{
				foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
				{
					if (driveInfo.IsReady)
					{
						drive = driveInfo.RootDirectory.ToString();
						break;
					}
				}
			}
			if (drive.EndsWith(":\\"))
			{
				drive = drive.Substring(0, drive.Length - 2);
			}
			string volumeSerial = Security.getVolumeSerial(drive);
			string cpuid = Security.getCPUID();
			return cpuid.Substring(13) + cpuid.Substring(1, 4) + volumeSerial + cpuid.Substring(4, 4);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005614 File Offset: 0x00003814
		internal static string getVolumeSerial(string drive)
		{
			ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");
			managementObject.Get();
			string result = managementObject["VolumeSerialNumber"].ToString();
			managementObject.Dispose();
			return result;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005658 File Offset: 0x00003858
		internal static string getCPUID()
		{
			string text = "";
			ManagementClass managementClass = new ManagementClass("win32_processor");
			ManagementObjectCollection instances = managementClass.GetInstances();
			foreach (ManagementBaseObject managementBaseObject in instances)
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				if (text == "")
				{
					text = managementObject.Properties["processorID"].Value.ToString();
					break;
				}
			}
			return text;
		}

		// Token: 0x0400007A RID: 122
		internal static string EncryptionKey = "2f4980a36b48be1694fb3104b2ff9f00";

		// Token: 0x0400007B RID: 123
		internal static string dllhash = Security.ComputeHash(string.Format("{0}SafeGuard.dll", AppDomain.CurrentDomain.BaseDirectory));

		// Token: 0x0400007C RID: 124
		internal static string newtonhash = Security.ComputeHash(string.Format("{0}Newtonsoft.Json.dll", AppDomain.CurrentDomain.BaseDirectory));
	}
}
