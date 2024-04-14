using Core.Dto.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Notification
{
    public interface IFPRSBlastLogDA
    {
        long Insert(InsertFPRSBlastLog request);
        void UpsertFPRSBlastSummary(FPRSBlastSummary request);
    }
}
