
## Notification Hub
This is the Notification Hub for the Bike-Finder PWA. It enables sending notitications from the CMS to Frontend users in realtime. Don't forget to create a squidex rule (see Backend CMS folder).


## Prerequisites

### Install Docker Desktop

Refer [https://www.docker.com/products/docker-desktop](https://www.docker.com/products/docker-desktop) to install Docker Desktop

### Install Visual Studio 2019

Refer [https://visualstudio.microsoft.com/downloads/](https://visualstudio.microsoft.com/downloads/) to install Visual Studio 2019

Install ASP.NET & Webdevelopment Package when installing Visual Studio

### Install .NET Core 3.1 SDK

Refer [https://dotnet.microsoft.com/download/dotnet-core/3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) to install the latest .NET Core 3.1 SDK


## Cloning and Running the Application

Go into the project Folder and install the packages using the following command
```
dotnet restore
```
Open Docker Desktop

Open the NotificationHub.sln within Visual Studio 

Instead of IIS Express, choose Docker Run Configuration in the top and press **Ctrl + F5**

The application runs on [localhost:51439](http://localhost:51439)

To test if the application runs properly, check [http://localhost:51439/api/Notification/Test](http://localhost:51439/api/Notification/Test)

