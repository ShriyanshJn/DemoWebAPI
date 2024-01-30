// For minimal based API :- map + http verb + url + method
// For controller based API :- map + controller

var builder = WebApplication.CreateBuilder(args);

// Add services to the container for MapController to work
builder.Services.AddControllers();
var app = builder.Build();

// All statements starting with app.Use is used to add middleware component, which acts as assemblers and creates a middleware pipeline as a whole
// Chain of Responsibility design pattern
app.UseHttpsRedirection();

//app.UseRouting();

//app.UseAuthorization();

//app.UseAuthentication();



// MINIMAL BASED API APPROACH

//app.MapGet("/shirts", () =>
//{
//    return "Reading all the shirts";
//});

//app.MapGet("/shirts/{id}", (int id) =>
//{
//    return $"Reading shirt with ID : {id}";
//});

//app.MapPost("/shirts", () =>
//{
//    return "Creating a shirt";
//});

//app.MapPut("/shirts/{id}", (int id) =>
//{
//    return $"Updating shirt with ID : {id}";
//});

//app.MapDelete("/shirts/{id}", (int id) =>
//{
//    return $"Deleting shirt with ID : {id}";
//});



// CONTROLLER BASED API APPROACH

// Map http request to controller
app.MapControllers();

app.Run();

