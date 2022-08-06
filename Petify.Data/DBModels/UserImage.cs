using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class UserImage
    {
        public string Id { get; set; } = null!;
        public byte[]? UserPetImage { get; set; }
    }
}
