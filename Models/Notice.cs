namespace AttendanceSystemAPI.Models
{
    public class Notice
    {
        public Guid Id { get; set; }
        public Guid NoticeCreator { get; set; } //user guid of the person who created the notice
        public string Title { get; set; }
        public string NoticeText { get; set; }
        public DateTime NoticeShowDate { get; set; } //The date the notice will be shown on notice board - time doesnt matter
    }
}
