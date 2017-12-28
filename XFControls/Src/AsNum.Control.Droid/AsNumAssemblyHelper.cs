using Plugin.HtmlLabel.Android;

namespace AsNum.XFControls.Droid {
    public static class AsNumAssemblyHelper {
        public static void HoldAssembly() {
            // Do nothing, just hold assembly,
            // prevent linker remove this assembly.
            HtmlLabelRenderer.Initialize();
        }
    }
}