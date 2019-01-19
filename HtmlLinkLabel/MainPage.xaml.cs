using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using OpenGraphNet;
using OpenGraphNet.Metadata;

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

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var t = String.Copy(MessageContent);

            var parseUrl = this.Urlize(MessageContent);

            foreach(var url in parseUrl)
            {
                var anchor = $@"<a href=""{url}"">{url}</a>";
                t = t.Replace(url, anchor);
            }

            Html = t;

            var firstFoundUrl = parseUrl.FirstOrDefault();
            if (!String.IsNullOrEmpty(firstFoundUrl))
            {
                await CreateThumbnail(firstFoundUrl);
            }
        }

        IList<String> Urlize(String messageContent)
        {

            var result = new List<String>();

            if (String.IsNullOrEmpty(messageContent)) return result;

            var urlRegex = @"\b(?:(?:(?:https?|ftp):)?\/\/)(?:\S+(?::\S*)?@)?(?:(?!(?:10|127)(?:\.\d{1,3}){3})(?!(?:169\.254|192\.168)(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z0-9\u00a1-\uffff][a-z0-9\u00a1-\uffff_-]{0,62})?[a-z0-9\u00a1-\uffff]\.)+(?:[a-z\u00a1-\uffff]{2,}\.?))(?::\d{2,5})?(?:[/?#]\S*)?(?!(\""|\”))\b";

            MatchCollection collection = Regex.Matches(messageContent, urlRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            foreach(Match item in collection)
            {
                result.Add(item.Value);
            }

            return result;
        }

        async Task CreateThumbnail(string pageUrl)
        {
            try
            {
                OpenGraph graph = await OpenGraph.ParseUrlAsync(pageUrl);
                var title = graph.Title ?? "";
                var imageUrl = graph.Image;
                var url = graph.OriginalUrl?.Host ?? "";

                IList<StructuredMetadata> t;
                if (graph.Metadata.TryGetValue("og:description", out t)) 
                {
                    var x = t.FirstOrDefault().Value ?? "";
                    thumbView.DescriptionText = x;
                }

                thumbView.UrlText = url.ToUpper();
                thumbView.TitleText = title;
                thumbView.ImageUrl = imageUrl.AbsoluteUri;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
