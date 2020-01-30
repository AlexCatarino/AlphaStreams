﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using QuantConnect.AlphaStream.Infrastructure;
using QuantConnect.AlphaStream.Models;
using QuantConnect.AlphaStream.Requests;

namespace QuantConnect.AlphaStream.Tests
{
    [TestFixture]
    public class AlphaStreamRestClientTests
    {
        const string TestAlphaId = "8f81cbb82c0527bca80ed85b0";
        const string TestAuthorId = "604b579e6e335059d878dc6b412d1c15";

        [Test]
        public async Task GetsAlphaById()
        {
            var request = new GetAlphaByIdRequest {Id = TestAlphaId};
            var response = await ExecuteRequest(request).ConfigureAwait(false);
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Id, TestAlphaId);
        }

        [Test]
        public async Task GetAlphaInsights()
        {
            var request = new GetAlphaInsightsRequest {Id = TestAlphaId};
            var response = await ExecuteRequest(request).ConfigureAwait(false);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response);
        }

        [Test]
        public async Task GetAuthorById()
        {
            var request = new GetAuthorByIdRequest {Id = TestAuthorId};
            var response = await ExecuteRequest(request).ConfigureAwait(false);
            Assert.AreEqual(response.Id, TestAuthorId);
            Assert.AreEqual(response.Language, Language.Python);
        }

        [Test]
        public async Task GetAlphaPrices()
        {
            var request = new GetAlphaPricesRequest { Id = TestAlphaId };
            var response = await ExecuteRequest(request).ConfigureAwait(false);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response);
            var first = response.FirstOrDefault();
            Assert.AreEqual(first.PriceType, PriceType.Ask);
            Assert.AreEqual(first.SharedPrice, 100m);
            Assert.AreEqual(first.ExclusivePrice, null);
        }

        [Test]
        public async Task GetAlphaErrors()
        {
            var request = new GetAlphaErrorsRequest { Id = "5443d94e213604f4fefbab185" };
            var response = await ExecuteRequest(request).ConfigureAwait(false);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response);
            var first = response.FirstOrDefault();
            Assert.AreEqual(first.Error.Substring(0, 10), "Algorithm.");
            Assert.AreEqual(first.StackTrace.Substring(0, 10), "System.Exc");
        }

        [Test]
        public async Task GetAlphaList()
        {
            var request = new GetAlphaListRequest();
            var response = await ExecuteRequest(request).ConfigureAwait(false);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response);
        }

        [Test]
        public async Task SearchAlphas()
        {
            var request = new SearchAlphasRequest
            {
                Author = TestAuthorId,
                AssetClasses = {AssetClass.Crypto},
                Accuracy = Range.Create(0, 1d),
                SharedFee = Range.Create(0, 999999999m),
                ExclusiveFee = Range.Create(0, 999999999m),
                Sharpe = Range.Create(0d, null),
                // this is the quantconnect symbol security identifier string
                Symbols = new List<string> {"BTCUSD XJ" },
                Uniqueness = Range.Create(0d, 100d)
            };
            var response = await ExecuteRequest(request).ConfigureAwait(false);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response);
        }

        [Test]
        public async Task SearchAuthors()
        {
            var request = new SearchAuthorsRequest
            {
                Biography = "QuantConnect",
                Languages = { "C#" },
                SignedUp = Range.Create(Time.UnixEpoch, DateTime.Today),
                AlphasListed = Range.Create(0, int.MaxValue),
                ForumComments = Range.Create(0, int.MaxValue),
                ForumDiscussions = Range.Create(0, int.MaxValue),
                LastLogin = Range.Create(Time.UnixEpoch, DateTime.Today),
                Projects = Range.Create(0, int.MaxValue)
            };
            var response = await ExecuteRequest(request).ConfigureAwait(false);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response);
        }

        [Test]
        public async Task Subscribe()
        {
            var request = new SubscribeRequest { Id = TestAlphaId, Exclusive = false };
            var response = await ExecuteRequest(request).ConfigureAwait(false);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Messages.Count);
            Assert.AreEqual("Subscribed successfully", response.Messages[0]);
        }

        [Test]
        public async Task Unubscribe()
        {
            var request = new UnsubscribeRequest { Id = TestAlphaId };
            var response = await ExecuteRequest(request).ConfigureAwait(false);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Messages.Count);
            Assert.AreEqual("Subscription cancelled", response.Messages[0]);
        }

        [Test]
        public async Task CreateConversation()
        {
            var request = new CreateConversationRequest
            {
                Id = "118d1cbc375709792ea4d823a",
                From = "support@quantconnect.com",
                Message = "Hello World!",
                Subject = "Alpha Conversation",
                CC = "support@quantconnect.com"
            };
            var response = await ExecuteRequest(request).ConfigureAwait(false);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
        }

        [Test]
        public async Task CreateBid()
        {
            var createRequest = new CreateBidPriceRequest
            {
                Id = TestAlphaId,
                SharedPrice = 1,
                GoodUntil = DateTime.Now.AddDays(1).ToUnixTime()
            };
            var createResponse = await ExecuteRequest(createRequest).ConfigureAwait(false);
            Assert.IsNotNull(createResponse);
            Assert.IsTrue(createResponse.Success);

            var request = new GetAlphaPricesRequest { Id = TestAlphaId };
            var response = await ExecuteRequest(request).ConfigureAwait(false);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response);
            var last = response.FirstOrDefault();
            Assert.AreEqual(last.SharedPrice, 100);
        }

        private static async Task<T> ExecuteRequest<T>(IRequest<T> request)
        {
            var service = new AlphaStreamRestClient(Credentials.Test);
            return await service.Execute(request).ConfigureAwait(false);
        }
    }
}