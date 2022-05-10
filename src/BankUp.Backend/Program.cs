using BankUp.Backend;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => 
        webBuilder.UseStartup<Startup>())
    .Build()
    .Run();