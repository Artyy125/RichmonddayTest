using Elmah;
using RichmondDay.Context;
using RichmondDay.Interface;
using RichmondDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RichmondDay.Concrete
{
    public class Info : IInfo
    {
        enum Output
        {
            Deleted
        }
        private IRichmonddayDbContext _db;
        public Info(IRichmonddayDbContext db)
        {
            _db = db;
        }
        public List<RichmonddayInfoModel> GetAllInfo(string sortOrder)
        {
            try
            {
               List<RichmonddayInfoModel> allInfo = _db.RichmonddayInfoes.Select(r => new RichmonddayInfoModel
               {
                   Email = r.Email,
                   FirstName = r.FirstName,
                   LastName = r.LastName,
                   Id = r.Id
               }).ToList();
                switch (sortOrder)
                {
                    case "fDesc":
                        allInfo = allInfo.OrderBy(s => s.FirstName).ToList();
                        break;
                    case "lDesc":
                        allInfo = allInfo.OrderBy(s => s.LastName).ToList();
                        break;
                }
                return allInfo;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public Task<int> Save(RichmonddayInfoModel data)
        {
            try
            {
                RichmonddayInfo info = new RichmonddayInfo
                {
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Email = data.Email
                };
                _db.RichmonddayInfoes.Add(info);
                _db.SaveChanges();
                return Task.FromResult(info.Id);
            }
            catch (Exception ex)
            {
                ErrorLog.GetDefault(null).Log(new Error(ex));
                return Task.FromResult(0);
            }
        }
        public RichmonddayInfoModel Update()
        {
            try
            {
                return new RichmonddayInfoModel();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public Task<string> Delete(int id)
        {
            try
            {
                RichmonddayInfo info = _db.RichmonddayInfoes.Where(r => r.Id == id).FirstOrDefault();
                _db.RichmonddayInfoes.Remove(info);
                _db.SaveChanges();
                return Task.FromResult(Output.Deleted.ToString());
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}