# Software 2 - Class Exercise 11
# Goals
Convert the application to an API

# Instructions
This exercise is going to be a bit different.  You will mainly be following some tutorials and trying to apply them to your project.
It will be broken down into sections so that if you get stuck, your mentor will be able to easily help.

## Adding the Project

Rather than converting the console project, because it's useful for testing and may have other uses in the future, we're going to add a NEW project to contain the Web API. To accomplish this:

1. Right click on the solution in the Solution Explorer (the one at the very top) and choose  `Add->New Project`.
1. Click `Asp.Net Core Web API` and then click `Next`.
1. Give the new project a name such as `PetShop.WebApi` and click `Next`.
1. Accept all the defaults on by clicking `Create`.

The `Asp.Net Core Web API` template contains everything you need to get up and running with a Web Api. Specifically it adds a sample Controller in `WeatherForcatController.cs` and a model for the sample controller named `WeatherForcast.cs` along with some default configuration files. Most importantly, it adds a default Program.cs that creates the Web Host with some default configurations. 

In previous versions of Asp.Net the Program.cs would crate the web host and then feed it a `Startup` object (located in Starup.cs typically) and this Startup object would be where you do things like fill configure services for dependency injection. However, with the template we used, this is not necessary and you can simply add your service configurations to Program.cs. We'll do this next.

## Configuring Services (Setting up Dependency Injection and other configurations) 
1. Copy the code you have inside the `CreateServiceCollection` method in your CLI project's `Program.cs` and paste it the `WebApi` project's Program.cs just before the line the line that reads 
`var app = builder.Build();`
1. Replace `var servicecollection = new SerivceCollection().` with `builder.Services.` 
1. If you plan to use `Automapper` add that NuGet package to your `WebAPI` project and add a line to `Program.cs` to setup Dependency Injection for Automapper: `builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());` this line can be anywhere before the `builder.Build();` line.

## Get Ready to Run!
Take note of the comment that mentions `Swagger/OpenAPI`. Swagger is a great built-in means of testing the endpoints in your new API, but details on Swagger are beyond the scope of this course. Feel free to peruse the URL provided int eh comment for more info. For now we'll see what it does and examine the sample `WeatherForcastConroller`:
1. We need to tell Visual Studio we want to run the Web API project when we click GO or press F5. Right click on your API project and select `Set as Startup Project`.
1. Make sure that `https` is selected in the dropdown to the immediate right of the green GO arrow near the top of the screen.
1. Click the GO arrow (or press F5) and after your project builds, a web browser will automatically open and load the Swagger page for your API application. 
1. Play around a bit with the swagger page, specifically noting that you can expand the row with the blue `GET` image. And after you do there is a `Try It Out` button. Click it and play a bit. There's lots of detail available in Swagger, all pulled from the Sample Controller code. 

## Adding a Controller
Add a controller to replace the functionality of the program file.  For now, you will only have to do the `GET` methods.  One to get a product and the other to get an order.  If you have time, make the `POST` methods to add a product and order.
1. Add a new controller object to the `Controllers` folder. (Rt click, choose add->new object).
1. Edit the object and make it look like the sample controller, except instead of just returning an object, have it call your Product code to get a list of products and return that list.

Your new controller should look something like this 
```
using Microsoft.AspNetCore.Mvc;
using PetShop.DomainEntities.Validators;
using PetShop.DomainService;
using PetShop.WebApi.Mappers;
using PetShop.WebApi.Requests;
using PetShop.WebApi.Responses;

namespace PetShop.WebApi.Controllers {
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductsController(
        IProductService productService
    ) : ControllerBase {
        private readonly IProductService productService = productService;

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<ProductResponseModel[]>> GetProducts(bool InStockOnly = false) {
            var productEntities = await productService.GetProductsAsync(InStockOnly).ConfigureAwait(false);

            return Ok(productEntities.ToProductResponseModelArray());
        }
    }
}
```

## Test
Run your application and play with the Swagger page to see how your new controller works.  If you don't know how the routing works, do some research before testing. 

If you get stuck, take a look at the controllers in this project for comparison and/or talk to your mentor. 

For grins, expand a method in Swagger. Click `Try It Out`. Fill out whatever data it needs and click `Execute`. Copy the request URL Swagger displays. Open a new browser tab and paste the URL. Compare what you see there to the response in Swagger.

Swagger will suffice for testing `POST` and all the other types of API methods too. However, if you prefer, you may download some kind of program to interact with your application.  Postman is a popular application.  There are a few more good ones too, so feel free to do your research to find which http request tool you like most.

You may delete the sample Controller and related files at this point if you wish.