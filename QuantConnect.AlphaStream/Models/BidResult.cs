﻿using Newtonsoft.Json;
using QuantConnect.Util;
using System;

namespace QuantConnect.AlphaStream.Models
{
    /// <summary>
    /// Result of a bid to license an Alpha
    /// </summary>
    public class BidResult
    {
        /// <summary>
        /// True if the bid is successful
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Information about the active bid
        /// </summary>
        [JsonProperty("active-bid")]
        public Bid ActiveBid { get; set; }

        /// <summary>
        /// Time that the next auction will occur
        /// </summary>
        [JsonProperty("next-auction-time"), JsonConverter(typeof(DoubleUnixSecondsDateTimeJsonConverter))]
        public DateTime NextAuctionTime { get; set; }

        /// <summary>
        /// Alpha's capacity for the next auction
        /// </summary>
        [JsonProperty("next-auction-capacity")]
        public decimal NextAuctionCapacity { get; set; }

        /// <summary>
        /// Minimum capital required to place a bid
        /// </summary>
        [JsonProperty("minimum-capital-required")]
        public decimal MinimumCapitalRequired { get; set; }

        /// <summary>
        /// Returns a string that represents the BidResult object
        /// </summary>
        /// <returns>A string that represents the BidResult object</returns>
        public override string ToString()
        {
            var activeBid = Success && ActiveBid != null ? $"Active bid: {ActiveBid}" : "No active bid";
            return $"{activeBid} Next auction at {NextAuctionTime} for ${NextAuctionCapacity}. Minimum capital required : ${MinimumCapitalRequired}";
        }
    }
}