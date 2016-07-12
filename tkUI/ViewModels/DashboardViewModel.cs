using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;

using tkUI.Helper_Classes;
using tkUI.Subpages.GraphQuickBoxes.Utils;
using tkUI.Subpages.GraphQuickBoxes.ViewModels;

using System.Windows;

namespace tkUI.ViewModels
{
    /*lass DashboardViewModel : ObservableObject, IPageViewModel, ISourceIcons*/
    class DashboardViewModel : PageFromNavigation
    {

        #region Fields

        private ICommand _changePageCommand;

        private IBoxes _currentPageViewModel;
        private List<IBoxes> _pageViewModels;

        static ComboBox [] _selectorDateRange;
        static ComboBox _currentSelectorDateRange;
        static bool _initSelector;


        //TODO: Track down this behaviour.
        /* Note: Since we need to make the first QuickBox IsChecked = true to show the border in the selected state,
         * we set the property Checked of the first ViewModel to true. But this causes to call the ChangePageCommand
         * before the View is rendered (and therefore the Chart added to the container) causing to lose this object.
         * So when the ChangePageCommand is called for first time (this is done automatically by the IsChecked property)
         * we don't actually execute the command by using the bool _avoidChange.
         */
        static bool _avoidChange;

        #endregion // Fields

        #region Constructors

        public DashboardViewModel()
        {
            // Add available Pages
            PageViewModels.Add(new ExpectedPaymentViewModel() { Checked = true });
            PageViewModels.Add(new EmployeesInCompanyViewModel());
            PageViewModels.Add(new EmployeesHiredViewModel());
            PageViewModels.Add(new EmployeesDismissedViewModel());

            if (!_initSelector)
            {
                // Create and fill the Comboboxes
                _selectorDateRange = new ComboBox[PageViewModels.Count];
                for (int i = 0; i < _selectorDateRange.Length; ++i)
                {
                    _selectorDateRange[i] = new ComboBox();
                    _selectorDateRange[i].ItemsSource = ComboboxesGraph.Items;
                    _selectorDateRange[i].SelectedIndex = 0;
                }
                // Default Selector
                _currentSelectorDateRange = _selectorDateRange[0];
                SetBindingComboboxRangeDate(0);
                _initSelector = true;
            }
                // Set default graph
                CurrentPageViewModel = PageViewModels[0];
            
        }

        #endregion // Constructors

        #region Interface Implementations
        public override string Name
        {
            get
            {
                return "Dashboard";
            }
        }

        public override string Clicked
        {
            get
            {
                return "";
            }
        }

        public override string Hover
        {
            get
            {
                return "";
            }
        }


        public override string Normal
        {
            get
            {
                return "Resources/IconsNavigation/DashboardIcon.png";
            }
        }

        public override string Selected
        {
            get
            {
                return "Resources/IconsNavigation/DashboardSelectedIcon.png";
            }
        }

        #endregion // Interface Implementations


        #region Properties / Commands

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null || !_avoidChange) // _avoidChange -> See field definition for more info
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IBoxes)p),
                        p => p is IBoxes);
                    _avoidChange = true;
                }

                return _changePageCommand;
            }
        }

        public List<IBoxes> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IBoxes>();

                return _pageViewModels;
            }
        }

        public IBoxes CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        public static ComboBox CurrentSelectorDateRange
        {
            get { return _currentSelectorDateRange; }
            set { _currentSelectorDateRange = value; }
        }


        #endregion

        #region Methods

        private void ChangeViewModel(IBoxes viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
            // old
            //ChangeComboboxRangeDate(ViewsMapper(viewModel));
            // new
            DeleteBindingComboboxRangeDate();
            int id = ViewsMapper(viewModel);
            _currentSelectorDateRange = _selectorDateRange[id];
            SetBindingComboboxRangeDate(id);

        }

        private int ViewsMapper(IBoxes page)
        {
            int index = 0;
            for (int i = 0; i < PageViewModels.Count; ++i)
            {
                if (PageViewModels[i].BoxName.Equals(page.BoxName))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private void ChangeComboboxRangeDate(int i)
        {
            _currentSelectorDateRange.SelectedIndex = _selectorDateRange[i].SelectedIndex;
        }

        private void SetBindingComboboxRangeDate(int i)
        {
            Binding bind = new Binding();
            bind.Source = _selectorDateRange[i];
            bind.Path = new PropertyPath("SelectedIndex");
            _currentSelectorDateRange.SetBinding(ComboBox.SelectedIndexProperty, bind);
        }

        private void DeleteBindingComboboxRangeDate()
        {
            BindingOperations.ClearBinding(_currentSelectorDateRange, ComboBox.SelectedIndexProperty);
        }

    }
        #endregion

}

