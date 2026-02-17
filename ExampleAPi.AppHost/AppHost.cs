var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ExampleAPi>("exampleapi")
    .WithExternalHttpEndpoints();

builder.Build().Run();
