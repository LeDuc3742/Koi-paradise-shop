using System.Linq;
using KoiParadise.Data;
using KoiParadise.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiParadise.Pages;

public class LoginModel : PageModel
{
	public const string ErrorKey = "error";
	
	private const string userNotFoundErrorCode = "user_not_found";
	private const string wrongPasswordErrorCode = "wrong_passwrod";
	
	private readonly DataContext dataContext;
	
	private string lastErrorCode = string.Empty;
	
	public LoginModel(DataContext dataContext)
	{
		this.dataContext = dataContext;
	}
	
	public string GetQueryString(string key)
	{
		return Request.Query.ContainsKey(key) ? Request.Query[key].ToString() : string.Empty;
	}
	
	public string GenerateErrorQueryString()
	{
		return this.lastErrorCode == string.Empty ? string.Empty : ErrorKey + "=" + this.lastErrorCode;
	}
	
	public bool IsLoginInfoValid(string username, string password)
	{
		if (username == string.Empty || password == string.Empty)
		{
			this.lastErrorCode = "unknown";
			return false;
		}
		
		foreach (UserInfo info in this.dataContext.UserInfos.ToList())
		{
			if (info.Name == username)
			{
				if (info.Password != password)
				{
					this.lastErrorCode = wrongPasswordErrorCode;
					return false;
				}
				
				return true;
			}
		}
		
		this.lastErrorCode = userNotFoundErrorCode;
		return false;
	}
	
	public string GetErrorMessage()
	{
		if (!Request.Query.ContainsKey(ErrorKey))
			return string.Empty;
		
		string errorCode = Request.Query[ErrorKey].ToString();
		
		if (errorCode == userNotFoundErrorCode)
			return "Không tìm thấy tên người dùng";
		
		if (errorCode == wrongPasswordErrorCode)
			return "Mật khẩu chưa đúng";
		
		return "Đã xảy ra lỗi, vui lòng thử lại sau";
	}
}
