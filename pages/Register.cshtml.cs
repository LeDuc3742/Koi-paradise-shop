using System.Linq;
using KoiParadise.Data;
using KoiParadise.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiParadise.Pages;

public class RegisterModel : PageModel
{
	private const string ErrorKey = "error";
	
	private const string userExistErrorCode = "user_exists";
	private const string passwordErrorCode = "password_error";
	
	private readonly DataContext dataContext;
	
	private string lastErrorCode = string.Empty;
	
	public RegisterModel(DataContext dataContext)
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
	
	public void AddUser(string username, string password)
	{
		_ = this.dataContext.Add(new UserInfo { Name = username, Password = password });
		_ = this.dataContext.SaveChanges();
	}
	
	public bool IsRegisterInfoValid(string username, string password, string passwordConfirmation)
	{
		if (username == string.Empty || password == string.Empty || passwordConfirmation == string.Empty)
		{
			this.lastErrorCode = "unknown";
			return false;
		}
		
		if (passwordConfirmation != password)
		{
			this.lastErrorCode = passwordErrorCode;
			return false;
		}
		
		foreach (UserInfo info in this.dataContext.UserInfos.ToList())
		{
			if (info.Name == username)
			{
				this.lastErrorCode = userExistErrorCode;
				return false;
			}
		}
		
		return true;
	}
	
	public string GetErrorMessage()
	{
		if (!Request.Query.ContainsKey(ErrorKey))
			return string.Empty;
		
		string errorCode = Request.Query[ErrorKey].ToString();
		
		if (errorCode == userExistErrorCode)
			return "Tên người dùng đã tồn tại, xin hãy chọn tên khác";
		
		if (errorCode == passwordErrorCode)
			return "Xác nhận mật khẩu chưa đúng";
		
		return "Đã xảy ra lỗi, vui lòng thử lại sau";
	}
}
