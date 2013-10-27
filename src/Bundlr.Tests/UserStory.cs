using System;
using System.Linq;
using Bundlr.Web.Application;
using Crowbar;
using NUnit.Framework;

namespace Bundlr.Tests
{
    [TestFixture]
    public abstract class UserStory
    {
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
        public class ThenAttribute : Attribute
        {
            public ThenAttribute(string description)
            {
                Description = description;
            }

            public string Description { get; private set; }
        }

        protected static readonly MvcApplication<App> Application = MvcApplication.Create<App>("Bundlr.Web", "Web.Custom.config");

        [Test]
        public void Execute()
        {
            Test();
        }

        protected abstract void Test();
    }

    public class When_requesting_index : UserStory
    {
        [Then("should include all scripts in bundles")]
        protected override void Test()
        {
            Application.Execute(client =>
            {
                var response = client.Get("/");
                response.ShouldHaveStatusCode(HttpStatusCode.OK);

                var scripts = response
                    .AsCsQuery()
                    .Find("script")
                    .SelectionHtml()
                    .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim());

                scripts.ShouldContainerInOrder(
                    "<script src=\"/assets/global.js\"></script>",
                    "<script src=\"/assets/file.js\"></script>",
                    "<script src=\"/assets/plain.js\"></script>",
                    "<script src=\"/assets/virtual.js\"></script>",
                    "<script src=\"/assets/handlebars.runtime.js\"></script>",
                    "<script src=\"/assets/handlebars.templates.js\"></script>",
                    "<script src=\"/assets/hogan.js\"></script>",
                    "<script src=\"/assets/mustache.templates.js\"></script>",
                    "<script src=\"/assets/underscore.js\"></script>",
                    "<script src=\"/assets/underscore.templates.js\"></script>",
                    "<script src=\"/assets/templates-test.js\"></script>"
                );
            });
        }
    }
}