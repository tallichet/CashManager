using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Windows;

namespace CashManager.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Username.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UsernameProperty =
            DependencyProperty.Register("Username", typeof(string), typeof(MainWindow), new PropertyMetadata("cedric.tallichet@burningbox.ch"));



        public IEnumerable<Purchase> Purchases
        {
            get { return (IEnumerable<Purchase>)GetValue(PurchasesProperty); }
            set { SetValue(PurchasesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Purchases.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PurchasesProperty =
            DependencyProperty.Register("Purchases", typeof(IEnumerable<Purchase>), typeof(MainWindow), new PropertyMetadata(null));



        private void refresh_click(object sender, RoutedEventArgs e)
        {
            var client = new WebClient();
            var downlaodedString = client.DownloadString($"http://localhost:53868/user/{Username}/purchases");
            Purchases = JsonConvert.DeserializeObject<Purchase[]>(downlaodedString);
        }
    }
}
