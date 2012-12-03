function onLinkedInLoad() {
	IN.Event.on(IN, "auth", function () { onLinkedInLogin(); });
	IN.Event.on(IN, "logout", function () { onLinkedInLogout(); });
}

function onLinkedInLogin() {
// we pass field selectors as a single parameter (array of strings)
IN.API.Profile("me")
	.fields(["id", "firstName", "lastName", "pictureUrl", "publicProfileUrl", "headline", "industry", "interests", "emailAddress", "honors"])
	.result(function (result) {
		console.log(result);
		setLoginBadge(result.values[0]);
		var dados = {
			id: result.values[0].id
		};
		console.log("Primeira chamada: " + JSON.stringify(dados));

		$.ajax({
			type: "POST",
			url: "http://localhost:1677/Account/saveUser/",
			data: result.values[0],
			dataType: 'json',
			success: function (result) {
				//alert(json.stringify(dados));
				if (result.codigo == 1) {
					console.log(result.msg);
				}
				else {
					console.log("Ocorreu algum erro!");
					console.log(result);
				}
			}
		});
		
		$.ajax({
			type: "POST",
			url: "http://localhost:1677/Account/saveUser/",
			data: result.values[0],
			dataType: 'json',
			success: function (result) {
				//alert(json.stringify(dados));
				if (result.codigo == 1) {
					console.log(result.msg);
					alert("Entrei");
				}
				else {
					console.log("Ocorreu algum erro!");
					console.log(result);
				}
			}
		});
		
		window.location = "http://localhost:1677/CommonUser/Home";
		
	});
}

function setSessionBadge() {

}