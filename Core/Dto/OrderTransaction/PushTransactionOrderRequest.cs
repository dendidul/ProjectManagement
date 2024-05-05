using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.OrderTransaction
{
    public class PushTransactionOrderRequest
    {

        public string fileNo { get; set; }
        public string cardNumber { get; set; }
        public string contactCode { get; set; }
        public string contactName { get; set; }
        public string phoneNo { get; set; }
        public string eventCode { get; set; }
        public bool isAutoApprove { get; set; }
        public List<PushTransactionOrderDetailRequest> details { get; set; }

    }

    public class PushTransactionOrderDetailRequest
    {

        public string receiptNo { get; set; }
        public DateTime receiptdate { get; set; }
        public decimal totalPrice { get; set; }
        public string outletId { get; set; }
        public string outletName { get; set; }
        public string cabangId { get; set; }

        public List<PushTransactionOrderDetailProductRequest> detailProducts { get; set; }


    }

    public class PushTransactionOrderDetailProductRequest
    {

        public string productCode { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public decimal totalPrice { get; set; }



    }

    public class TransactionHeader
    {
        public Int64 orderTransactionId { get; set; }
        public string noSo { get; set; }
        public string contactCode { get; set; }
        public string cardNumber { get; set; }
        public string cardName { get; set; }
        public string phoneNo { get; set; }
        public decimal totalPrice { get; set; }

        public string outletId { get; set; }
        public DateTime receiptDate { get; set; }
        public string outletName { get; set; }
        public string cabangId { get; set; }
    }

    public class TransactionDetailsProduct
    {
        public Int64 orderTransactionProductId { get; set; }
        public string sku { get; set; }
        public int quantity { get; set; }
        public decimal priceNominal { get; set; }
        public decimal totalPrice { get; set; }
    }



}
