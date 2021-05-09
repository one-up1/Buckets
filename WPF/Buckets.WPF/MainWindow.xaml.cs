using Buckets.Common.Model;
using Buckets.ViewModel;
using System;
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

        private void bFillContainer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.SelectedContainer.Fill(double.Parse(tbAmount.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bEmptyContainer_Click(object sender, RoutedEventArgs e)
        {
            if (tbAmount.Text.Length == 0)
                viewModel.SelectedContainer.Empty();
            else
            {
                try
                {
                    viewModel.SelectedContainer.Empty(double.Parse(tbAmount.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Container_Full(object sender, ContainerFullEventArgs e)
        {
            ContainerViewModel container = (ContainerViewModel)sender;
            Debug.WriteLine($"{container} full, overflow={e.Overflow}");
        }

        private void AddContainer(ContainerViewModel container)
        {
            container.Full += Container_Full;
            viewModel.Containers.Add(container);
        }
    }
}
