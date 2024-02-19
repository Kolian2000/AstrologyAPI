namespace NewWebApi.Models
{
	public class Card
	{
		
		public int Id { get; set; }
		public string Name { get; set; }
		public int Number { get; set; }
		public string Suit { get; set; }
		public bool IsArcane { get; set; }
		public string PicturePath { get; set; }
		public Desc? Desc { get; set; }
		
		



	}
}