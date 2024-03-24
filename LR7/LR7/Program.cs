using LR7.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();    // ��������� UserService � ������� �������� Scoped
                                                            // => ������� ��������� ��� ������� HTTP-������
builder.Services.AddScoped<IVehicleService, VehicleService>();    // ��������� UserService � ������� �������� Scoped
                                                                  // => ������� ��������� ��� ������� HTTP-������
builder.Services.AddScoped<IParkingSectionService, ParkingSectionService>();    // ��������� UserService � ������� �������� Scoped
                                                                                // => ������� ��������� ��� ������� HTTP-������
builder.Services.AddScoped<IParkingSpaceService, ParkingSpaceService>();    // ��������� UserService � ������� �������� Scoped
                                                                            // => ������� ��������� ��� ������� HTTP-������

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
