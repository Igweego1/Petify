namespace Petify.WebApi.Model
{
    public class FeedBackViewModel
    {
        public string? Message { get; set; }
        public string? CreatedBy { get; set; }
        public string? UserId { get; set; }
        public DateTime Created { get; set; }
    }
}
