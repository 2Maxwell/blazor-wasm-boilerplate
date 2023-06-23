using System;
using System.Security.Claims;
using Blazored.SessionStorage;
using FSH.BlazorWebAssembly.Client.Infrastructure.ApiClient;
using FSH.BlazorWebAssembly.Client.Infrastructure.Common;
using FSH.BlazorWebAssembly.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace FSH.BlazorWebAssembly.Client.Components.Common;
public partial class PersonCard
{
    [Parameter]
    public string? Class { get; set; }
    [Parameter]
    public string? Style { get; set; }
    [Parameter]
    public string? MandantId { get; set; }

    [CascadingParameter]
    protected Task<AuthenticationState> AuthState { get; set; } = default!;

    [Inject]
    protected IUsersClient UsersClient { get; set; } = default!;
    //[Inject]
    //protected ISessionStorageService sessionStorage { get; set; } = default!;

    private string? UserId { get; set; }
    private string? Email { get; set; }
    private string? FullName { get; set; }
    private string? ImageUri { get; set; }
    //private string? MandantId { get; set; }
    public int UserMandantId { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadUserData();
        }
    }

    private async Task LoadUserData()
    {
        var user = (await AuthState).User;
        if (user.Identity?.IsAuthenticated == true)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                FullName = user.GetFullName();
                UserId = user.GetUserId();
                Email = user.GetEmail();
                ImageUri = string.IsNullOrEmpty(user?.GetImageUrl()) ? string.Empty : (Config[ConfigNames.ApiBaseUrl] + user?.GetImageUrl());
                //MandantId = await sessionStorage.GetItemAsStringAsync("currentMandantId");
                StateHasChanged();
            }

            //if (await ApiHelper.ExecuteCallGuardedAsync(() => UsersClient.GetByIdAsync(UserId), Snackbar) is UserDetailsDto currentUser)
            //{
            //    UserMandantId = Convert.ToInt32(currentUser.MandantId);
            //    await sessionStorage.SetItemAsStringAsync("currentMandantId", UserMandantId.ToString());

            //    // MandantId = UserMandantId.ToString();

            //    
            //}
        }
    }
}