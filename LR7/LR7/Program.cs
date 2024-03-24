using LR7.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();    // –еЇстрац≥€ UserService з областю видимост≥ Scoped
                                                            // => окремий екземпл€р дл€ кожного HTTP-запиту
builder.Services.AddScoped<IVehicleService, VehicleService>();    // –еЇстрац≥€ UserService з областю видимост≥ Scoped
                                                                  // => окремий екземпл€р дл€ кожного HTTP-запиту
builder.Services.AddScoped<IParkingSectionService, ParkingSectionService>();    // –еЇстрац≥€ UserService з областю видимост≥ Scoped
                                                                                // => окремий екземпл€р дл€ кожного HTTP-запиту
builder.Services.AddScoped<IParkingSpaceService, ParkingSpaceService>();    // –еЇстрац≥€ UserService з областю видимост≥ Scoped
                                                                            // => окремий екземпл€р дл€ кожного HTTP-запиту

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
