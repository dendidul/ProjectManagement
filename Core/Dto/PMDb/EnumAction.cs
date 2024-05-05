using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public enum EnumAction
    {
        CreatedNew = 1,
        Edited = 2,
        Deleted = 3,
        Completed = 4,
        InProgress = 5,
        InReview = 6,
        NeedRepair = 7
    }
}
