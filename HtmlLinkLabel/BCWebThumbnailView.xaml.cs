using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace HtmlLinkLabel
{
    public partial class BCWebThumbnailView : ContentView
    {

        private static BindableProperty UrlTextProperty = BindableProperty.Create(
                                                         propertyName: nameof(UrlText),
                                                         returnType: typeof(string),
                                                         declaringType: typeof(BCWebThumbnailView),
                                                         defaultValue: "UrlText",
                                                         defaultBindingMode: BindingMode.OneWay);
        public String UrlText
        {
            get { return (String)base.GetValue(UrlTextProperty); }
            set { base.SetValue(UrlTextProperty, value); }
        }

        private static BindableProperty TitleTextProperty = BindableProperty.Create(
                                                         propertyName: nameof(TitleText),
                                                         returnType: typeof(string),
                                                         declaringType: typeof(BCWebThumbnailView),
                                                         defaultValue: "TitleText",
                                                         defaultBindingMode: BindingMode.OneWay);
        public String TitleText {
            get { return (String)base.GetValue(TitleTextProperty); }
            set { base.SetValue(TitleTextProperty, value); }
        }

        private static BindableProperty DescriptionTextProperty = BindableProperty.Create(
                                                         propertyName: nameof(DescriptionText),
                                                         returnType: typeof(string),
                                                         declaringType: typeof(BCWebThumbnailView),
                                                         defaultValue: "DescriptionText",
                                                         defaultBindingMode: BindingMode.OneWay);
        public String DescriptionText {
            get { return (String)base.GetValue(DescriptionTextProperty); }
            set { base.SetValue(DescriptionTextProperty, value); }
        }

        private static BindableProperty ImageUrlProperty = BindableProperty.Create(
                                                         propertyName: nameof(ImageUrl),
                                                         returnType: typeof(string),
                                                         declaringType: typeof(BCWebThumbnailView),
                                                         defaultValue: "DescriptionText",
                                                         defaultBindingMode: BindingMode.OneWay);
        public String ImageUrl {
            get { return (String)base.GetValue(ImageUrlProperty); }
            set { base.SetValue(ImageUrlProperty, value); }
        }


        public BCWebThumbnailView()
        {
            InitializeComponent();
            //this.BindingContext = this;\\
            this.Content.BindingContext = this;
        }
    }
}
