using Application.ExternalAPI.LoyaltyAPI;
using Application.ExternalAPI.NewKalbeConnect;
using Application.Repositories.CustomerLink;
using Application.Repositories.Notification;
using Application.Repositories.OrderTransaction;
using Core.Dto.Common;
using Core.Dto.CustomerLink;
using Core.Dto.Enum;
using Core.Dto.ExternalAPI.NewKalbeconnect;
using Core.Dto.Notification;
using Core.Dto.OrderTransaction;
using Newtonsoft.Json;
using OpenTracing;
using OpenTracing.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Executor
{
    public interface IQueuedExecutor
    {
        bool Run();
    }

    public class QueuedExecutor : IQueuedExecutor
    {
        private INewKalbeConnectAPI _newKalbeConnectAPI;

        private ILoyaltyAPI _loyaltyAPI;
        private ICustomerLinkDA _customerLinkDA;
        private INotificationDA _notificationDA;
        private IFPRSBlastLogDA _fPRSBlastLogDA;
        private IOrderTransactionDA _orderTransactionDA;

        public QueuedExecutor(INewKalbeConnectAPI newKalbeConnectAPI, ILoyaltyAPI loyaltyAPI, ICustomerLinkDA customerLinkDA, INotificationDA notificationDA, IFPRSBlastLogDA fPRSBlastLogDA, IOrderTransactionDA orderTransactionDA)
        {

            _loyaltyAPI = loyaltyAPI;
            _customerLinkDA = customerLinkDA;
            _notificationDA = notificationDA;
            _fPRSBlastLogDA = fPRSBlastLogDA;
            _newKalbeConnectAPI = newKalbeConnectAPI;
            _orderTransactionDA = orderTransactionDA;
        }

        public bool Run()
        {

            try
            {

                var logs = new LogAPI();

                Console.WriteLine("=================================================================");
                Console.WriteLine("Get Data");
                //fprs_blast_log_summary where phone_number = '082130687990'

                // var getdata = _customerLinkDA.GetUnsentLink();

                var getdata = _orderTransactionDA.GetUnsentPointOrder();
                // var getdata = _customerLinkDA.GetUnsentLink().Where(x => x.PhoneNo == "082130687990").ToList();


                var jumlahdata = getdata != null ? getdata.Count : 0;

                Console.WriteLine("jumlah data : " + jumlahdata);

                Console.WriteLine("=================================================================");

                var returnValue = new List<Task<bool>>();

                var splitBlastlinkList = SplitList(getdata, 5);

                foreach (var BlastLinkList in splitBlastlinkList)
                {
                    if (BlastLinkList != null && BlastLinkList.Count > 0)
                    {
                        foreach (var i in BlastLinkList)
                        {
                            //var errorMessage = string.Empty;
                            //var actionName = "QueuedExecutor/Run";
                            //using var scope = _tracer.BuildSpan(actionName).StartActive(true);
                            //string traceId = _tracer.ActiveSpan.Context.TraceId;

                            try
                            {




                                returnValue.Add(Task.Factory.StartNew(() =>
                                {

                                    PushTransactionOrderRequest request = new PushTransactionOrderRequest();

                                    List<PushTransactionOrderDetailProductRequest> ListdetailsProduct = new List<PushTransactionOrderDetailProductRequest>();

                                    PushTransactionOrderDetailProductRequest detailProduct = new PushTransactionOrderDetailProductRequest();

                                    List<PushTransactionOrderDetailRequest> ListdetailRequest = new List<PushTransactionOrderDetailRequest>();

                                    PushTransactionOrderDetailRequest detailRequest = new PushTransactionOrderDetailRequest();

                                    List<TransactionDetailsProduct> ListTransactionDetails = new List<TransactionDetailsProduct>();

                                    List<PushTransactionOrderDetailProductRequest> ListPushTransactionDetailsProduct = new List<PushTransactionOrderDetailProductRequest>();


                                    var transactionDetailsProduct = _orderTransactionDA.GetDetailsTransaction(i.orderTransactionId);

                                    foreach (var j in transactionDetailsProduct)
                                    {
                                        TransactionDetailsProduct product = new TransactionDetailsProduct();

                                        product.sku = j.sku;
                                        product.quantity = j.quantity;
                                        product.priceNominal = j.priceNominal;
                                        product.totalPrice = j.totalPrice;

                                        ListTransactionDetails.Add(product);

                                    }

                                    foreach (var k in transactionDetailsProduct)
                                    {
                                        PushTransactionOrderDetailProductRequest dataRequestProduct = new PushTransactionOrderDetailProductRequest();

                                        dataRequestProduct.productCode = k.sku;
                                        dataRequestProduct.quantity = k.quantity;
                                        dataRequestProduct.price = k.priceNominal;
                                        dataRequestProduct.totalPrice = k.totalPrice;

                                        ListPushTransactionDetailsProduct.Add(dataRequestProduct);

                                    }



                                    detailRequest.receiptNo = i.noSo;
                                    detailRequest.receiptdate = Convert.ToDateTime(i.receiptDate);
                                    detailRequest.totalPrice = i.totalPrice;
                                    detailRequest.outletId = i.outletId;
                                    detailRequest.outletName = i.outletName;
                                    detailRequest.cabangId = i.cabangId;
                                    detailRequest.detailProducts = ListPushTransactionDetailsProduct;
                                    ListdetailRequest.Add(detailRequest);


                                    request.phoneNo = i.phoneNo;
                                    request.cardNumber = i.cardNumber;
                                    request.fileNo = i.noSo;
                                    request.contactCode = i.contactCode;
                                    request.contactName = i.cardName;
                                    request.eventCode = "";
                                    request.isAutoApprove = false;
                                    request.details = ListdetailRequest;


                                    var result = _loyaltyAPI.PushTransactionOrder(request);

                                    //106102  106 Berhasil Dikirim
                                    //106103  106 Gagal Dikirim

                                    if (result != null)
                                    {
                                        if (result.Status.Code == "200")
                                        {

                                            _orderTransactionDA.UpdateResultSendPoint(
                                                i.orderTransactionId,
                                                "106102",
                                                result.Data != null ? Convert.ToInt32(result.Data.point.pointTransaction) : 0,
                                                "Success"
                                            );



                                            //_orderTransactionDA.UpdateResultSendPoint(
                                            //    i.orderTransactionId,
                                            //    "106101",
                                            //    0,
                                            //    result.Status != null ? result.Status.MessageInd.ToString() : ""
                                            //);

                                            Console.WriteLine("\nPengiriman point untuk no. so " + i.noSo + " telah berhasil");

                                        }
                                        else
                                        {
                                            _orderTransactionDA.UpdateResultSendPoint(
                                                i.orderTransactionId,
                                                "106103",
                                                0,
                                                result.Status != null ? result.Status.Errors.FirstOrDefault().Message.ToString() : ""
                                            );

                                            //_orderTransactionDA.UpdateResultSendPoint(
                                            //    i.orderTransactionId,
                                            //    "106101",
                                            //    0,
                                            //    result.Status != null ? result.Status.MessageInd.ToString() : ""
                                            //);

                                            Console.WriteLine("\n Pengiriman point untuk no. so " + i.noSo + " telah gagal karena " + result.Status.Errors.FirstOrDefault().Message.ToString());
                                        }
                                    }



                                  

                                    return true;

                                }));



                            }
                            catch (Exception ex)
                            {
                                logs.ResponseParam = ex.Message;
                                logs.ResponseDate = DateTime.Now;

                                Console.WriteLine("\n========= error pada order  " + i.noSo + "with error : "+ex.Message);


                            }






                        }
                        Task.WaitAll(returnValue.ToArray());


                    }

                    else
                    {
                        Console.WriteLine("antrian send point kosong ");
                    }


                }


                Console.WriteLine("\n=================================================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine("terjadi error saat send point :" + ex.Message);
            }





            return true;

        }


        public static List<List<TransactionHeader>> SplitList(List<TransactionHeader> listAll, int nSize = 10)
        {
            var list = new List<List<TransactionHeader>>();

            for (int i = 0; i < listAll.Count; i += nSize)
            {
                list.Add(listAll.GetRange(i, Math.Min(nSize, listAll.Count - i)));
            }

            return list;
        }












    }



}
