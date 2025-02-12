# Software 2 - Class Exercise 11
# Goals
Convert the application to an API

# Instructions
This exercise is going to be a bit different.  You will mainly be following some tutorials and trying to apply them to your project.
It will be broken down into sections so that if you get stuck, your mentor will be able to easily help.

## Converting the Project
<strike>
You will only need to do this on the main `PetStore` project.  Follow this tutorial to convert the project: https://dotnettutorials.net/lesson/build-asp-net-core-web-api-project/ ~~
</strike>

Rather than converting the console project, because it's useful for testing and may have other uses in teh futere, we're going to add a NEW project to contain the Web API. To accomplish this:

1. Right click on the solution in the Solution Explorer (the one at the very top) and choose  `Add->New Project`.
1. Click `Asp.Net Core Web API` and then click `Next`.
1. Give the new project a name such as `PetShop.WebApi` and click `Next`.
1. Accept all the defaults on by clicking `Create`.

## Adding WebHost
<strike>
In this step, it will be okay to wipe out your program class and replace it with only what you need to run the API. We will replace the functionality we get with the console input with web endpoints. Be sure to copy and save the section where you are adding your services before deleting everything.

https://dotnettutorials.net/lesson/adding-web-host-builder/
</strike>

The project you just added contains it's own `Program.cs` which contains the code that is run when we run the Web Api project. (Just like the Program.cs in the CLI project contains the code that is run when we run the CLI.) But don't worry about this code too much just yet. We're going to move it around quite a bit.
1. Add a class new class called `Startup` to the WebApi project. (We'll look more at this in a bit for new leave the class empty but make sure it is public.)
actually dont' need startup anymore.. 
copy services configuration from CLI's Program.cs
and add references to other projects and?????

talk about swagger??!?!?!?

itmes below this line are old instructions
----------------------------------------


## Adding Startup file
This class is where you will add your services that we had in the program file.  

https://dotnettutorials.net/lesson/configuring-startup-class/

## Adding a Controller
Add a controller to replace the functionality of the program file.  For now, you will only have to do the `GET` methods.  One to get a product and the other to get an order.  If you have time, make the `POST` methods to add a product and order.

https://dotnettutorials.net/lesson/adding-controller-in-asp-net-core/

## Test
When you run your application, a web browser should open.  If you don't know how the routing works, do some research before testing.  This is how the I set my route up: `https://localhost:62235/product/getorder/1`.  Product is my controller, getorder is my method, and 1 is my id.

Talk to your mentor if you get stuck and can't figure out how to access your data using the web browser.

For testing `POST` methods, you will probably need to download some kind of program to interact with your application.  Postman is a popular application.  There are a few more good ones too, so feel free to do your research to find which http request tool you like most.