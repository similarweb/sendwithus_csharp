﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * sendwithus API interface.
 * 
 * Reference: https://github.com/sendwithus/sendwithus_csharp
 * 
 * Author: Chris Hennig
 *  Email: hennig.chris@gmail.com
 *  Github: chennig
 */

[assembly: CLSCompliant(true)]
namespace Sendwithus
{
    /// <summary>
    /// Contains the sendwithus API settings
    /// </summary>
    public abstract class SendwithusClient
    {
        public const string API_VERSION = "v1";
        public const string API_PASSWORD = ""; // API uses an empty string as the password
        public const string CLIENT_VERSION = "1.0.0";
        public const string CLIENT_LANGUAGE = "csharp";
        public const string API_PROTO = "https";
        public const string API_HOST = "api.sendwithus.com";
        public const string API_PORT = "443";
        public const string SWU_API_HEADER = "X-SWU-API-KEY";
        public const string SWU_CLIENT_HEADER = "X-SWU-API-CLIENT";
        public const int DEFAULT_RETRY_COUNT = 3;
        public const Int32 DEFAULT_TIMEOUT_MILLISECONDS = 10000; // TODO: check against other APIs to confirm default

        public static string ApiKey { get; set; }
        public static int RetryCount { get; set; } = DEFAULT_RETRY_COUNT;

        private static TimeSpan _timeout = new TimeSpan(days: 0, hours: 0, minutes: 0, seconds: 0, milliseconds: DEFAULT_TIMEOUT_MILLISECONDS);

        /// <summary>
        /// Sets the timeout setting for the API client to the given timeout, in milliseconds
        /// </summary>
        /// <param name="timeout">The new timeout to use, in milliseconds</param>
        public static void SetTimeoutInMilliseconds(int timeout)
        {
            _timeout = new TimeSpan(days: 0, hours: 0, minutes: 0, seconds: 0, milliseconds: timeout);
        }

        /// <summary>
        /// Gets the timeout setting for the API client
        /// </summary>
        /// <returns>The timeout setting</returns>
        public static TimeSpan GetTimeout()
        {
            return _timeout;
        }
    }
}
