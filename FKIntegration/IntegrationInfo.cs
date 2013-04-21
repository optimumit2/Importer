using MXDokFK;

namespace FKIntegration
{
    public static class IntegrationInfo
    {
        private readonly static ItgInfoClass _itgInfoClass;

        static IntegrationInfo()
        {
            _itgInfoClass = new ItgInfoClass();
        }

        public static string Description
        {
            get { return _itgInfoClass.get_Description(0); }
        }

        public static string Version
        {
            get { return _itgInfoClass.get_Version(0); }
        }        
    }
}
