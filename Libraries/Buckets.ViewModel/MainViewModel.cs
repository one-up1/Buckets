using System.Collections.ObjectModel;

namespace Buckets.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ContainerViewModel selectedContainer;

        public ObservableCollection<ContainerViewModel> Containers { get; } = new();

        public ObservableCollection<BucketViewModel> OtherBuckets { get; } = new();

        public ContainerViewModel SelectedContainer
        {
            get => selectedContainer;
            set
            {
                if (selectedContainer != value)
                {
                    if (selectedContainer is BucketViewModel)
                        OtherBuckets.Add(selectedContainer as BucketViewModel);
                    if (value is BucketViewModel)
                        OtherBuckets.Remove(value as BucketViewModel);

                    selectedContainer = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsContainerSelected));
                    OnPropertyChanged(nameof(IsBucketSelected));
                }
            }
        }

        public bool IsContainerSelected => SelectedContainer != null;

        public bool IsBucketSelected => SelectedContainer is BucketViewModel;
    }
}
