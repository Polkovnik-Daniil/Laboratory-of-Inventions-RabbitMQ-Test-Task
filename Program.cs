using Laboratory_of_Inventions_RabbitMQ.Consumers;
using Laboratory_of_Inventions_RabbitMQ.Database;
using Laboratory_of_Inventions_RabbitMQ.Services.Background;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Added configuration reading for the logger
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();
    //Add Database
    string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AppDbContext>(options =>
                                                    options.UseSqlServer(connection));

    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    // Add RabbitMq
    builder.Services.AddMassTransit(mt =>
    {
        mt.AddConsumer<NotifyTransactionConsumer>();
        mt.UsingRabbitMq((context, config) =>
        {
            config.Host("rabbitmq://localhost", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
            config.ReceiveEndpoint("NotifyTransactionsQueue", e =>
            {
                e.ConfigureConsumer<NotifyTransactionConsumer>(context);
            });
            config.ClearSerialization();
            config.UseRawJsonDeserializer();
            config.ConfigureEndpoints(context);
        });
    });
    builder.Services.AddHostedService<RabbitBackgroundWorker>();
    // adding a background service to check the folder and files verification
    builder.Services.AddHostedService<InitialUnreadFolderAndFilesVerificationBackgroundService>();
    // adding a background service to track changes in the folder
    builder.Services.AddHostedService<FileWatcherBackgroundService>();
    // Add logger
    builder.Host.UseSerilog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    // Add a logger to middleware 
    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}