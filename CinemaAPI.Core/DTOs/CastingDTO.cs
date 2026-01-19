using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.Core.DTOs
{
    public class CastingDTO
    {
        public int Id { get; set; }
        
        [Required]
        public int PersonId { get; set; }
        
        [Required]
        public int FilmId { get; set; }
        
        [Required]
        public int OccupationId { get; set; }
        
        // Navigation properties for API responses
        public PersonDTO Person { get; set; }
        public FilmDTO Film { get; set; }
        public OccupationDTO Occupation { get; set; }
    }
}