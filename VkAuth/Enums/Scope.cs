namespace VkAuth.Enums
{
    public enum Scope
    {
        Notify = 1 << 0,
        Friends = 1 << 1,
        Photos = 1 << 2,
        Audio = 1 << 3,
        Video = 1 << 4,
        Pages = 1 << 5,
        Link = 1 << 6,
        Status = 1 << 7,
        Notes = 1 << 8,
        Messages = 1 << 9,
        Wall = 1 << 10,
        Ads = 1 << 11,
        Offline = 1 << 12,
        Docs = 1 << 13,
        Groups = 1 << 14,
        Notifications = 1 << 15,
        Stats = 1 << 16,
        Email = 1 << 17,
        Market = 1 << 18,
    }
}