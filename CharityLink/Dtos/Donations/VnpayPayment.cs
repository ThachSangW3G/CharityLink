using VNPAY.NET;
namespace CharityLink.Dtos.Donations
{
    public class VnpayPayment
    {
        private string _tmnCode;
        private string _hashSecret;
        private string _baseUrl;
        private string _callbackUrl;

        private readonly IVnpay _vnpay;

        public VnpayPayment(IVnpay vnpay)
        {
            // Khởi tạo giá trị cho _tmnCode, _hashSecret, _baseUrl, _callbackUrl tại đây.
            _vnpay = vnpay;
            _vnpay.Initialize(_tmnCode, _hashSecret, _baseUrl, _callbackUrl);
        }
    }
}
