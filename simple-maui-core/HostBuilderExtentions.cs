
#if ANDROID
using Android.Content;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace Nau.Simple.Maui.Core
{

    public static class HostBuilderExtentions
    {
        public static MauiAppBuilder ConfigureAppearingDisappearingEvents(this MauiAppBuilder builder)
        {
            builder.ConfigureMauiHandlers(handlers =>
            {

                PageHandler.PlatformViewFactory = (info) =>
                {
                    (info.VirtualView as ContentPage).Parent = info.MauiContext.Services.GetRequiredService<Window>();
                    var group = new PageContentViewGroup(info.MauiContext.Context)
                    {
                        CrossPlatformLayout = info.VirtualView
                    };

                    return group;
                };
               
            });

            return builder;
        }
    }

    public class PageContentViewGroup : ContentViewGroup
    {

        IPageController ContentPage => (IPageController)CrossPlatformLayout;
        public PageContentViewGroup(Context context) : base(context)
        {
            SetClipChildren(false);
        }
        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            ContentPage.SendAppearing();
        }


        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();
            ContentPage.SendDisappearing();
        }

    }
}
#elif IOS

using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace Nau.Simple.Maui.Core
{

    public static class HostBuilderExtentions
    {
        public static MauiAppBuilder ConfigureAppearingDisappearingEvents(this MauiAppBuilder builder)
        {
            builder.ConfigureMauiHandlers(handlers =>
            {

                PageHandler.PlatformViewFactory = (info) =>
                {
                    (info.VirtualView as ContentPage).Parent = info.MauiContext.Services.GetRequiredService<Window>();
                    var vc = new CustomPageViewController(info.VirtualView, info.MauiContext);
                   (info.VirtualView.Handler as PageHandler).ViewController = new CustomPageViewController(info.VirtualView, info.MauiContext);

                    return (Microsoft.Maui.Platform.ContentView)vc.View;
                };
               
            });

            return builder;
        }
    }

    public class CustomPageViewController : PageViewController
    {
        IPageController ContentPage => (IPageController)CurrentView;

        public CustomPageViewController(IView page, IMauiContext mauiContext) : base(page, mauiContext)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ContentPage.SendAppearing();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            ContentPage.SendDisappearing();
        }

    }
}

#endif