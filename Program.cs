using assignment_five;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

string connectionString =
    "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True;User Id=s26136;Password=___;Encrypt=False";

using (DatabaseManager dbManager = new(connectionString)) { }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
