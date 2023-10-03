using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorketHealth.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Conexion de DB
builder.Services.AddDbContext<WorketHealthContext>(c => {
    c.UseSqlServer(builder.Configuration.GetConnectionString("ProjectAzureDsn"));
});
#endregion

//Agregar Servicoi Identity a la aplicacion
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<WorketHealthContext>();

//Esta es la linea para la url de retorno al acceder
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Home/Login");
    options.AccessDeniedPath = new PathString("/Home/Bloqueado");
});

//Estas son opciones de configuracion del identity
//builder.Services.Configure<IdentityOptions>(options => {
//    options.Password.RequiredLength = 5;
//    options.Password.RequireLowercase = true;
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
//    options.Lockout.MaxFailedAccessAttempts = 3;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

#region Ejecucion de Migraciones Pendientes
/*
 * Esta codigo aniadido nos ayudara para acceder a nuestro context(MercuryContext)
 * el metodo MigrateAsync -> aplica de manera asincrona cualquier migracion pendiente 
 * para el contexto en al base de datos y creaba la db en caso no exista, este codigo
 * se ejecutara al inicio de nuestra aplicaci�n.
*/
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<WorketHealthContext>();
        await context.Database.MigrateAsync();
    } catch(Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Ocurrio un error durante la migraci�n");
    }
}
#endregion

app.UseStaticFiles();

app.UseRouting();

//Se agrega la authenticacion
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
