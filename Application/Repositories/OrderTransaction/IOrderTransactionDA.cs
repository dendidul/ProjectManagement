using Core.Dto.OrderTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.OrderTransaction
{
    public interface IOrderTransactionDA
    {
        List<TransactionHeader> GetUnsentPointOrder();
        List<TransactionDetailsProduct> GetDetailsTransaction(Int64 orderTransactionId);
        void UpdateResultSendPoint(Int64 OrderTransactionId, string status, int point, string errorMessage);
    }
}
