using System.Security.Claims;

namespace ExpensemanagmentAPi;

public static class MyExtensions
{
    public static int UserId(this ClaimsPrincipal principal)
    {
        var userId = principal.Identity?.Name;
        if (!string.IsNullOrEmpty(userId))
        {
            return Int32.Parse(userId);
        }

        throw new Exception("user id invalid");
    }
}