using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MitMediator.AutoApi.HttpMediator;
using SmokeTestBlazorClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseApiUrl = "api";
var httpClientName = "baseHttpClient";
builder.Services.AddHttpClient(httpClientName, client => { client.BaseAddress = new Uri("http://localhost:5035/"); });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IClientMediator, HttpMediator>(c => new HttpMediator(c, baseApiUrl, httpClientName));


await builder.Build().RunAsync();