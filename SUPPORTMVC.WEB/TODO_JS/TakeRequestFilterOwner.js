$(document).ready(function () {
    $.ajax({
        type: 'POST',
        url: '/RequestList/FilterTakeRequestAttendant/',
        dataType: 'json',
        data: {},
        success: function (FilterTakeRequestAttendantList) {
            //$("#RequestAttendantOther").empty();
            $.each(FilterTakeRequestAttendantList,
                function (i, selectedRequestAttendant) {
                    $("#RequestAttendantOther").append('<option value="' +
                        selectedRequestAttendant.Value +
                        '">' +
                        selectedRequestAttendant.Text +
                        '</option>');
                });
        },
        error: function (ex) {
            alert('Görevliler bilgisine ulaşılamadı!' + ex);
        }
    });
});