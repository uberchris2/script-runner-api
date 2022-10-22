using System.Management.Automation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/endpoint", async () =>
{
    using (var ps = PowerShell.Create())
    {
        ps.AddScript("ping google.com");
        //ps.AddParameters(scriptParameters);
        var pipelineObjects = await ps.InvokeAsync().ConfigureAwait(false);
        foreach (var item in pipelineObjects)
        {
            Console.WriteLine(item.BaseObject.ToString());
        }
    }
    return "Hello World!";
})
.WithName("EndpointName");

app.Run();
