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
    class OpeningDetailViewModel : MvxViewModel
    {
        private IOpeningService _openingService;
        private IDialogService _dialogService;

        public OpeningDetailViewModel(IOpeningService openingService, IDialogService dialogService)
        {
            _openingService = openingService;
            _dialogService = dialogService;
        }

        private OpeningDetail _detail;
        public OpeningDetail Detail
        {
            get { return _detail; }
            set { _detail = value; RaisePropertyChanged(() => Detail); }
        }

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; RaisePropertyChanged(() => FileName); RaisePropertyChanged(() => Uri); }
        }

        public string Uri => $"http://openings.moe/video/{FileName}";

        public void Init(string fileName)
        {
            FileName = fileName;
        }

        public override void Start()
        {
            DoRetrieveOpeningDetail();
            base.Start();
        }

        private async void DoRetrieveOpeningDetail()
        {
            _dialogService = Mvx.Resolve<IDialogService>();
            var dialog = await _dialogService.showLoadingDialog("Loading. . .");

            try
            {
                Detail = await _openingService.RetrieveOpeningDetail(FileName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error Opening Detail - {ex.Message}");
            }
            finally
            {
                _dialogService.CloseDialog(dialog);
            }
        }
    }
}
