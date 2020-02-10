using System.Collections.Generic;
using System.IO;
using Xunit;

namespace SIS.MvcFramework.Tests
{
    public class ViewEngineTests
    {
        [Theory]
        [InlineData("OnlyHtmlView")]
        [InlineData("ForForeachIfView")]
        [InlineData("ViewModelView")]
        public void GetHtmlTest(string viewName)
        {
            var viewModel = new TestViewModel()
            {
                Name = "Kiro",
                Year = 2020,
                Numbers = new List<int> { 1, 10, 100, 1000, 10000 },
            };

            var testContent = File.ReadAllText($"ViewTests/{viewName}.html");
            var expectedContent = File.ReadAllText($"ViewTests/{viewName}.Expected.html");

            IViewEngine viewEngine = new ViewEngine();
            var actualResult = viewEngine.GetHtml(testContent, viewModel);

            Assert.Equal(expectedContent, actualResult);
        }
    }
}
