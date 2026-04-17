using CommunityToolkit.Mvvm.ComponentModel;

namespace TaskManager.ViewModels
{
    /// <summary>
    /// Base ViewModel providing common functionality for all ViewModels.
    /// Implements <see cref="ObservableObject"/> and provides properties to indicate busy state for UI binding
    /// </summary>
    public abstract partial class BaseViewModel : ObservableObject
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (SetProperty(ref _isBusy, value))
                {
                    OnPropertyChanged(nameof(IsNotBusy));
                }
            }
        }

        public bool IsNotBusy
        {
            get => !IsBusy;
        }
    }
}