using System;
using System.Collections.ObjectModel;
using Android.Widget;
using assignment;
using Xamarin.Forms;
using Switch = Xamarin.Forms.Switch;

namespace assignment
{
    public class CustomProductCell : ViewCell
    {
        public const int RowHeight = 80;

        public CustomProductCell()
        {
            var ProductLabel = new Label { FontAttributes = FontAttributes.Bold };
            ProductLabel.SetBinding(Label.TextProperty, "ProductName");
            
            var commentLabel = new Label { TextColor = Color.Gray };
            commentLabel.SetBinding(Label.TextProperty, "Description");
            
            var priceLabel = new Label { TextColor = Color.Gray };
            priceLabel.SetBinding(Label.TextProperty, new Binding("Price", stringFormat: "${0:0.00}"));
            
            var numInteractionLabel2 = new Label { TextColor = Color.Gray, Text = "#Interactions:--"};
            
            
            var numInteractionLabel = new Label { TextColor = Color.Gray };
            
            numInteractionLabel.SetBinding(Label.TextProperty, "NumInteractions");
            StackLayout stack1 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                Children = { ProductLabel }
            };
            
            StackLayout stack2 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { commentLabel, priceLabel }
            };
            
            StackLayout stack3 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { numInteractionLabel2,numInteractionLabel }
            };
            StackLayout finalStack = new StackLayout
            {
                Children = { stack1,stack2,stack3 }
            };

            View = finalStack;




        }

    }
}