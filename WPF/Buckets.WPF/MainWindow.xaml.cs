using Buckets.Common.Model;
using Buckets.ViewModel;
using System.Diagnostics;
using System.Windows;

namespace Buckets.WPF
{
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainViewModel();
            DataContext = viewModel;
        }

        private void bCreateBucket_Click(object sender, RoutedEventArgs e)
        {
            double content = tbBucketContent.Text.Length == 0 ? 0 : double.Parse(tbBucketContent.Text);
            Bucket bucket;
            if (tbBucketCapacity.Text.Length == 0)
                bucket = Bucket.GetDefault(content);
            else
                bucket = Bucket.Get(double.Parse(tbBucketCapacity.Text), content);
            AddContainer(new BucketViewModel(bucket));
        }

        private void bCreateRainBarrel_Click(object sender, RoutedEventArgs e)
        {
            double content = tbRainBarrelContent.Text.Length == 0 ? 0 : double.Parse(tbRainBarrelContent.Text);
            RainBarrel rainBarrel;
            if (rbRainBarrelSmall.IsChecked == true)
                rainBarrel = RainBarrel.GetSmall(content);
            else if (rbRainBarrelLarge.IsChecked == true)
                rainBarrel = RainBarrel.GetLarge(content);
            else
                rainBarrel = RainBarrel.Get(content);
            AddContainer(new ContainerViewModel(rainBarrel));
        }

        private void bCreateOilBarrel_Click(object sender, RoutedEventArgs e)
        {
            double content = tbOilBarrelContent.Text.Length == 0 ? 0 : double.Parse(tbOilBarrelContent.Text);
            OilBarrel oilBarrel = OilBarrel.Get(content);
            viewModel.Containers.Add(new ContainerViewModel(oilBarrel));
        }

        private void Container_Full(object sender, ContainerFullEventArgs e)
        {
            Container container = (Container)sender;
            Debug.WriteLine($"{container} full, overflow={e.Overflow}");
        }

        private void AddContainer(ContainerViewModel container)
        {
            container.Full += Container_Full;
            viewModel.Containers.Add(container);
        }
    }
}
