var reqID = -1;
var modalbody = "#m_modal_4_body";
var requestsList = [];
$(function () {

    $('#m_modal_4').on('show.bs.modal',
        function (e) {
            var btn = $(e.relatedTarget);
            reqID = btn.data("request-id");
            $(modalbody).load("/RequestList/ShowRequestCloseModal/" + reqID);
        });
});

function RequestListJS(e) {

        $.ajax({
            type: "POST",
            url: "/RequestList/RequestList",
            data: "{ }",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
            },
            success: function (result) {

                requestsList = JSON.stringify(result.d);
                console.log(requestsList);
            }
        });
    }
};