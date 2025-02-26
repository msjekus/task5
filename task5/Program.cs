using Microsoft.Extensions.Options;
using task5.Config;
using task5.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<StudentConfig>(builder.Configuration.GetSection("Student"));
builder.Services.AddControllers();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseMiddleware<ColorMiddleware>();

app.MapGet("/home", async (HttpContext context, IOptions<StudentConfig> options) =>
{
    var student = options.Value;
    string color = context.Items["Color"]?.ToString()?? "black";
    await context.Response.WriteAsync($"<h1 style ='color:{color}'>Student:{student.FirstName} {student.LastName}, Age: {student.Age}</h1>");
});

app.MapGet("/academy", async (HttpContext context, IOptions<StudentConfig> options) =>
{
    var student = options.Value;
    string color = context.Items["Color"]?.ToString() ?? "black";
    string subjects = string.Join(", ", student.Subjects);
    await context.Response.WriteAsync($"<h1 style='color:{color}'>Subjects: {subjects}</h1>");
});

app.Run();
