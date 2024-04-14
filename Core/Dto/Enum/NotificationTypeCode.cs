using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Enum
{
    public enum NotificationTypeCode
    {
        Link_Upload = 102101,
        blastMessage = 104101,
        blastMessageAfterFailWA = 104102,
        blastMessageAfterFailWAFPE= 104103,
        blastMessageFPE = 104104,
        blastMessageWA = 104105,
        blastMessageWAFPE = 104106,
        reminderMessage = 104107,
        reminderMessageWA = 104108,
        retrySendSMS = 104109


    }
}
