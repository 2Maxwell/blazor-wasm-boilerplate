using Blazored.SessionStorage;
using FSH.BlazorWebAssembly.Client.Components.Person;
using FSH.BlazorWebAssembly.Client.Infrastructure.ApiClient;
using FSH.BlazorWebAssembly.Client.Shared;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace FSH.BlazorWebAssembly.Client.Components.Person;

public class ContactAutoComplete : MudAutocomplete<int>
{
    [Inject]
    private IStringLocalizer<ContactAutoComplete> L { get; set; } = default!;
    [Inject]
    private IPersonsClient PersonsClient { get; set; } = default!;
    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;
    [Inject]
    private ISessionStorageService SessionStorage { get; set; } = default!;

    [Parameter]
    public int _companyId { get; set; }
    private List<ContactDto> _contacts = new List<ContactDto>();
    private int _mandantId { get; set; } = 0;

    public override Task SetParametersAsync(ParameterView parameters)
    {
        Label = L["Contact Name"];
        Variant = Variant.Outlined;
        Dense = false;
        Margin = Margin.Normal;
        ResetValueOnEmptyText = true;
        SearchFunc = SearchPerson;
        ToStringFunc = GetPersonName;
        Clearable = true;
        MinCharacters = 0;
        return base.SetParametersAsync(parameters);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_mandantId == 0)
        {
            _mandantId = Convert.ToInt32(await SessionStorage.GetItemAsStringAsync("currentMandantId"));
        }

        if (firstRender &&
            _value != default &&
            await ApiHelper.ExecuteCallGuardedAsync(
                () => PersonsClient.GetAsync(_value), Snackbar) is { } person)
        {
            ContactDto contact = person.Adapt<ContactDto>();
            _contacts.Add(contact);
            ForceRender(true);
        }
        else
        {
            if (_value > 0 && _contacts.Find(x => x.Id == _value) is null)
            {
                PersonDto personDto = await PersonsClient.GetAsync(_value);
                ContactDto contactDto = personDto.Adapt<ContactDto>();

                _contacts.Add(contactDto);
                ForceRender(true);
            }
        }
    }

    private async Task<IEnumerable<int>> SearchPerson(string value)
    {

        SearchContactsRequest filter = new();
        {
            filter.MandantId = _mandantId;
            filter.PageSize = 25;
            filter.CompanyId = _companyId;
            filter.Name = value;
        };


        if (await ApiHelper.ExecuteCallGuardedAsync(
                () => PersonsClient.SearchContactsAsync(filter), Snackbar)
            is PaginationResponseOfContactDto response)
        {
            _contacts = response.Data.ToList();
        }

        return _contacts.Select(x => x.Id);
    }

    private string GetPersonName(int id)
    {
        if (id == 0) return string.Empty;
        var person = _contacts.Find(b => b.Id == id);
        string result = string.Empty;
        if (person != null)
        {
            result = person.Name!;
            result += ", " + person.FirstName;
            result += ", " + person.Email;
            result += ", " + person.Telephone;
            result += ", " + person.Telefax;
        }

        return result;
    }
}
