using DMS_Beta.Styles;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DMS_Beta.Pages
{
    /// <summary>
    /// Interaction logic for NotificationsPage.xaml
    /// </summary>
    public partial class NotificationsPage : Page
    {
        public NotificationsPage()
        {
            InitializeComponent();
        }

        private void NotificationsPageLoad(object sender, RoutedEventArgs e)
        {
            List<StackPanel> lp = new List<StackPanel>();
            for (int i = 0; i < (int)(6 * 380 / this.WindowWidth) + 1; i++)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Margin = new Thickness(0, 0, 0, 0);
                sp.Height = 80;
                sp.Width = this.Width;
                sp.Width = this.Width;
                for (int j = i * ((int)this.WindowWidth / 380); j < (int)(this.WindowWidth / 380) * (i + 1); j++)
                {
                    if (j < 6)
                    {
                        NotificationCard card = new NotificationCard();
                        card.title.Text += j.ToString();
                        card.Margin = new Thickness(5, 5, 0, 0);
                        sp.Children.Add(card);
                    }
                }
                lp.Add(sp);
            }
            foreach (var item in lp)
            {
                listview.Children.Add(item);
            }
        }
    }
}
