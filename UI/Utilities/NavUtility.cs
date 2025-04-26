using System.Diagnostics;
using System.Text;

namespace UI.Utilities
{
    public class NavUtility
    {
        public static void DeletePage(INavigation navigation, string pageName)
        {
            var pageToDelete = navigation.NavigationStack.FirstOrDefault(x => x.GetType().Name == pageName);

            if (pageToDelete != null)
            {
                navigation.RemovePage(pageToDelete);
            }
        }

        public static void Examine(INavigation navigation)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var page in navigation.NavigationStack)
            {
                builder.AppendLine(page.GetType().Name);
            }

            builder.AppendLine("------------paginas");
            Debug.WriteLine(builder.ToString());
        }
    }
}
