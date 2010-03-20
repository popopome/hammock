﻿using Hammock.Retries;
using NUnit.Framework;

namespace Hammock.Tests
{
    partial class RestClientTests
    {
        [Test]
        [Category("Async")]
        [Category("Retries")]
        public void Can_set_retry_policy_asynchronously()
        {
            var retryPolicy = new RetryPolicy { RetryCount = 5 };
            retryPolicy.RetryOn(new NetworkError(),
                                new Timeout(),
                                new ConnectionClosed());

            var client = new RestClient
            {
                RetryPolicy = retryPolicy,
                Authority = "http://api.twitter.com",
                VersionPath = "1"
            };

            var request = new RestRequest
            {
                Path = "statuses/home_timeline.json",
                Credentials = BasicAuthForTwitter
            };

            var asyncResult = client.BeginRequest(request);
            var response = client.EndRequest(asyncResult);
            Assert.IsNotNull(response);
        }
    }
}