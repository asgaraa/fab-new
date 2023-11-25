namespace Fab.Models.ContactFolder
{
    public class ContactInformations : BaseEntity
    {
        public string LocationLink { get; set; }
        public string Tel1 { get; set; }
        //isnotrequiredonfrontside
        public string? Tel2 { get; set; }
        public string? Tel3 { get; set; }
        //public string? Fax1{ get; set; }
        public string SocialLink1 { get; set; }
        public string SocialLink2 { get; set; }
        public string SocialLink3 { get; set; }
        public string SocialLink4 { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
    }
}
