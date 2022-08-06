using Petify.Core.Model;

namespace Petify.Consume.Models
{
    public class AuthModelComponents
    {
        public List<RegisterViewModel> register { get; set; }

        public RegisterViewModel user { get; set; }
    }

    public class PetProfileComponents
    {
        public List<PetProfileViewModel> profiles { get; set; }

        public PetProfileViewModel profile { get; set; }
    }
}
