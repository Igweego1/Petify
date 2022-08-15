using Microsoft.EntityFrameworkCore;
using Petify.Abstraction;
using Petify.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Facade
{
    public class FeedbackItem : IFeedBackItem
    {
        private readonly PetifyLiveContext _context;

        public FeedbackItem(PetifyLiveContext context)
        {
            _context = context;
        }
        public void DeleteFeedBack(int Id)
        {
            var feedbackContains = _context.FeedBacks.FirstOrDefault(x => x.Id == Id);
            _context.Entry(feedbackContains).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public FeedBack GetFeedBackById(int Id)
        {
            if (Id != 0)
            {
                var result = _context.FeedBacks.Where(x => x.Id == Id).FirstOrDefault();
                return result;
            }
            else
            {
                return null;
            }
        }

        public FeedBack GetFeedBackByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<FeedBack> GetFeedBackListByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public List<FeedBack> GetListFeedBack()
        {
            return _context.FeedBacks.ToList();
        }

        public FeedBack GetValue(string name, FeedBack defaultValue)
        {
            throw new NotImplementedException();
        }

        public void SaveFeedBack(FeedBack value)
        {
            if (value != null)
            {
                _context.FeedBacks.Add(value);
                _context.SaveChanges();
            }
        }

        public void UpdateFeedBack(int Id, FeedBack value)
        {
            throw new NotImplementedException();
        }
    }

}
