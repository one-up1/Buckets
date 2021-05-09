using Buckets.Common.Model;
using System.Collections.ObjectModel;

namespace Buckets.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Container> Containers { get; } = new();
    }
}
