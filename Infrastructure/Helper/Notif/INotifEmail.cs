using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper.Notif
{
    public interface INotifEmail
    {
        bool SendEmail(string mailFrom, string mailTo, string emailSubject, string emailMessage);
     }
}
