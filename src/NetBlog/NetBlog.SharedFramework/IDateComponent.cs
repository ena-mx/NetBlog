namespace NetBlog.SharedFramework
{
    using System;

    public interface IDateComponent
    {
        DateTime ServerDate { get; }

        DateTime ConvertToServerTimeZone(DateTime date);
    }
}
