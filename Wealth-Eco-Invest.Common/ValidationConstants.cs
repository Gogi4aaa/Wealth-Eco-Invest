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
            public const int DescriptionMaxLength = 90;
            public const int ImageUrlMaxLength = 1000;
        }

        public static class Category
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 2;
        }
	}
}
