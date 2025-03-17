using Microsoft.EntityFrameworkCore;
using ExpensesApp.Models;
using ExpensesApp.Repositories;
using YourProjectNamespace.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Dodaj kontekst bazy danych
builder.Services.AddDbContext<ExpensesDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dodaj us³ugi MVC
builder.Services.AddControllersWithViews();

// Zarejestruj repozytorium
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();

var app = builder.Build();

// Konfiguracja œrodowiska
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Konfiguracja tras
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();