(function () {
    $(function () {
        var rootUrl = window.swashbuckleConfig.rootUrl;
        $.each(window.swashbuckleConfig.discoveryPaths, function (key, value) {
            if ($("#input_baseUrl option[value='" + rootUrl + "/" + value + "']").length < 1) {
                $('#input_baseUrl').append($("<option></option>")
                    .attr("value", rootUrl + "/" + value)
                    .text(rootUrl + "/" + value));
            }
        });

        $(".response").find('h4:contains(Curl)').hide();
        $(".response .block.curl").hide();

        $('#input_baseUrl').change(function () {
            window.swaggerUi.headerView.trigger("update-swagger-ui", { url: $("#input_baseUrl").val(), apiKey: $("#input_apiKey").val() });
        });

        $('#swaggerJson').attr('href', $('#input_baseUrl').val());
        $('#swaggerEditor').attr('href', 'http://editor.swagger.io/#/?import=' + $('#input_baseUrl').val());

        $('#authTokenForm').submit(function (event) {
            event.preventDefault();

            var url = document.location.origin + '/security/authorization/tokenrequest';

            var username = $('#username').val();
            var password = $('#password').val();

            if (username === "")
                username = "debugUsername";

            if (password === "")
                password = "debugPassword";

            var postData = { Username: username, Password: password };

            $.when(
                $.ajax({
                    url: url,
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    data: JSON.stringify(postData)
                })
            )
            .done(function (data) {
                $('#input_apiKey').val(data.Value.AuthenticationToken);
                $('#message-bar').html('&nbsp;');
                $('#input_apiKey').change();
            })
            .fail(function (xhr, ajaxOptions, thrownError) {
                // TODO: do something here, bad stuff is happening, the API failed.
                var response = JSON.parse(xhr.responseText);
                $('#message-bar').html('<div class=\'warning\'>' + response.Error.Message + '</div>');
            });
        });
    });
})();
