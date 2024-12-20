using BlazorDownloadFile;
using Blazored.LocalStorage;
using EduTrack.Components;
using EduTrack.Components.Models;
using EduTrack.Models;
using EduTrack.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();


builder.Services.AddHttpContextAccessor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorization();


var connectionString = "Server=localhost;Port=5432;Database=EduTrack;User ID=postgres;Password=446556;Include Error Detail=true";


builder.Services.AddDbContextFactory<EduTrackContext>(opt => opt.UseNpgsql(connectionString));
builder.Services.AddDbContextFactory<UserDbContext>(opt => opt.UseNpgsql(connectionString));


builder.Services.AddScoped<ISubmissionService, SubmissionService>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IDataService<Course>, DataService<Course>>();
builder.Services.AddScoped<IDataService<CourseAssignment>, DataService<CourseAssignment>>();
builder.Services.AddScoped<IDataService<Assignment>, DataService<Assignment>>();
builder.Services.AddScoped<IDataService<Submission>, DataService<Submission>>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<UserStateService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddBlazorDownloadFile();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/login";
		options.LogoutPath = "/logout";
	});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();