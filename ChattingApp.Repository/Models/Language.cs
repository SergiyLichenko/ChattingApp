using ChattingApp.Repository.Domain;

namespace ChattingApp.Repository.Models
{
    public class Language
    {
        public int Id { get; set; }
        public LanguageType LanguageType { get; set; }
    }
}