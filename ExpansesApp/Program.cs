using ExpensesApp.Models; // Poprawiona przestrze� nazw
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dodaj us�ugi do kontenera.
builder.Services.AddControllersWithViews();

// Rejestracja kontekstu bazy danych z u�yciem Entity Framework
builder.Services.AddDbContext<ExpensesDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Pobiera po��czenie z appsettings.json

// Je�li korzystasz z Razor Pages, mo�esz doda�:
// builder.Services.AddRazorPages(); // Je�li u�ywasz Razor Pages

var app = builder.Build();

// Konfiguracja potoku ��da� HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Obs�uga b��d�w w produkcji
    app.UseHsts(); // W��cz HSTS (HTTP Strict Transport Security)
}

app.UseHttpsRedirection(); // Przekierowanie na HTTPS
app.UseStaticFiles(); // U�ywaj plik�w statycznych (np. CSS, JS)

app.UseRouting(); // Umo�liwia routowanie zapyta�

app.UseAuthorization(); // U�ywa autoryzacji (je�li masz zabezpieczenia)

// Definicja domy�lnej trasy
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Expenses}/{action=Index}/{id?}");

app.Run(); // Uruchamia aplikacj�
