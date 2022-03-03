using System.Text.RegularExpressions;

namespace WPFMaskedTextBox.Filters
{
	public class RegExFilter : DefaultFilter
	{
		public static readonly RegExFilter UNumberFilter = new RegExFilter(@"^\d*$");
		public static readonly RegExFilter NumberFilter = new RegExFilter(@"^-?\d*$");
		public static readonly RegExFilter UDecimalFilter = new RegExFilter(@"^\d*([.,]\d*)?$");
		public static readonly RegExFilter DecimalFilter = new RegExFilter(@"^-?\d*([\.,]\d*)?$");

		public int? MaxLength { get; }

		private readonly Regex _regEx;

		public RegExFilter(string regExp, int? maxLength = null)
		{
			_regEx = string.IsNullOrEmpty(regExp) ? null : new Regex(regExp);
			AddTextValidator(CheckRegExp);
			MaxLength = maxLength;
			AddTextValidator(CheckMaxLength);
		}

		public RegExFilter(int? maxLength) : this(string.Empty, maxLength) { }

		private bool CheckRegExp(string newText)
		{
			return _regEx is null || _regEx.Match(newText).Success;
		}

		private bool CheckMaxLength(string newText)
		{
			return !MaxLength.HasValue || MaxLength.Value >= newText.Length;
		}
	}
}
