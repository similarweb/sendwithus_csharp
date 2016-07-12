﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus DripCampaignDeactivateResponse class
    /// </summary>
    public class DripCampaignDeactivateResponse
    {
        public bool success { get; set; }
        public string status { get; set; }
        public string recipient_address { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DripCampaignDeactivateResponse()
        {
            this.success = false;
            this.status = String.Empty;
            this.recipient_address = String.Empty;
        }
    }
}