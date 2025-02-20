﻿using System.Threading.Tasks;
using SmartAbp.Localization;
using SmartAbp.MultiTenancy;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace SmartAbp.Web.Menus
{
    public class SmartAbpMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var administration = context.Menu.GetAdministration();
            var l = context.GetLocalizer<SmartAbpResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    SmartAbpMenus.Home,
                    l["Menu:Home"],
                    "~/",
                    icon: "fas fa-home",
                    order: 0
                )
            );

            context.Menu.AddItem(
                new ApplicationMenuItem(
                    "BooksStore",
                    l["书籍商城"],
                    icon: "fa fa-book"
                ).AddItem(
                    new ApplicationMenuItem(
                        "SmartAbp.Books",
                        l["书籍列表"],
                        url: "/Books"
                    )
                )
            );
            context.Menu.AddItem(
                new ApplicationMenuItem(
                    "StationsStore",
                    l["工作站"],
                    icon: "fa fa-book"
                ).AddItem(
                    new ApplicationMenuItem(
                        "SmartAbp.Stations",
                        l["工作站列表"],
                        url: "/Stations"
                    )
                )
            );

            if (MultiTenancyConsts.IsEnabled)
            {
                administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
            }
            else
            {
                administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
            }

            administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
            administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);
        }
    }
}
