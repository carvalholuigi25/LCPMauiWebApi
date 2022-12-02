function getDataFromSession() {
    var kname = "oidc.user:https://localhost:7285:LCPMauiWebApi.Client";
    let data = sessionStorage.getItem(kname);
    if (data) {
        console.log(JSON.parse(data));
    } else {
        console.log(`The data of ${kname} is empty!`);
    }
}

document.addEventListener("DOMContentLoaded", () => {
    getDataFromSession();
});