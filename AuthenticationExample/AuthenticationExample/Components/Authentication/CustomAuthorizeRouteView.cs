namespace AuthenticationExample.Components.Authentication;

using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;

using RouteData = Microsoft.AspNetCore.Components.RouteData;

public sealed class CustomAuthorizeRouteView : RouteView
{
    private static readonly RenderFragment<AuthenticationState> DefaultNotAuthorizedContent
        = _ => builder => builder.AddContent(0, "Not authorized");
    private static readonly RenderFragment DefaultAuthorizingContent
        = builder => builder.AddContent(0, "Authorizing...");

    private readonly RenderFragment renderAuthorizeRouteViewCoreDelegate;
    private readonly RenderFragment<AuthenticationState> renderAuthorizedDelegate;
    private readonly RenderFragment<AuthenticationState> renderNotAuthorizedDelegate;
    private readonly RenderFragment renderAuthorizingDelegate;

    public CustomAuthorizeRouteView()
    {
        void RenderBaseRouteViewDelegate(RenderTreeBuilder builder) => base.Render(builder);
        renderAuthorizedDelegate = _ => RenderBaseRouteViewDelegate;
        renderNotAuthorizedDelegate = authenticationState => builder => RenderNotAuthorizedInDefaultLayout(builder, authenticationState);
        renderAuthorizingDelegate = RenderAuthorizingInDefaultLayout;
        renderAuthorizeRouteViewCoreDelegate = RenderAuthorizeRouteViewCore;
    }

    [Parameter]
    public RenderFragment<AuthenticationState>? NotAuthorized { get; set; }

    [Parameter]
    public RenderFragment? Authorizing { get; set; }

    [Parameter]
    public object? Resource { get; set; }

    [Parameter]
    public Type NotAuthorizedLayout { get; set; } = default!;

    [CascadingParameter]
    private Task<AuthenticationState>? ExistingCascadedAuthenticationState { get; set; }

    protected override void Render(RenderTreeBuilder builder)
    {
        if (ExistingCascadedAuthenticationState != null)
        {
            renderAuthorizeRouteViewCoreDelegate(builder);
        }
        else
        {
            builder.OpenComponent<CascadingAuthenticationState>(0);
            builder.AddAttribute(1, nameof(CascadingAuthenticationState.ChildContent), renderAuthorizeRouteViewCoreDelegate);
            builder.CloseComponent();
        }
    }

    private void RenderAuthorizeRouteViewCore(RenderTreeBuilder builder)
    {
        builder.OpenComponent<AuthorizeRouteViewCore>(0);
        builder.AddAttribute(1, nameof(AuthorizeRouteViewCore.RouteData), RouteData);
        builder.AddAttribute(2, nameof(AuthorizeRouteViewCore.Authorized), renderAuthorizedDelegate);
        builder.AddAttribute(3, nameof(AuthorizeRouteViewCore.Authorizing), renderAuthorizingDelegate);
        builder.AddAttribute(4, nameof(AuthorizeRouteViewCore.NotAuthorized), renderNotAuthorizedDelegate);
        builder.AddAttribute(5, nameof(AuthorizeRouteViewCore.Resource), Resource);
        builder.CloseComponent();
    }

    private void RenderContentInDefaultLayout(RenderTreeBuilder builder, RenderFragment content)
    {
        builder.OpenComponent<LayoutView>(0);
        builder.AddAttribute(1, nameof(LayoutView.Layout), DefaultLayout);
        builder.AddAttribute(2, nameof(LayoutView.ChildContent), content);
        builder.CloseComponent();
    }

    private void RenderContentInNotAuthorizedLayout(RenderTreeBuilder builder, RenderFragment content)
    {
        builder.OpenComponent<LayoutView>(0);
        builder.AddAttribute(1, nameof(LayoutView.Layout), NotAuthorizedLayout);
        builder.AddAttribute(2, nameof(LayoutView.ChildContent), content);
        builder.CloseComponent();
    }

    private void RenderNotAuthorizedInDefaultLayout(RenderTreeBuilder builder, AuthenticationState authenticationState)
    {
        var content = NotAuthorized ?? DefaultNotAuthorizedContent;
        RenderContentInNotAuthorizedLayout(builder, content(authenticationState));
    }

    private void RenderAuthorizingInDefaultLayout(RenderTreeBuilder builder)
    {
        var content = Authorizing ?? DefaultAuthorizingContent;
        RenderContentInDefaultLayout(builder, content);
    }

#pragma warning disable CA1812
    private sealed class AuthorizeRouteViewCore : AuthorizeViewCore
    {
        [Parameter]
        public RouteData RouteData { get; set; } = default!;

        protected override IAuthorizeData[]? GetAuthorizeData()
            => AttributeAuthorizeDataCache.GetAuthorizeDataForType(RouteData.PageType);
    }
#pragma warning restore CA1812
}
