using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Core
{
    public class MainViewModel : BaseViewModel
    {
        #region Service
        readonly IFirebaseService _firebaseService;
        #endregion
        #region Properties
        bool _ledStatus;
        public bool LedStatus
        {
            get => _ledStatus;
            set => SetProperty(ref _ledStatus, value);
        }
        #endregion
        #region Commands
        public ICommand LigarCommand { get; }
        public ICommand DesligarCommand { get; }
        public ICommand GetStatusCommand { get; }
        

        #endregion
        #region Init
        public MainViewModel(INavigationService navigationService,
                             IFirebaseService firebaseService) 
            : base(navigationService, "Main Page")
        {
            _firebaseService = firebaseService;
            LigarCommand = new Command(async () => await ExecuteLigarCommandAsync());
            DesligarCommand = new Command(async () => await ExecuteDesligarCommandAsync());
            GetStatusCommand = new Command(async () => await ExecuteGetStatusCommandAsync());
        
        }

        protected override async Task InitializeAsync()
        {
            await GetLedStatus();
            await base.InitializeAsync();
        }

        #endregion

        public async Task GetLedStatus()
        {
            var teste = await _firebaseService.ObterLedStatus();
            LedStatus = teste.led_status == "true";
        }

        public async Task ExecuteDesligarCommandAsync()
        {
            await _firebaseService.AtualizarLed(false);
            await GetLedStatus();
        }

        public async Task ExecuteLigarCommandAsync()
        {
            await _firebaseService.AtualizarLed(true);
            await GetLedStatus();

        }
        public async Task ExecuteGetStatusCommandAsync()
        {
            await GetLedStatus();
        }

    }
}
