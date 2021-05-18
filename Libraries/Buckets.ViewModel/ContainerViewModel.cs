using Buckets.Common.Model;
using System.Runtime.CompilerServices;

namespace Buckets.ViewModel
{
    public abstract class ContainerViewModel : ViewModelBase
    {
        private readonly Container container;

        protected ContainerViewModel(Container container)
        {
            this.container = container;
        }

        public Container Container
        {
            get => container;
        }

        public double Capacity
        {
            get => container.Capacity;
        }

        public double Content
        {
            get => container.Content;
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
            container.Fill(amount, force);
            OnPropertyChanged();
        }

        public void Empty()
        {
            container.Empty();
            OnPropertyChanged();
        }

        public void Empty(double amount)
        {
            container.Empty(amount);
            OnPropertyChanged();
        }

        protected void OnPropertyChanged()
        {
            OnPropertyChanged(nameof(DisplayValue));
        }
    }
}