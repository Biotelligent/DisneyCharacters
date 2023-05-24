using System;
namespace DisneyCharacters.Model
{
	public class CharacterModel
	{
		public long Id { get; set; }
        public string Name { get; set; }
        public Uri ImageUrl { get; set; }
		public string[] Films { get; set; }
        public string[] ShortFilms { get; set; }
        public string[] ParkAttractions { get; set; }

		/// <summary>
		/// Map from the DTO to the subset of elements we use.
		/// Another typical copy method is to access parent.MemberwiseClone();
		/// </summary>
		/// <param name="map"></param>
		public CharacterModel(CharacterDataDto map)
		{
			this.Id = map.Id;
			this.Name = map.Name;
			this.ImageUrl = map.ImageUrl;
			this.Films = map.Films;
			this.ShortFilms = map.ShortFilms;
			this.ParkAttractions = map.ParkAttractions;

		}

		public int Popularity => Films.Length + ShortFilms.Length + ParkAttractions.Length;
	}
}

