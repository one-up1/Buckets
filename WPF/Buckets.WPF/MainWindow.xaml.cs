using Buckets.Common.Model;
using Buckets.ViewModel;
using Buckets.ViewModel.Event;
using System;
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
            try
            {
                double content = tbBucketContent.Text.Length == 0 ? 0 : double.Parse(tbBucketContent.Text);
                Bucket bucket;
                if (tbBucketCapacity.Text.Length == 0)
                    bucket = Bucket.GetDefault(content);
                else
                    bucket = Bucket.Get(double.Parse(tbBucketCapacity.Text), content);

                BucketViewModel bucketViewModel = new BucketViewModel(bucket);
                AddContainer(bucketViewModel);
                viewModel.OtherBuckets.Add(bucketViewModel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void bCreateRainBarrel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double content = tbRainBarrelContent.Text.Length == 0 ? 0 : double.Parse(tbRainBarrelContent.Text);
                RainBarrel rainBarrel;
                if (rbRainBarrelSmall.IsChecked == true)
                    rainBarrel = RainBarrel.GetSmall(content);
                else if (rbRainBarrelLarge.IsChecked == true)
                    rainBarrel = RainBarrel.GetLarge(content);
                else
                    rainBarrel = RainBarrel.Get(content);

                AddContainer(new RainBarrelViewModel(rainBarrel));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void bCreateOilBarrel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double content = tbOilBarrelContent.Text.Length == 0 ? 0 : double.Parse(tbOilBarrelContent.Text);
                OilBarrel oilBarrel = OilBarrel.Get(content);

                AddContainer(new OilBarrelViewModel(oilBarrel));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void bFillContainer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.SelectedContainer.Fill(double.Parse(tbAmount.Text),
                    cbForceFill.IsChecked == true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void bFillBucket_Click(object sender, RoutedEventArgs e)
        {
            if (cbFillBucket.SelectedItem != null)
            {
                BucketViewModel sourceBucket = viewModel.SelectedContainer as BucketViewModel;
                BucketViewModel targetBucket = cbFillBucket.SelectedItem as BucketViewModel;
                try
                {
                    double amount = tbAmount.Text.Length == 0 ? sourceBucket.Content : double.Parse(tbAmount.Text);
                    sourceBucket.Fill(targetBucket, amount, cbForceFill.IsChecked == true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Container_Full(object sender, ContainerFullEventArgs e)
        {
            ContainerViewModel container = sender as ContainerViewModel;
            if (e.Overflow == 0)
                MessageBox.Show($"{container} is full", Title, MessageBoxButton.OK, MessageBoxImage.Information);
            else if (MessageBox.Show($"{container} will overflow with {e.Overflow}, continue?", Title,
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (e is BucketOverflowEventArgs)
                    ((BucketOverflowEventArgs)e).SourceBucket.Fill((BucketViewModel)sender, e.Amount, true);
                else
                    container.Fill(e.Amount, true);
            }
        }

        private void AddContainer(ContainerViewModel container)
        {
            container.Full += Container_Full;
            viewModel.Containers.Add(container);
        }
    }
}
