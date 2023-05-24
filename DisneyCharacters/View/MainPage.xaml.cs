using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace DisneyCharacters.View;

public partial class MainPage : ContentPage
{
    // Track the previous item 
    int previousCarouselIndex = -1;

	public MainPage(CharacterViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

        carouselView.ItemsUpdatingScrollMode = ItemsUpdatingScrollMode.KeepLastItemInView;
        if (! DebugConstants.UseDefaultCarouselAnimation)
        {
            carouselView.IsScrollAnimated = DebugConstants.UseDefaultCarouselAnimation;
            carouselView.Scrolled += OnCarouselViewScrolled;
        }
	}

    void OnCarouselViewScrolled(System.Object sender, Microsoft.Maui.Controls.ItemsViewScrolledEventArgs e)
    {
        Debug.WriteLine("OnCarouselViewScrolled CenterItemIndex: " + e.CenterItemIndex);
        if (previousCarouselIndex == -1)
        {
            previousCarouselIndex = e.CenterItemIndex;
        }
        if (e.CenterItemIndex != previousCarouselIndex && previousCarouselIndex != -1 && !DebugConstants.UseDefaultCarouselAnimation)
        {
            AnimateAndGotoAsync(previousCarouselIndex, e.CenterItemIndex);
        }
    }

    /// <summary>
    /// Animate a transition for scrolling left or right.
    /// This is triggered when the carousel scroll has moved more than half the card, and is ready to change.
    /// We take over the animation of the scroll window - so adjust and slide to an approximation of the start point
    /// </summary>
    /// <param name="oldIndex"></param>
    /// <param name="newIndex"></param>
    async void AnimateAndGotoAsync(int oldIndex, int newIndex)
    {
        carouselView.CancelAnimations();

        // The correct element (card) to select should be:
        // var animView = goLeft ? carouselView.VisibleViews.First() : carouselView.VisibleViews.Last();
        var animView = carouselView;
        animView.CancelAnimations();
        Debug.WriteLine($"AnimateAndGotoAsync old: {oldIndex} new: {newIndex}");

        if (oldIndex != newIndex)
        {
            previousCarouselIndex = newIndex;
            var goLeft = (newIndex > oldIndex);

            // Half the width - distance to edge
            var edgeDist = (DeviceDisplay.MainDisplayInfo.Width / 2) / DeviceDisplay.MainDisplayInfo.Density;
            var startDist = edgeDist / 3;
            animView.TranslationX = goLeft ? -startDist : startDist;
            carouselView.ScrollTo(oldIndex, position: ScrollToPosition.MakeVisible, animate: false);

            if (goLeft)
            {
                await Task.WhenAll(
                    animView.TranslateTo(-edgeDist, -200, 500, easing: Easing.CubicInOut),
                    animView.RotateTo(-35, 500, Easing.CubicInOut),
                    animView.ScaleTo(0.5, 700, easing: Easing.CubicInOut)
                );
            }
            else
            {
                await Task.WhenAll(
                    animView.TranslateTo(edgeDist, -200, 500, easing: Easing.CubicInOut),
                    animView.RotateTo(35, 500, Easing.CubicInOut),
                    animView.ScaleTo(0.5, 700, easing: Easing.CubicInOut)
                );
            }

            animView.Rotation = 0;
            animView.TranslationX = 0;
            animView.TranslationY = 0;
            animView.Scale = 1;

        }

        carouselView.ScrollTo(newIndex, position: ScrollToPosition.MakeVisible, animate: false);
    }
}

