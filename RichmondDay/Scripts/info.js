    $(document).ready(function () {
        $("input[name=btnEdit].btn").click(function (e) {
            var idClicked = e.target.id;
            var param = $("#" + idClicked).attr("value");
            var textboxId = idClicked.substring(1, idClicked.length);
            var infoId = textboxId;
            textboxId = "T" + textboxId;
            var firstName = $('#txtFirstName.' + textboxId).val();
            var lastName = $('#txtLastName.' + textboxId).val();
            var email = $('#txtEmail.' + textboxId).val();
            if (param == "Edit") {
                $("." + textboxId).prop('disabled', false);
                $("#" + idClicked).val('Update');
            }
            else if (param == "Update") {
                $("." + textboxId).prop('disabled', true);
                var infoParams = {
                    'id': infoId,
                    'FirstName': firstName,
                    'lastName': lastName,
                    'email': email,
                    'sortOrder': $('#sortOrder').val(),
                    'pageNumber': $('#pageNumber').val()
                }
                $.ajax({
                    type: "POST",
                    url: '/Home/UpdateInfo',
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(infoParams),
                    success: function (result) {
                    },
                    error: function () {
                    }
                });
                $("#" + idClicked).val('Edit');
            };
        });
    $("input[name=btnDelete].btn").click(function (e) {
            var idClicked = e.target.id;
            var textboxId = idClicked.substring(1, idClicked.length);
            var infoId = textboxId;
            var infoParams = {
        'id': infoId,
                'sortOrder': $('#sortOrder').val(),
                'pageNumber': $('#pageNumber').val()
            }
            $.ajax({
        type: "POST",
                url: '/Home/DeleteInfo',
                dataType: 'html',
                contentType: 'application/x-www-form-urlencoded',
                data: {
                    id: infoId, sortOrder: $('#sortOrder').val(), pageNumber: $('#pageNumber').val()
                },
                success: function (result) {
                    $("#allInfo").empty();
                    $("#allInfo").html(result)
                },

                error: function () {

    }
    })
        });
    });