using Buckets.Common.Model;
using System;

namespace Buckets.ViewModel
{
    public delegate void FullEventHandler(object sender, ContainerFullEventArgs e);

    public class ContainerViewModel : ViewModelBase
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
                }
            }
        }
        

        public override string ToString()
        {
            return $"{Capacity} {container.GetType().Name}\n{Content}\n";
        }

        public void Fill(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount",
                    "amount must be greater than 0");
            }
            Content += amount;

            if (Content == Capacity)
            {
                OnFull(new ContainerFullEventArgs());
            }
            else if (Content > Capacity)
            {
                OnFull(new ContainerFullEventArgs(Content - Capacity));
                Content = Capacity;
            }
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

        public void Empty()
        {
            Content = 0;
        }

        protected void OnFull(ContainerFullEventArgs e)
        {
            FullEventHandler full = Full;
            if (full != null)
            {
                full(this, e);
            }
        }
    }
}
