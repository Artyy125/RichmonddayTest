using RichmondDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichmondDay.Interface
{
    public interface IInfo
    {
        List<RichmonddayInfoModel> GetAllInfo(string sortOrder);
        Task<int> Save(RichmonddayInfoModel data);
        RichmonddayInfoModel Update();
        Task<string> Delete(int id);

    }
}
