using System.Collections.ObjectModel;

namespace Buckets.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ContainerViewModel selectedContainer;

        public ObservableCollection<ContainerViewModel> Containers { get; } = new();

        public ContainerViewModel SelectedContainer
        {
            get => selectedContainer;
            set
            {
                if (selectedContainer != value)
                {
                    selectedContainer = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsContainerSelected));
                }
            }
        }

        public bool IsContainerSelected => SelectedContainer != null;
    }
}
