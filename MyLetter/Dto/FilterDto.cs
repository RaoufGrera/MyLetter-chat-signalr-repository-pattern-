using MyLetter.EF.Models;

namespace MyLetter.Dto
{
    public class FilterDto
    {
        public List<Home> home { get; set; }
        public List<Education> education { get; set; }
        public List<Sector> secotr { get; set; }
    }
}
