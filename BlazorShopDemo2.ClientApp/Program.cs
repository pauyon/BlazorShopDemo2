using BlazorShopDemo2.ClientApp;
using BlazorShopDemo2.ClientApp.Services;
using BlazorShopDemo2.ClientApp.Services.IService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseApiUrl")) });
builder.Services.AddScoped<IProductService, ProductService>();

await builder.Build().RunAsync();