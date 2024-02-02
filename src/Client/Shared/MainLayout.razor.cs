using Blazored.SessionStorage;
using FSH.BlazorWebAssembly.Client.Components.Dialogs;
using FSH.BlazorWebAssembly.Client.Infrastructure.ApiClient;
using FSH.BlazorWebAssembly.Client.Infrastructure.Preferences;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Security.Claims;

namespace FSH.BlazorWebAssembly.Client.Shared;

public partial class MainLayout
{
    [Inject]
    protected ISessionStorageService sessionStorage { get; set; } = default!;
    [Inject]
    protected IUsersClient UsersClient { get; set; } = default!;
    [Inject]
    protected IMandantsClient MandantsClient { get; set; } = default!;
    //[Inject]
    //private IGeneralClient GeneralClient { get; set; } = default!;

    [CascadingParameter]
    public Task<AuthenticationState> AuthState { get; set; } = default!;

    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;
    [Parameter]
    public EventCallback OnDarkModeToggle { get; set; }
    [Parameter]
    public EventCallback<bool> OnRightToLeftToggle { get; set; }
    public MandantDto? _mandantDto { get; set; } = null;

    private string? UserId { get; set; }
    public int MandantId { get; set; } = 0;
    public bool _loaded { get; set; } = false;
    private bool _drawerOpen;
    private bool _rightToLeft;

    public int _cashierId { get; set; }
    public string _cashierRegisterName { get; set; } = "";

    protected override async Task OnInitializedAsync()
    {
        MandantId = Convert.ToInt32(await sessionStorage.GetItemAsStringAsync("currentMandantId"));

        if (MandantId == 0)
        {
            var user = (await AuthState).User;
            if (user.Identity?.IsAuthenticated == true)
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    UserId = user.GetUserId();
                    // string? mandantId = user.GetMandantId();
                }
            }

            if (await ApiHelper.ExecuteCallGuardedAsync(() => UsersClient.GetByIdAsync(UserId), Snackbar) is UserDetailsDto currentUser)
            {
                MandantId = Convert.ToInt32(currentUser.MandantId);
                await sessionStorage.SetItemAsStringAsync("currentMandantId", MandantId.ToString());
            }
        }

        if (MandantId > 0)
        {
            _mandantDto = await MandantsClient.GetAsync(MandantId);
        }


        if (await ClientPreferences.GetPreference() is ClientPreference preference)
        {
            _rightToLeft = preference.IsRTL;
            _drawerOpen = preference.IsDrawerOpen;
        }

        _loaded = true;
    }

    private async Task RightToLeftToggle()
    {
        bool isRtl = await ClientPreferences.ToggleLayoutDirectionAsync();
        _rightToLeft = isRtl;

        await OnRightToLeftToggle.InvokeAsync(isRtl);
    }

    public async Task ToggleDarkMode()
    {
        await OnDarkModeToggle.InvokeAsync();
    }

    private async Task DrawerToggle()
    {
        _drawerOpen = await ClientPreferences.ToggleDrawerAsync();
    }

    private void Logout()
    {
        var parameters = new DialogParameters
            {
                { nameof(Dialogs.Logout.ContentText), $"{L["Logout Confirmation"]}"},
                { nameof(Dialogs.Logout.ButtonText), $"{L["Logout"]}"},
                { nameof(Dialogs.Logout.Color), Color.Error}
            };

        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };
        DialogService.Show<Dialogs.Logout>(L["Logout"], parameters, options);
    }

    private void Profile()
    {
        Navigation.NavigateTo("/account");
    }

    private async void OpenCashierDialog()
    {
        if (_cashierId == 0)
        {
            int cashierId = Convert.ToInt32(await sessionStorage.GetItemAsStringAsync("currentCashierId"));
            if (cashierId > 0)
            {
                _cashierId = cashierId;
            }
            else
            {
                // Dialog ChashierRegister ausführen und speichern
                var parametersCRDialog = new DialogParameters
                {
                    ["MandantId"] = MandantId // _mandantDto.Id,
                };
                var dialogCR = DialogService.ShowModal<CashierRegisterDialog>(parametersCRDialog);
                var resultCR = await dialogCR.Result;
            }
        }

        _cashierId = Convert.ToInt32(await sessionStorage.GetItemAsStringAsync("currentCashierId"));

        if (_cashierId > 0)
        {
            var currentCashierRegister = await sessionStorage.GetItemAsync<Dictionary<string, string>>("currentCashierRegister");
            _cashierRegisterName = currentCashierRegister["currentCashierRegisterName"];
            var parameters = new DialogParameters
            {
                ["mandantDto"] = _mandantDto,
                ["CashierRegisterId"] = _cashierId, // Convert.ToInt32(currentCashierRegister["currentCashierRegisterId"]),
                ["CashierRegisterName"] = currentCashierRegister["currentCashierRegisterName"],
            };

            var dialog = DialogService.ShowModal<CashierDialog>(parameters);
            var result = await dialog.Result;
            StateHasChanged();
        }
    }

    private async void OpenNightAuditDialog()
    {
        var parameters = new DialogParameters
        {
            ["mandantDto"] = _mandantDto,
        };

        var dialog = DialogService.ShowModal<NightAuditDialog>(parameters);
        var result = await dialog.Result;
        if (!result.Canceled)
        {
            // Info anzeigen das NightAudit erledigt ist und das Programm neu gestartet werden muss.


            //GetNightAuditRequest getNightAuditRequest = new GetNightAuditRequest
            //{
            //    MandantId = MandantId,
            //    Date = _mandantDto!.HotelDate,
            //};
            //GetNightAuditResponse? nightAuditResponse = await GeneralClient.GetNightAuditAsync(getNightAuditRequest);
        }

    }
}