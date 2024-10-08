﻿using System.Net.Http;

namespace Wealth_Eco_Invest.Common
{
    public class GeneralApplicationConstants
    {
        //Admin
        public const string AdminAreaName = "Admin";
        public const string AdminRoleName = "Administrator";
        public const string DevelopmentAdminEmail = "administrator@gmail.com";
        public const string AdminId = "fc919f02-cc04-4ddf-8e25-7a02ba70fdd5";
        public const int UsersPerPage = 10;

        //User
        public const string UserRoleName = "User";

		//Announces
		public const int DefaultPage = 1;
        public const int MaxAnnouncesPerPage = 8;

        //emailSending
        public const string EmailFrom = "georgi-07@abv.bg";
        public const string SmtpMail = "wealthecoinvest.contacts@gmail.com";
        public const string SmtpPassword = "random-Password1";
        public const string SmtpHost = "in-v3.mailjet.com";
        public const int SmtpPort = 587;

        //newsURL
        public const string NewsUrl =
			"https://newsapi.org/v2/everything?apiKey=8a9b459dee964c2291e961c51aa7c822&q=environment&language=en&sortBy=popularity&pageSize=8";

        public const int TotalNews = 40;//change how much news i want on main page
        public const int NewsPerPage = 8; //change how much news will be on one page


        //Error message
        public const string CommonErrorMessage = "Възникна неочаквана грешка!";

		//cloudinary
		public const string CloudinaryApi =
			"cloudinary://312758823981794:Et5OYp91YC5IB8gzO8HWFCxuiQA@dzrp3c1cv";

    }
}
