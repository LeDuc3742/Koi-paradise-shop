using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiParadise.Pages;

public class IndexModel : PageModel
{
	public void OnGet()
	{
		if (Request.Query.ContainsKey("logout"))
		{
			Response.Cookies.Delete("username");
			Response.Redirect("/Index");
		}
	}
}
