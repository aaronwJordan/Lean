/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using System;
using System.Collections.Generic;
using QuantConnect.Data;
using QuantConnect.Indicators;
using QuantConnect.Interfaces;

namespace QuantConnect.Algorithm.CSharp
{
    /// <summary>
    /// Basic template algorithm simply initializes the date range and cash. This is a skeleton
    /// framework you can use for designing an algorithm.
    /// </summary>
    /// <meta name="tag" content="using data" />
    /// <meta name="tag" content="using quantconnect" />
    /// <meta name="tag" content="trading and orders" />
    public class BasicTemplateAlgorithm : QCAlgorithm
    {
		// private Symbol _spy = QuantConnect.Symbol.Create("SPY", SecurityType.Equity, Market.USA);
		private readonly string _symbol = "SPY";
		private ExponentialMovingAverage _emaFast;
		private ExponentialMovingAverage _emaSlow;

		/// <summary>
		/// Initialise the data and resolution required, as well as the cash and start-end dates for your algorithm. All algorithms must initialized.
		/// </summary>
		public override void Initialize()
        {
            SetStartDate(2016, 01, 01);  //Set Start Date
            SetEndDate(2018, 08, 01);    //Set End Date
            SetCash(100000);             //Set Strategy Cash

            // Find more symbols here: http://quantconnect.com/data
            // Forex, CFD, Equities Resolutions: Tick, Second, Minute, Hour, Daily.
            // Futures Resolution: Tick, Second, Minute
            // Options Resolution: Minute Only.
            AddSecurity(SecurityType.Equity, _symbol, Resolution.Hour);
			_emaFast = EMA(_symbol, 7, Resolution.Hour);
			_emaSlow = EMA(_symbol, 21, Resolution.Hour);

			// There are other assets with similar methods. See "Selecting Options" etc for more details.
			// AddFuture, AddForex, AddCfd, AddOption
			SetWarmup(TimeSpan.FromDays(30));
		}

        /// <summary>
        /// OnData event is the primary entry point for your algorithm. Each new data point will be pumped in here.
        /// </summary>
        /// <param name="data">Slice object keyed by symbol containing the stock data</param>
        public override void OnData(Slice data)
        {
			if (!_emaFast.IsReady) { return; }
			if (!_emaSlow.IsReady) { return; }

			if (_emaFast > _emaSlow)
			{
				SetHoldings(_symbol, 1);
				Debug("Long");
			}
			else if (_emaFast < _emaSlow)
			{
				Liquidate(_symbol);
				Debug("Short");
			}
		} 
    }
}
