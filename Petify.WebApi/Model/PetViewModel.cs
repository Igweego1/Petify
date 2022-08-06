﻿namespace Petify.WebApi.Model
{
    public class PetViewModel
    {
        public string? PetName { get; set; }
        public int PetTypeId { get; set; }
        public int? PetGenderId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int AllergyId { get; set; }
        public string? Description { get; set; }
        public string? PetAddress { get; set; }
        public string? PetCity { get; set; }
        public string? CreatedBy { get; set; }
        public string? UserId { get; set; }
        public DateTime Created { get; set; }
        public string? Image { get; set; }
    }


   
}
