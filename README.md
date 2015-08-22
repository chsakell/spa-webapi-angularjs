<h2 style="color:brown">Building Single Page Applications using Web API and AngularJS</h2>

<h3 style="font-weight:normal;">Frameworks - Libraries</h3>
<ol>
<li>ASP.NET Web API</li>
<li>Entity Framework</li>
<li>Autofac</li>
<li>FluentValidation</li>
<li>AngularJS</li>
<li>Bootstrap 3</li>
<li>3rd part libraries</li>
</ol>

<h3 style="font-weight:normal;">Installation instructions</h3>
<ol>
<li>Build solution to restore packages</li>
<li>Rebuild solution</li>
<li>Change the connection strings inside the HomeCinema.Data/App.config and HomeCinema.Web/Web.config
   accoarding to your development environment</li>
<li>Open Package Manager Console</li>
<li>Select HomeCinema.Data as Default project and run the following commands
   <ol>
   <li>add-migration "initial"</li>
   <li>update-database -verbose</li>
   </ol></li>
<li>Run HomeCinema.Web application</li>
</ol>

<h3 style="font-weight:normal;">Bower installations - you don't have to run them</h3>
<ol>
<li>bower install angucomplete-alt --save</li>
<li>bower install angular-base64</li>
<li>bower install angular-file-upload</li>
<li>bower install tg-angular-validator</li>
<li>bower install bootstrap</li>
<li>bower install raty</li>
<li>bower install angular-loading-bar</li>
<li>bower install angular-bootstrap</li>
</ol>
