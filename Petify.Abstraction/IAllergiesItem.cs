using Petify.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Abstraction
{
    public interface IAllergiesItem
    {
        void SaveAllergy(Allergy value);
        void DeleteAllergy(int Id);
        void UpdateAllergy(int Id, Allergy value);
        Allergy GetAllergyByName(string name);
        Allergy GetValue(string name, Allergy defaultValue);
        Allergy GetAllergyById(int Id);
        List<Allergy> GetListAllergy();
        List<Allergy> GetAllergyListByUserId(string userId);
    }
}
