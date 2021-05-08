using Buckets.Model;
using System.Windows;

namespace Buckets.WPF
{
    public partial class MainWindow : Window
    {
        private Container container;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void bCreateBucket_Click(object sender, RoutedEventArgs e)
        {
            double content = tbBucketContent.Text.Length == 0 ? 0 : double.Parse(tbBucketContent.Text);
            if (tbBucketCapacity.Text.Length == 0)
            {
                container = Bucket.GetDefault(content);
            }
            else
            {
                container = Bucket.Get(double.Parse(tbBucketCapacity.Text), content);
            }
            SetDescription("bucket");
        }

        private void bCreateRainBarrel_Click(object sender, RoutedEventArgs e)
        {
            double content = tbRainBarrelContent.Text.Length == 0 ? 0 : double.Parse(tbRainBarrelContent.Text);
            if (rbRainBarrelDefault.IsChecked == true)
            {
                container = RainBarrel.Get(content);
            }
            else if (rbRainBarrelSmall.IsChecked == true)
            {
                container = RainBarrel.GetSmall(content);
            }
            else if (rbRainBarrelLarge.IsChecked == true)
            {
                container = RainBarrel.GetLarge(content);
            }
            SetDescription("rain barrel");
        }

        private void bCreateOilBarrel_Click(object sender, RoutedEventArgs e)
        {
            double content = tbOilBarrelContent.Text.Length == 0 ? 0 : double.Parse(tbOilBarrelContent.Text);
            container = OilBarrel.Get(content);
            SetDescription("oil barrel");
        }

        private void SetDescription(string typeName)
        {
            container.Full += Container_Full;
            lDescription.Content = $"{container.Capacity} {typeName}";
            lContent.Content = container.Content.ToString();
        }

        private void Container_Full(double overflow)
        {
        }
    }
}
