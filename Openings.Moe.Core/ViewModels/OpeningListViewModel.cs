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
                Search(value);
                RaisePropertyChanged(() => SearchQuery);
            }
        }

        private bool _sortMode = false;
        public bool SortMode
        {
            get { return _sortMode; }
            set { _sortMode = value; RaisePropertyChanged(() => SortMode); }
        }

        private List<string> _sortType = new List<string> { "Source", "Song Title", "Song Artist" };
        private List<string> _filterType = new List<string> { "Opening", "Ending", "All" };

        private List<string> _options;
        public List<string> Options
        {
            get { return _options; }
            set { _options = value; RaisePropertyChanged(() => Options); }
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

        private void Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                FilteredOpenings = Openings;
            }
            else
            {
                query = query.ToLower();
                FilteredOpenings = Openings
                    .Where(o => 
                        o.Source.ToLower().Contains(query)
                        || (o.Song != null 
                            && (o.Song.Artist.ToLower().Contains(query) 
                                || o.Song.Title.ToLower().Contains(query)))
                    )
                    .ToList();
            }
        }

        private bool isSort = false;
        private bool isFilter = false;

        public IMvxCommand OpenSortCommand => new MvxCommand(DoOpenSort);

        private void DoOpenSort()
        {
            Options = _sortType;
            isSort = !isSort;
            isFilter = false;
            SortMode = isSort;
        }

        public IMvxCommand OpenFilterCommand => new MvxCommand(DoOpenFilter);

        private void DoOpenFilter()
        {
            Options = _filterType;
            isFilter = !isFilter;
            isSort = false;
            SortMode = isFilter;
        }

        public IMvxCommand SortCommand => new MvxCommand<string>(DoSort);

        private void DoSort(string param)
        {
            if (isFilter)
            {
                switch (param)
                {
                    case "All":
                        FilteredOpenings = Openings;
                        break;
                    default:
                        FilteredOpenings = Openings.Where(o => o.Title.ToLower().Contains(param.ToLower())).ToList();
                        break;
                }
                return;
            }

            switch (param)
            {
                case "Source":
                    FilteredOpenings = FilteredOpenings.OrderBy(o => o.Source).ToList();
                    break;
                case "Song Artist":
                    FilteredOpenings = FilteredOpenings
                        .Where(o => o.Song != null)
                        .OrderBy(o => o.Song.Artist)
                        .Concat(FilteredOpenings
                            .Where(x => x.Song == null)
                            .OrderBy(x => x.Source))
                        .ToList();
                    break;
                case "Song Title":
                    FilteredOpenings = FilteredOpenings
                        .Where(o => o.Song != null)
                        .OrderBy(o => o.Song.Title)
                        .Concat(FilteredOpenings
                            .Where(x => x.Song == null)
                            .OrderBy(x => x.Source))
                        .ToList();
                    break;
            }
        }
        
        public IMvxCommand OpenDetailCommand => new MvxCommand<Opening>(DoOpenDetail);

        private void DoOpenDetail(Opening param)
        {
            ShowViewModel<OpeningDetailViewModel>(new { fileName = param.File });
        }


    }
}
