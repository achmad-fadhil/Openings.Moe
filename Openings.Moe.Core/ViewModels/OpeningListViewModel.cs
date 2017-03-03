using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Openings.Moe.Core.Models;
using Openings.Moe.Core.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openings.Moe.Core.ViewModels
{
    class OpeningListViewModel : MvxViewModel
    {
        private IOpeningService _openingService;
        private IDialogService _dialogService;

        private List<Opening> _filteredOpenings;
        public List<Opening> FilteredOpenings
        {
            get { return _filteredOpenings; }
            private set { _filteredOpenings = value; RaisePropertyChanged(() => FilteredOpenings); }
        }

        private List<Opening> _openings;
        public List<Opening> Openings
        {
            get { return _openings; }
            set { _openings = value; RaisePropertyChanged(() => Openings); }
        }

        private string _searchQuery;
        public String SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                _searchQuery = value;
                Filter(value);
                RaisePropertyChanged(() => SearchQuery);
            }
        }

        public OpeningListViewModel(IOpeningService openingService, IDialogService dialogService)
        {
            _openingService = openingService;
            _dialogService = dialogService;
        }

        public override void Start()
        {
            DoRetrieveOpeningsList();
            base.Start();
        }

        private async void DoRetrieveOpeningsList()
        {
            _dialogService = Mvx.Resolve<IDialogService>();
            var dialog = await _dialogService.showLoadingDialog("Loading. . .");

            try
            {
                Openings = await _openingService.RetrieveAllOpenings();
                FilteredOpenings = Openings;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                _dialogService.CloseDialog(dialog);
            }
        }

        private void Filter(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                FilteredOpenings = Openings;
            }
            else
            {
                FilteredOpenings = Openings.Where(o => o.Source.ToLower().Contains(query)).ToList();
            }
        }

        public IMvxCommand OpenDetailCommand => new MvxCommand<Opening>(DoOpenDetail);

        private void DoOpenDetail(Opening param)
        {
            ShowViewModel<OpeningDetailViewModel>(new { fileName = param.File });
        }


    }
}
