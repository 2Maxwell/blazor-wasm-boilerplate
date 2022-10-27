using System.Collections.ObjectModel;

namespace FSH.WebApi.Shared.Authorization;

public static class FSHAction
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string Generate = nameof(Generate);
    public const string Clean = nameof(Clean);
    public const string UpgradeSubscription = nameof(UpgradeSubscription);
}

public static class FSHResource
{
    public const string Tenants = nameof(Tenants);
    public const string Dashboard = nameof(Dashboard);
    public const string Hangfire = nameof(Hangfire);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Products = nameof(Products);
    public const string Brands = nameof(Brands);
    public const string Categorys = nameof(Categorys);
    public const string Rooms = nameof(Rooms);
    public const string Packages = nameof(Packages);
    public const string Mandants = nameof(Mandants);
    public const string ItemGroups = nameof(ItemGroups);
    public const string Items = nameof(Items);
    public const string PriceSchema = nameof(PriceSchema);
    public const string Taxes = nameof(Taxes);

}

public static class FSHPermissions
{
    private static readonly FSHPermission[] _all = new FSHPermission[]
    {
        new("View Dashboard", FSHAction.View, FSHResource.Dashboard),
        new("View Hangfire", FSHAction.View, FSHResource.Hangfire),
        new("View Users", FSHAction.View, FSHResource.Users),
        new("Search Users", FSHAction.Search, FSHResource.Users),
        new("Create Users", FSHAction.Create, FSHResource.Users),
        new("Update Users", FSHAction.Update, FSHResource.Users),
        new("Delete Users", FSHAction.Delete, FSHResource.Users),
        new("Export Users", FSHAction.Export, FSHResource.Users),
        new("View UserRoles", FSHAction.View, FSHResource.UserRoles),
        new("Update UserRoles", FSHAction.Update, FSHResource.UserRoles),
        new("View Roles", FSHAction.View, FSHResource.Roles),
        new("Create Roles", FSHAction.Create, FSHResource.Roles),
        new("Update Roles", FSHAction.Update, FSHResource.Roles),
        new("Delete Roles", FSHAction.Delete, FSHResource.Roles),
        new("View RoleClaims", FSHAction.View, FSHResource.RoleClaims),
        new("Update RoleClaims", FSHAction.Update, FSHResource.RoleClaims),
        new("View Products", FSHAction.View, FSHResource.Products, IsBasic: true),
        new("Search Products", FSHAction.Search, FSHResource.Products, IsBasic: true),
        new("Create Products", FSHAction.Create, FSHResource.Products),
        new("Update Products", FSHAction.Update, FSHResource.Products),
        new("Delete Products", FSHAction.Delete, FSHResource.Products),
        new("Export Products", FSHAction.Export, FSHResource.Products),
        new("View Brands", FSHAction.View, FSHResource.Brands, IsBasic: true),
        new("Search Brands", FSHAction.Search, FSHResource.Brands, IsBasic: true),
        new("Create Brands", FSHAction.Create, FSHResource.Brands),
        new("Update Brands", FSHAction.Update, FSHResource.Brands),
        new("Delete Brands", FSHAction.Delete, FSHResource.Brands),
        new("Generate Brands", FSHAction.Generate, FSHResource.Brands),
        new("Clean Brands", FSHAction.Clean, FSHResource.Brands),
        new("View Tenants", FSHAction.View, FSHResource.Tenants, IsRoot: true),
        new("Create Tenants", FSHAction.Create, FSHResource.Tenants, IsRoot: true),
        new("Update Tenants", FSHAction.Update, FSHResource.Tenants, IsRoot: true),
        new("Upgrade Tenant Subscription", FSHAction.UpgradeSubscription, FSHResource.Tenants, IsRoot: true),
        new("View Categorys", FSHAction.View, FSHResource.Categorys, IsBasic: true),
        new("Search Categorys", FSHAction.Search, FSHResource.Categorys, IsBasic: true),
        new("Create Categorys", FSHAction.Create, FSHResource.Categorys),
        new("Update Categorys", FSHAction.Update, FSHResource.Categorys),
        new("Delete Categorys", FSHAction.Delete, FSHResource.Categorys),
        new("View Rooms", FSHAction.View, FSHResource.Rooms, IsBasic: true),
        new("Search Rooms", FSHAction.Search, FSHResource.Rooms, IsBasic: true),
        new("Create Rooms", FSHAction.Create, FSHResource.Rooms),
        new("Update Rooms", FSHAction.Update, FSHResource.Rooms),
        new("Delete Rooms", FSHAction.Delete, FSHResource.Rooms),
        new("View Packages", FSHAction.View, FSHResource.Packages, IsBasic: true),
        new("Search Packages", FSHAction.Search, FSHResource.Packages, IsBasic: true),
        new("Create Packages", FSHAction.Create, FSHResource.Packages),
        new("Update Packages", FSHAction.Update, FSHResource.Packages),
        new("Delete Packages", FSHAction.Delete, FSHResource.Packages),
        new("View Mandants", FSHAction.View, FSHResource.Mandants, IsBasic: true),
        new("Search Mandants", FSHAction.Search, FSHResource.Mandants, IsBasic: true),
        new("Create Mandants", FSHAction.Create, FSHResource.Mandants),
        new("Update Mandants", FSHAction.Update, FSHResource.Mandants),
        new("Delete Mandants", FSHAction.Delete, FSHResource.Mandants),
        new("View ItemGroup", FSHAction.View, FSHResource.ItemGroups, IsBasic: true),
        new("Search ItemGroup", FSHAction.Search, FSHResource.ItemGroups, IsBasic: true),
        new("Create ItemGroup", FSHAction.Create, FSHResource.ItemGroups),
        new("Update ItemGroup", FSHAction.Update, FSHResource.ItemGroups),
        new("Delete ItemGroup", FSHAction.Delete, FSHResource.ItemGroups),
        new("View Item", FSHAction.View, FSHResource.Items, IsBasic: true),
        new("Search Item", FSHAction.Search, FSHResource.Items, IsBasic: true),
        new("Create Item", FSHAction.Create, FSHResource.Items),
        new("Update Item", FSHAction.Update, FSHResource.Items),
        new("Delete Item", FSHAction.Delete, FSHResource.Items),
        new("View PriceSchema", FSHAction.View, FSHResource.PriceSchema, IsBasic: true),
        new("Search PriceSchema", FSHAction.Search, FSHResource.PriceSchema, IsBasic: true),
        new("Create PriceSchema", FSHAction.Create, FSHResource.PriceSchema),
        new("Update PriceSchema", FSHAction.Update, FSHResource.PriceSchema),
        new("Delete PriceSchema", FSHAction.Delete, FSHResource.PriceSchema),
        new("View Tax", FSHAction.View, FSHResource.Taxes, IsBasic: true),
        new("Search Tax", FSHAction.Search, FSHResource.Taxes, IsBasic: true),
        new("Create Tax", FSHAction.Create, FSHResource.Taxes),
        new("Update Tax", FSHAction.Update, FSHResource.Taxes),
        new("Delete Tax", FSHAction.Delete, FSHResource.Taxes),
    };

    public static IReadOnlyList<FSHPermission> All { get; } = new ReadOnlyCollection<FSHPermission>(_all);
    public static IReadOnlyList<FSHPermission> Root { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsRoot).ToArray());
    public static IReadOnlyList<FSHPermission> Admin { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => !p.IsRoot).ToArray());
    public static IReadOnlyList<FSHPermission> Basic { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsBasic).ToArray());
}

public record FSHPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}