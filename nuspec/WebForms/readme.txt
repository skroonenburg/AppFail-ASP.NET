-------------------------------------
            The Basics           
-------------------------------------

Thanks for installing Appfail.  Appfail monitors your web application's unhandled exceptions and reports them to Appfail's cloud service, for rich failure analytics and notifications.

You're almost ready to have your application report failures to Appfail.net...
Just follow these steps:

1. If you have not done so already, create an account with Appfail.net and add an application for this web-site. Make sure you enter the correct "Base URL" for this application when joining (this is explained in more detail below).

2. Open web.config and insert the API token provided in the Appfail dashboard:
<appfail apiToken="YOUR API TOKEN HERE" />


*** Remember, by default it takes your site up to 1 minute to report failures to Appfail ***


-------------------------------------
       What is the base URL?
-------------------------------------

When registering you provide a 'base URL' for your web-site in the Appfail portal.
Incoming requests to your application must contain this base URL (no explicit match required on port, https/http and www/no-www)

For example: If your ASP.NET application receives requests starting with:

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

Appfail is smart enough to know how to handle this! You don't need to do anything to support these URL variations.


-------------------------------------
      Failures not reporting?
-------------------------------------

Have you waited a few minutes & no failures are showing in the Appfail dashboard?
Please see our troubleshooting article for common causes:
http://support.appfail.net/kb/troubleshooting/there-are-no-failures-showing-for-my-application


-------------------------------------
   Advanced Module Configuration
-------------------------------------
The Appfail reporting module has a lot of advanced configuration settings, including:

  * the ability to failure reporting batch sizes & intervals/timing
  * the ability to restrict data that is reported to appfail, including masking certain post/query/cookie/server variable values, and ignoring reports from particular URLS

Read about the various configuration options at:
http://support.appfail.net/kb/aspnet-module-configuration


-------------------------------------
    Support
-------------------------------------

If you have any questions, please head to our discussion site:
http://support.appfail.net/discussions