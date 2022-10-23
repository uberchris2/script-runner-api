using System.Management.Automation;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

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

app.MapGet("/runscript/{scriptName}", async (string scriptName) =>
{
    //TODO: add logging
    var connectionString = app.Configuration["ScriptContainer:ConnectionString"];
    var containerName = app.Configuration["ScriptContainer:Container"];
    var scriptContents = await GetScriptFromBlob(connectionString, containerName, scriptName);

    string outputString = "No output";
    using (var ps = PowerShell.Create())
    {
        ps.AddScript(scriptContents);
        //TODO: allow PS parameters 
        //ps.AddParameters(scriptParameters);
        var pipelineObjects = await ps.InvokeAsync().ConfigureAwait(false);
        // foreach (var item in pipelineObjects)
        // {
        //     Console.WriteLine(item.BaseObject.ToString());
        // }
        outputString = string.Join(
            Environment.NewLine, 
            pipelineObjects.Select(x => x.BaseObject.ToString()).ToArray());
    }
    return outputString;
})
.WithName("RunScript");

app.Run();

static async Task<string> GetScriptFromBlob(string connectionString, string containerName, string scriptName)
{

    var blobServiceClient = new BlobServiceClient(connectionString);
    var container = blobServiceClient.GetBlobContainerClient(containerName);
    var blobClient = container.GetBlobClient(scriptName);
    BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync();
    return downloadResult.Content.ToString();
}