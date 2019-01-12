using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace HtmlLinkLabel.Converters
{
    public class HtmlLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var formatted = new FormattedString();

            foreach (var item in ProcessString((string)value))
                formatted.Spans.Add(CreateSpan(item));

            return formatted;
        }

        private Span CreateSpan(StringSection section)
        {
            var span = new Span()
            {
                Text = section.Text
            };

            if (!string.IsNullOrEmpty(section.Link))
            {
                span.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    Command = _navigationCommand,
                    CommandParameter = section.Link
                });
                span.TextColor = Color.Blue;
                // Underline coming soon from https://github.com/xamarin/Xamarin.Forms/pull/2221
                // Currently available in Nightly builds if you wanted to try, it does work :)
                // As of 2018-07-22. But not avail in 3.2.0-pre1.
                // span.TextDecorations = TextDecorations.Underline;
            }

            return span;
        }

        public IList<StringSection> ProcessString(string rawText)
        {
            const string spanPattern = @"(<a.*?>.*?</a>)";

            var sections = new List<StringSection>();

            if (String.IsNullOrEmpty(rawText)) return sections;

            MatchCollection collection = Regex.Matches(rawText, spanPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            var lastIndex = 0;

            foreach (Match item in collection)
            {
                var foundText = item.Value;
                sections.Add(new StringSection() { Text = rawText.Substring(lastIndex, item.Index - lastIndex) });
                lastIndex = item.Index + item.Length;

                // Get HTML href 
                var html = new StringSection()
                {
                    Link = Regex.Match(item.Value, @"(?<=href=(\""|\“))[\S]+(?=(\""|\”))", RegexOptions.IgnoreCase).Value,
                    Text = Regex.Replace(item.Value, "<.*?>", string.Empty)
                };

                sections.Add(html);
            }

            sections.Add(new StringSection() { Text = rawText.Substring(lastIndex) });

            return sections;
        }

        public class StringSection
        {
            public string Text { get; set; }
            public string Link { get; set; }
        }

        private ICommand _navigationCommand = new Command<string>((url) =>
        {
            Console.WriteLine($"Open for {url}");
            try
            {
                Device.OpenUri(new Uri(url));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        });

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
