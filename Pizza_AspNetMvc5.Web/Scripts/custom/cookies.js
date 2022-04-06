/* Javascript to show and hide cookie banner using localstorage */
/* Shows the Cookie banner */
function showCookieBanner() {
	let cookieBanner = document.getElementById("cookie-banner");
	cookieBanner.style.display = "block";
}

/* Hides the Cookie banner and saves the value to localstorage */
function hideCookieBanner() {
	document.cookie = "isCookieAccepted=yes";
	let cookieBanner = document.getElementById("cookie-banner");
	cookieBanner.style.display = "none";
}

/* Checks the localstorage and shows Cookie banner based on it. */
function initializeCookieBanner() {
	let isCookieAccepted = document.cookie.getItem("isCookieAccepted");
	if (isCookieAccepted === null) {
		document.cookie = "isCookieAccepted=no";
		showCookieBanner();
	}
	if (isCookieAccepted === "no") {
		showCookieBanner();
	}
}