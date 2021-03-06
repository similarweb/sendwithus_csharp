using NUnit.Framework;
using Sendwithus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace SendwithusTest
{
    /// <summary>
    /// Unit testing class for the Customer API calls
    /// </summary>
    [TestFixture]
    public class CustomerTest
    {
        public const string DEFAULT_CUSTOMER_EMAIL_ADDRESS = "sendwithus.test@gmail.com";
        public const string NEW_CUSTOMER_EMAIL_ADDRESS = "sendwithus.test+new@gmail.com";
        private const string INVALID_CUSTOMER_EMAIL_ADDRESS = "invalid_email_address";
        private const string DEFAULT_CUSTOMER_LOCALE = "de-DE";
        private const Int64 LOG_CREATED_AFTER_TIME = 1234567890;
        private const Int64 LOG_CREATED_BEFORE_TIME = 9876543210;

        /// <summary>
        /// Sets the API
        /// </summary>
        [SetUp]
        public void InitializeUnitTesting()
        {
            // Set the API key
            SendwithusClient.ApiKey = "CSHARP_CUSTOMER_TEST_API_KEY";
        }

        /// <summary>
        /// Tests the API call GET /customers/customer@example.com
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestGetCustomerAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("GET /customers/{0}", DEFAULT_CUSTOMER_EMAIL_ADDRESS));
            try
            {
                var customerResponse = await Customer.GetCustomerAsync(DEFAULT_CUSTOMER_EMAIL_ADDRESS);

                // Validate the response
                SendwithusClientTest.ValidateResponse(customerResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call GET /customers/customer@example.com with an invalid email address
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestGetCustomerWithInvalidEmailAddressAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("GET /customers/{0}", INVALID_CUSTOMER_EMAIL_ADDRESS));
            try
            {
                var customerResponse = await Customer.GetCustomerAsync(INVALID_CUSTOMER_EMAIL_ADDRESS);
                Assert.Fail("Failed to throw exception");
            }
            catch (AggregateException exception)
            {
                // Make sure the response was HTTP 400 Bad Request
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Tests the API call POST /customers with the minimum parameters
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestCreateOrUpdateCustomerWithMinimumParametersAsync()
        {
            Trace.WriteLine("POST /customers");

            // Build the new customer and send the create customer request
            try
            {
                // Build the customer
                var customer = new Customer(NEW_CUSTOMER_EMAIL_ADDRESS);

                // Make the API call
                var genericApiCallStatus = await Customer.CreateOrUpdateCustomerAsync(customer);

                // Validate the response
                SendwithusClientTest.ValidateResponse(genericApiCallStatus);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /customers with all parameters
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestCreateOrUpdateCustomerWithAllParametersAsync()
        {
            Trace.WriteLine("POST /customers");

            // Build the new customer and send the create customer request
            try
            {
                var genericApiCallStatus = await BuildAndSendCreateCustomerRequest();

                // Validate the response
                SendwithusClientTest.ValidateResponse(genericApiCallStatus);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call DELETE /customers/(:email)
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestDeleteCustomerAsync()
        {
            Trace.WriteLine(String.Format("DELETE /customers", NEW_CUSTOMER_EMAIL_ADDRESS));

            // Make the API call
            try
            {
                var genericApiCallStatus = await Customer.DeleteCustomerAsync(NEW_CUSTOMER_EMAIL_ADDRESS);

                // Validate the response
                SendwithusClientTest.ValidateResponse(genericApiCallStatus);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call GET /customers/matt@sendwithus.com/logs?count={count}&created_lt={timestamp}&created_gt={timestamp} with no parameters
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestGetCustomerEmailLogsWithNoParametersAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("GET /customers/{0}/logs", DEFAULT_CUSTOMER_EMAIL_ADDRESS));
            try
            {
                var customerEmailLogsResponse = await Customer.GetCustomerEmailLogsAsync(DEFAULT_CUSTOMER_EMAIL_ADDRESS);

                // Validate the response
                SendwithusClientTest.ValidateResponse(customerEmailLogsResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call GET /customers/matt@sendwithus.com/logs?count={count}&created_lt={timestamp}&created_gt={timestamp} with all parameters
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestGetCustomerEmailLogsWithAllParametersAsync()
        {

            Trace.WriteLine(String.Format("GET /customers/{0}/logs", DEFAULT_CUSTOMER_EMAIL_ADDRESS));

            // Build the query parameters
            var queryParameters = new Dictionary<string, object>();
            queryParameters.Add("count", 2);
            queryParameters.Add("created_lt", LOG_CREATED_BEFORE_TIME);
            queryParameters.Add("created_gt", LOG_CREATED_AFTER_TIME);

            // Make the API call
            try
            {
                var customerEmailLogsResponse = await Customer.GetCustomerEmailLogsAsync(DEFAULT_CUSTOMER_EMAIL_ADDRESS, queryParameters);

                // Validate the response
                SendwithusClientTest.ValidateResponse(customerEmailLogsResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Builds a new customer and sends the create customer API request
        /// </summary>
        /// <returns>The API response to the Create Customer call</returns>
        public static async Task<GenericApiCallStatus> BuildAndSendCreateCustomerRequest()
        {
            // Build the customer
            var customer = new Customer(NEW_CUSTOMER_EMAIL_ADDRESS);
            customer.data.Add("first_name", "Matt");
            customer.data.Add("city", "San Francisco");
            customer.locale = DEFAULT_CUSTOMER_LOCALE;

            // Make the API call
            return await Customer.CreateOrUpdateCustomerAsync(customer);
        }
    }
}
