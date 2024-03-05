using System.Net.Http;

namespace Wealth_Eco_Invest.Common
{
    public class GeneralApplicationConstants
    {
        //Admin
        public const string AdminAreaName = "Admin";
        public const string AdminRoleName = "Administrator";
        public const string DevelopmentAdminEmail = "administrator@gmail.com";
        public const string AdminId = "6721a91c-5048-435e-88b2-63ea2c4c7c19";
        public const int UsersPerPage = 10;

        //User
        public const string UserRoleName = "User";

		//Announces
		public const int DefaultPage = 1;
        public const int MaxAnnouncesPerPage = 8;

        //emailSending
        public const string EmailFrom = "wealthecoinvest.contacts@gmail.com";
        public const string SmtpMail = "georgi-07@abv.bg";
        public const string SmtpPassword = "g2dRHcIj9rB8DAS0";
        public const string SmtpHost = "smtp-relay.sendinblue.com";
        public const int SmtpPort = 587;

        //newsURL
        public const string NewsUrl =
			"https://newsapi.org/v2/everything?apiKey=8a9b459dee964c2291e961c51aa7c822&q=environment&language=en&sortBy=popularity&pageSize=8";

        public const int TotalNews = 40;//change how much news i want on main page
        public const int NewsPerPage = 8; //change how much news will be on one page

    }
}
