namespace Wealth_Eco_Invest.Services.Data.Models.News
{
	using Newtonsoft.Json;

	public class EnvironmentNewsServiceModel
	{
		public class Articles
		{
			[JsonProperty("results")]
			public List<Result> Results { get; set; }

			[JsonProperty("totalResults")]
			public int TotalResults { get; set; }

			[JsonProperty("page")]
			public int Page { get; set; }

			[JsonProperty("count")]
			public int Count { get; set; }

			[JsonProperty("pages")]
			public int Pages { get; set; }
		}

		public class Country
		{
			[JsonProperty("type")]
			public string Type { get; set; }

			[JsonProperty("label")]
			public Label Label { get; set; }
		}

		public class Label
		{
			[JsonProperty("eng")]
			public string Eng { get; set; }
		}

		public class Location
		{
			[JsonProperty("type")]
			public string Type { get; set; }

			[JsonProperty("label")]
			public Label Label { get; set; }

			[JsonProperty("country")]
			public Country Country { get; set; }
		}

		public class Result
		{
			[JsonProperty("uri")]
			public string Uri { get; set; }

			[JsonProperty("lang")]
			public string Lang { get; set; }

			[JsonProperty("isDuplicate")]
			public bool IsDuplicate { get; set; }

			[JsonProperty("date")]
			public string Date { get; set; }

			[JsonProperty("time")]
			public string Time { get; set; }

			[JsonProperty("dateTime")]
			public DateTime DateTime { get; set; }

			[JsonProperty("dateTimePub")]
			public DateTime DateTimePub { get; set; }

			[JsonProperty("dataType")]
			public string DataType { get; set; }

			[JsonProperty("sim")]
			public double Sim { get; set; }

			[JsonProperty("url")]
			public string Url { get; set; }

			[JsonProperty("title")]
			public string Title { get; set; }

			[JsonProperty("body")]
			public string Body { get; set; }

			[JsonProperty("source")]
			public Source Source { get; set; }

			[JsonProperty("image")]
			public string Image { get; set; }

			[JsonProperty("eventUri")]
			public string EventUri { get; set; }

			[JsonProperty("location")]
			public object Location { get; set; }

			[JsonProperty("wgt")]
			public int Wgt { get; set; }

			[JsonProperty("relevance")]
			public int Relevance { get; set; }
		}

		public class Root
		{
			[JsonProperty("articles")]
			public Articles Articles { get; set; }
		}

		public class Source
		{
			[JsonProperty("uri")]
			public string Uri { get; set; }

			[JsonProperty("dataType")]
			public string DataType { get; set; }

			[JsonProperty("location")]
			public Location Location { get; set; }

			[JsonProperty("locationValidated")]
			public bool LocationValidated { get; set; }
		}

	}
	
}
