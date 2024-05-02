namespace NewWebApi.Models
{
    public class PaymentRequest
    {
    public int MERCHANT_ID { get; set; }
    public decimal AMOUNT { get; set; }
    public int intid { get; set; }
    public string MERCHANT_ORDER_ID { get; set; }
    public string P_EMAIL { get; set; }
    public string P_PHONE { get; set; }
    public int CUR_ID { get; set; }
    public string SIGN { get; set; }
    public string us_key { get; set; }
    public string payer_account { get; set; }
    public decimal commission { get; set; }
    }
}