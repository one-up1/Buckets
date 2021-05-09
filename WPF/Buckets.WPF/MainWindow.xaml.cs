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
            if (tbBucketCapacity.Text.Length == 0)
            {
                viewModel.Containers.Add(Bucket.GetDefault(content));
            }
            else
            {
                viewModel.Containers.Add(Bucket.Get(double.Parse(tbBucketCapacity.Text), content));
            }
        }

        private void bCreateRainBarrel_Click(object sender, RoutedEventArgs e)
        {
            double content = tbRainBarrelContent.Text.Length == 0 ? 0 : double.Parse(tbRainBarrelContent.Text);
            if (rbRainBarrelDefault.IsChecked == true)
            {
                viewModel.Containers.Add(RainBarrel.Get(content));
            }
            else if (rbRainBarrelSmall.IsChecked == true)
            {
                viewModel.Containers.Add(RainBarrel.GetSmall(content));
            }
            else if (rbRainBarrelLarge.IsChecked == true)
            {
                viewModel.Containers.Add(RainBarrel.GetLarge(content));
            }
        }

        private void bCreateOilBarrel_Click(object sender, RoutedEventArgs e)
        {
            double content = tbOilBarrelContent.Text.Length == 0 ? 0 : double.Parse(tbOilBarrelContent.Text);
            viewModel.Containers.Add(OilBarrel.Get(content));
        }

        private void Container_Full(object sender, ContainerFullEventArgs e)
        {
            Container container = (Container)sender;
            Debug.WriteLine($"{container} full, overflow={e.Overflow}");
        }

        private void AddContainer(Container container)
        {
            container.Full += Container_Full;
            viewModel.Containers.Add(container);
        }

        /*private void SetDescription(string typeName)
        {
            container.Full += Container_Full;
            lDescription.Content = $"{container.Capacity} {typeName}";
            lContent.Content = container.Content.ToString();
        }*/
    }
}
