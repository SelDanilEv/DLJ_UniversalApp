namespace Infrastructure.Models
{
    public class MenuItem : BaseModel
    {
        public string Name { get; set; }

        public ActionLink Link { get; set; } 

        public bool IsBeta { get; set; }

        public int Priority { get; set; }
    }
}
