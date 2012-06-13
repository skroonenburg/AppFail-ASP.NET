*************************************
***       Important Reading       ***
*************************************

Thanks for installing Appfail. You're almost ready to have your application report failures to Appfail.net

1. If you have not done so already, create an account with Appfail.net and add an application for this web-site.

2. Open web.config and insert the API token provided in the Appfail dashboard:
<appfail apiToken="YOUR API TOKEN HERE" />



*** Remember, by default it takes your site up to 1 minute to report failures to Appfail ***



If you are experiencing problems then please ensure that you have also done the following:

*************************************
       BASE URL CONFIGURATION
*************************************

The base URL configured for your web-site in the Appfail portal must match the URL of incoming requests to this web-site (with any port removed from the URL)

For example: If your ASP.NET application receives requests at:

http://yoursite.com 
    then the base URL is http://yoursite.com

http://myintranet
    then the base URL is http://myintranet

http://localhost
    then the base URL is http://localhost

http://localhost/MySubApp
    then the base URL is http://localhost/MySubApp

http://localhost:8080
    then the base URL is http://localhost


Don't worry if your site accepts requests on https & www variations, eg:
https://yoursite.com
http://yoursite.com
https://www.yoursite.com
https://yoursite.com

Appfail is smart enough to know how to handle this! You don't need to do anything to support these URLs.


Check out http://support.appfail.net for more information.