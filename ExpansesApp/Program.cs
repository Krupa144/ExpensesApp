using ExpensesApp.Models; // Poprawiona przestrzeñ nazw
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dodaj us³ugi do kontenera.
builder.Services.AddControllersWithViews();

// Rejestracja kontekstu bazy danych z u¿yciem Entity Framework
builder.Services.AddDbContext<ExpensesDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Pobiera po³¹czenie z appsettings.json

// Jeœli korzystasz z Razor Pages, mo¿esz dodaæ:
// builder.Services.AddRazorPages(); // Jeœli u¿ywasz Razor Pages

var app = builder.Build();

// Konfiguracja potoku ¿¹dañ HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Obs³uga b³êdów w produkcji
    app.UseHsts(); // W³¹cz HSTS (HTTP Strict Transport Security)
}

app.UseHttpsRedirection(); // Przekierowanie na HTTPS
app.UseStaticFiles(); // U¿ywaj plików statycznych (np. CSS, JS)

app.UseRouting(); // Umo¿liwia routowanie zapytañ

app.UseAuthorization(); // U¿ywa autoryzacji (jeœli masz zabezpieczenia)

// Definicja domyœlnej trasy
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Expenses}/{action=Index}/{id?}");

app.Run(); // Uruchamia aplikacjê
