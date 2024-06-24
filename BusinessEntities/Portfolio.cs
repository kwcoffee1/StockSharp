#region S# License
/******************************************************************************************
NOTICE!!!  This program and source code is owned and licensed by
StockSharp, LLC, www.stocksharp.com
Viewing or use of this code requires your acceptance of the license
agreement found at https://github.com/StockSharp/StockSharp/blob/master/LICENSE
Removal of this comment is a violation of the license agreement.

Project: StockSharp.BusinessEntities.BusinessEntities
File: Portfolio.cs
Created: 2015, 11, 11, 2:32 PM

Copyright 2010 by StockSharp, LLC
*******************************************************************************************/
#endregion S# License
namespace StockSharp.BusinessEntities
{
	using System;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Runtime.Serialization;

	using StockSharp.Messages;
	using StockSharp.Localization;

	/// <summary>
	/// Portfolio, describing the trading account and the size of its generated commission.
	/// </summary>
	[Serializable]
	[DataContract]
	[Display(
		ResourceType = typeof(LocalizedStrings),
		Name = LocalizedStrings.PortfolioKey,
		Description = LocalizedStrings.PortfolioDescKey)]
	public class Portfolio : Position
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Portfolio"/>.
		/// </summary>
		public Portfolio()
		{
		}

		private string _name;

		/// <summary>
		/// Portfolio code name.
		/// </summary>
		[DataMember]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.NameKey,
			Description = LocalizedStrings.PortfolioNameKey,
			GroupName = LocalizedStrings.GeneralKey)]
		public string Name
		{
			get => _name;
			set
			{
				if (_name == value)
					return;

				_name = value;
				NotifyChanged();
			}
		}

		/// <summary>
		/// Exchange board, for which the current portfolio is active.
		/// </summary>
		[DataMember]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.BoardKey,
			Description = LocalizedStrings.PortfolioBoardKey,
			GroupName = LocalizedStrings.GeneralKey)]
		public ExchangeBoard Board { get; set; }

		private PortfolioStates? _state;

		/// <summary>
		/// Portfolio state.
		/// </summary>
		[DataMember]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.StateKey,
			Description = LocalizedStrings.PortfolioStateKey,
			GroupName = LocalizedStrings.GeneralKey)]
		[Browsable(false)]
		public PortfolioStates? State
		{
			get => _state;
			set
			{
				if (_state == value)
					return;

				_state = value;
				NotifyChanged();
			}
		}

		/// <inheritdoc />
		[Browsable(false)]
		public override string StrategyId { get => base.StrategyId; set => base.StrategyId = value; }

		/// <inheritdoc />
		[Browsable(false)]
		public override Sides? Side { get => base.Side; set => base.Side = value; }

		/// <summary>
		/// Portfolio associated with the orders received through the orders log.
		/// </summary>
		public static Portfolio AnonymousPortfolio { get; } = new Portfolio
		{
			Name = Extensions.AnonymousPortfolioName,
		};

		/// <summary>
		/// Create virtual portfolio for simulation.
		/// </summary>
		/// <returns>Simulator.</returns>
		public static Portfolio CreateSimulator() => new()
		{
			Name = Extensions.SimulatorPortfolioName,
			BeginValue = 1000000,
		};

		/// <summary>
		/// To copy the current portfolio fields to the <paramref name="destination" />.
		/// </summary>
		/// <param name="destination">The portfolio, in which fields should be copied.</param>
		public void CopyTo(Portfolio destination)
		{
			base.CopyTo(destination);

			destination.Name = Name;
			destination.Board = Board;
			//destination.Connector = Connector;
			destination.State = State;
		}

		/// <inheritdoc />
		public override string ToString() => Name;

		/// <inheritdoc />
		public override Position Clone()
		{
			var clone = new Portfolio();
			CopyTo(clone);
			return clone;
		}
	}
}
