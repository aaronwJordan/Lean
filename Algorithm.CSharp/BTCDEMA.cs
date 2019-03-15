using QuantConnect.Data;
using QuantConnect.Indicators;
using System;

namespace QuantConnect.Algorithm.CSharp
{
	public class BTCDEMA : QCAlgorithm
	{
		private readonly string _symbol = "BTCUSD";
		private DoubleExponentialMovingAverage _emaFast;
		private DoubleExponentialMovingAverage _emaSlow;

		/// <summary>
		/// Initialise the data and resolution required, as well as the cash and start-end dates for your algorithm. All algorithms must initialized.
		/// </summary>
		public override void Initialize()
		{
			SetStartDate(2018, 11, 18);  //Set Start Date
			SetEndDate(2019, 03, 14);    //Set End Date
			SetCash(1000);             //Set Strategy Cash

			// Find more symbols here: http://quantconnect.com/data
			// Forex, CFD, Equities Resolutions: Tick, Second, Minute, Hour, Daily.
			// Futures Resolution: Tick, Second, Minute
			// Options Resolution: Minute Only.
			AddCrypto(_symbol, Resolution.Hour, Market.GDAX);
			_emaFast = DEMA(_symbol, 7, Resolution.Hour);
			_emaSlow = DEMA(_symbol, 21, Resolution.Hour);

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
			if (IsWarmingUp) { return; }
			if (!_emaFast.IsReady) { return; }
			if (!_emaSlow.IsReady) { return; }

			if (!Portfolio.Invested && _emaFast > _emaSlow)
			{
				SetHoldings(_symbol, 1);
				Debug($"LONG: {Portfolio.CashBook}\n\n");
			}

			if (Portfolio.Invested && _emaFast < _emaSlow)
			{
				Liquidate(_symbol);
				Debug($"LIQUIDATE: {Portfolio.CashBook}\n\n");
			}
		}
	}
}
