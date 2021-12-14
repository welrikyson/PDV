using System;

namespace PDV.Ultis.Moc
{
    public static class IncrementalId
    {
        private static int CurrentId = 444;
        public static string Next => Convert.ToString(++CurrentId, null);
    }
}
