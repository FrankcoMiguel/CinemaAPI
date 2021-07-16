namespace CinemaAPI.Core.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Description { get; set; }
        public int? Age { get; set; }
        public string IsActive { get; set; }
        public string PictureReference { get; set; }
    }
}
