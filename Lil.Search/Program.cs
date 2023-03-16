using Lil.Search.Interfaces;
using Lil.Search.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ICustomersService, CustomersService>();
builder.Services.AddSingleton<IProductsService, ProductsService>();
builder.Services.AddSingleton<ISalesServices, SalesService>();


//Necesito indicar donde estan los servicios a los que nos vamos a comunicar, para lograr esto voy a utilizar
//la caracteristica de agregar un HttpClientFactory en dotnet core a traves de la invocacion del metodo AddHttpClient
//indicandole un nombre de servicio 'customersService' seguido de una expresion que indique cual va ser el resultado
//de hacer esto, aqui podria decirle la direccion de forma fija 'c.BaseAddress = new Uri("http://localhost..."]);' pero,
//me voy a basar en la infraestructura de configuracion que tiene dotnet core para sacar de el archivo de configuracion 
//o de variables de ambiente la direccion del servicio.
builder.Services.AddHttpClient("customersService", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Services:Customers"]);
});

builder.Services.AddHttpClient("productsService", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Services:Products"]);
});

builder.Services.AddHttpClient("salesService", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Services:Sales"]);
});


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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
