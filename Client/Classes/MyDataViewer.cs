namespace LCPMauiWebApi.Client.Classes
{
    public class MyDataViewer
    {
        public string? Key { get; set; } = "oidc.user:https://localhost:7285:LCPMauiWebApi.Client";
        public string? TypeData { get; set; } = EnumSessionTypes.session.ToString();
        public string? Msg { get; set; } = "Success";
    }

    public enum EnumSessionTypes
    {
        local = 0,
        session = 1,
    };
}
