using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using ProCrew_Assignment.Data;
using ProCrew_Assignment.Interfaces;
using ProCrew_Assignment.Mapper;
using ProCrew_Assignment.Repository;
using ProCrew_Assignment.Services;

var builder = WebApplication.CreateBuilder(args);

 
  

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
  builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(connectionString));

var firebaseConfig = builder.Configuration.GetSection("FirebaseConfig");
FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile("C:/Users/Lenovo/source/repos/ProCrew Assignment/ProCrew Assignment/procrew-d052e-firebase-adminsdk-l7y7n-ae206f84e1.json"),

});
  

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Auto Mapper
builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

builder.Services.AddScoped<IProduct, ProductRep>(); 
builder.Services.AddScoped<AuditService>();

 

builder.Services.AddLogging();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
  
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
