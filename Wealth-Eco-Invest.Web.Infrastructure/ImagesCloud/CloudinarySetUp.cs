﻿namespace Wealth_Eco_Invest.Web.Infrastructure.ImagesCloud
{
	using CloudinaryDotNet;
	using CloudinaryDotNet.Actions;
	using Newtonsoft.Json.Linq;
	using static Common.GeneralApplicationConstants;
	public class CloudinarySetUp
	{
		private readonly Cloudinary cloudinary;

		public CloudinarySetUp()
		{
			cloudinary = new Cloudinary(CloudinaryApi);
			cloudinary.Api.Secure = true;
		}

		public async Task UploadAsync(string filePath)
		{
			var uploadParams = new ImageUploadParams()
			{
				File = new FileDescription(filePath),
				Folder = "Wealth-Eco-Invest",
				UseFilename = true,
				UniqueFilename = false,
				Overwrite = true
			};
			var uploadResult = await this.cloudinary.UploadAsync(uploadParams);
		}

		public string GenerateImageUrl(string fileName)
		{
			var myTransformation = cloudinary.Api.UrlImgUp.Add("Wealth-Eco-Invest");

			var generatedUrl = myTransformation.BuildUrl(fileName);

			return generatedUrl;
		}

		public async Task<JToken> GetImageDetails(string fileName)//it works but here we don't need it
		{
			var getResourceParams = new GetResourceParams(fileName)
			{
				QualityAnalysis = true
			};
			var getResourceResult = await cloudinary.GetResourceAsync(getResourceParams);
			var resultJson = getResourceResult.JsonObj;

			// Log quality analysis score to the console
			return resultJson["quality_analysis"];
		}
	}
}
