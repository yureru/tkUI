using System.Windows;
using System.Windows.Media;

/// <summary>
/// Classes used to provide Image as Dependency Properties.
/// Each one is used for a certain State: Normal, When the image is selected, when is clicked, and
/// when is on hover.
/// </summary>
namespace tkUI
{
    /// <summary>
    /// Image used in a normal state.
    /// </summary>
    public class Icons
    {
        public static readonly DependencyProperty ImageProperty;

        static Icons()
        {
            var metadata = new FrameworkPropertyMetadata((ImageSource)null);
            ImageProperty = DependencyProperty.RegisterAttached("Image", typeof(ImageSource), typeof(Icons), metadata);
        }

        public static ImageSource GetImage(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(ImageProperty);
        }

        public static void SetImage(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(ImageProperty, value);
        }
    }

    /// <summary>
    /// Image used in a clicked state.
    /// </summary>
    public class IconsHover
    {
        public static readonly DependencyProperty ImageHoverProperty;

        static IconsHover()
        {
            var metadata = new FrameworkPropertyMetadata((ImageSource)null);
            ImageHoverProperty = DependencyProperty.RegisterAttached("ImageHover", typeof(ImageSource), typeof(IconsHover), metadata);
        }

        public static ImageSource GetImageHover(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(ImageHoverProperty);
        }

        public static void SetImageHover(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(ImageHoverProperty, value);
        }
    }

    /// <summary>
    /// Image used in a clicked state.
    /// </summary>
    public class IconsClicked
    {
        public static readonly DependencyProperty ImageClickedProperty;

        static IconsClicked()
        {
            var metadata = new FrameworkPropertyMetadata((ImageSource)null);
            ImageClickedProperty = DependencyProperty.RegisterAttached("ImageClicked", typeof(ImageSource), typeof(IconsSelected), metadata);
        }

        public static ImageSource GetImageClicked(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(ImageClickedProperty);
        }

        public static void SetImageClicked(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(ImageClickedProperty, value);
        }
    }

    /// <summary>
    /// Image used in a selected state.
    /// </summary>
    public class IconsSelected
    {
        public static readonly DependencyProperty ImageSelectedProperty;

        static IconsSelected()
        {
            var metadata = new FrameworkPropertyMetadata((ImageSource)null);
            ImageSelectedProperty = DependencyProperty.RegisterAttached("ImageSelected", typeof(ImageSource), typeof(IconsSelected), metadata);
        }

        public static ImageSource GetImageSelected(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(ImageSelectedProperty);
        }

        public static void SetImageSelected(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(ImageSelectedProperty, value);
        }
    }
}
