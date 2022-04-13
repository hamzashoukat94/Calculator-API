// <copyright file="SimpleCalculatorControllerTest.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace FirstAPICalculator.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using FirstAPICalculator.Standard;
    using FirstAPICalculator.Standard.Controllers;
    using FirstAPICalculator.Standard.Exceptions;
    using FirstAPICalculator.Standard.Http.Client;
    using FirstAPICalculator.Standard.Http.Response;
    using FirstAPICalculator.Standard.Utilities;
    using FirstAPICalculator.Tests.Helpers;
    using Newtonsoft.Json.Converters;
    using NUnit.Framework;

    /// <summary>
    /// SimpleCalculatorControllerTest.
    /// </summary>
    [TestFixture]
    public class SimpleCalculatorControllerTest : ControllerTestBase
    {
        /// <summary>
        /// Controller instance (for all tests).
        /// </summary>
        private SimpleCalculatorController controller;

        /// <summary>
        /// Setup test class.
        /// </summary>
        [OneTimeSetUp]
        public void SetUpDerived()
        {
            this.controller = this.Client.SimpleCalculatorController;
        }

        /// <summary>
        /// Test the addition of values.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task TestTestSum()
        {
            // Parameters for the API call
            Standard.Models.OperationTypeEnum operation = ApiHelper.JsonDeserialize<Standard.Models.OperationTypeEnum>("\"SUM\"");
            double x = 10;
            double y = 3;

            // Perform API call
            double result = 0;
            try
            {
                result = await this.controller.CalculateAsync(operation, x, y);
            }
            catch (ApiException)
            {
            }

            // Test response code
            Assert.AreEqual(200, this.HttpCallBackHandler.Response.StatusCode, "Status should be 200");

            // Test whether the captured response is as we expected
            Assert.IsNotNull(result, "Result should exist");
            Assert.AreEqual(13, result, AssertPrecision, "Response should match expected value");
        }

        /// <summary>
        /// Multiplication of two operands.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task TestTestMultiply()
        {
            // Parameters for the API call
            Standard.Models.OperationTypeEnum operation = ApiHelper.JsonDeserialize<Standard.Models.OperationTypeEnum>("\"MULTIPLY\"");
            double x = 5;
            double y = 6;

            // Perform API call
            double result = 0;
            try
            {
                result = await this.controller.CalculateAsync(operation, x, y);
            }
            catch (ApiException)
            {
            }

            // Test response code
            Assert.IsTrue(
                    (this.HttpCallBackHandler.Response.StatusCode >= 200)
                    && (this.HttpCallBackHandler.Response.StatusCode <= 208),
                    "Status should be between 200 and 208");

            // Test whether the captured response is as we expected
            Assert.IsNotNull(result, "Result should exist");
            Assert.AreEqual(30, result, AssertPrecision, "Response should match expected value");
        }

        /// <summary>
        /// Subtraction of two operands.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task TestTestSubtract()
        {
            // Parameters for the API call
            Standard.Models.OperationTypeEnum operation = ApiHelper.JsonDeserialize<Standard.Models.OperationTypeEnum>("\"SUBTRACT\"");
            double x = 9;
            double y = 5;

            // Perform API call
            double result = 0;
            try
            {
                result = await this.controller.CalculateAsync(operation, x, y);
            }
            catch (ApiException)
            {
            }

            // Test response code
            Assert.AreEqual(200, this.HttpCallBackHandler.Response.StatusCode, "Status should be 200");

            // Test whether the captured response is as we expected
            Assert.IsNotNull(result, "Result should exist");
            Assert.AreEqual(4, result, AssertPrecision, "Response should match expected value");
        }

        /// <summary>
        /// Division of two operands.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task TestTestDivision()
        {
            // Parameters for the API call
            Standard.Models.OperationTypeEnum operation = ApiHelper.JsonDeserialize<Standard.Models.OperationTypeEnum>("\"DIVIDE\"");
            double x = 25;
            double y = 5;

            // Perform API call
            double result = 0;
            try
            {
                result = await this.controller.CalculateAsync(operation, x, y);
            }
            catch (ApiException)
            {
            }

            // Test response code
            Assert.AreEqual(200, this.HttpCallBackHandler.Response.StatusCode, "Status should be 200");

            // Test whether the captured response is as we expected
            Assert.IsNotNull(result, "Result should exist");
            Assert.AreEqual(5, result, AssertPrecision, "Response should match expected value");
        }
    }
}