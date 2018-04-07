using System;

public class AccessToken
{
    public string accessToken { get; set; }

    public string refreshToken { get; set; }

    public DateTime expiresOn { get; set; }
}