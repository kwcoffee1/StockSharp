#region S# License
/******************************************************************************************
NOTICE!!!  This program and source code is owned and licensed by
StockSharp, LLC, www.stocksharp.com
Viewing or use of this code requires your acceptance of the license
agreement found at https://github.com/StockSharp/StockSharp/blob/master/LICENSE
Removal of this comment is a violation of the license agreement.

Project: StockSharp.Messages.Messages
File: BoardMessage.cs
Created: 2015, 11, 11, 2:32 PM

Copyright 2010 by StockSharp, LLC
*******************************************************************************************/
#endregion S# License
namespace StockSharp.Messages
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Runtime.Serialization;
	using System.Xml.Serialization;

	using Ecng.Serialization;

	using StockSharp.Localization;

	/// <summary>
	/// The message contains information about the electronic board.
	/// </summary>
	[DataContract]
	[Serializable]
	public class BoardMessage : BaseSubscriptionIdMessage<BoardMessage>
	{
		/// <summary>
		/// Exchange code, which owns the board. Maybe be the same <see cref="Code"/>.
		/// </summary>
		[DataMember]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.ExchangeInfoKey,
			Description = LocalizedStrings.BoardExchangeCodeKey,
			GroupName = LocalizedStrings.GeneralKey)]
		public string ExchangeCode { get; set; }

		/// <summary>
		/// Board code.
		/// </summary>
		[DataMember]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.CodeKey,
			Description = LocalizedStrings.BoardCodeKey,
			GroupName = LocalizedStrings.GeneralKey)]
		public string Code { get; set; }

		/// <summary>
		/// Securities expiration times.
		/// </summary>
		[DataMember]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.ExpiryDateKey,
			Description = LocalizedStrings.SecExpirationTimeKey,
			GroupName = LocalizedStrings.GeneralKey)]
		public TimeSpan ExpiryTime { get; set; }

		private WorkingTime _workingTime = new() { IsEnabled = true };

		/// <summary>
		/// Board working hours.
		/// </summary>
		[DataMember]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.WorkingTimeKey,
			Description = LocalizedStrings.WorkingHoursKey,
			GroupName = LocalizedStrings.GeneralKey)]
		public WorkingTime WorkingTime
		{
			get => _workingTime;
			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				if (WorkingTime == value)
					return;

				_workingTime = value;
			}
		}

		[field: NonSerialized]
		private TimeZoneInfo _timeZone = TimeZoneInfo.Utc;

		/// <summary>
		/// Information about the time zone where the exchange is located.
		/// </summary>
		[DataMember]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.TimeZoneKey,
			Description = LocalizedStrings.BoardTimeZoneKey,
			GroupName = LocalizedStrings.GeneralKey)]
		[XmlIgnore]
		//[Ecng.Serialization.TimeZoneInfo]
		public TimeZoneInfo TimeZone
		{
			get => _timeZone;
			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				if (TimeZone == value)
					return;

				_timeZone = value;
			}
		}

		/// <inheritdoc />
		public override DataType DataType => DataType.Board;

		/// <summary>
		/// Initializes a new instance of the <see cref="BoardMessage"/>.
		/// </summary>
		public BoardMessage()
			: base(MessageTypes.Board)
		{
		}

		/// <inheritdoc />
		public override void CopyTo(BoardMessage destination)
		{
			base.CopyTo(destination);

			destination.Code = Code;
			destination.ExchangeCode = ExchangeCode;
			destination.ExpiryTime = ExpiryTime;
			destination.WorkingTime = WorkingTime.Clone();
			destination.TimeZone = TimeZone;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return base.ToString() + $",Code={Code},Ex={ExchangeCode}";
		}
	}
}