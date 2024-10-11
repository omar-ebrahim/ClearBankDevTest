using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IPaymentService
    {
        /// <summary>
        /// Makes a payment request
        /// </summary>
        /// <param name="request">The <see cref="MakePaymentRequest"/> request</param>
        /// <returns>A <see cref="MakePaymentResult"/> object containing the success or failure</returns>
        MakePaymentResult MakePayment(MakePaymentRequest request);
    }
}
