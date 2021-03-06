﻿using System.Runtime.Serialization;

namespace Gist.GitHub
{
    [DataContract]
    public class User
    {
        [DataMember(Name = "login")]
        public string Login { get; set; }
        [DataMember(Name = "id")]
        public long Id { get; set; }
        [DataMember(Name = "avatar_url")]
        public string AvatarUrl { get; set; }
        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}