namespace Varesin.Domain.Enumeration
{
    public enum AccessCode
    {
        // دسترسی کامل
        FullAccess = 0,

        // مدیریت کاربران
        UserManagement = 100,
        CreateUser = 101,
        DeleteUser = 102,
        ViewUser = 103,
        EditUser = 104,
        AccessManagement = 105,

        //
        SlideShowManagement = 600,
        CreateSlideShow = 601,
        ViewSlideShow = 602,
        EditSlideShow = 603,
        DeleteSlideShow = 604,

        //
        PaymentManagement = 700,
        ViewPayment = 701,

        InfoManagement = 800,
        ViewAndManageInfo = 801,

        ContatUsManagement = 900,
        ViewContactUs = 901,

        InstagramManagement = 1000,
        InstagramSharing = 1001,
        InstagramTagManagement = 1002,


        PostManagement = 2000,
        CreatePost = 2001,
        DeletePost = 2002,
        EditPost = 2003,
        ViewPost = 2004,
        PostFileManagement = 2005,

        NewsManagement = 3000,
        CreateNews = 3001,
        DeleteNews = 3002,
        EditNews = 3003,
        ViewNews = 3004,
        NewsFileManagement = 3005,

    }
}
