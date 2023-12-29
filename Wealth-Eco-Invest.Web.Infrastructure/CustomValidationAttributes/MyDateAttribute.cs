namespace Wealth_Eco_Invest.Web.Infrastructure.CustomValidationAttributes
{
	using System.ComponentModel.DataAnnotations;

	public class MyDateAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)// Return a boolean value: true == IsValid, false != IsValid
		{
			DateTime d = Convert.ToDateTime(value);
			return d >= DateTime.Now; //Dates Greater than or equal to today are valid (true)
		}
	}
}
