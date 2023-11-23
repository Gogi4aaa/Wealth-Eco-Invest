namespace Wealth_Eco_Invest.Common
{
	public static class ValidationConstants
	{
		public static class User
		{
			public const int UsernameMaxLength = 20;
			public const int UsernameMinLength = 4;
		}

        public static class Announce
        {
            public const int TitleMaxLength = 30;
            public const int TitleMinLength = 5;
            public const int DescriptionMaxLength = 90;
            public const int DescriptionMinLength = 5;
            public const int ImageUrlMaxLength = 1000;
            public const int PriceMinLength = 1;
            public const int PriceMaxLength = 100_000;
        }

        public static class Category
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 2;
        }
	}
}
