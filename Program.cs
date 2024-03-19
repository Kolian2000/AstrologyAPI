using NewWebApi.Interface;
using NewWebApi.Services;
using System.Text.Json.Serialization;
using System.Security.Cryptography.Xml;
using Newtonsoft.Json.Serialization;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowOrigin", opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddControllers();
builder.Services.AddQuartz(q =>
{
	q.UseMicrosoftDependencyInjectionJobFactory();
	var jobKey = new JobKey("HoroscopeJob");
	q.AddJob<GetHoroscopeSubscribersJob>(opt => opt.WithIdentity(jobKey));
	
	q.AddTrigger(opts => opts
	.ForJob(jobKey)
	.WithIdentity(jobKey.Name + "_trigger")
	.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(13,38))
	);
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

// builder.Services.AddScoped<DataBaseConnection>();
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
	.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddQuartzHostedService

builder.Services.AddScoped<IReposi, DBConnection>();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IOpenServices, OpenAi>((HttpClient client) =>
{
	client.BaseAddress = new Uri("https://api.openai.com/v1/chat/completions");
	client.DefaultRequestHeaders.Add("Authorization", $"Bearer {builder.Configuration["OpenAI:Apikey2"]}");
});
var app = builder.Build();
app.UseCors("AllowOrigin");
// app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.Use(async (HttpContext context, Func<Task> next) =>
{
	if(context.Request.Path.StartsWithSegments("/tset"))
	{
		await context.Response.WriteAsync("Ok");
	}
	else
	{
		await next();
	}
});


// app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
