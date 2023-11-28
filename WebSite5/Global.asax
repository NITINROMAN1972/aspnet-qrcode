<%@ Application Language="C#" %>
<%@ Import Namespace="WebSite5" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);

        // Define a route for user profiles
        //RouteTable.Routes.MapPageRoute("Download", "Download/{userId}", "~/Download.aspx");
    }

</script>
