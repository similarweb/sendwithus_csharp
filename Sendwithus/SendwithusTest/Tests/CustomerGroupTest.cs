using NUnit.Framework;
using Sendwithus;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace SendwithusTest
{
    /// <summary>
    /// Unit testing class for the Customer Groups API calls
    /// </summary>
    [TestFixture]
    public class CustomerGroupTest
    {
        private const string DEFAULT_CUSTOMER_GROUP_ID = "grp_7zpRYpExEBPpd6dGvyAfcT";
        private const string INVALID_CUSTOMER_GROUP_ID = "invalid_customer_group_id";
        private const string DEFAULT_CUSTOMER_GROUP_NAME = "new_group";
        private const string DEFAULT_CUSTOMER_GROUP_DESCRIPTION = "a description of the group";

        /// <summary>
        /// Sets the API 
        /// </summary>
        [SetUp]
        public void InitializeUnitTesting()
        {
            // Set the API key
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
        }

        /// <summary>
        /// Tests the API call GET /groups
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Test]
        public async Task TestGetCustomerGroupsAsync()
        {
            Trace.WriteLine("GET /groups");

            // Make the API call
            try
            {
                var customerGroupResponseMultipleGroups = await CustomerGroup.GetCustomeGroupsAsync();

                // Validate the response
                SendwithusClientTest.ValidateResponse(customerGroupResponseMultipleGroups);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /groups with only the group name
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Test]
        public async Task TestCreateCustomerGroupWithNameOnlyAsync()
        {
            Trace.WriteLine("POST /groups with only the group name");

            // Make the API call
            var groupName = String.Format("{0}_{1}", DEFAULT_CUSTOMER_GROUP_NAME, SendwithusClientTest.RandomString(10));
            try
            {
                var customerGroupResponse = await CustomerGroup.CreateCustomerGroupAsync(groupName);

                // Validate the response
                SendwithusClientTest.ValidateResponse(customerGroupResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /groups with a name and description
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Test]
        public async Task TestCreateCustomerGroupWithNameAndDescriptionAsync()
        {
            Trace.WriteLine("POST /groups with a name and description");

            // Make the API call
            var groupName = String.Format("{0}_{1}", DEFAULT_CUSTOMER_GROUP_NAME, SendwithusClientTest.RandomString(10));
            try
            {
                var customerGroupResponse = await CustomerGroup.CreateCustomerGroupAsync(groupName, DEFAULT_CUSTOMER_GROUP_DESCRIPTION);

                // Validate the response
                SendwithusClientTest.ValidateResponse(customerGroupResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call PUT /groups/(:group_id) with only the name of the group
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Test]
        public async Task TestUpdateCustomerGroupNameAsync()
        {
            Trace.WriteLine(String.Format("PUT /groups/{0} with a new name", DEFAULT_CUSTOMER_GROUP_ID));

            // Make the API call
            var groupName = String.Format("{0}_{1}", DEFAULT_CUSTOMER_GROUP_NAME, SendwithusClientTest.RandomString(10));
            try
            {
                var customerGroupResponse = await CustomerGroup.UpdateCustomerGroupNameAsync(DEFAULT_CUSTOMER_GROUP_ID, groupName);

                // Validate the response
                SendwithusClientTest.ValidateResponse(customerGroupResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call PUT /groups/(:group_id) with only the description of the group
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Test]
        public async Task TestUpdateCustomerGroupDescriptionAsync()
        {
            Trace.WriteLine(String.Format("PUT /groups/{0} with a new description", DEFAULT_CUSTOMER_GROUP_ID));

            // Make the API call
            var groupDescription = String.Format("{0}_{1}", DEFAULT_CUSTOMER_GROUP_DESCRIPTION, SendwithusClientTest.RandomString(10));
            try
            {
                var customerGroupResponse = await CustomerGroup.UpdateCustomerGroupDescriptionAsync(DEFAULT_CUSTOMER_GROUP_ID, groupDescription);

                // Validate the response
                SendwithusClientTest.ValidateResponse(customerGroupResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call PUT /groups/(:group_id) with the name and description of the group
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Test]
        public async Task TestUpdateCustomerGroupNameAndDescriptionAsync()
        {
            Trace.WriteLine(String.Format("PUT /groups/{0} with a new name and description", DEFAULT_CUSTOMER_GROUP_ID));

            // Make the API call
            var groupName = String.Format("{0}_{1}", DEFAULT_CUSTOMER_GROUP_NAME, SendwithusClientTest.RandomString(10));
            var groupDescription = String.Format("{0}_{1}", DEFAULT_CUSTOMER_GROUP_DESCRIPTION, SendwithusClientTest.RandomString(10));
            try
            {
                var customerGroupResponse = await CustomerGroup.UpdateCustomerGroupNameAndDescriptionAsync(DEFAULT_CUSTOMER_GROUP_ID, groupName, groupDescription);

                // Validate the response
                SendwithusClientTest.ValidateResponse(customerGroupResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call PUT /groups/(:group_id) with an invalid customer group ID
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Test]
        public async Task TestUpdateCustomerGroupNameInvalidGroupIdAsync()
        {
            Trace.WriteLine(String.Format("PUT /groups/{0} with an invalid customer group ID", INVALID_CUSTOMER_GROUP_ID));

            // Make the API call
            var groupName = String.Format("{0}_{1}", DEFAULT_CUSTOMER_GROUP_NAME, SendwithusClientTest.RandomString(10));
            try
            {
                var customerGroupResponse = await CustomerGroup.UpdateCustomerGroupNameAsync(INVALID_CUSTOMER_GROUP_ID, groupName);
            }
            catch (AggregateException exception)
            {
                // Make sure the response was HTTP 400 Bad Request 
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Tests the API call DELETE /groups/(:group_id)
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Test]
        public async Task TestDeleteCustomerGroupAsync()
        {

            // Add a new customer group so that it can be deleted for this test
            var groupName = String.Format("{0}_{1}", DEFAULT_CUSTOMER_GROUP_NAME, SendwithusClientTest.RandomString(10));
            var customerGroupResponse = await CustomerGroup.CreateCustomerGroupAsync(groupName);
            var groupId = customerGroupResponse.group.id;

            // Make the API call
            Trace.WriteLine(String.Format("DELETE /groups/{0}", groupId));
            try
            {
                var genericApiCallStatus = await CustomerGroup.DeleteCustomerGroupAsync(groupId);

                // Validate the response
                SendwithusClientTest.ValidateResponse(genericApiCallStatus);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }
    }
}
