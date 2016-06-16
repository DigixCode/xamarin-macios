using System;
using System.Threading.Tasks;
using NUnit.Framework;

#if !XAMCORE_2_0
using MonoMac.AppKit;
using MonoMac.CoreAnimation;
using MonoMac.CoreGraphics;
using MonoMac.Foundation;
using CGRect = System.Drawing.RectangleF;
#else
using AppKit;
using CoreAnimation;
using CoreGraphics;
using Foundation;
#endif

namespace Xamarin.Mac.Tests
{
	[TestFixture]
	public class CALayerTests
	{
		[Test]
		public void CALayer_ValuesTests ()
		{
			CALayer layer = new CALayer ();
			CGRect frame = new CGRect (10, 10, 10, 10);
			using (var provider = new CGDataProvider (new byte [(int) frame.Width * (int) frame.Height * 4 ])) {
				using (var image = new CGImage ((int) frame.Width, (int) frame.Height, 8, 32, (int) frame.Width * 4, CGColorSpace.CreateDeviceRGB (), CGBitmapFlags.None, provider, null, false, CGColorRenderingIntent.Default)) {
					NSImage NSImage = new NSImage ();

					layer.Contents = image;
					CGImage arrayImage = layer.Contents;
					Assert.AreEqual (image.Handle, arrayImage.Handle);
					
					layer.SetContents (NSImage);
					NSImage arrayNSImage = layer.GetContentsAs<NSImage> ();
					Assert.AreEqual (NSImage.Handle, arrayNSImage.Handle);

					layer.SetContents (null); // Should not throw
					layer.Contents = null; // Should not throw
				}
			}
		}
	}
}

