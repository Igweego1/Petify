using Petify.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Abstraction
{
    public interface IFeedBackItem
    {
        void SaveFeedBack(FeedBack value);
        void DeleteFeedBack(int Id);
        void UpdateFeedBack(int Id, FeedBack value);
        FeedBack GetFeedBackByName(string name);
        FeedBack GetValue(string name, FeedBack defaultValue);
        FeedBack GetFeedBackById(int Id);
        List<FeedBack> GetListFeedBack();
        List<FeedBack> GetFeedBackListByUserId(string userId);
    }
}
