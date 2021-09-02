// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;
using Jeebs.Calendar.Models;
using static F.CalendarF;

namespace Jeebs.Calendar
{
	/// <summary>
	/// Export calendar model as VCalendar (ICS format)
	/// </summary>
	public class VCalendar : CalendarBase
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="calendar">Calendar</param>
		public VCalendar(CalendarModel calendar) : this(calendar, DefaultTimezone) { }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="calendar">Calendar</param>
		/// <param name="tzid">Timezone</param>
		public VCalendar(CalendarModel calendar, string tzid) : base(calendar, tzid) { }

		/// <summary>
		/// Convert calendar to ICS format
		/// </summary>
		/// <param name="domain">Calendar / app domain</param>
		public string ToString(string domain)
		{
			// Add Header
			var builder = new StringBuilder();
			builder.Append(GetHeader());

			// Add Timezone
			builder.Append(GetTimezone());

			// Add Events
			var counter = 0;
			foreach (var e in calendar.Events)
			{
				var uid = GenerateEventUid(counter++, calendar.LastModified, domain);
				builder.Append(GetEvent(calendar.LastModified, tzid, uid, e));
			}

			// Add Footer
			builder.Append(GetFooter());

			// Return VCalendar string
			return builder.ToString();
		}

		/// <summary>
		/// Get Header
		/// </summary>
		internal static string GetHeader()
		{
			var builder = new StringBuilder();
			builder.AppendLine("BEGIN:VCALENDAR");
			builder.AppendLine("VERSION:2.0");
			builder.AppendLine("PRODID:-//bfren//NONSGML Jeebs.Calendar//EN");
			builder.AppendLine("CALSCALE:GREGORIAN");
			builder.AppendLine("X-PUBLISHED-TTL:PT1H");
			return builder.ToString();
		}

		/// <summary>
		/// Get GMT Timezone
		/// </summary>
		internal static string GetTimezoneGmt()
		{
			var builder = new StringBuilder();
			builder.AppendLine("BEGIN:VTIMEZONE");
			builder.AppendLine("TZID:Europe/London");
			builder.AppendLine("BEGIN:STANDARD");
			builder.AppendLine("TZNAME:GMT");
			builder.AppendLine("DTSTART:19710101T020000");
			builder.AppendLine("TZOFFSETFROM:+0100");
			builder.AppendLine("TZOFFSETTO:+0000");
			builder.AppendLine("RRULE:FREQ=YEARLY;BYMONTH=10;BYDAY=-1SU");
			builder.AppendLine("END:STANDARD");
			builder.AppendLine("BEGIN:DAYLIGHT");
			builder.AppendLine("TZNAME:BST");
			builder.AppendLine("DTSTART:19710101T010000");
			builder.AppendLine("TZOFFSETFROM:+0000");
			builder.AppendLine("TZOFFSETTO:+0100");
			builder.AppendLine("RRULE:FREQ=YEARLY;BYMONTH=3;BYDAY=-1SU");
			builder.AppendLine("END:DAYLIGHT");
			builder.AppendLine("END:VTIMEZONE");
			return builder.ToString();
		}

		/// <summary>
		/// Get Event
		/// </summary>
		/// <param name="lastModified">Calendar last modified date</param>
		/// <param name="tzid">Timezone ID</param>
		/// <param name="uid">Event ID</param>
		/// <param name="e">Event model</param>
		internal static string GetEvent(DateTime lastModified, string tzid, string uid, EventModel e)
		{
			var builder = new StringBuilder();
			builder.AppendLine("BEGIN:VEVENT");
			builder.AppendLine($"UID:{uid}");
			builder.AppendLine($"CREATED:{Format(DateTime.Now)}");
			builder.AppendLine($"LAST-MODIFIED:{Format(lastModified)}");
			builder.AppendLine($"DTSTAMP:{Format(lastModified)}");
			builder.AppendLine($"SUMMARY:{e.Summary}");
			builder.AppendLine($"DESCRIPTION:{e.Description}");
			builder.AppendLine($"LOCATION:{e.Location}");

			if (e.IsAllDay)
			{
				builder.AppendLine($"DTSTART;TZID={tzid};VALUE=DATE:{Format(e.Start, false)}");
			}
			else
			{
				builder.AppendLine($"DTSTART;TZID={tzid}:{Format(e.Start)}");
				builder.AppendLine($"DTEND;TZID={tzid}:{Format(e.End)}");
			}

			builder.AppendLine("END:VEVENT");
			return builder.ToString();
		}

		/// <summary>
		/// Get Footer
		/// </summary>
		internal static string GetFooter()
		{
			var builder = new StringBuilder();
			builder.AppendLine("END:VCALENDAR");
			return builder.ToString();
		}

		/// <summary>
		/// Format date with or without the time
		/// </summary>
		/// <param name="dt">Date Time</param>
		/// <param name="withTime">If true will include the time</param>
		internal static string Format(DateTime dt, bool withTime = true) =>
			dt.ToString(withTime ? @"yyyyMMdd\THHmmss" : "yyyyMMdd");

		/// <summary>
		/// Get Timezone information
		/// </summary>
		public virtual string GetTimezone() =>
			GetTimezoneGmt();
	}
}
