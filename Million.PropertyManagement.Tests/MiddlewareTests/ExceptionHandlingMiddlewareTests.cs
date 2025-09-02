using NUnit.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.IO;
using Moq;

[TestFixture]
public class ExceptionHandlingMiddlewareTests
{
    [Test]
    public async Task Invoke_HandlesException_ReturnsProblemDetails()
    {
        var context = new DefaultHttpContext();
        var loggerMock = new Mock<ILogger<ExceptionHandlingMiddleware>>();
        var middleware = new ExceptionHandlingMiddleware(async (ctx) =>
        {
            throw new System.Exception("Test exception");
        }, loggerMock.Object);

        var responseStream = new MemoryStream();
        context.Response.Body = responseStream;

        await middleware.Invoke(context);

        responseStream.Seek(0, SeekOrigin.Begin);
        var responseText = new StreamReader(responseStream).ReadToEnd();

        Assert.IsTrue(responseText.Contains("Unexpected error"));
    }
}