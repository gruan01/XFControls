using Plugin.HtmlLabel.iOS;
using System;
namespace AsNum.XFControls.iOS
{
	public static class AsNumAssemblyHelper
	{
		public static void HoldAssembly()
		{
            // Do nothing, just hold assembly,
            // prevent linker remove this assembly.
            HtmlLabelRenderer.Initialize();
        }
	}
}