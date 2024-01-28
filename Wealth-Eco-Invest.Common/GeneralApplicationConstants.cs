namespace Wealth_Eco_Invest.Common
{
    public class GeneralApplicationConstants
    {
        //Admin
        public const string AdminAreaName = "Admin";
        public const string AdminRoleName = "Administrator";
        public const string DevelopmentAdminEmail = "administrator@gmail.com";

        //Announces
        public const int DefaultPage = 1;
        public const int MaxAnnouncesPerPage = 6;

        //emailSending
        public const string EmailFrom = "wealthecoinvest.contacts@gmail.com";
        public const string SmtpMail = "georgi-07@abv.bg";
        public const string SmtpPassword = "g2dRHcIj9rB8DAS0";
        public const string SmtpHost = "smtp-relay.sendinblue.com";
        public const int SmtpPort = 587;

        //newsURL
        public const string NewsUrl =
			"https://newsapi.ai/api/v1/article/getArticles?query=%7B%22%24query%22%3A%7B%22%24and%22%3A%5B%7B%22conceptUri%22%3A%22http%3A%2F%2Fen.wikipedia.org%2Fwiki%2FEnvironmental_protection%22%7D%2C%7B%22lang%22%3A%22eng%22%7D%5D%7D%2C%22%24filter%22%3A%7B%22forceMaxDataTimeWindow%22%3A%2231%22%7D%7D&resultType=articles&articlesSortBy=date&includeArticleLocation=true&includeArticleImage=true&apiKey=ab8ea46a-2334-4e23-a3f1-dea99308270b&articlesCount=8&articlesPage=1&articlesSortBy=socialScore&includeArticleAuthors=false&includeArticleSentiment=false&includeArticleAuthors=false&includeSourceTitle=false&includeArticleLocation=true&includeSourceLocation=true";
        
    }
}
