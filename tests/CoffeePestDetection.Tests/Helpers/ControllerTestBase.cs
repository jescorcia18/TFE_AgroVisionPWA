
namespace CoffeePestDetection.Tests.Helpers;

public abstract class ControllerTestBase
{
    protected static OkObjectResult GetOkResult(IActionResult result)
    {
        return Assert.IsType<OkObjectResult>(result);
    }

    protected static ApiResponse<T> GetApiResponse<T>(IActionResult result)
    {
        var okResult = Assert.IsType<OkObjectResult>(result);

        return Assert.IsType<ApiResponse<T>>(okResult.Value);
    }

    protected static T GetResponseData<T>(IActionResult result)
    {
        var response = GetApiResponse<T>(result);

        Assert.NotNull(response.Data);

        return response.Data!;
    }
}
