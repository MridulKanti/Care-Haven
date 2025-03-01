using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Reflection;
using CareHaven.Services;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using CareHaven.Data;
using CareHaven.Exceptions;
using CareHaven.Models;


namespace CareHaven.Tests
{
    [TestFixture]
    public class Tests
    {

        private ApplicationDbContext _context;
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            _context = new ApplicationDbContext(options);

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7277");

        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
            _httpClient.Dispose(); // Dispose the HttpClient
        }

        //[Test, Order(1)]
        //public async Task Backend_Test_Post_Method_Register_Admin_Returns_HttpStatusCode_OK()
        //{
        //    ClearDatabase();
        //    string uniqueId = Guid.NewGuid().ToString();

        //    // Generate a unique userName based on a timestamp
        //    string uniqueUsername = $"abcd_{uniqueId}";
        //    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        //    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
        //    HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        //    Console.WriteLine(response.StatusCode);
        //    string responseString = await response.Content.ReadAsStringAsync();

        //    Console.WriteLine(responseString);
        //    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        //}

        //[Test, Order(2)]
        //public async Task Backend_Test_Post_Method_Login_Admin_Returns_HttpStatusCode_OK()
        //{
        //    ClearDatabase();

        //    string uniqueId = Guid.NewGuid().ToString();

        //    // Generate a unique userName based on a timestamp
        //    string uniqueUsername = $"abcd_{uniqueId}";
        //    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        //    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
        //    HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        //    // Print registration response
        //    string registerResponseBody = await response.Content.ReadAsStringAsync();
        //    Console.WriteLine("Registration Response: " + registerResponseBody);

        //    // Login with the registered user
        //    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
        //    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

        //    // Print login response
        //    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
        //    Console.WriteLine("Login Response: " + loginResponseBody);

        //    Assert.That(loginResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        //}

        //[Test, Order(3)]
        //public async Task Backend_Test_Get_All_Orphanages_With_Token_By_User_Returns_HttpStatusCode_OK()
        //{
        //    ClearDatabase();
        //    string uniqueId = Guid.NewGuid().ToString();

        //    // Generate a unique userName based on a timestamp
        //    string uniqueUsername = $"abcd_{uniqueId}";
        //    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        //    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"User\"}}";
        //    HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        //    // Print registration response
        //    string registerResponseBody = await response.Content.ReadAsStringAsync();
        //    Console.WriteLine("Registration Response: " + registerResponseBody);

        //    // Login with the registered user
        //    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
        //    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

        //    // Print login response
        //    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
        //    Console.WriteLine("Login Response: " + loginResponseBody);

        //    Assert.That(loginResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        //    string responseBody = await loginResponse.Content.ReadAsStringAsync();

        //    dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

        //    string token = responseMap.token;

        //    Assert.That(token, Is.Not.Null);

        //    // Use the token to get all feeds
        //    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        //    HttpResponseMessage apiResponse = await _httpClient.GetAsync("/api/orphanages");

        //    // Print feed response
        //    string apiResponseBody = await apiResponse.Content.ReadAsStringAsync();
        //    Console.WriteLine("apiResponseBody: " + apiResponseBody);

        //    Assert.That(apiResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        //}


        //[Test, Order(4)]
        //public async Task Backend_Test_Get_All_Orphanages_Without_Token_By_User_Returns_HttpStatusCode_Unauthorized()
        //{
        //    ClearDatabase();
        //    string uniqueId = Guid.NewGuid().ToString();

        //    // Generate a unique userName based on a timestamp
        //    string uniqueUsername = $"abcd_{uniqueId}";
        //    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        //    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"User\"}}";
        //    HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        //    // Print registration response
        //    string registerResponseBody = await response.Content.ReadAsStringAsync();
        //    Console.WriteLine("Registration Response: " + registerResponseBody);

        //    // Login with the registered user
        //    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
        //    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

        //    // Print login response
        //    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
        //    Console.WriteLine("Login Response: " + loginResponseBody);

        //    Assert.That(loginResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        //    string responseBody = await loginResponse.Content.ReadAsStringAsync();

        //    HttpResponseMessage apiResponse = await _httpClient.GetAsync("/api/orphanages");

        //    // Print feed response
        //    string apiResponseBody = await apiResponse.Content.ReadAsStringAsync();
        //    Console.WriteLine("apiResponseBody: " + apiResponseBody);

        //    Assert.That(apiResponse.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        //}

        [Test, Order(5)]
        public async Task Backend_Test_GetAll_Method_Get_All_Orphanages_In_Orphanage_Service_Fetches_All_Orphanages_Successfully()
        {
            ClearDatabase();

            // Set up orphanage data
            var orphanageData1 = new Dictionary<string, object>
    {
        { "OrphanageId", 1 },
        { "OrphanageName", "Orphanage One" },
        { "Description", "First orphanage description" },
        { "Founder", "Founder One" },
        { "EstablishmentDate", DateTime.Now },
        { "Status", "Active" }
    };

            var orphanageData2 = new Dictionary<string, object>
    {
        { "OrphanageId", 2 },
        { "OrphanageName", "Orphanage Two" },
        { "Description", "Second orphanage description" },
        { "Founder", "Founder Two" },
        { "EstablishmentDate", DateTime.Now },
        { "Status", "Inactive" }
    };

            var orphanage1 = new Orphanage();
            foreach (var kvp in orphanageData1)
            {
                var propertyInfo = typeof(Orphanage).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(orphanage1, kvp.Value);
                }
            }

            var orphanage2 = new Orphanage();
            foreach (var kvp in orphanageData2)
            {
                var propertyInfo = typeof(Orphanage).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(orphanage2, kvp.Value);
                }
            }

            _context.Orphanages.Add(orphanage1);
            _context.Orphanages.Add(orphanage2);
            await _context.SaveChangesAsync();

            // Load assembly and types
            string assemblyName = "CareHaven";
            Assembly assembly = Assembly.Load(assemblyName);
            string serviceName = "CareHaven.Services.OrphanageService";

            Type serviceType = assembly.GetType(serviceName);

            // Get the GetAllOrphanages method
            MethodInfo getAllOrphanagesMethod = serviceType.GetMethod("GetAllOrphanages");

            // Check if method exists
            if (getAllOrphanagesMethod != null)
            {
                var service = Activator.CreateInstance(serviceType, _context);
                var retrievedOrphanagesTask = (Task<IEnumerable<Orphanage>>)getAllOrphanagesMethod.Invoke(service, null);
                var retrievedOrphanages = await retrievedOrphanagesTask;

                // Assert the retrieved orphanages are not null and match the expected count
                Assert.That(retrievedOrphanages, Is.Not.Null);
                Assert.That(retrievedOrphanages.Count(), Is.EqualTo(2));
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test, Order(6)]
        public async Task Backend_Test_Get_Method_Get_Orphanage_By_Id_In_Orphanage_Service_Fetches_Orphanage_Successfully()
        {
            ClearDatabase();

            // Set up orphanage data
            var orphanageData = new Dictionary<string, object>
    {
        { "OrphanageId", 1 },
        { "OrphanageName", "Orphanage One" },
        { "Description", "First orphanage description" },
        { "Founder", "Founder One" },
        { "EstablishmentDate", DateTime.Now },
        { "Status", "Active" }
    };

            var orphanage = new Orphanage();
            foreach (var kvp in orphanageData)
            {
                var propertyInfo = typeof(Orphanage).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(orphanage, kvp.Value);
                }
            }

            _context.Orphanages.Add(orphanage);
            await _context.SaveChangesAsync();

            // Load assembly and types
            string assemblyName = "CareHaven";
            Assembly assembly = Assembly.Load(assemblyName);
            string serviceName = "CareHaven.Services.OrphanageService";

            Type serviceType = assembly.GetType(serviceName);

            // Get the GetOrphanageById method
            MethodInfo getOrphanageByIdMethod = serviceType.GetMethod("GetOrphanageById");

            // Check if method exists
            if (getOrphanageByIdMethod != null)
            {
                var service = Activator.CreateInstance(serviceType, _context);
                var retrievedOrphanageTask = (Task<Orphanage>)getOrphanageByIdMethod.Invoke(service, new object[] { 1 });
                var retrievedOrphanage = await retrievedOrphanageTask;

                // Assert the retrieved orphanage is not null and properties match
                Assert.That(retrievedOrphanage, Is.Not.Null);
                Assert.That(retrievedOrphanage.OrphanageName, Is.EqualTo(orphanage.OrphanageName));
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test, Order(7)]
        public async Task Backend_Test_Post_Method_Add_Orphanage_In_Orphanage_Service_Adds_Orphanage_Successfully()
        {
            ClearDatabase();

            // Set up orphanage data
            var orphanageData = new Dictionary<string, object>
    {
        { "OrphanageId", 1 },
        { "OrphanageName", "Orphanage One" },
        { "Description", "First orphanage description" },
        { "Founder", "Founder One" },
        { "EstablishmentDate", DateTime.Now},
        { "Status", "Active" }
    };

            var orphanage = new Orphanage();
            foreach (var kvp in orphanageData)
            {
                var propertyInfo = typeof(Orphanage).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(orphanage, kvp.Value);
                }
            }

            // Load assembly and types
            string assemblyName = "CareHaven";
            Assembly assembly = Assembly.Load(assemblyName);
            string serviceName = "CareHaven.Services.OrphanageService";

            Type serviceType = assembly.GetType(serviceName);

            // Get the AddOrphanage method
            MethodInfo addOrphanageMethod = serviceType.GetMethod("AddOrphanage");

            // Check if method exists
            if (addOrphanageMethod != null)
            {
                var service = Activator.CreateInstance(serviceType, _context);
                var addOrphanageTask = (Task<bool>)addOrphanageMethod.Invoke(service, new object[] { orphanage });
                var result = await addOrphanageTask;


                // Verify that the orphanage was added
                var retrievedOrphanage = await _context.Orphanages.FindAsync(1);

                // Assert the retrieved orphanage is not null and properties match
                Assert.That(retrievedOrphanage, Is.Not.Null);
                Assert.That(retrievedOrphanage.OrphanageName, Is.EqualTo(orphanage.OrphanageName));
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test, Order(8)]
        public async Task Backend_Test_Delete_Method_Delete_Orphanage_In_Orphanage_Service_Deletes_Orphanage_Successfully()
        {
            ClearDatabase();

            // Set up orphanage data
            var orphanageData = new Dictionary<string, object>
    {
        { "OrphanageId", 1 },
        { "OrphanageName", "Orphanage One" },
        { "Description", "First orphanage description" },
        { "Founder", "Founder One" },
        { "EstablishmentDate", DateTime.Now },
        { "Status", "Active" }
    };

            var orphanage = new Orphanage();
            foreach (var kvp in orphanageData)
            {
                var propertyInfo = typeof(Orphanage).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(orphanage, kvp.Value);
                }
            }
            _context.Orphanages.Add(orphanage);
            await _context.SaveChangesAsync();

            // Load assembly and types
            string assemblyName = "CareHaven";
            Assembly assembly = Assembly.Load(assemblyName);
            string serviceName = "CareHaven.Services.OrphanageService";

            Type serviceType = assembly.GetType(serviceName);

            // Get the DeleteOrphanage method
            MethodInfo deleteOrphanageMethod = serviceType.GetMethod("DeleteOrphanage");

            // Check if method exists
            if (deleteOrphanageMethod != null)
            {
                var service = Activator.CreateInstance(serviceType, _context);
                var deleteOrphanageTask = (Task<bool>)deleteOrphanageMethod.Invoke(service, new object[] { 1 });
                var result = await deleteOrphanageTask;

                // Verify that the orphanage was deleted
                var retrievedOrphanage = await _context.Orphanages.FindAsync(1);

                // Assert the retrieved orphanage is null, indicating deletion
                Assert.That(retrievedOrphanage, Is.Null);
            }
            else
            {
                Assert.Fail();
            }
        }



        [Test, Order(9)]
        public async Task Backend_Test_GetAll_Method_Get_All_Donations_In_Donation_Service_Fetches_All_Donations_Successfully()
        {
            ClearDatabase();

            // Set up user data
            var userData = new Dictionary<string, object>
    {
        { "UserId", 1 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "User" }
    };

            var user = new User();
            foreach (var kvp in userData)
            {
                var propertyInfo = typeof(User).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(user, kvp.Value);
                }
            }
            _context.Users.Add(user);
            _context.SaveChanges();

            // Set up orphanage data
            var orphanageData = new Dictionary<string, object>
    {
        { "OrphanageId", 1 },
        { "OrphanageName", "Orphanage One" },
        { "Description", "First orphanage description" },
        { "Founder", "Founder One" },
        { "EstablishmentDate", DateTime.Now },
        { "Status", "Active" }
    };

            var orphanage = new Orphanage();
            foreach (var kvp in orphanageData)
            {
                var propertyInfo = typeof(Orphanage).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(orphanage, kvp.Value);
                }
            }
            _context.Orphanages.Add(orphanage);
            await _context.SaveChangesAsync();

            // Set up donation data
            var donationData1 = new Dictionary<string, object>
    {
        { "DonationId", 1 },
        { "UserId", 1 },
        { "OrphanageId", 1 },
        { "Amount", 100.00m },
        { "DonationDate", DateTime.Now}
    };

            var donationData2 = new Dictionary<string, object>
    {
        { "DonationId", 2 },
        { "UserId", 1 },
        { "OrphanageId", 1 },
        { "Amount", 200.00m },
        { "DonationDate", DateTime.Now}
    };

            var donation1 = new Donation();
            foreach (var kvp in donationData1)
            {
                var propertyInfo = typeof(Donation).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(donation1, kvp.Value);
                }
            }

            var donation2 = new Donation();
            foreach (var kvp in donationData2)
            {
                var propertyInfo = typeof(Donation).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(donation2, kvp.Value);
                }
            }

            _context.Donations.Add(donation1);
            _context.Donations.Add(donation2);
            await _context.SaveChangesAsync();

            // Load assembly and types
            string assemblyName = "CareHaven";
            Assembly assembly = Assembly.Load(assemblyName);
            string serviceName = "CareHaven.Services.DonationService";

            Type serviceType = assembly.GetType(serviceName);

            // Get the GetAllDonations method
            MethodInfo getAllDonationsMethod = serviceType.GetMethod("GetAllDonations");

            // Check if method exists
            if (getAllDonationsMethod != null)
            {
                var service = Activator.CreateInstance(serviceType, _context);
                var retrievedDonationsTask = (Task<IEnumerable<Donation>>)getAllDonationsMethod.Invoke(service, null);
                var retrievedDonations = await retrievedDonationsTask;

                // Assert the retrieved donations are not null and match the expected count
                Assert.That(retrievedDonations, Is.Not.Null);
                Assert.That(retrievedDonations.Count(), Is.EqualTo(2));
            }
            else
            {
                Assert.Fail();
            }
        }


    //    [Test, Order(10)]
    //    public async Task Backend_Test_Post_Method_Add_Orphanage_In_OrphanageService_Throws_OrphanageException_If_OrphanageName_Already_Exists()
    //    {
    //        ClearDatabase();

    //        // Set up orphanage data
    //        var orphanageData = new Dictionary<string, object>
    //{
    //    { "OrphanageId", 1 },
    //    { "OrphanageName", "Orphanage One" },
    //    { "Description", "First orphanage description" },
    //    { "Founder", "Founder One" },
    //    { "EstablishmentDate", DateTime.Now },
    //    { "Status", "Active" }
    //};

    //        var orphanage = new Orphanage();
    //        foreach (var kvp in orphanageData)
    //        {
    //            var propertyInfo = typeof(Orphanage).GetProperty(kvp.Key);
    //            if (propertyInfo != null)
    //            {
    //                propertyInfo.SetValue(orphanage, kvp.Value);
    //            }
    //        }
    //        _context.Orphanages.Add(orphanage);
    //        await _context.SaveChangesAsync();

    //        // Load assembly and types
    //        string assemblyName = "CareHaven";
    //        Assembly assembly = Assembly.Load(assemblyName);
    //        string serviceName = "CareHaven.Services.OrphanageService";

    //        Type serviceType = assembly.GetType(serviceName);

    //        // Get the AddOrphanage method
    //        MethodInfo addOrphanageMethod = serviceType.GetMethod("AddOrphanage");

    //        // Check if method exists
    //        if (addOrphanageMethod != null)
    //        {
    //            var service = Activator.CreateInstance(serviceType, _context);

    //            // Attempt to add a new orphanage with the same name
    //            var newOrphanageData = new Dictionary<string, object>
    //    {
    //        { "OrphanageId", 2 },
    //        { "OrphanageName", "Orphanage One" },
    //        { "Description", "Second orphanage description" },
    //        { "Founder", "Founder Two" },
    //        { "EstablishmentDate", DateTime.Now },
    //        { "Status", "Pending" }
    //    };

    //            var newOrphanage = new Orphanage();
    //            foreach (var kvp in newOrphanageData)
    //            {
    //                var propertyInfo = typeof(Orphanage).GetProperty(kvp.Key);
    //                if (propertyInfo != null)
    //                {
    //                    propertyInfo.SetValue(newOrphanage, kvp.Value);
    //                }
    //            }

    //            try
    //            {
    //                var addOrphanageTask = (Task<bool>)addOrphanageMethod.Invoke(service, new object[] { newOrphanage });
    //                Console.WriteLine("res" + addOrphanageTask.Result);
    //                // If no exception is thrown, fail the test
    //                Assert.Fail();
    //            }
    //            catch (Exception ex)
    //            {
    //                Assert.That(ex.InnerException, Is.Not.Null);
    //                Assert.That(ex.InnerException, Is.InstanceOf<OrphanageException>());
    //                Assert.That(ex.InnerException.Message, Is.EqualTo("Orphanage with the same name already exists"));
    //            }
    //        }
    //        else
    //        {
    //            Assert.Fail();
    //        }
    //    }

    //    [Test, Order(11)]
    //    public async Task Backend_Test_Post_Method_AddFeedback_In_Feedback_Service_Posts_Successfully()
    //    {
    //        ClearDatabase();

    //        // Add user
    //        var userData = new Dictionary<string, object>
    //{
    //    { "UserId",42 },
    //    { "Username", "testuser" },
    //    { "Password", "testpassword" },
    //    { "Email", "test@example.com" },
    //    { "MobileNumber", "1234567890" },
    //    { "UserRole", "User" }
    //};

    //        var user = new User();
    //        foreach (var kvp in userData)
    //        {
    //            var propertyInfo = typeof(User).GetProperty(kvp.Key);
    //            if (propertyInfo != null)
    //            {
    //                propertyInfo.SetValue(user, kvp.Value);
    //            }
    //        }
    //        _context.Users.Add(user);
    //        _context.SaveChanges();
    //        // Add loan application
    //        string assemblyName = "CareHaven";
    //        Assembly assembly = Assembly.Load(assemblyName);
    //        string ServiceName = "CareHaven.Services.FeedbackService";
    //        string typeName = "CareHaven.Models.Feedback";

    //        Type serviceType = assembly.GetType(ServiceName);
    //        Type modelType = assembly.GetType(typeName);

    //        MethodInfo method = serviceType.GetMethod("AddFeedback", new[] { modelType });

    //        if (method != null)
    //        {
    //            var feedbackData = new Dictionary<string, object>
    //        {
    //            { "FeedbackId", 11 },
    //            { "UserId", 42 },
    //            { "FeedbackText", "Great experience!" },
    //            { "Date", DateTime.Now }
    //        };
    //            var feedback = new Feedback();
    //            foreach (var kvp in feedbackData)
    //            {
    //                var propertyInfo = typeof(Feedback).GetProperty(kvp.Key);
    //                if (propertyInfo != null)
    //                {
    //                    propertyInfo.SetValue(feedback, kvp.Value);
    //                }
    //            }
    //            var service = Activator.CreateInstance(serviceType, _context);
    //            var result = (Task<bool>)method.Invoke(service, new object[] { feedback });

    //            var addedFeedback = await _context.Feedbacks.FindAsync(11);
    //            Assert.That(addedFeedback, Is.Not.Null);
    //            Assert.That(addedFeedback.FeedbackText, Is.EqualTo("Great experience!"));
    //        }
    //        else
    //        {
    //            Assert.Fail();
    //        }
    //    }

        [Test, Order(12)]
        public async Task Backend_Test_Delete_Method_Feedback_In_Feeback_Service_Deletes_Successfully()
        {
            // Add user
            ClearDatabase();

            var userData = new Dictionary<string, object>
    {
        { "UserId",42 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "User" }
    };

            var user = new User();
            foreach (var kvp in userData)
            {
                var propertyInfo = typeof(User).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(user, kvp.Value);
                }
            }
            _context.Users.Add(user);
            _context.SaveChanges();

            var feedbackData = new Dictionary<string, object>
            {
                { "FeedbackId", 11 },
                { "UserId", 42 },
                { "FeedbackText", "Great experience!" },
                { "Date", DateTime.Now }
            };
            var feedback = new Feedback();
            foreach (var kvp in feedbackData)
            {
                var propertyInfo = typeof(Feedback).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(feedback, kvp.Value);
                }
            }
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
            // Add loan application
            string assemblyName = "CareHaven";
            Assembly assembly = Assembly.Load(assemblyName);
            string ServiceName = "CareHaven.Services.FeedbackService";
            string typeName = "CareHaven.Models.Feedback";

            Type serviceType = assembly.GetType(ServiceName);
            Type modelType = assembly.GetType(typeName);


            MethodInfo deletemethod = serviceType.GetMethod("DeleteFeedback", new[] { typeof(int) });

            if (deletemethod != null)
            {
                var service = Activator.CreateInstance(serviceType, _context);
                var deleteResult = (Task<bool>)deletemethod.Invoke(service, new object[] { 11 });

                var deletedFeedbackFromDb = await _context.Feedbacks.FindAsync(11);
                Assert.That(deletedFeedbackFromDb, Is.Null);
            }
            else
            {
                Assert.Fail();
            }
        }

    //    [Test, Order(13)]
    //    public async Task Backend_Test_Get_Method_GetFeedbacksByUserId_In_Feedback_Service_Fetches_Successfully()
    //    {
    //        ClearDatabase();

    //        // Add user
    //        var userData = new Dictionary<string, object>
    //{
    //    { "UserId", 330 },
    //    { "Username", "testuser" },
    //    { "Password", "testpassword" },
    //    { "Email", "test@example.com" },
    //    { "MobileNumber", "1234567890" },
    //    { "UserRole", "User" }
    //};

    //        var user = new User();
    //        foreach (var kvp in userData)
    //        {
    //            var propertyInfo = typeof(User).GetProperty(kvp.Key);
    //            if (propertyInfo != null)
    //            {
    //                propertyInfo.SetValue(user, kvp.Value);
    //            }
    //        }
    //        _context.Users.Add(user);
    //        _context.SaveChanges();

    //        var feedbackData = new Dictionary<string, object>
    //{
    //    { "FeedbackId", 13 },
    //    { "UserId", 330 },
    //    { "FeedbackText", "Great experience!" },
    //    { "Date", DateTime.Now }
    //};

    //        var feedback = new Feedback();
    //        foreach (var kvp in feedbackData)
    //        {
    //            var propertyInfo = typeof(Feedback).GetProperty(kvp.Key);
    //            if (propertyInfo != null)
    //            {
    //                propertyInfo.SetValue(feedback, kvp.Value);
    //            }
    //        }
    //        _context.Feedbacks.Add(feedback);
    //        _context.SaveChanges();

    //        // Add loan application
    //        string assemblyName = "CareHaven";
    //        Assembly assembly = Assembly.Load(assemblyName);
    //        string ServiceName = "CareHaven.Services.FeedbackService";
    //        string typeName = "CareHaven.Models.Feedback";

    //        Type serviceType = assembly.GetType(ServiceName);
    //        Type modelType = assembly.GetType(typeName);

    //        MethodInfo method = serviceType.GetMethod("GetFeedbacksByUserId");

    //        if (method != null)
    //        {
    //            var service = Activator.CreateInstance(serviceType, _context);
    //            var result = (Task<IEnumerable<Feedback>>)method.Invoke(service, new object[] { 330 });
    //            Assert.That(result, Is.Not.Null); var check = true;
    //            foreach (var item in result.Result)
    //            {
    //                check = false;
    //                Assert.That(item.FeedbackText, Is.EqualTo("Great experience!"));

    //            }
    //            if (check == true)
    //            {
    //                Assert.Fail();

    //            }
    //        }
    //        else
    //        {
    //            Assert.Fail();
    //        }
    //    }

        private void ClearDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

    }
}