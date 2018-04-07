using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
   private static string  urlapi = ConfigurationManager.AppSettings["API"];

    protected void Page_Load(object sender, EventArgs e)
    {


    }


    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string usuarioText = txtUsuario.Text.ToString();
            string application = txtaplicacion.Text.ToString();

            string formaStringApp = Encoding.ASCII.GetString(Encoding.UTF8.GetBytes(usuarioText + ":" + application));
            var encrypUserApps = StringCipher.Encrypt(formaStringApp);

            using (WebClient client = new WebClient())
            {
                var jsonData = new { key = encrypUserApps };

                var dataString = JsonConvert.SerializeObject(jsonData);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.UseDefaultCredentials = true;
                string response = client.UploadString(urlapi + "Seguridad/UsuarioLogin", "POST", dataString);
                JObject jsonObject = JObject.Parse(response);
                string jsonToken = Convert.ToString(jsonObject["data"]);

                ResultToken account = null;
                if (!string.IsNullOrEmpty(jsonToken))
                {
                    account = JsonConvert.DeserializeObject<ResultToken>(jsonToken);
                    if (account != null)
                    {
                        string token = Base64Encode(JsonConvert.SerializeObject(account.token));
                        string url = Base64Encode(account.pagina);
                        string usuario = Base64Encode(account.usuario);
                        string urlFormat = string.Concat(account.url, "?key=", token.ToString(), "&url=", url, "&usr=", usuario);
                        if (account.url != null)
                            Response.Write("<script>window.open('" + urlFormat + "','_blank');</script>");

                    }
                    else
                    {
                        Response.Write("<script>alert('Usuario incorrecto')</script>");
                    }

                }
                else
                {
                    Response.Write("<script>alert('Usuario incorrecto')</script>");
                }

            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = ASCIIEncoding.ASCII.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string plainText)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(plainText);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }



}