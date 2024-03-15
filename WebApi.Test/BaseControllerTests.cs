using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Tests
{
    public class BaseControllerTests
    {
        protected static void CreateHttpContext(ControllerBase controller)
        {
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }
    }
}
