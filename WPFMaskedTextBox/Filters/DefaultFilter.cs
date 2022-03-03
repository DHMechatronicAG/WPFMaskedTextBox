using System;
using System.Linq;

namespace WPFMaskedTextBox.Filters
{
	public class DefaultFilter
	{
		private Func<string, bool> _isTextValidCheckers;

		public static readonly DefaultFilter IntegerFilter = new DefaultFilter(item => int.TryParse(item, out _));
		public static readonly DefaultFilter NullFilter = new DefaultFilter();

		public DefaultFilter() { }

		public DefaultFilter(Func<string, bool> additionalValidator)
		{
			AddTextValidator(additionalValidator);
		}

		protected void AddTextValidator(Func<string, bool> additionalValidator)
		{
			if (_isTextValidCheckers is null || !_isTextValidCheckers.GetInvocationList().Contains(additionalValidator))
			{
				_isTextValidCheckers += additionalValidator;
			}
		}

		public bool IsTextValid(string newText)
		{
			if (_isTextValidCheckers is null)
			{
				return true;
			}

			foreach (var isTextValidChecker in _isTextValidCheckers.GetInvocationList())
			{
				Func<string, bool> checker = isTextValidChecker as Func<string, bool>;

				if (checker is null)
				{
					continue;
				} else if (!checker.Invoke(newText))
				{
					return false;
				}
			}

			return true;
		}
	}
}
