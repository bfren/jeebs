// Mileage Tracker
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;
using Npgsql.Internal;
using Npgsql.Internal.Postgres;
using NpgsqlTypes;

namespace Jeebs.Data.Clients.PostgreSql.TypeHandlers;

#pragma warning disable NPG9001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
/// <summary>
/// Provides a factory for creating legacy PostgreSQL date and time type resolvers for Npgsql.
/// </summary>
public sealed class LegacyDateAndTimeResolverFactory : PgTypeInfoResolverFactory
{
	/// <inheritdoc/>
	public override IPgTypeInfoResolver CreateResolver() =>
		new Resolver();

	/// <inheritdoc/>
	public override IPgTypeInfoResolver CreateArrayResolver() =>
		new ArrayResolver();

	/// <inheritdoc/>
	public override IPgTypeInfoResolver CreateRangeResolver() =>
		new RangeResolver();

	/// <inheritdoc/>
	public override IPgTypeInfoResolver CreateRangeArrayResolver() =>
		new RangeArrayResolver();

	/// <inheritdoc/>
	public override IPgTypeInfoResolver CreateMultirangeResolver() =>
		new MultirangeResolver();

	/// <inheritdoc/>
	public override IPgTypeInfoResolver CreateMultirangeArrayResolver() =>
		new MultirangeArrayResolver();

	private const string Date = "pg_catalog.date";
	private const string Time = "pg_catalog.time";
	private const string DateRange = "pg_catalog.daterange";
	private const string DateMultirange = "pg_catalog.datemultirange";

	private class Resolver : IPgTypeInfoResolver
	{
		protected TypeInfoMappingCollection Mappings => field ??= AddMappings(new());

		public PgTypeInfo? GetTypeInfo(Type? type, DataTypeName? dataTypeName, PgSerializerOptions options)
			=> type == typeof(object) ? Mappings.Find(type, dataTypeName, options) : null;

		private static TypeInfoMappingCollection AddMappings(TypeInfoMappingCollection mappings)
		{
			mappings.AddStructType<DateTime>(Date,
				static (options, mapping, _) => options.GetTypeInfo(typeof(DateTime), new DataTypeName(mapping.DataTypeName))!,
				matchRequirement: MatchRequirement.DataTypeName);

			mappings.AddStructType<TimeSpan>(Time,
				static (options, mapping, _) => options.GetTypeInfo(typeof(TimeSpan), new DataTypeName(mapping.DataTypeName))!,
				isDefault: true);

			return mappings;
		}
	}

	private sealed class ArrayResolver : Resolver, IPgTypeInfoResolver
	{
		private new TypeInfoMappingCollection Mappings => field ??= AddMappings(new(base.Mappings));

		public new PgTypeInfo? GetTypeInfo(Type? type, DataTypeName? dataTypeName, PgSerializerOptions options)
			=> type == typeof(object) ? Mappings.Find(type, dataTypeName, options) : null;

		private static TypeInfoMappingCollection AddMappings(TypeInfoMappingCollection mappings)
		{
			mappings.AddStructArrayType<DateTime>(Date);
			mappings.AddStructArrayType<TimeSpan>(Time);

			return mappings;
		}
	}

	private class RangeResolver : IPgTypeInfoResolver
	{
		protected TypeInfoMappingCollection Mappings => field ??= AddMappings(new());

		public PgTypeInfo? GetTypeInfo(Type? type, DataTypeName? dataTypeName, PgSerializerOptions options)
			=> type == typeof(object) ? Mappings.Find(type, dataTypeName, options) : null;

		private static TypeInfoMappingCollection AddMappings(TypeInfoMappingCollection mappings)
		{
			mappings.AddStructType<NpgsqlRange<DateTime>>(DateRange,
				static (options, mapping, _) => options.GetTypeInfo(typeof(NpgsqlRange<DateTime>), new DataTypeName(mapping.DataTypeName))!,
				matchRequirement: MatchRequirement.DataTypeName);

			return mappings;
		}
	}

	private sealed class RangeArrayResolver : RangeResolver, IPgTypeInfoResolver
	{
		private new TypeInfoMappingCollection Mappings => field ??= AddMappings(new(base.Mappings));

		public new PgTypeInfo? GetTypeInfo(Type? type, DataTypeName? dataTypeName, PgSerializerOptions options)
			=> type == typeof(object) ? Mappings.Find(type, dataTypeName, options) : null;

		private static TypeInfoMappingCollection AddMappings(TypeInfoMappingCollection mappings)
		{
			mappings.AddStructArrayType<NpgsqlRange<DateTime>>(DateRange);

			return mappings;
		}
	}

	private class MultirangeResolver : IPgTypeInfoResolver
	{
		protected TypeInfoMappingCollection Mappings => field ??= AddMappings(new());

		public PgTypeInfo? GetTypeInfo(Type? type, DataTypeName? dataTypeName, PgSerializerOptions options)
			=> type == typeof(object) ? Mappings.Find(type, dataTypeName, options) : null;

		private static TypeInfoMappingCollection AddMappings(TypeInfoMappingCollection mappings)
		{
			mappings.AddType<NpgsqlRange<DateTime>[]>(DateMultirange,
				static (options, mapping, _) => options.GetTypeInfo(typeof(NpgsqlRange<DateTime>[]), new DataTypeName(mapping.DataTypeName))!,
				matchRequirement: MatchRequirement.DataTypeName);

			return mappings;
		}
	}

	private sealed class MultirangeArrayResolver : MultirangeResolver, IPgTypeInfoResolver
	{
		private new TypeInfoMappingCollection Mappings => field ??= AddMappings(new(base.Mappings));

		public new PgTypeInfo? GetTypeInfo(Type? type, DataTypeName? dataTypeName, PgSerializerOptions options)
			=> type == typeof(object) ? Mappings.Find(type, dataTypeName, options) : null;

		private static TypeInfoMappingCollection AddMappings(TypeInfoMappingCollection mappings)
		{
			mappings.AddArrayType<NpgsqlRange<DateTime>[]>(DateMultirange);

			return mappings;
		}
	}
}

#pragma warning restore NPG9001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
