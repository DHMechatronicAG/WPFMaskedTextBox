using System;
using System.Collections.Generic;
using WPFMaskedTextBox.Enums;
using WPFMaskedTextBox.Filters;

namespace WPFMaskedTextBox
{
	public class FilterProvider
	{
		private static readonly Lazy<FilterProvider> InstanceLazy = new Lazy<FilterProvider>(() => new FilterProvider());
		public static FilterProvider Instance => InstanceLazy.Value;

		private readonly Dictionary<FilterType, DefaultFilter> _predefinedFilters = new Dictionary<FilterType, DefaultFilter>();

		private FilterProvider()
		{
			LoadValues();
		}

		private void LoadValues()
		{
			_predefinedFilters.Add(FilterType.Any, DefaultFilter.NullFilter);
			_predefinedFilters.Add(FilterType.Number, RegExFilter.NumberFilter);
			_predefinedFilters.Add(FilterType.Decimal, RegExFilter.DecimalFilter);
			_predefinedFilters.Add(FilterType.UNumber, RegExFilter.UNumberFilter);
			_predefinedFilters.Add(FilterType.UDecimal, RegExFilter.UDecimalFilter);
		}

		public DefaultFilter FilterForMaskedType(FilterType type)
		{
			var filter = _predefinedFilters[type];
			return filter ?? DefaultFilter.NullFilter;
		}
	}
}
