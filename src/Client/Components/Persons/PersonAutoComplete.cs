using Blazored.SessionStorage;
using FSH.BlazorWebAssembly.Client.Infrastructure.ApiClient;
using FSH.BlazorWebAssembly.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace FSH.BlazorWebAssembly.Client.Components.Person;

public class PersonAutoComplete : MudAutocomplete<int>
{
    [Inject]
    private IStringLocalizer<PersonAutoComplete> L { get; set; } = default!;
    [Inject]
    private IPersonsClient PersonsClient { get; set; } = default!;
    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;
    [Inject]
    private ISessionStorageService SessionStorage { get; set; } = default!;
    private List<PersonDto> _persons = new List<PersonDto>();
    private int _mandantId { get; set; } = 0;

    [Parameter]
    public string? DisplayName { get; set; } = "Person Name, Firstname, Zip";

    public override Task SetParametersAsync(ParameterView parameters)
    {
        Label = L[DisplayName!];
        Variant = Variant.Outlined;
        Dense = false;
        Margin = Margin.Normal;
        ResetValueOnEmptyText = true;
        SearchFunc = SearchPerson;
        ToStringFunc = GetPersonName;
        Clearable = true;
        MinCharacters = 3;
        return base.SetParametersAsync(parameters);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender &&
            _value != default &&
            await ApiHelper.ExecuteCallGuardedAsync(
                () => PersonsClient.GetAsync(_value), Snackbar) is { } person)
        {
            _persons.Add(person);
            ForceRender(true);
        }
        else
        {
            if (_value > 0 && _persons.Find(x => x.Id == _value) is null)
            {
                PersonDto personDto = await PersonsClient.GetAsync(_value);
                _persons.Add(personDto);
                ForceRender(true);
            }
        }
    }

    private async Task<IEnumerable<int>> SearchPerson(string value)
    {
        var filter = new SearchPersonsRequest
        {
            MandantId = _mandantId != 0 ? _mandantId : Convert.ToInt32(await SessionStorage.GetItemAsStringAsync("currentMandantId")),
            PageSize = 25,
            SearchString = value
        };

        _mandantId = filter.MandantId;

        if (await ApiHelper.ExecuteCallGuardedAsync(
                () => PersonsClient.SearchAsync(filter), Snackbar)
            is PaginationResponseOfPersonDto response)
        {
            _persons = response.Data.ToList();
        }

        return _persons.Select(x => x.Id);
    }

    private string GetPersonName(int id)
    {
        if (id == 0 ) return string.Empty;
        var person = _persons.Find(b => b.Id == id);
        string result = string.Empty;
        if (person != null)
        {
            result = person.Name!;
            result += ", " + person.FirstName;
            result += ", " + person.Address1;
            result += ", " + person.Zip;
            result += ", " + person.City;
        }

        return result;
    }
}
