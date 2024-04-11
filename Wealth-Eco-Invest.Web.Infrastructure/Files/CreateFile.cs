namespace Wealth_Eco_Invest.Web.Infrastructure.Files
{
	using ViewModels.Announce;

	public class CreateFile
	{
		public static void CreateImageFile(AnnounceFormModel model)//here view Model-a
		{
			string fileName = model.ImageUrl == null ? model.ProductImage.FileName : model.ImageUrl;

			string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
			using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
			{
				model.ProductImage.CopyTo(fileStream);
			}
		}
	}
}
