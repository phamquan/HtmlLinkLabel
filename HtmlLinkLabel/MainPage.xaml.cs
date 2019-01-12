using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HtmlLinkLabel
{
    public partial class MainPage : ContentPage
    {
        String _Html;
        public String Html
        {
            get
            {
                return _Html;
            }
            set
            {
                _Html = value;
                OnPropertyChanged(nameof(Html));
            }
        }

        String _MessageContent = String.Empty;
        public String MessageContent
        {
            get
            {
                return _MessageContent;
            }
            set
            {
                _MessageContent = value;
                OnPropertyChanged(nameof(MessageContent));
            }
        }

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            Html = @"<a href=""bizwork://profile/nghi"">@Nghi</a> xem cai nay di. <a href=""https://google.com"">google.com</a> MS: <a href=""http://microsoft.com"">Microsoft</a>.";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Html = MessageContent;

            var parseUrl = this.Urlize(MessageContent);
            DisplayAlert("Hello", parseUrl, "OK");
        }

        string Urlize(String messageContent)
        {
            if (String.IsNullOrEmpty(messageContent)) return string.Empty;

            var urlRegex = @"\b(?:(?:(?:https?|ftp):)?\/\/)(?:\S+(?::\S*)?@)?(?:(?!(?:10|127)(?:\.\d{1,3}){3})(?!(?:169\.254|192\.168)(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z0-9\u00a1-\uffff][a-z0-9\u00a1-\uffff_-]{0,62})?[a-z0-9\u00a1-\uffff]\.)+(?:[a-z\u00a1-\uffff]{2,}\.?))(?::\d{2,5})?(?:[/?#]\S*)?(?!(\""|\”))\b";

            MatchCollection collection = Regex.Matches(messageContent, urlRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            var result = String.Empty;

            foreach(Match item in collection)
            {
                result += item.Value;
            }

            return result;
        }
    }
}
