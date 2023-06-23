using FSH.BlazorWebAssembly.Client.Infrastructure.ApiClient;
using Blazored.SessionStorage;
using FSH.BlazorWebAssembly.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using FSH.BlazorWebAssembly.Client.Pages.Environment;

namespace FSH.BlazorWebAssembly.Client.Components.Company;

public class CompanyAutoComplete : MudAutocomplete<int>
{
    [Inject]
    private IStringLocalizer<CompanyAutoComplete> L { get; set; } = default!;
    [Inject]
    private ICompanysClient CompanysClient { get; set; } = default!;
    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;
    [Inject]
    private ISessionStorageService SessionStorage { get; set; } = default!;
    private List<CompanySearchDto> _companys = new List<CompanySearchDto>();
    private int _mandantId { get; set; } = 0;

    public override Task SetParametersAsync(ParameterView parameters)
    {
        //Label = L["Company Name, Zip, City"];
        Variant = Variant.Outlined;
        Dense = false;
        Margin = Margin.Normal;
        ResetValueOnEmptyText = true;
        SearchFunc = SearchCompany;
        ToStringFunc = GetCompanyDetail;
        Clearable = true;
        MinCharacters = 2;
        return base.SetParametersAsync(parameters);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender &&
            _value != default &&
            await ApiHelper.ExecuteCallGuardedAsync(
                () => CompanysClient.GetAsync(_value), Snackbar) is { } companyDto)
        {
            CompanySearchDto csDto = new();
            csDto.Id = companyDto.Id;
            csDto.Name1 = companyDto.Name1;
            csDto.Name2 = companyDto.Name2;
            csDto.Address1 = companyDto.Address1;
            csDto.Zip = companyDto.Zip;
            csDto.City = companyDto.City;

            _companys.Add(csDto);
            ForceRender(true);
        }
        else
        {
            if (_value > 0 && _companys.Find(x => x.Id == _value) is null)
            {
                CompanySearchDto companySearchDto = await CompanysClient.GetCompanySearchAsync(_value);
                _companys.Add(companySearchDto);
                ForceRender(true);
            }

        }
    }

    private async Task<IEnumerable<int>> SearchCompany(string value)
    {
        var filter = new SearchCompanysRequest
        {
            MandantId = _mandantId != 0 ? _mandantId : Convert.ToInt32(await SessionStorage.GetItemAsStringAsync("currentMandantId")),
            PageSize = 25,
            SearchString = value
        };

        _mandantId = filter.MandantId;

        if (await ApiHelper.ExecuteCallGuardedAsync(
                () => CompanysClient.SearchAsync(filter), Snackbar)
            is PaginationResponseOfCompanySearchDto response)
        {
            _companys = response.Data.ToList();
        }

        return _companys.Select(x => x.Id);
    }

    private string GetCompanyDetail(int id)
    {
        if (id == 0) return string.Empty;
        var company = _companys.Find(b => b.Id == id);
        string result = string.Empty;
        if (company != null)
        {
            result = company.Name1!;
            result += ", " + company.Name2;
            result += ", " + company.Address1;
            result += ", " + company.Zip;
            result += ", " + company.City;
        }

        return result;
    }
}
