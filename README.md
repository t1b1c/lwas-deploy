# lwas-deploy
blue theme web application powered by lwas

get it [here](https://github.com/t1b1c/lwas-deploy/releases/latest)

## Prerequisites
1. IIS 6 or later
2. ASP.NET 2.0, .NET Framework 3.5
3. Sql Server 2005 or later
4. [OpenXML SDK v2.0](http://www.microsoft.com/en-us/download/details.aspx?id=5124)
5. Your application files - ask them from your developer. You should get an sql script and a zip file with the following structure:
```
|_ config
  |_ connections.xml
  |_ routes.xml
  |_ settings.xml
  |_ views.xml
  |_ zones.xml
|_ portfolio
  |_ *.xml
|_ roles
  |_ access.xml
  |_ roles.xml
  |_ users.xml
|_ translation
  |_ translation.xml
```

## Installation
1. Folder preparation
  1. Unzip to a resulting folder
  2. Copy `config/*.xml`, `roles/*.xml` and `translation/*.xml` to `/App_Data/config/`
2. IIS configuration
  1. Configure an IIS web site or application for the resulting folder
  2. Must use an ASP.NET 2.0 application pool. 
  3. Grant write acces to App_Data and its children for the application pool user.
3. Sql Server configuration
  1. Create a database and run the sql script against it  
  2. Create a login for the IIS application. For easy setup grant `dbo` to that login on your database
  3. In `/App_Data/config/connections.xml` edit the `string` attribute accordingly to point to your database using the configured login

## Deployment
Open `/App_Data/config/routes.xml` . By default `lwas-deploy` contains an empty `routes.xml` and should have been replaced after folder preparation.
The `routes.xml` file defines your menu structure. If your menu differs from the structure bellow then it has been customized and you should follow the steps bellow under **Customizations and extensions** upon comepletion of **Deployment**.

Most applications structure follow this template:
```
|_ Ecrane
  |_ document.aspx
  |_ document.aspx.cs
  |_ *.xml
|_ Rapoarte
  |_ raport.aspx
  |_ raport.aspx.cs
  |_ *.xml
|_ Nomenclatoare
  |_ document.aspx
  |_ document.aspx.cs
  |_ *.xml
|_ Neafisate
  |_ document.aspx
  |_ document.aspx.cs
  |_ *.xml
```
That structure is mirrored in `routes.xml` as
```xml
<routes name="">
  <route name="Ecrane">
    <screen name="some screen" />
    <screen name="another screen" />
    <screen name="yet one more screen" />
  </route>
  <route name="Rapoarte">
    <screen name="a report" />
  </route>
  <route name="Nomenclatoare">
    <screen name="some list" />
    <screen name="another list" />
  </route>
  <route name="Neafisate">
    <screen name="some hidden screen" />
    <screen name="another hidden screen" />
  </route>
</routes>
```

Notice how 
- `<route name="Ecrane">` reflects the folder `/Ecrane`, 
- `<route name="Rapoarte">` reflects the folder `/Rapoarte` 
- and so on.

All leaf nodes in the `routes.xml` are xml files in `zip from developer/portfolio/` so what you have to do is to copy them according to the `routes.xml` in the related folder in your IIS website or application root. 

E.g.: 
```
the screen            zip from developer/portfolio/some screen.xml
is routed by          <screen name="some screen" />
and should go to      /Ecrane/some screen.xml
```
## Customizations and extensions
Often your application differs from standard either with a customized menu or with a calendar extensions.
Please note this are most frequent differencies, some other might be in place. Consult your lwas developer about this.

### Customized menu 
You have a customized menu whenever your menu structure differs from `Ecrane` and `Rapoarte`. In this case you have few more steps to follow:

1. ask your developer for the customized `/menu/dropdownmenu.ascx` and place the files he gives into `/menu` folder overwritting the existing ones
2. for every missing folder definition in `routes.xml` copy-rename the original `/Ecrane` folder (that is without any *.xml file in it)
3. deploy the corresponding screens to those folders as instructed in **Deployment**
 
### Calendar extension
If you have a calendar screen in your application that screen is a lwas extension and is not contained in `lwas-deploy`.
Ask your developer for it and he should give you an entire folder most often named `calendar` to be placed in the root of your IIS website or application, i.e. `/calendar`.

Some extensions might have a more elaborated deployment, consult your developer on them.

## Verify installation
Open a web browser and navigate to your web site or application.
Try to use `admin` user with `admin` password. If it fails ask your developer for credentials to login to your application.
Your lwas application should load without errors.
