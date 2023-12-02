namespace Wealth_Eco_Invest.Web.Infrastructure.Extensions
{
	using System.Security.Claims;
	using static Common.GeneralApplicationConstants;
	public static class ClaimsPrincipalExtensions
	{
		/// <summary>
		/// Get logged user id
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public static string? GetId(this ClaimsPrincipal user)
		{
			return user.FindFirstValue(ClaimTypes.NameIdentifier);
		}

		/// <summary>
		/// Get logged user email
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public static string? GetEmail(this ClaimsPrincipal user)
		{
			return user.FindFirstValue(ClaimTypes.Email);
		}

		/// <summary>
		/// Check is logged user is admin
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public static bool IsAdmin(this ClaimsPrincipal user)
		{
			return user.IsInRole(AdminRoleName);
		}
	}
}
