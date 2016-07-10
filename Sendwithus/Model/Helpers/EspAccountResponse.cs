﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus EspAccountResponse
    /// </summary>
    public class EspAccountResponse
    {
        public bool success { get; set; }
        public string status { get; set; }
        public EspAccount esp_account { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public EspAccountResponse()
        {
            success = false;
            status = String.Empty;
            esp_account = new EspAccount();
        }
    }
}