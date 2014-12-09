using Abp.Application.Navigation;
using Abp.Localization;

namespace Ipek_Helpdesk.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class Ipek_HelpdeskNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Moderator",
                        new LocalizableString("Moderator", Ipek_HelpdeskConsts.LocalizationSourceName),
                        url: "/Moderator",
                        icon: "fa fa-home"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Agent",
                        new LocalizableString("Agent", Ipek_HelpdeskConsts.LocalizationSourceName),
                        url: "/Agent",
                        icon: "fa fa-info"
                        )
                );
        }
    }
}
