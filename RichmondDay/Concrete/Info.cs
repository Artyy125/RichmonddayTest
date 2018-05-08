using RichmondDay.Context;
using RichmondDay.Interface;
using RichmondDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RichmondDay.Concrete
{
    public class Info : IInfo
    {
        enum Output
        {
            Succeeded,
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
                    default:
                        allInfo = allInfo.OrderByDescending(s => s.LastName).ToList();
                        break;
                }
                return allInfo;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public string Save()
        {
            try
            {
                return Output.Succeeded.ToString();
            }
            catch (Exception ex)
            {

                throw;
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
        public string Delete()
        {
            try
            {
                return Output.Deleted.ToString();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}