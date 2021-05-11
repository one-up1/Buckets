using Buckets.Common.Model;
using Buckets.ViewModel.Event;
using System;

namespace Buckets.ViewModel
{
    public delegate void FullEventHandler(object sender, ContainerFullEventArgs e);

    public abstract class ContainerViewModel : ViewModelBase
    {
        public event FullEventHandler Full;

        private readonly Container container;

        public ContainerViewModel(Container container)
        {
            this.container = container;
        }

        public double Capacity
        {
            get => container.Capacity;
        }

        public double Content
        {
            get => container.Content;
            set
            {
                if (container.Content != value)
                {
                    container.Content = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DisplayValue));
                }
            }
        }

        public string DisplayValue
        {
            get => $"{ToString()}\n{Content}\n";
        }

        public override string ToString()
        {
            return Capacity.ToString();
        }

        public void Fill(double amount, bool force)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount",
                    "amount must be greater than 0");
            }

            double content = Content + amount;
            if (force)
                Content = content > Capacity ? Capacity : content;
            else if (content > Capacity)
                OnFull(new ContainerFullEventArgs(content - Capacity, amount));
            else if ((Content = content) == Capacity)
                OnFull(new ContainerFullEventArgs());
        }

        public void Empty()
        {
            Content = 0;
        }

        public void Empty(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount",
                    "amount must be greater than 0");
            }
            if (Content == 0)
            {
                throw new InvalidOperationException("Container is empty");
            }
            if (Content < amount)
            {
                throw new ArgumentOutOfRangeException("amount",
                    $"There is only {Content} left...");
            }

            Content -= amount;
        }

        protected void OnFull(ContainerFullEventArgs e)
        {
            Full?.Invoke(this, e);
        }
    }
}
