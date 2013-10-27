# Templar

Provides pre-compilation support for HTML templates for the [Microsoft ASP.NET Web Optimization Framework](https://www.nuget.org/packages/Microsoft.AspNet.Web.Optimization/). Currently supports [mustache](http://mustache.github.io/) (courtesy of [Hogan.js](http://twitter.github.io/hogan.js/)), [Handlebars.js](http://handlebarsjs.com/), and [underscore.js](http://underscorejs.org/#template).

## Usage

``` csharp
    // we need to use a custom virtual path provider to map virtual files.
	var virtualPathProvider = new TemplarVirtualPathProvider(BundleTable.VirtualPathProvider);
    BundleTable.VirtualPathProvider = virtualPathProvider;
	
	var bundle = new TemplarScriptBundle("~/js", virtualPathProvider)
		.Include("~/assets/hogan.js")
		.IncludeMustacheTemplates(
			virtualPath: "~/assets/app.templates.js", 
			global: "app", 
			templatesVirtualPath: "~/assets"
		);

	BundleTable.Bundles.Add(bundle);
```

Bundles and pre-compiles mustache templates found at `~/assets`. The pre-compiled templates will be available from the JavaScript variable `app.templates`. When `BundleTable.EnableOptimization` is disabled the pre-compiled templates will be served from `~/assets/app.templates.js`; otherwise they will be part of the bundle.

Normally `*.js` will be served by the `StaticFileHandler`. We don't want this behavior for our pre-compiled templates when `BundleTable.EnableOptimization` is disabled thus we must add an entry to Web.config telling it to by-pass the handler for our specific route.

``` xml

  <system.webServer>
    <handlers>
      <add name="MustacheScriptHandler"
		   verb="GET"
		   path="assets/app.templates.js"
		   type="System.Web.Handlers.TransferRequestHandler" />
	</handlers>
  </system.webServer>
```

Please note that if you're using a URL that is not normally served by the `StaticFileHandler` it isn't necessary to add an HTTP handler entry to Web.config.

## License

Licensed under [MIT](http://opensource.org/licenses/mit-license.php). Please refer to LICENSE for more information.
