using System;
using KoiParadise.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KoiParadise;

public class Program
{
	private static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
		
		// Add services to the container.
		_ = builder.Services.AddRazorPages();
		_ = builder.Services.AddDbContext<DataContext>(options => 
			options.UseSqlite(builder.Configuration["ConnectionStrings:Default"] ?? throw new InvalidOperationException()));
		
		WebApplication app = builder.Build();
		
		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			_ = app.UseExceptionHandler("/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			_ = app.UseHsts();
		}
		
		_ = app.UseHttpsRedirection();
		_ = app.UseStaticFiles();
		
		_ = app.UseRouting();
		
		_ = app.UseAuthorization();
		
		_ = app.MapRazorPages();
		
		app.Run();
	}
}
