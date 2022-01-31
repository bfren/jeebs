// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text;
using Jeebs.Calendar.Models;
using static F.CalendarF;

namespace Jeebs.Calendar;

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
		builder.AppendMax75("BEGIN:VCALENDAR");
		builder.AppendMax75("VERSION:2.0");
		builder.AppendMax75("PRODID:-//bfren//NONSGML Jeebs.Calendar//EN");
		builder.AppendMax75("CALSCALE:GREGORIAN");
		builder.AppendMax75("X-PUBLISHED-TTL:PT1H");
		return builder.ToString();
	}

	/// <summary>
	/// Get GMT Timezone
	/// </summary>
	internal static string GetTimezoneGmt()
	{
		var builder = new StringBuilder();
		builder.AppendMax75("BEGIN:VTIMEZONE");
		builder.AppendMax75("TZID:Europe/London");
		builder.AppendMax75("BEGIN:STANDARD");
		builder.AppendMax75("TZNAME:GMT");
		builder.AppendMax75("DTSTART:19710101T020000");
		builder.AppendMax75("TZOFFSETFROM:+0100");
		builder.AppendMax75("TZOFFSETTO:+0000");
		builder.AppendMax75("RRULE:FREQ=YEARLY;BYMONTH=10;BYDAY=-1SU");
		builder.AppendMax75("END:STANDARD");
		builder.AppendMax75("BEGIN:DAYLIGHT");
		builder.AppendMax75("TZNAME:BST");
		builder.AppendMax75("DTSTART:19710101T010000");
		builder.AppendMax75("TZOFFSETFROM:+0000");
		builder.AppendMax75("TZOFFSETTO:+0100");
		builder.AppendMax75("RRULE:FREQ=YEARLY;BYMONTH=3;BYDAY=-1SU");
		builder.AppendMax75("END:DAYLIGHT");
		builder.AppendMax75("END:VTIMEZONE");
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
		builder.AppendMax75("BEGIN:VEVENT");
		builder.AppendMax75($"UID:{uid}");
		builder.AppendMax75($"CREATED:{Format(DateTime.Now)}");
		builder.AppendMax75($"LAST-MODIFIED:{Format(lastModified)}");
		builder.AppendMax75($"DTSTAMP:{Format(lastModified)}");
		builder.AppendMax75($"SUMMARY:{e.Summary}");
		builder.AppendMax75($"DESCRIPTION:{e.Description}");
		builder.AppendMax75($"LOCATION:{e.Location}");

		if (e.IsAllDay)
		{
			builder.AppendMax75($"DTSTART;TZID={tzid};VALUE=DATE:{Format(e.Start, false)}");
		}
		else
		{
			builder.AppendMax75($"DTSTART;TZID={tzid}:{Format(e.Start)}");
			builder.AppendMax75($"DTEND;TZID={tzid}:{Format(e.End)}");
		}

		if (e.Free)
		{
			builder.AppendMax75("TRANSP:TRANSPARENT");
		}

		builder.AppendMax75("END:VEVENT");
		return builder.ToString();
	}

	/// <summary>
	/// Get Footer
	/// </summary>
	internal static string GetFooter()
	{
		var builder = new StringBuilder();
		builder.AppendMax75("END:VCALENDAR");
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
