using System;
using System.Data.Entity;
using ChattingApp.Repository.Domain;
using ChattingApp.Repository.Models;

namespace ChattingApp.Repository.Helpers
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<AuthContext>
    {
        protected override void Seed(AuthContext authContext)
        {
            if(authContext == null) throw new ArgumentNullException(nameof(authContext));

            authContext.Languages.Add(new Language() { LanguageType = LanguageType.En});
            authContext.Languages.Add(new Language() { LanguageType = LanguageType.Ru });
            authContext.Languages.Add(new Language() { LanguageType = LanguageType.Uk });

            base.Seed(authContext);
        }
    }
}