using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StickyNotes.AppResources
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppBaseStyles : ResourceDictionary
    {
		public AppBaseStyles ()
		{
			InitializeComponent ();
		}
	}
}