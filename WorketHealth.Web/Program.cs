using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using WorketHealth.DataAccess;
using WorketHealth.DataAccess.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Conexion de DB
builder.Services.AddDbContext<WorketHealthContext>(c => {
    c.UseSqlServer(builder.Configuration.GetConnectionString("ProjectAzureDsn"));
});
#endregion

//Agregar Servicoi Identity a la aplicacion
builder.Services.AddIdentity<AppUsuario, IdentityRole>(options =>
{
    // Otras configuraciones aquí...
    options.SignIn.RequireConfirmedAccount = false; // Deshabilitar la confirmación de cuenta
    options.SignIn.RequireConfirmedEmail = false; // Deshabilitar la confirmación de correo electrónico
    options.Password.RequireNonAlphanumeric = true; // Requiere caracteres no alfanuméricos en la contraseña
}).AddEntityFrameworkStores<WorketHealthContext>().AddDefaultTokenProviders();

//Esta es la linea para la url de retorno al acceder
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Home/Login");
    options.AccessDeniedPath = new PathString("/Home/Bloqueado");
});

//// Configurar el LicenseContext desde appsettings.json
//var epPlusSettings = builder.Configuration.GetSection("EPPlus").Get<EPPlusSettings>();
//if (epPlusSettings != null)
//{
//    if (epPlusSettings.LicenseContext == "Commercial")
//    {
//        ExcelPackage.LicenseContext = LicenseContext.Commercial;
//    }
//    else if (epPlusSettings.LicenseContext == "NonCommercial")
//    {
//        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//    }
//}
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
        //await context.Database.MigrateAsync();   //se pueso en nota para probar el DbInitializer (ingresa datos iniciales a las tablas al primer uso)
        context.Database.Migrate(); // Aplicar migraciones
        // Llama al DbInitializer para agregar datos iniciales (si es necesario)
        // var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        // var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        //  WorketHealthInitializer.Initialize(context);

        // Llama al método Initialize directamente en la clase estática
        WorketHealthInitializer.Initialize(context);

    } catch(Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Ocurrió un error durante la migración o inicialización de la base de datos");
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
